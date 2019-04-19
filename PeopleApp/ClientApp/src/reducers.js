import { combineReducers } from 'redux-immutable';
import routerReducer from './containers/Router/reducer';
import weatherReducer from './containers/Weather/reducer';
import authPageReducer from './containers/AuthPage/reducer';
import remindersPageReducer from './containers/RemindersPage/reducer';
import statusesDictionaryReducer from './containers/HOC/dictionaries/Statuses/reducer';
import periodsDictionaryReducer from './containers/HOC/dictionaries/Periods/reducer';
import typesDictionaryReducer from './containers/HOC/dictionaries/Types/reducer';
import appealsJournalReducer from './containers/AppealsJournal/reducer';
import incidentsReducer from './containers/Incidents/reducer';
import footerReducer from './containers/Footer/reducer';
import headerReducer from './containers/Header/reducer';
import notificationReducer from './containers/Notification/reducer';
import createRequestReducer from './containers/CreateRequestPage/reducer';
import homePageReducer from './containers/HomePage/reducer';
import appealReducer from './containers/Appeal/reducer';

const appReducer = combineReducers({
  ...routerReducer,
  ...weatherReducer,
  ...authPageReducer,
  ...remindersPageReducer,
  ...statusesDictionaryReducer,
  ...periodsDictionaryReducer,
  ...typesDictionaryReducer,
  ...appealsJournalReducer,
  ...incidentsReducer,
  ...footerReducer,
  ...headerReducer,
  ...notificationReducer,
  ...createRequestReducer,
  ...homePageReducer,
  ...appealReducer,
});

export default (state, action) => {
  return appReducer(state, action);
};
