import { takeEvery, select, put } from 'redux-saga/effects';
import { replace } from 'react-router-redux';
import { URL_PARAM_TYPE, URL_PARAM_PAGE } from '../../constants/paramsUrl';
import { CHANGE_CURRENT_URL_PARAM } from './constants';
import { updateUrlQueryString } from '../../helpers/paramsUrl';

const getLocation = state => state.getIn(['router', 'location']);
const getPage = state => state.getIn(['router', 'currentPage']);

function* changeCurrentUrlParam({ payload: { name, value } }) {
  const currentLocation = yield select(getLocation);
  const currentPage = yield select(getPage);

  const values = [];
  values.push([name, value]);

  if (name === URL_PARAM_TYPE && currentPage) {
    values.push([URL_PARAM_PAGE, 1]);
  }

  yield put(replace(`${currentLocation.pathname}${updateUrlQueryString(values)}`));
}

export function* routerSaga() {
  yield takeEvery(CHANGE_CURRENT_URL_PARAM, changeCurrentUrlParam);
}