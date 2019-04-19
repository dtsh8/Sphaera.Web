import { CHANGE_CURRENT_URL_PARAM } from './constants';

export const changeCurrentUrlParam = (name, value) => {
  return {
    type: CHANGE_CURRENT_URL_PARAM,
    payload: {
      name,
      value
    }
  };
};
