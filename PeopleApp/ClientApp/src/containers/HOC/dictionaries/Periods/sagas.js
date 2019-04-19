import { put, call, takeEvery } from 'redux-saga/effects';
import * as actions from './actions.js';
import { getPeriods } from '../../../../api/dictionaries';

import { FETCH_PERIODS } from './constants';

function* fetchPeriodsAsync() {
  try {
    const response = yield call(getPeriods);
    yield put(actions.fetchPeriodsSuccess(response));
  } catch (error) {
    yield put(actions.fetchPeriodsError());
  }
}

export function* watchFetchPeriods() {
  yield takeEvery(FETCH_PERIODS, fetchPeriodsAsync);
}
