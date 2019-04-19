import { fromJS } from 'immutable';
import moment from 'moment';
import { FETCH_WEATHER, FETCH_WEATHER_ERROR, FETCH_WEATHER_SUCCESS } from './constants';

const weatherSuccess = (state, { forecastData, cityName }) => {
  return state
    .set('forecast', {
      date: moment(forecastData.dt * 1000),
      temp: forecastData.main.temp,
      humidity: forecastData.main.humidity,
      weather: forecastData.weather[0],
      pressure: forecastData.main.pressure * 0.75,
      wind: forecastData.wind.speed
    })
    .set('loading', false)
    .set('city', `${forecastData.name}, ${forecastData.sys.country}`)
    .set('cityName', cityName);
};

export const storeName = 'weather';

const initialState = fromJS({
  loading: false,
  forecast: {},
  city: 'Rostov-on-Don, RU',
  cityName: 'Ростов-на-Дону'
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case FETCH_WEATHER:
        return state.set('loading', true);
      case FETCH_WEATHER_SUCCESS:
        return weatherSuccess(state, action.payload);
      case FETCH_WEATHER_ERROR:
        return state.set('loading', false);
      default:
        return state;
    }
  }
};
