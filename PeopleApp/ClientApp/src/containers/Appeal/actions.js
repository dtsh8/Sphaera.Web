import {
  LOAD_APPEAL,
  APPEAL_LOADED,
  DOWNLOAD_FILE,
  DOWNLOAD_ALL_FILES,
} from './constants';

export const getAppeal = (incidentId, cardId) => {
  return {
    type: LOAD_APPEAL,
    payload: {
      incidentId,
      cardId
    }
  };
};

export const appealLoaded = (data) => {
  return {
    type: APPEAL_LOADED,
    payload: {
      data
    }
  };
};

export const downloadFile = (incidentId, cardId, fileName) => {
  return {
    type: DOWNLOAD_FILE,
    payload: {
      incidentId,
      cardId,
      fileName
    }
  };
};

export const downloadAllFiles = (incidentId, cardId, fileNames) => {
  return {
    type: DOWNLOAD_ALL_FILES,
    payload: {
      incidentId,
      cardId,
      fileNames
    }
  };
};