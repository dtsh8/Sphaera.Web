import { fromJS } from 'immutable';
import { LOADED_STATISTIC } from './constants';

export const storeName = 'homeStore';

const setStatistic = (state, payload) => {
  return state.set('statistic', payload.statistic);
};

const initialState = fromJS({
  statistic: []
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case LOADED_STATISTIC:
        return setStatistic(state, action.payload);
      default:
        return state;
    }
  }
};
