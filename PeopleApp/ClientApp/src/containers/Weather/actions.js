import { FETCH_WEATHER, FETCH_WEATHER_ERROR, FETCH_WEATHER_SUCCESS } from './constants';

export const fetchWeather = (userLocation) => {
  return {
    type: FETCH_WEATHER,
    userLocation
  };
};

export const fetchWeatherSuccess = (forecastData, cityName) => {
  return {
    type: FETCH_WEATHER_SUCCESS,
    payload: {
      forecastData,
      cityName
    }
  };
};

export const fetchWeatherError = () => {
  return { type: FETCH_WEATHER_ERROR };
};
