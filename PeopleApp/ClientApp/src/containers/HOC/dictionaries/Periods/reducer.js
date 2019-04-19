import { fromJS } from 'immutable';
import { FETCH_PERIODS, FETCH_PERIODS_ERROR, FETCH_PERIODS_SUCCESS } from './constants';

const periodsSuccess = (state, payload) => {
  return state.set('periods', {
    count: payload.count,
    results: payload.results,
    loading: false
  });
};

const periodsFetchLoading = (state, value) => {
  return state.set('periods', {
    loading: value
  });
};

export const storeName = 'dictionary-periods';

const initialState = fromJS({
  periods: {
    count: 0,
    results: [],
    loading: false
  }
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case FETCH_PERIODS:
        return periodsFetchLoading(state, true);
      case FETCH_PERIODS_SUCCESS:
        return periodsSuccess(state, action.payload);
      case FETCH_PERIODS_ERROR:
        return periodsFetchLoading(state, true);
      default:
        return state;
    }
  }
};
