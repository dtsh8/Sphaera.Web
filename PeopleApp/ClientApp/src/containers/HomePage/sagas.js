import { call, put, takeEvery } from 'redux-saga/effects';
import { LOAD_STATISTIC } from './constants';
import { statisticLoaded } from './actions';
import context from '../../api/api';
import { sagasDelay } from '../../helpers/common';

function* fetchStatistic() {
  yield call(context.card.initialize);
  // delete delay after be initialize promise
  yield call(sagasDelay, 500);

  const statistic = yield call(context.card.getStatistics);

  yield put(statisticLoaded(statistic));
}

export function* homeSagas() {
  yield takeEvery(LOAD_STATISTIC, fetchStatistic);
}
