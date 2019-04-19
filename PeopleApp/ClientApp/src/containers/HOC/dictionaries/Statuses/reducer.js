import { fromJS } from 'immutable';
import { FETCH_STATUSES, FETCH_STATUSES_ERROR, FETCH_STATUSES_SUCCESS } from './constants';

const statusesSuccess = (state, payload) => {
  return state.set('statuses', {
    count: payload.statuses.length,
    results: payload.statuses,
    loading: false
  });
};

const statusesFetchLoading = (state, value) => {
  return state.set('statuses', {
    loading: value
  });
};

export const storeName = 'dictionary-statuses';

const initialState = fromJS({
  statuses: {
    count: 0,
    results: [],
    loading: false
  }
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case FETCH_STATUSES:
        return statusesFetchLoading(state, true);
      case FETCH_STATUSES_SUCCESS:
        return statusesSuccess(state, action.payload);
      case FETCH_STATUSES_ERROR:
        return statusesFetchLoading(state, true);
      default:
        return state;
    }
  }
};
