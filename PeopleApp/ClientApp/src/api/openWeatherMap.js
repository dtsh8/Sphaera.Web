import axios from 'axios';
import context from './api';
import { handleAxiosError } from '../helpers/errorHandles';

const apiToken = '8a4ff15fee885262ec8547d531dc4b77';

export const fetchForecastByCityName = ({ userLocation, defaultCity }) => {
  const requestUrl = 'https://cors-anywhere.herokuapp.com/http://api.openweathermap.org/data/2.5/weather';

  if (!!userLocation && !!userLocation.length) {
    const location = Array.isArray(userLocation) ? userLocation : JSON.parse(userLocation);

    return handleAxiosError(axios.get(requestUrl, {
      params: {
        APPID: apiToken,
        lat: location[0],
        lon: location[1],
        units: 'metric',
        lang: 'ru'
      }
    }));
  }

  return handleAxiosError(axios.get(requestUrl, {
    params: {
      APPID: apiToken,
      q: defaultCity,
      units: 'metric',
      lang: 'ru'
    }
  }));
};


export const getCityNameByGeo = ({ lat, lon }) => {
  const requestUrl = 'https://nominatim.openstreetmap.org/reverse';

  return handleAxiosError(axios.get(requestUrl, {
    params: {
      format: 'json',
      addressdetails: 1,
      limit: 1,
      lat,
      lon
    }
  }));
};


export const getCitiesForAutocomplete = (cityName) => {
  return context.address.find(cityName);
};

export const getGeoByCityName = (cityName) => {
  const requestUrl = 'https://nominatim.openstreetmap.org/search';

  return handleAxiosError(axios.get(requestUrl, {
    params: {
      format: 'json',
      addressdetails: 1,
      limit: 1,
      q: cityName
    }
  }));
};
