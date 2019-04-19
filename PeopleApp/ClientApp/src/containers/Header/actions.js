import { LOAD_HEADER_EMERGENCY_DATA, LOAD_HEADER_EMERGENCY_DATA_SUCCESS, LOAD_HEADER_EMERGENCY_DATA_ERROR } from './constants';

export const loadHeaderEmergencyData = () => {
  return {
    type: LOAD_HEADER_EMERGENCY_DATA
  };
};

export const loadHeaderDataEmergencySuccess = (data) => {
  return {
    type: LOAD_HEADER_EMERGENCY_DATA_SUCCESS,
    payload: data
  };
};

export const loadHeaderDataError = (error) => {
  return {
    type: LOAD_HEADER_EMERGENCY_DATA_ERROR,
    error
  };
};
