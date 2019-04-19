import { fromJS } from 'immutable';
import { APPEAL_LOADED } from './constants';

const appealLoaded = (state, { data }) => {
  return state.set('appeal', fromJS(data));
};

export const storeName = 'appealCard';

const initialState = fromJS({
  appeal: {}
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case APPEAL_LOADED:
        return appealLoaded(state, action.payload);
      default:
        return state;
    }
  }
};