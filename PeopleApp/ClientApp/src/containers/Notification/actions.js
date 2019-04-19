import { SHOW_NOTIFICATION, HIDE_NOTIFICATION } from './constants';

export const showNotification = (type, text) => {
  return {
    type: SHOW_NOTIFICATION,
    payload: {
      type,
      text
    }
  };
};

export const hideNotification = () => {
  return {
    type: HIDE_NOTIFICATION
  };
};