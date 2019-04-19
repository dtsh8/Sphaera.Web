import { put, call, takeEvery } from 'redux-saga/effects';
import { hideNotification } from './actions';
import { SHOW_NOTIFICATION } from './constants';
import { sagasDelay } from '../../helpers/common';

function* autoDismissNotification() {
  yield call(sagasDelay, 5000);
  yield put(hideNotification());
}

export function* notificationSagas() {
  yield takeEvery(SHOW_NOTIFICATION, autoDismissNotification);
}
