import { fromJS } from 'immutable';
import { LOAD_HEADER_EMERGENCY_DATA_SUCCESS, LOAD_HEADER_EMERGENCY_DATA_ERROR } from './constants';

export const storeName = 'header';

const headerEmergencyDataLoadedSuccess = (state, data) => {
  if (data.length) {
    return state
      .set('emergencyData', {
        isEnabled: true,
        title: data[0].cardIndexName,
        content: data[0].comment
      });
  }
  return state;
};

const headerEmergencyDataLoadedError = (state, error) => {
  return state
    .set('emergencyError', error.toString());
};

const initialState = fromJS({
  emergencyData: {
    isEnabled: false,
    title: '',
    content: ''
  },
  emergencyError: ''
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case LOAD_HEADER_EMERGENCY_DATA_SUCCESS:
        return headerEmergencyDataLoadedSuccess(state, action.payload);
      case LOAD_HEADER_EMERGENCY_DATA_ERROR:
        return headerEmergencyDataLoadedError(state, action.error);
      default:
        return state;
    }
  }
};
