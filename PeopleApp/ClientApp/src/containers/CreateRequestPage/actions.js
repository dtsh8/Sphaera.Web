import {
  SEND_REQUESTPAGE_FORM,
  SEND_REQUESTPAGE_FORM_ERROR,
  SEND_REQUESTPAGE_FORM_SUCCESS,
  GET_ADDRESS,
  SET_ADDRESS,
  ADDRESS_LOADED,
  GET_AUTOCOMPLETE_ADDRESS,
  AUTOCOMPLETE_ADDRESS_LOADED,
  ADDRESS_AND_COORDS_LOADED,
} from './constants';

export const submitFormRequestError = (error) => {
  return {
    type: SEND_REQUESTPAGE_FORM_ERROR,
    error
  };
};

export const submitFormRequestSuccess = () => {
  return {
    type: SEND_REQUESTPAGE_FORM_SUCCESS,
  };
};

export const onSubmitForm = (data) => {
  return {
    type: SEND_REQUESTPAGE_FORM,
    data
  };
};

export const getAddress = (location) => {
  return {
    type: GET_ADDRESS,
    payload: {
      location
    }
  };
};

export const setAddress = (address) => {
  return {
    type: SET_ADDRESS,
    payload: {
      searchField: address
    }
  };
};


export const addressLoaded = (address) => {
  return {
    type: ADDRESS_LOADED,
    payload: {
      address
    }
  };
};

export const getAutocompleteAddress = (cityName, location) => {
  return {
    type: GET_AUTOCOMPLETE_ADDRESS,
    payload: {
      cityName,
      location
    }
  };
};

export const autocompleteAddressLoaded = (cities) => {
  return {
    type: AUTOCOMPLETE_ADDRESS_LOADED,
    payload: {
      cities
    }
  };
};

export const addressAndCoordLoaded = (address, coord) => {
  return {
    type: ADDRESS_AND_COORDS_LOADED,
    payload: {
      address,
      coord
    }
  };
};
