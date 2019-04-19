import { all } from 'redux-saga/effects';
import { watchFetchWeather } from './containers/Weather/sagas';
import { authSaga } from './containers/AuthPage/sagas';
import { remindersSaga } from './containers/RemindersPage/sagas';
import { watchFetchStatuses } from './containers/HOC/dictionaries/Statuses/sagas';
import { watchFetchPeriods } from './containers/HOC/dictionaries/Periods/sagas';
import { watchFetchTypes } from './containers/HOC/dictionaries/Types/sagas';
import { appealsSaga } from './containers/AppealsJournal/sagas';
import { incidentsSaga } from './containers/Incidents/sagas';
import { watchSubmitFormRequest } from './containers/CreateRequestPage/sagas';
import { footerSaga } from './containers/Footer/sagas';
import { headerEmergencySaga } from './containers/Header/sagas';
import { notificationSagas } from './containers/Notification/sagas';
import { homeSagas } from './containers/HomePage/sagas';
import { routerSaga } from './containers/Router/sagas';
import { appealCardSagas } from './containers/Appeal/sagas';

export default function* sagas() {
  yield all([
    watchFetchWeather(),
    authSaga(),
    remindersSaga(),
    watchFetchStatuses(),
    watchFetchPeriods(),
    watchFetchTypes(),
    appealsSaga(),
    incidentsSaga(),
    notificationSagas(),
    footerSaga(),
    watchSubmitFormRequest(),
    headerEmergencySaga(),
    homeSagas(),
    routerSaga(),
    appealCardSagas(),
  ]);
}
