import { fromJS } from 'immutable';
import { FETCH_TYPES, FETCH_TYPES_ERROR, FETCH_TYPES_SUCCESS } from './constants';

const typesSuccess = (state, payload) => {
  return state.set('types', {
    count: payload.length,
    results: payload,
    loading: false
  });
};

const typesFetchLoading = (state, value) => {
  return state.set('types', {
    loading: value
  });
};

export const storeName = 'dictionary-types';

const initialState = fromJS({
  types: {
    count: 0,
    results: [],
    loading: false
  }
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case FETCH_TYPES:
        return typesFetchLoading(state, true);
      case FETCH_TYPES_SUCCESS:
        return typesSuccess(state, action.payload);
      case FETCH_TYPES_ERROR:
        return typesFetchLoading(state, true);
      default:
        return state;
    }
  }
};
