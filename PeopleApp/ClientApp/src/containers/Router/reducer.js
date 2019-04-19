import { fromJS } from 'immutable';
import { LOCATION_CHANGE } from 'react-router-redux';
import { getPageFromCurrentUrl, getTypeFromCurrentUrl } from '../../helpers/paramsUrl';

export const storeName = 'router';

const initialState = fromJS({
  location: null,
  breadcrumbs: [],
  currentPage: null,
  currentType: null
});

const onLocationChange = (state, payload) => {
  return state
    .set('location', payload)
    .set('breadcrumbs', payload.pathname.split('/'))
    .set('currentPage', getPageFromCurrentUrl() || null)
    .set('currentType', getTypeFromCurrentUrl() || null);
};

export default {
  [storeName]: (state = initialState, { type, payload } = {}) => {
    if (type === LOCATION_CHANGE) {
      return onLocationChange(state, payload);
    }

    return state;
  }
};
