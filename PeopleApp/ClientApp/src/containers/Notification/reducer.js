import { fromJS } from 'immutable';
import { SHOW_NOTIFICATION, HIDE_NOTIFICATION } from './constants';


const hideNotification = (state) => {
  return state
    .set('show', false)
    .set('text', '');
};

const showNotification = (state, { type, text }) => {
  return state
    .set('show', true)
    .set('type', type)
    .set('text', text);
};

export const storeName = 'notification';

const initialState = fromJS({
  show: false,
  type: 'success',
  text: ''
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case SHOW_NOTIFICATION:
        return showNotification(state, action.payload);
      case HIDE_NOTIFICATION:
        return hideNotification(state);
      default:
        return state;
    }
  }
};
