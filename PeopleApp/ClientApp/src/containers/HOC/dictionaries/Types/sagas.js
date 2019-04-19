import { put, call, takeEvery } from 'redux-saga/effects';
import { fetchTypesSuccess, fetchTypesError } from './actions';
import context from '../../../../api/api';

import { FETCH_TYPES } from './constants';

function* fetchTypesAsync() {
  try {
    const types = yield call(context.cardIndex.getTree, 0);
    yield put(fetchTypesSuccess(types));
  } catch (error) {
    yield put(fetchTypesError());
  }
}

export function* watchFetchTypes() {
  yield takeEvery(FETCH_TYPES, fetchTypesAsync);
}
