import { FETCH_PERIODS, FETCH_PERIODS_ERROR, FETCH_PERIODS_SUCCESS } from './constants';

export const fetchPeriods = () => {
  return { type: FETCH_PERIODS };
};

export const fetchPeriodsSuccess = (data) => {
  return {
    type: FETCH_PERIODS_SUCCESS,
    payload: data
  };
};

export const fetchPeriodsError = () => {
  return { type: FETCH_PERIODS_ERROR };
};
