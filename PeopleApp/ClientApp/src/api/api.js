import $ from 'jquery';
import axios from 'axios';
import 'devextreme/integration/jquery';
import ArrayStore from 'devextreme/data/array_store';
import DataSource from 'devextreme/data/data_source';
import * as signalR from '@aspnet/signalr';

/* eslint-disable no-underscore-dangle */
/* eslint-disable no-console */

window.jQuery = $;
axios.defaults.headers.post['Content-Type'] = 'application/json';

// TODO: Как научим Kestrel понимать Web-Soket'ы - переключим.
const httpConnectionOptions = {};
httpConnectionOptions.transport = signalR.HttpTransportType.LongPolling;
const hubReconnectTimeout = 5000;

//const addr = '/';
// const addr = 'https://people.infra.sphaera.ru:5009/'; //For testing
const addr = 'http://localhost:5000/'; //For testing
let idmAddr;

const axiosRequestConfig = {
  withCredentials: true // Todo: withCredentials: true нужно только для тестирования фронта локально.
};

$.app = $.app || {};
$.app.cardsArrayStore = $.app.cardsArrayStore
  || new ArrayStore({
    key: 'missionId',
    data: []
  });
$.app.cardsDataSource = $.app.cardsDataSource
  || new DataSource({
    store: $.app.cardsArrayStore,
    paginate : false
  });

$.app.appealCardsArrayStore = $.app.appealCardsArrayStore
  || new ArrayStore({
    key: 'missionId',
    data: []
  });
$.app.appealCardsDataSource = $.app.appealCardsDataSource
  ||new DataSource({
    store: $.app.appealCardsArrayStore,
    paginate : false
  });

$.app.emergencyCardsArrayStore = $.app.emergencyCardsArrayStore
  || new ArrayStore({
    key: 'missionId',
    data: []
  });
$.app.emergencyCardsArrayStore.options = {};
$.app.emergencyCardsDataSource = $.app.emergencyCardsDataSource
  || new DataSource({
    store: $.app.emergencyCardsArrayStore,
    paginate : false
  });

$.app.resArrayStore = $.app.resArrayStore
  || new ArrayStore({
    key: 'resourceCode',
    data: []
  });
$.app.resDataSource = $.app.resDataSource
  || new DataSource({
    store: $.app.resArrayStore,
    paginate : false
  });

async function get(url, params = null) {
  try {
    const requestConfig = Object.assign({}, axiosRequestConfig);
    requestConfig.params = params;
    const response = await axios.get(url, requestConfig);
    return response.data;
  } catch (error) {
    console.log(error);
    return error;
  }
}

function post(url, object, params) {
  const config = {
    ...axiosRequestConfig,
    params
  };
  return axios.post(url, object, config)
    .then((result) => {
      return result.data;
    })
    .catch((error) => {
      return error;
    });
}

function put(url, object) {
  return axios.put(url, object, axiosRequestConfig)
    .then((result) => {
      return result.data;
    })
    .catch((error) => {
      return error;
    });
}

function del(url) {
  return axios.delete(url, axiosRequestConfig)
    .then((result) => {
      return result.data;
    })
    .catch((error) => {
      return error;
    });
}

function startSignalRConnection(connection) {
  const state = connection.connection.connectionState;
  if (state === 2) { /* Disconnected */
    connection.sphaeraConnectPromise = connection.start().catch((err) => {
      console.error(err);
      return new Promise((success, reject) => {
        setTimeout(() => {
          startSignalRConnection(connection).then(res => success(res), error => reject(error));
        }, hubReconnectTimeout);
      });
    });
  }

  return connection.sphaeraConnectPromise;
}

function InitSignalRConnection(hubUrl) {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl, httpConnectionOptions)
    .configureLogging(signalR.LogLevel.Error)
    .build();
  // Ключ -> список действий которые нужно выполнить при реконнекте.
  connection.sphaeraOnReconnect = {};
  connection.onclose(() => {
    startSignalRConnection((connection)).then((r) => {
      Object.values(connection.sphaeraOnReconnect).forEach(v => v.forEach(callback => callback()));
      return r;
    });
  });
  return connection;
}


function updateCardsArray(cardsList, cardsArrayStore) {
  if (cardsArrayStore.options.cardsSessionId !== cardsList.sessionId) {
    return;
  }
  for (const card of cardsList.cards) {
    cardsArrayStore.update(card.missionId, card)
      .fail((err) => {
        if (err.__id === 'E4009') { /* Not Found */
          cardsArrayStore.insert(card)
            .fail((error) => {
              console.error(error.toString());
            });
        } else {
          console.error(err.toString());
        }
      });
  }
}

if (!$.app.cardsConnection) {
  $.app.cardsConnection = InitSignalRConnection(`${addr}hubs/cards`);
  $.app.cardsConnection.on('updateCard',
    (data) => {
      updateCardsArray(data, $.app.cardsArrayStore);
      $.app.cardsDataSource.reload();
    });
  $.app.cardsConnection.on('updateAppealCard',
    (data) => {
      updateCardsArray(data, $.app.appealCardsArrayStore);
      $.app.appealCardsDataSource.reload();
    });
  $.app.cardsConnection.on('updateEmergencyCard',
    (data) => {
      updateCardsArray(data, $.app.emergencyCardsArrayStore);
      $.app.emergencyCardsDataSource.reload();
    });
}

if (!$.app.resConnection) {
  $.app.resConnection = InitSignalRConnection(`${addr}hubs/resources`);
  $.app.resConnection.on('updateResource',
    (data) => {
      $.each(data,
        function update() {
          const resource = this;
          $.app.resArrayStore.update(resource.resourceCode, resource)
            .fail((err) => {
              if (err.__id === 'E4009') { /* Not Found */
                $.app.resArrayStore.insert(resource)
                  .fail((error) => {
                    console.error(error.toString());
                  });
              } else {
                console.error(err.toString());
              }
            });
        });
      $.app.resDataSource.reload();
    });
}

if (!$.app.mapObjectsConnection) {
  $.app.mapObjectsConnection = InitSignalRConnection(`${addr}hubs/mapObjects`);
}

if (!$.app.assignmentConnection) {
  $.app.assignmentConnection = InitSignalRConnection(`${addr}hubs/assignment`);
}

function getAppealCards(filter) {
  $.app.cardsConnection.sphaeraOnReconnect = {
    ...$.app.cardsConnection.sphaeraOnReconnect,
    getAppealCards: [() => getCardsCommon(filter, 'getAppealCards', $.app.appealCardsArrayStore, $.app.appealCardsDataSource)]
  };
  return startSignalRConnection($.app.cardsConnection)
    .then(() => {
      return getCardsCommon(filter, 'getAppealCards', $.app.appealCardsArrayStore, $.app.appealCardsDataSource);
    })
    .catch((error) => {
      return error;
    });
}

function getCardsCommon(filter, functionName, cardsArrayStore, cardsDataSource) {
  const state = $.app.cardsConnection.connection.connectionState;
  if (state !== 1) { /* Connected */
    return;
  }

  cardsArrayStore = cardsArrayStore || $.app.cardsArrayStore;
  cardsDataSource = cardsDataSource || $.app.cardsDataSource;
  $.app.cardsConnection.invoke(functionName, filter)
    .then((data) => {
      const { cards } = data;
      const options = cardsArrayStore.options || {};
      cardsArrayStore.options = {
        ...options,
        cardsSessionId: data.sessionId
      };
      cardsArrayStore.clear();
      $.each(cards,
        function insert() {
          cardsArrayStore.insert(this);
        });
      cardsDataSource.reload();
    })
    .catch(err => console.error(err.toString()));
}

function getEddsCards(filter) {
  $.app.cardsConnection.sphaeraOnReconnect = {
    ...$.app.cardsConnection.sphaeraOnReconnect,
    getEddsCards: [() => getCardsCommon(filter, 'getEddsCards')]
  };
  return startSignalRConnection($.app.cardsConnection)
    .then(() => {
      return getCardsCommon(filter, 'getEddsCards');
    })
    .catch((error) => {
      return error;
    });
}

function getInitialCards(filter) {
  $.app.cardsConnection.sphaeraOnReconnect = {
    ...$.app.cardsConnection.sphaeraOnReconnect,
    getCards: [() => getCardsCommon(filter, 'getCards')]
  };
  return startSignalRConnection($.app.cardsConnection)
    .then(() => {
      return getCardsCommon(filter, 'getCards');
    })
    .catch(err => console.error(err.toString()));
}

function startEmergencyCardsHub() {
  return startSignalRConnection($.app.cardsConnection);
}

function insertCard(card) {
  return $.app.cardsConnection.invoke('insertCard', card)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function updateCard(card) {
  return $.app.cardsConnection.invoke('updateCard', card)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function closeCard(incidentId, cardId) {
  return $.app.cardsConnection.invoke('closeCard', incidentId, cardId)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function rejectCard(incidentId, cardId, message) {
  return $.app.cardsConnection.invoke('rejectCard', incidentId, cardId, message)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function getCardStatistics() {
  return $.app.cardsConnection
    .invoke('getStatistics')
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function getAssignedResources(missionId) {
  const state = $.app.resConnection.connection.connectionState;
  if (state !== 1) {
    return;
  }
  $.app.resConnection.invoke('getAssigned', missionId)
    .then((data) => {
      $.app.resArrayStore.clear();
      $.each(data,
        function insert() {
          $.app.resArrayStore.insert(this);
        });
      $.app.resDataSource.reload();
    })
    .catch(err => console.error(err.toString()));
}

function getAssignedResourcesList(missionId) {
  const state = $.app.resConnection.connection.connectionState;
  if (state !== 1) {
    return Promise.reject(new Error('Соединение не установлено.'));
  }
  return $.app.resConnection.invoke('getAssigned', missionId)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function startResourcesHub(missionId) {
  const state = $.app.resConnection.connection.connectionState;
  if (state !== 2) {
    return Promise.resolve();
  }
  return $.app.resConnection.start().then(() => {
    if (missionId) {
      getAssignedResources(missionId);
    }
  }).catch((err) => {
    console.error(err);
  });
}

function getAvailableResources(stationCode) {
  return $.app.resConnection.invoke('getAvailable', stationCode)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function getResourceHistory(missionId, resourceCode) {
  return $.app.resConnection.invoke('getHistory', missionId, resourceCode)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function getResourceState(resourceCode) {
  return $.app.resConnection.invoke('getState', resourceCode)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function setMissionResourceStateChange(obj) {
  return $.app.resConnection.invoke('setMissionStateChange', obj)
    .then((data) => {
      return data;
    }).catch(err => console.error(err.toString()));
}

function setGlobalResourceStateChange(obj) {
  return $.app.resConnection.invoke('setGlobalStateChange', obj)
    .then((data) => {
      return data;
    }).catch(err => console.error(err.toString()));
}

function getResources(resourceType, stationCode) {
  return $.app.resConnection.invoke('getList', resourceType, stationCode)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

async function putResource(obj) {
  try {
    return await $.app.resConnection.invoke('update', obj);
  } catch (error) {
    console.error(error);
    throw error;
  }
}

async function newResource(obj) {
  try {
    return await $.app.resConnection.invoke('new', obj);
  } catch (error) {
    console.error(error);
    throw error;
  }
}

function startMapObjectsHub() {
  return startSignalRConnection($.app.mapObjectsConnection);
}

async function getVideoCameras(mapBounds) {
  await startSignalRConnection($.app.mapObjectsConnection);
  try {
    return await $.app.mapObjectsConnection.invoke('getVideoCameras', mapBounds);
  } catch (error) {
    console.error(error);
    throw error;
  }
}

function startAssignmentHub() {
  return startSignalRConnection($.app.assignmentConnection);
}

function getAssignments(incidentId, cardId) {
  return $.app.assignmentConnection.invoke('get', incidentId, cardId)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function getAssignmentsNoDataSourceUpdate(incidentId, cardId) {
  return $.app.assignmentConnection.invoke('get', incidentId, cardId)
    .then((data) => {
    return data;
    })
    .catch(err => console.error(err.toString()));
}

function getAssignmentReports(dateFrom, dateTo) {
    return $.app.assignmentConnection.invoke('getReport', dateFrom, dateTo)
        .then((data) => {
            $.app.tasksArrayStore.clear();
            data.map(item => {
                $.app.tasksArrayStore.insert(item);
            });
            $.app.tasksDataSource.reload();
        }).catch(err => console.error(err.toString()));
}

function getAssignmentTemplates() {
    return $.app.assignmentConnection.invoke('getTemplates')
        .then((data) => {
            $.app.tasksArrayStore.clear();
            data.map(item => {
                $.app.tasksArrayStore.insert(item);
            });
            $.app.tasksDataSource.reload();
        }).catch(err => console.error(err.toString()));
}

function getAssignmentHistory(assignmentId) {
    return $.app.assignmentConnection.invoke('getHistory')
        .then((data) => {
            $.app.tasksArrayStore.clear();
            data.map(item => {
                $.app.tasksArrayStore.insert(item);
            });
            $.app.tasksDataSource.reload();
        }).catch(err => console.error(err.toString()));
}

function setAssignment(assignment) {
  return $.app.assignmentConnection.invoke('put', assignment)
    .then((data) => {
      if (data) {
        let arr = $.app.tasksArrayStore;
        return arr.insert(data)
          .then(() => {
            let ds = $.app.tasksDataSource;
            return ds.reload()
              .then(() => {
                return data;
              });
          });
      }
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function getAssignmentFiles(incidentId, cardId) {
  return $.app.assignmentConnection.invoke('getAssignmentFiles', incidentId, cardId)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function getAssignmentFile(incidentId, cardId, fileName) {
  return $.app.assignmentConnection.invoke('getAssignmentFile', incidentId, cardId, fileName)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function setAssignmentFile(incidentId, cardId, fileName, description, fileBody) {
  return $.app.assignmentConnection.invoke('setAssignmentFile', incidentId, cardId, fileName, description, fileBody)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

function setAssignmentState(request) {
  return $.app.assignmentConnection.invoke('setAssignmentState', request)
    .then((data) => {
      return data;
    })
    .catch(err => console.error(err.toString()));
}

// startAssignmentHub().then(() => getAssignments('', '').then((data) => {
//   console.info("Поручения");
//   console.info(data);
// }));

async function findAddressesAsync(str) {
  try {
    return await get(`${addr}api/address/find`, { search: str });
  } catch (error) {
    return error;
  }
}

async function findLocality(municipalityFiasCode, searchString) {
  try {
    return await get(`${addr}api/address/FindLocality`, { searchString, municipalityFiasCode });
  } catch (error) {
    return error;
  }
}

async function findStreet(municipalityFiasCode, locality, searchString) {
  try {
    return await post(`${addr}api/address/FindStreet`, locality, { searchString, municipalityFiasCode });
  } catch (error) {
    return error;
  }
}

async function getAssignmentStatesAsync() {
  try {
    return await get(`${addr}api/assignmentState/getList`);
  } catch (error) {
    return error;
  }
}

async function getCardIndexTreeAsync(serviceTypeId, dispatchServiceId) {
  if (serviceTypeId === undefined || serviceTypeId === null || dispatchServiceId === undefined || dispatchServiceId === null) {
    serviceTypeId = 0;
  }

  return get(`${addr}api/cardIndex/getTree`, { serviceTypeId, dispatchServiceId });
}

async function getCardIndexLevelAsync(serviceTypeId, parentCode, dispatchServiceId) {
  try {
    return await get(`${addr}api/cardIndex/getLevel`, { serviceTypeId, parentCode, dispatchServiceId });
  } catch (error) {
    return error;
  }
}

async function getCardIndexObjectAsync(serviceTypeId, code, dispatchServiceId) {
  try {
    return await get(`${addr}api/cardIndex/get`, { serviceTypeId, code, dispatchServiceId });
  } catch (error) {
    return error;
  }
}

async function getEmergencyCardsAsync() {
  try {
    return await get(`${addr}api/card/GetEmergencyList`);
  } catch (error) {
    return error;
  }
}

async function putCardIndexLevelAsync(serviceTypeId, parentCode, cardIndex) {
  try {
    return await put(`${addr}api/cardIndex/apply?serviceTypeId=${serviceTypeId}&parentCode=${parentCode}`, cardIndex)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getCardTypesAsync() {
  try {
    return await get(`${addr}api/cardType/getList`);
  } catch (error) {
    return error;
  }
}

async function getClaimStatesAsync() {
  try {
    return await get(`${addr}api/claimState/getList`);
  } catch (error) {
    return error;
  }
}

async function getDispatchServicesAsync(serviceIds = '') {
  try {
    return await get(`${addr}api/dispatchService/getList?serviceIds`, { serviceIds });
  } catch (error) {
    return error;
  }
}

async function getOrganizationsAsync() {
  try {
    return await get(`${addr}api/organization/getList`);
  } catch (error) {
    return error;
  }
}

async function putOrganizationAsync(object) {
  try {
    return await put(`${addr}api/organization/update`, object)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function delOrganizationAsync(code) {
  try {
    return await del(`${addr}api/organization/delete?code=${code}`)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getIncidentStatesAsync() {
  try {
    return await get(`${addr}api/incidentState/getList`);
  } catch (error) {
    return error;
  }
}

async function getMunicipalitiesAsync() {
  try {
    return await get(`${addr}api/municipality/getList`);
  } catch (error) {
    return error;
  }
}

async function getResourceTypesAsync() {
  try {
    return await get(`${addr}api/resourceType/getList`);
  } catch (error) {
    return error;
  }
}

async function putResourceTypeAsync(object) {
  try {
    return await put(`${addr}api/resourceType/update`, object)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function delResourceTypeAsync(resTypeCode) {
  try {
    return await del(`${addr}api/resourceType/delete?resTypeCode=${resTypeCode}`)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getStationsAsync(orgCode = '') {
  try {
    return await get(`${addr}api/station/getList`, { orgCode });
  } catch (error) {
    return error;
  }
}

async function putStationAsync(object) {
  try {
    return await put(`${addr}api/station/update`, object)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function delStationAsync(stationCode) {
  try {
    return await del(`${addr}api/station/delete?stationCode=${stationCode}`)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getFilesAsync(incidentId, cardId) {
  try {
    return await get(`${addr}api/file/getList`, { incidentId, cardId });
  } catch (error) {
    return error;
  }
}

async function getFileAsync(incidentId, cardId, fileName) {
  try {
    return await get(`${addr}api/file/get`, { incidentId, cardId, fileName });
  } catch (error) {
    return error;
  }
}

async function putFileAsync(incidentId, cardId, fileName, fileBody, description = '') {
  try {
    return await post(
      `${addr}api/file/update?incidentId=${incidentId}&cardId=${cardId}&fileName=${fileName}&description=${description}`,
      JSON.stringify(fileBody))
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function delFileAsync(incidentId, cardId, fileName) {
  try {
    return await del(`${addr}api/file/delete?incidentId=${incidentId}&cardId=${cardId}&fileName=${fileName}`)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getIncidentAsync(incidentId, cardId, exceptServiceTypeIds) {
  try {
    return await post(`${addr}api/incident/get`, { incidentId, cardId, exceptServiceTypeIds })
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function putIncidentAsync(object) {
  try {
    return await post(`${addr}api/incident/update`, object)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getIncidentHistoryAsync(incidentId) {
  try {
    return await get(`${addr}api/incident/getHistory`, { incidentId });
  } catch (error) {
    return error;
  }
}

async function getVisitsSummaryAsync(period, date) {
  try {
    return await get(`${addr}api/motomo/getVisitsSummary`, { period, date });
  } catch (error) {
    return error;
  }
}

async function getReferrersArrayAsync(period, date) {
  try {
    return await get(`${addr}api/motomo/getReferrersArray`, { period, date });
  } catch (error) {
    return error;
  }
}

async function getActionsArrayAsync(period, date) {
  try {
    return await get(`${addr}api/motomo/getActionsArray`, { period, date });
  } catch (error) {
    return error;
  }
}

async function getNoticesAsync(incidentId) {
  try {
    return await get(`${addr}api/notice/getList`, { incidentId });
  } catch (error) {
    return error;
  }
}

async function putNoticeAsync(object) {
  try {
    return await put(`${addr}api/notice/update`, object)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getReactionPlanesAsync(incidentId, cardId) {
  try {
    return await get(`${addr}api/reactionPlan/getList`, { incidentId, cardId });
  } catch (error) {
    return error;
  }
}

async function getGlobalResourceStatesAsync(resourceType = '', enabledOnly) {
  try {
    if (enabledOnly === undefined || enabledOnly === null) {
      enabledOnly = false;
    }
    return await get(`${addr}api/resourceState/getGlobalList`, { resourceType, enabledOnly });
  } catch (error) {
    return error;
  }
}

async function getMissionResourceStatesAsync(resourceType = '', enabledOnly) {
  try {
    if (enabledOnly === undefined || enabledOnly === null) {
      enabledOnly = false;
    }
    return await get(`${addr}api/resourceState/getMissionList`, { resourceType, enabledOnly });
  } catch (error) {
    return error;
  }
}

async function putGlobalResourceStateAsync(object) {
  try {
    return await put(`${addr}api/resourceState/setGlobal`, object)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function putMissionResourceStateAsync(object) {
  try {
    return await put(`${addr}api/resourceState/setMission`, object)
      .then((result) => {
        return result;
      })
      .catch((error) => {
        console.error(error);
        return error;
      });
  } catch (error) {
    return error;
  }
}

async function getDocumentsTreeAsync() {
  try {
    return await get(`${addr}api/Document/GetTreeItems`);
  } catch (error) {
    console.error(error);
    return error;
  }
}

async function getDocumentAsync(id) {
  try {
    return await get(`${addr}api/Document/Get`, { id });
  } catch (error) {
    console.error(error);
    return error;
  }
}

function login() {
  const currentUrl = encodeURIComponent(window.location.href);
  window.location.href = `${addr}Account/Login?redirectUrl=${currentUrl}`;
}

function logout() {
  window.location.href = `${addr}Account/Logout`;
}

function profile() {
  const currentUrl = encodeURIComponent(window.location.href);
  window.location.href = `${addr}Account/Profile?redirectUrl=${currentUrl}`;
}

async function getProfile() {
  try {
    return await get(`${addr}api/account/profile`);
  } catch (error) {
    return error;
  }
}

async function getIsAuthenticated() {
  try {
    return await get(`${addr}api/account/isAuthenticated`);
  } catch (error) {
    return error;
  }
}

let gisServerSettings;
async function getGisServerSettings() {
  if (gisServerSettings === undefined) { 
    gisServerSettings = await get(`${addr}api/settings/GisServer`);
  }

  return gisServerSettings;
}

let videoServerUri;
async function getVideoServerUri() {
  if (videoServerUri === undefined) { 
    videoServerUri = await get(`${addr}api/settings/VideoServerUri`);
  }

  return videoServerUri;
}

export default {
  account: {
    login,
    logout,
    edit: profile,
    isAuthenticated: getIsAuthenticated,
    getProfile
  },
  address: {
    find: findAddressesAsync,
    findLocality,
    findStreet
  },
  assignmentState: {
    getList: getAssignmentStatesAsync
  },
  assignment: {
    initialize: startAssignmentHub,
    get: getAssignments,
    getNoDataSourceUpdate: getAssignmentsNoDataSourceUpdate,
    set: setAssignment,
    getFiles: getAssignmentFiles,
    getFileBody: getAssignmentFile,
    setFile: setAssignmentFile,
    setState: setAssignmentState,
    getReports: getAssignmentReports,
    getTemplates: getAssignmentTemplates,
    getHistory: getAssignmentHistory
  },
  card: {
    dataSource: $.app.cardsDataSource,
    arrayStore: $.app.cardsArrayStore,
    appealDataSource: $.app.appealCardsDataSource,
    appealArrayStore: $.app.appealCardsArrayStore,
    emergencyDataSource: $.app.emergencyCardsDataSource, // Датасорс штормовых предупреждений.
    emergencyArrayStore: $.app.emergencyCardsArrayStore, // Стор штормовых предупреждений.
    initializeEmergency: startEmergencyCardsHub, // Устанавливает signalR соединение для штормовых предупреждений.
    getEmergencyCards: getEmergencyCardsAsync, // Получить полный список штормовых предупреждений.
    initialize: getInitialCards,
    getEddsCards,
    getAppealCards,
    insert: insertCard,
    update: updateCard,
    close: closeCard,
    reject: rejectCard,
    getStatistics: getCardStatistics
  },
  cardIndex: {
    getTree: getCardIndexTreeAsync,
    getSiblings: getCardIndexLevelAsync,
    get: getCardIndexObjectAsync,
    set: putCardIndexLevelAsync
  },
  cardType: {
    getList: getCardTypesAsync
  },
  claimState: {
    getList: getClaimStatesAsync
  },
  dispatchService: {
    getList: getDispatchServicesAsync
  },
  documents: {
    getTree: getDocumentsTreeAsync,
    get: getDocumentAsync
  },
  file: {
    getList: getFilesAsync,
    getBody: getFileAsync,
    set: putFileAsync,
    del: delFileAsync
  },
  organization: {
    getList: getOrganizationsAsync,
    set: putOrganizationAsync,
    del: delOrganizationAsync
  },
  incident: {
    get: getIncidentAsync,
    set: putIncidentAsync,
    getHistory: getIncidentHistoryAsync
  },
  incidentState: {
    getList: getIncidentStatesAsync
  },
  municipality: {
    getList: getMunicipalitiesAsync
  },
  notice: {
    getList: getNoticesAsync,
    set: putNoticeAsync
  },
  reactionPlan: {
    getList: getReactionPlanesAsync
  },
  resource: {
    dataSource: $.app.resDataSource,
    arrayStore: $.app.resArrayStore,
    initialize: startResourcesHub,
    reload: getAssignedResources,
    getAssigned: getAssignedResources,
    getAssignedList: getAssignedResourcesList,
    getAvailable: getAvailableResources,
    getHistory: getResourceHistory,
    getState: getResourceState,
    setMissionState: setMissionResourceStateChange,
    setGlobalState: setGlobalResourceStateChange,
    getList: getResources,
    set: putResource,
    new: newResource
  },
  resourceState: {
    getGlobalList: getGlobalResourceStatesAsync,
    getMissionList: getMissionResourceStatesAsync,
    setGlobal: putGlobalResourceStateAsync,
    setMission: putMissionResourceStateAsync
  },
  resourceType: {
    getList: getResourceTypesAsync,
    set: putResourceTypeAsync,
    del: delResourceTypeAsync
  },
  station: {
    getList: getStationsAsync,
    set: putStationAsync,
    del: delStationAsync
  },
  statistics: {
    getVisits: getVisitsSummaryAsync,
    getReferrers: getReferrersArrayAsync,
    getActions: getActionsArrayAsync
  },
  mapObjects: {
    initialize: startMapObjectsHub,
    getVideoCameras,
    getVideoServerUri
  },
  settings: {
    getGisServerSettings
  }
};
