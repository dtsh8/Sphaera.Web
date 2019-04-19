import { call, put, takeEvery } from 'redux-saga/effects';
import { push } from 'react-router-redux';
import context from '../../api/api';
import { CHECK_AUTH, FETCH_USER, LOGIN_USER, REDIRECT_TO_PROFILE, USER_LOGOUT } from './constants';
import {
  userLogoutCompleted,
  fetchUserSuccess,
  fetchUserError,
  setIsUserAuthenticated
} from './actions';
import { ROUTE_HOME } from '../../constants/routes';

function* loginUser() {
  yield call(context.account.login);
}

function* userLogout() {
  yield call(context.account.logout);

  yield put(userLogoutCompleted());
  yield put(push(ROUTE_HOME));
}

function* checkAuth() {
  const isAuthenticated = yield call(context.account.isAuthenticated);

  yield put(setIsUserAuthenticated(isAuthenticated));
}

function* fetchUserAsync() {
  try {
    const response = yield call(context.account.getProfile);
    yield put(fetchUserSuccess(response));
  } catch (error) {
    yield put(fetchUserError());
  }
}

function* redirectToProfile() {
  yield call(context.account.edit);
}

export function* authSaga() {
  yield takeEvery(LOGIN_USER, loginUser);
  yield takeEvery(USER_LOGOUT, userLogout);
  yield takeEvery(FETCH_USER, fetchUserAsync);
  yield takeEvery(CHECK_AUTH, checkAuth);
  yield takeEvery(REDIRECT_TO_PROFILE, redirectToProfile);
}