import { put, call, takeEvery } from 'redux-saga/effects';
import context from '../../../../api/api';
import { fetchStatusesSuccess, fetchStatusesError } from './actions';
import { FETCH_STATUSES } from './constants';

function* fetchStatusesAsync() {
  try {
    const response = yield call(context.incidentState.getList);

    yield put(fetchStatusesSuccess(response));
  } catch (error) {
    yield put(fetchStatusesError());
  }
}

export function* watchFetchStatuses() {
  yield takeEvery(FETCH_STATUSES, fetchStatusesAsync);
}
