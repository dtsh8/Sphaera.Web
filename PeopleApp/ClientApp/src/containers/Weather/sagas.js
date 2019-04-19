import { put, call, takeEvery } from 'redux-saga/effects';
import * as actions from './actions.js';
import { fetchForecastByCityName, getCityNameByGeo } from '../../api/openWeatherMap';
import { FETCH_WEATHER } from './constants';
import { USER_LOGOUT } from '../AuthPage/constants';

function* fetchWeatherAsync({ userLocation }) {
  try {
    const forecastData = yield call(fetchForecastByCityName, { userLocation, defaultCity: 'Rostov-on-Don' });
    const cityNameReponse = yield call(getCityNameByGeo, forecastData.coord);
    const cityName = cityNameReponse.address.city;

    yield put(actions.fetchWeatherSuccess(forecastData, cityName));
  } catch (error) {
    yield put(actions.fetchWeatherError());
  }
}

function* userLogout() {
  try {
    const forecastData = yield call(fetchForecastByCityName, { undefined, defaultCity: 'Rostov-on-Don' });
    yield put(actions.fetchWeatherSuccess(forecastData, 'Ростов-на-Дону'));
  } catch (error) {
    yield put(actions.fetchWeatherError());
  }
}

export function* watchFetchWeather() {
  yield takeEvery(FETCH_WEATHER, fetchWeatherAsync);
  yield takeEvery(USER_LOGOUT, userLogout);
}
