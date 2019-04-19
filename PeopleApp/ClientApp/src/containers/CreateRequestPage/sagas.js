import { put, call, takeEvery } from 'redux-saga/effects';
import { push } from 'react-router-redux';
import { sendAppeal } from '../../api/appeals';
import { sendFile } from '../../api/files';
import {
  GET_ADDRESS,
  GET_AUTOCOMPLETE_ADDRESS,
  SEND_REQUESTPAGE_FORM,
  SET_ADDRESS,
} from './constants';
import { ROUTE_HOME } from '../../constants/routes';
import { getCityNameByGeo, getCitiesForAutocomplete, getGeoByCityName } from '../../api/openWeatherMap';
import {
  addressLoaded,
  submitFormRequestSuccess,
  submitFormRequestError,
  autocompleteAddressLoaded,
  addressAndCoordLoaded,
} from './actions';
import { defaultLocation } from '../LeafletMap/Container';

function* fetchRequestPageAsync({ data }) {
  try {
    const { files } = data;
    const response = yield call(sendAppeal, data);
    const { incidentId, cardId } = response;
    const appealsSearchParam = '?type=appeals';

    if (files && !!files.length) {
      for (let i = 0; i < files.length; i += 1) {
        const { fileName, fileBody } = files[i];
        const fileData = {
          incidentId,
          cardId,
          fileName,
          fileBody
        };
        yield call(sendFile, fileData);
      }
    }

    yield put(submitFormRequestSuccess());
    yield put(
      push({
        pathname: ROUTE_HOME,
        search: appealsSearchParam
      })
    );
  } catch (error) {
    yield put(submitFormRequestError(error));
  }
}

function* getAddress({ payload: { location } }) {
  const response = yield call(getCityNameByGeo, { lat: location[0], lon: location[1] });
  const formattedAddress = yield call(getCitiesForAutocomplete, response.display_name);
  yield put(addressLoaded(formattedAddress[0]));
}

function* setAddress({ payload: { searchField } }) {
  const response = yield call(getCitiesForAutocomplete, searchField);
  const addressWithCoords = yield call(getGeoByCityName, response && !!response.length ? response[0].fullAddress : '');
  const coord = addressWithCoords && !!addressWithCoords.length ?
    [parseFloat(addressWithCoords[0].lat), parseFloat(addressWithCoords[0].lon)] :
    defaultLocation;
  yield put(addressAndCoordLoaded(response[0], coord));
}

function* getAutocompleteAddress({ payload: { cityName } }) {
  const response = yield call(getCitiesForAutocomplete, cityName);
  const cities = response.map(city => city.fullAddress);
  yield put(autocompleteAddressLoaded(cities));
}

export function* watchSubmitFormRequest() {
  yield takeEvery(SEND_REQUESTPAGE_FORM, fetchRequestPageAsync);
  yield takeEvery(GET_ADDRESS, getAddress);
  yield takeEvery(SET_ADDRESS, setAddress);
  yield takeEvery(GET_AUTOCOMPLETE_ADDRESS, getAutocompleteAddress);
}
