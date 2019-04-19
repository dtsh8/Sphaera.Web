import { call, put, takeEvery } from 'redux-saga/effects';
import { GET_REMINDERS_CONTENT, GET_REMINDER_LIST } from './constants';
import { remindersContentLoaded, reminderListLoaded } from './actions';
import context from '../../api/api';

function* loadRemindersContent({ payload: { id } }) {
  const remindersResponse = yield call(context.documents.get, id);
  yield put(remindersContentLoaded(remindersResponse));
}

function* loadReminderList() {
  const response = yield call(context.documents.getTree);
  yield put(reminderListLoaded(response));
}

export function* remindersSaga() {
  yield takeEvery(GET_REMINDERS_CONTENT, loadRemindersContent);
  yield takeEvery(GET_REMINDER_LIST, loadReminderList);
}
