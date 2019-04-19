import { GET_APPEALS_LOAD, GET_APPEALS_SUCCESS, GET_APPEALS_ERROR, REDIRECT_TO_CARD } from './constants';

export const loadAppeals = () => {
  return {
    type: GET_APPEALS_LOAD
  };
};

export const getAppealsSuccess = (payload) => {
  return {
    type: GET_APPEALS_SUCCESS,
    payload
  };
};

export const getAppealsError = () => {
  return {
    type: GET_APPEALS_ERROR
  };
};

export const redirectToCard = (incidentId, cardId) => {
  return {
    type: REDIRECT_TO_CARD,
    payload: {
      incidentId,
      cardId
    }
  };
};