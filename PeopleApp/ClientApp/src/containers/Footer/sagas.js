import { put, call, takeEvery } from 'redux-saga/effects';
import { footerDataLoaded } from './actions.js';
import { getFooterData } from '../../api/common';
import { GET_FOOTER_DATA } from './constants';

function* loadFooterData() {
  const response = yield call(getFooterData);
  yield put(footerDataLoaded(response));
}

export function* footerSaga() {
  yield takeEvery(GET_FOOTER_DATA, loadFooterData);
}
