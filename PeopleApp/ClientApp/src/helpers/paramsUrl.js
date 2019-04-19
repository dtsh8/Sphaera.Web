import { parse, stringify } from 'qs';
import { URL_PARAM_PAGE, URL_PARAM_TYPE } from '../constants/paramsUrl';

function getUrlParams() {
  return parse(window.location.search, { ignoreQueryPrefix: true });
}

export function getQueryString(params) {
  return stringify(params, { addQueryPrefix: true });
}

export function updateUrlQueryString(values) {
  const params = getUrlParams();

  values.forEach(([paramName, value]) => {
    params[paramName] = value;
  });

  return getQueryString(params);
}

export function getPageFromCurrentUrl() {
  return getUrlParams()[URL_PARAM_PAGE];
}

export function getTypeFromCurrentUrl() {
  return getUrlParams()[URL_PARAM_TYPE];
}