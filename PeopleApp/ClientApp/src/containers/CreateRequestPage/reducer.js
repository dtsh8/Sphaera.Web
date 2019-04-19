import { fromJS } from 'immutable';
import {
  ADDRESS_LOADED,
  AUTOCOMPLETE_ADDRESS_LOADED,
  ADDRESS_AND_COORDS_LOADED,
  SEND_REQUESTPAGE_FORM,
  SEND_REQUESTPAGE_FORM_SUCCESS,
  SEND_REQUESTPAGE_FORM_ERROR,
} from './constants';

export const storeName = 'createRequest';

const addressLoaded = (state, { address }) => {
  return state.set('address', address);
};

const sendingAppeal = (state) => {
  return state.set('isAppealSending', true);
};

const appealSent = (state) => {
  return state.set('isAppealSending', false);
};

const autocompleteAddressLoaded = (state, { cities }) => {
  return state
    .set('autocompleteCities', cities);
};

const addressAndCoordLoaded = (state, { address, coord }) => {
  return state
    .set('address', address)
    .set('coord', coord);
};

const initialState = fromJS({
  address: null,
  autocompleteCities: [],
  coord: [],
  isAppealSending: false,
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case ADDRESS_LOADED:
        return addressLoaded(state, action.payload);
      case AUTOCOMPLETE_ADDRESS_LOADED:
        return autocompleteAddressLoaded(state, action.payload);
      case ADDRESS_AND_COORDS_LOADED:
        return addressAndCoordLoaded(state, action.payload);
      case SEND_REQUESTPAGE_FORM:
        return sendingAppeal(state);
      case SEND_REQUESTPAGE_FORM_SUCCESS:
        return appealSent(state);
      case SEND_REQUESTPAGE_FORM_ERROR:
        return appealSent(state);
      default:
        return state;
    }
  }
};
