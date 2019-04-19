import { fromJS } from 'immutable';
import { FOOTER_DATA_LOADED } from './constants';

export const storeName = 'footer';

const footerDataLoaded = (state, { footerData }) => {
  return state
    .set('footerData', footerData);
};
const initialState = fromJS({
  footerData: []
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case FOOTER_DATA_LOADED:
        return footerDataLoaded(state, action.payload);
      default:
        return state;
    }
  }
};
