import { call, put, takeEvery, all } from 'redux-saga/effects';
import { appealLoaded } from './actions';
import {
  LOAD_APPEAL,
  DOWNLOAD_FILE,
  DOWNLOAD_ALL_FILES,
} from './constants';
import { getFileList, getFile } from '../../api/files';
import { getIncident } from '../../api/incidents';
// import { getGeoByCityName } from '../../api/openWeatherMap';
import { base64ToArrayBuffer } from '../../helpers/common';

function* loadAppeal({ payload: { incidentId, cardId } }) {
  try {
    const response = yield call(getIncident, incidentId);
    const files = yield call(getFileList, incidentId, cardId);
    const card = response.cards.find(item => item.cardId === cardId);
    // const geo = yield call(getGeoByCityName, response.address.fullAddress);
    const appeal = {
      ...card,
      message: response.claim.message,
      files,
      address: response.address && response.address.fullAddress,
    };
    yield put(appealLoaded(appeal));
  } catch (e) {
    console.error(e);
  }
}

function* downloadFile({ payload: { incidentId, cardId, fileName } }) {
  try {
    const fileBody = yield call(getFile, incidentId, cardId, fileName);
    const blob = new Blob([base64ToArrayBuffer(fileBody)]);
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
  } catch (e) {
    console.error(e);
  }
}

function* downloadAllFiles({ payload: { incidentId, cardId, fileNames } }) {
  try {
    const arrayOfYieldFiles = fileNames.map(fileName => getFile(incidentId, cardId, fileName));

    const data = yield all(arrayOfYieldFiles);

    data.forEach((file, i) => {
      const blob = new Blob([base64ToArrayBuffer(file)]);
      const link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      link.download = fileNames[i];
      link.click();
    });
  } catch (e) {
    console.error(e);
  }
}

export function* appealCardSagas() {
  yield takeEvery(LOAD_APPEAL, loadAppeal);
  yield takeEvery(DOWNLOAD_FILE, downloadFile);
  yield takeEvery(DOWNLOAD_ALL_FILES, downloadAllFiles);
}
