import { fromJS } from 'immutable';
import unionBy from 'lodash/unionBy';
import sortBy from 'lodash/sortBy';
import { GET_APPEALS_LOAD, GET_APPEALS_SUCCESS, GET_APPEALS_ERROR } from './constants';
import { convertFromImmutableToJS } from '../../helpers/common';

const appealsSuccess = (state, payload) => {
  const incidentsMapData = convertFromImmutableToJS(state).appealsData.data || [];
  const unionIncidents = unionBy([...incidentsMapData, payload], 'incidentId');
  const sortedIncidents = sortBy(unionIncidents, (dateObj) => {
    return -(new Date(dateObj.created));
  });
  return state.set('appealsData', {
    data: sortedIncidents,
    count: sortedIncidents.length,
    loading: false,
  });
};

const appealsLoading = (state, value) => {
  return state.set('appealsData', {
    loading: value,
    data: [],
    count: 0,
  });
};

export const storeName = 'appeals';

const initialState = fromJS({
  appealsData: {
    loading: false,
    data: [],
    count: 0,
  }
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case GET_APPEALS_LOAD:
        return appealsLoading(state, true);
      case GET_APPEALS_SUCCESS:
        return appealsSuccess(state, action.payload);
      case GET_APPEALS_ERROR:
        return appealsLoading(state, true);
      default:
        return state;
    }
  }
};
