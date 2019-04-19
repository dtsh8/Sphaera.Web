import { FETCH_TYPES, FETCH_TYPES_ERROR, FETCH_TYPES_SUCCESS } from './constants';

export const fetchTypes = () => {
  return { type: FETCH_TYPES };
};

export const fetchTypesSuccess = (data) => {
  return {
    type: FETCH_TYPES_SUCCESS,
    payload: data
  };
};

export const fetchTypesError = () => {
  return { type: FETCH_TYPES_ERROR };
};
