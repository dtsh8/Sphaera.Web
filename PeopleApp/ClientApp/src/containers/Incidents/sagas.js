import { call, put, takeEvery } from 'redux-saga/effects';
import { connectIncidents } from '../../api/incidents';
import { createInsertEvents } from '../../helpers/api';
import { GET_INCIDENTS_MAP_LOAD, CHANGE_FILTER } from './constants';
import { getIncidentsSuccess, getIncidentsError, filterChanged } from './actions';

function* monitorInsertEvents(channel) {
  while (true) {
    const incident = yield call(channel.take);
    yield put(getIncidentsSuccess(incident));
  }
}

function* fetchIncidentsAsync() {
  try {
    yield call(connectIncidents);
    yield call(monitorInsertEvents, createInsertEvents());
  } catch (error) {
    yield put(getIncidentsError());
  }
}

function* changeFilterAsync({ payload }) {
  yield put(filterChanged(payload));
}

export function* incidentsSaga() {
  yield takeEvery(GET_INCIDENTS_MAP_LOAD, fetchIncidentsAsync);
  yield takeEvery(CHANGE_FILTER, changeFilterAsync);
}
