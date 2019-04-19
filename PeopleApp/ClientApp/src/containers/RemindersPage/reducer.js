import { fromJS, setIn } from 'immutable';
import { 
  REMINDERS_CONTENT_LOADED,
  REMINDER_LIST_LOADED,
  SET_REMINDER_NODE_EXPANDED,
  SET_REMINDER_NODE_COLLAPSED
} from './constants';

export const storeName = 'reminders';

const remindersContentLoaded = (state, payload) => {
  return state.set('remindersData', payload);
};

const reminderListLoaded = (state, payload) => {
  return state.set('reminderList', payload);
};

const setReminderNodeExpanded = (state, id, value) => {
  return state.setIn(['expandedNodes', id], value);
};

const initialState = fromJS({
  remindersData: null,
  reminderList: [],
  expandedNodes: {}
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case REMINDERS_CONTENT_LOADED:
        return remindersContentLoaded(state, action.payload);
      case REMINDER_LIST_LOADED:
        return reminderListLoaded(state, action.payload);
      case SET_REMINDER_NODE_EXPANDED:
        return setReminderNodeExpanded(state, action.payload, true);
      case SET_REMINDER_NODE_COLLAPSED:
        return setReminderNodeExpanded(state, action.payload, false);
      default:
        return state;
    }
  }
};
