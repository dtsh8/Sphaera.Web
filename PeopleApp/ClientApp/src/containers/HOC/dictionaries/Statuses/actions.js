import { FETCH_STATUSES, FETCH_STATUSES_ERROR, FETCH_STATUSES_SUCCESS } from './constants';

export const fetchStatuses = () => {
  return { type: FETCH_STATUSES };
};

export const fetchStatusesSuccess = (statuses) => {
  return {
    type: FETCH_STATUSES_SUCCESS,
    payload: {
      statuses
    }
  };
}; 

export const fetchStatusesError = () => {
  return { type: FETCH_STATUSES_ERROR };
};
