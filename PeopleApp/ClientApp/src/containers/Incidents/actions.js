import {
  GET_INCIDENTS_MAP_LOAD,
  GET_INCIDENTS_MAP_SUCCESS,
  GET_INCIDENTS_MAP_ERROR,
  CHANGE_FILTER,
  FILTER_CHANGED
} from './constants';

export const loadIncidents = () => {
  return {
    type: GET_INCIDENTS_MAP_LOAD
  };
};

export const getIncidentsSuccess = (payload) => {
  return {
    type: GET_INCIDENTS_MAP_SUCCESS,
    payload
  };
};

export const getIncidentsError = () => {
  return {
    type: GET_INCIDENTS_MAP_ERROR
  };
};

export const changeFilter = (payload) => {
  return {
    type: CHANGE_FILTER,
    payload
  };
};

export const filterChanged = (payload) => {
  return {
    type: FILTER_CHANGED,
    payload
  };
};
