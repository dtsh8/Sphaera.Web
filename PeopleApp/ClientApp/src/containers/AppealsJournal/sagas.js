import { call, put, takeEvery } from 'redux-saga/effects';
import { push } from 'react-router-redux';
import { stringify } from 'qs';
import { connectAppealsJournal } from '../../api/appeals';
import { createInsertAppealEvents } from '../../helpers/api';
import { GET_APPEALS_LOAD, REDIRECT_TO_CARD } from './constants';
import { getAppealsSuccess, getAppealsError } from './actions';
import { ROUTE_ONE_APPEAL } from '../../constants/routes';

function* monitorInsertEvents(channel) {
  while (true) {
    const appeal = yield call(channel.take);
    yield put(getAppealsSuccess(appeal));
  }
}

function* fetchAppealsAsync() {
  try {
    yield call(connectAppealsJournal);
    yield call(monitorInsertEvents, createInsertAppealEvents());
  } catch (error) {
    yield put(getAppealsError());
  }
}

function* redirectToCard({ payload: { cardId, incidentId } }) {
  const params = stringify({ incidentId, cardId });
  yield put(push({
    pathname: ROUTE_ONE_APPEAL,
    state: {
      cardId,
      incidentId
    },
    search: params
  }));
}

export function* appealsSaga() {
  yield takeEvery(GET_APPEALS_LOAD, fetchAppealsAsync);
  yield takeEvery(REDIRECT_TO_CARD, redirectToCard);
}
