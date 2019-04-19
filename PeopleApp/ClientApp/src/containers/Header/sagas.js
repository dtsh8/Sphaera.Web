import { put, call, takeEvery } from 'redux-saga/effects';
import { LOAD_HEADER_EMERGENCY_DATA } from './constants';
import context from '../../api/api';
import { loadHeaderDataEmergencySuccess, loadHeaderDataError } from './actions';

function* loadHeaderData() {
  try {
    const response = yield call(context.card.getEmergencyCards);

    yield put(loadHeaderDataEmergencySuccess(response));
  } catch (error) {
    yield put(loadHeaderDataError(error));
  }
}

export function* headerEmergencySaga() {
  yield takeEvery(LOAD_HEADER_EMERGENCY_DATA, loadHeaderData);
}
