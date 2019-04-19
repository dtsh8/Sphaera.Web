import {
  GET_REMINDERS_CONTENT,
  REMINDERS_CONTENT_LOADED,
  GET_REMINDER_LIST,
  REMINDER_LIST_LOADED,
  SET_REMINDER_NODE_EXPANDED,
  SET_REMINDER_NODE_COLLAPSED
} from './constants';

export const getReminderList = () => {
  return {
    type: GET_REMINDER_LIST
  };
};

export const reminderListLoaded = (payload) => {
  return {
    type: REMINDER_LIST_LOADED,
    payload
  };
};

export const getRemindersContent = (id) => {
  return {
    type: GET_REMINDERS_CONTENT,
    payload: {
      id
    }
  };
};

export const remindersContentLoaded = (payload) => {
  return {
    type: REMINDERS_CONTENT_LOADED,
    payload
  };
};

export const setReminderNodeExpanded = (id) => {
  return {
    type: SET_REMINDER_NODE_EXPANDED,
    payload: id
  };
};

export const setReminderNodeCollapsed = (id) => {
  return {
    type: SET_REMINDER_NODE_COLLAPSED,
    payload: id
  };
};
