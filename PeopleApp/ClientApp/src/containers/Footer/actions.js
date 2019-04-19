import { GET_FOOTER_DATA, FOOTER_DATA_LOADED } from './constants';

export const getFooterData = () => {
  return {
    type: GET_FOOTER_DATA
  };
};

export const footerDataLoaded = (footerData) => {
  return {
    type: FOOTER_DATA_LOADED,
    payload: { footerData }
  };
};