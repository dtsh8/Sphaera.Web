import { fromJS } from 'immutable';
import { findIndex } from 'lodash';
import {
  GET_INCIDENTS_MAP_LOAD,
  GET_INCIDENTS_MAP_SUCCESS,
  GET_INCIDENTS_MAP_ERROR,
  FILTER_CHANGED
} from './constants';
import { convertFromImmutableToJS } from '../../helpers/common';

export const storeName = 'incidentsStore';

const incidentsSuccess = (state, payload) => {
  const incidentsMapData = convertFromImmutableToJS(state).incidents.data || [];

  const index = findIndex(incidentsMapData, {
    incidentId: payload.incidentId,
    cardId: payload.cardId
  });

  if (index === -1) {
    const oneNewIncidentLength = 1;
    return state.set('incidents', {
      data: [...incidentsMapData, payload],
      count: incidentsMapData.length + oneNewIncidentLength,
      loading: false
    });
  }

  incidentsMapData[index] = payload;

  return state.set('incidents', {
    data: incidentsMapData,
    count: incidentsMapData.length,
    loading: false
  });
};

const incidentsLoading = (state, value) => {
  return state.set('incidents', {
    loading: value,
    ...convertFromImmutableToJS(state).incidents
  });
};

const changeFilter = (state, payload) => {
  return state.set('filters', {
    type: payload.type
  });
};

const initialState = fromJS({
  incidents: {
    data: [],
    count: 0,
    loading: false,
  },
  filters: {
    type: { id: -1, title: 'Все происшествия' }
  }
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case GET_INCIDENTS_MAP_LOAD:
        return incidentsLoading(state, true);
      case GET_INCIDENTS_MAP_SUCCESS:
        return incidentsSuccess(state, action.payload);
      case GET_INCIDENTS_MAP_ERROR:
        return incidentsLoading(state, true);
      case FILTER_CHANGED:
        return changeFilter(state, action.payload);
      default:
        return state;
    }
  }
};
