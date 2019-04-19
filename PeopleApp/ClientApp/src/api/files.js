import context from './api';

export const sendFile = (file) => {
  const {
    incidentId,
    cardId,
    fileName,
    fileBody
  } = file;

  return context.file.set(incidentId, cardId, fileName, fileBody);
};

export const getFileList = (incidentId, cardId) => {
  return context.file.getList(incidentId, cardId);
};

export const getFile = (incidentId, cardId, fileName) => {
  return context.file.getBody(incidentId, cardId, fileName);
};