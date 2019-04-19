import { fromJS } from 'immutable';
import {
  SET_USER_AUNTHENTICATED,
  FETCH_USER_SUCCESS,
  USER_AUTHENTICATED,
  USER_LOGOUT_COMPLETED
} from './constants';

export const storeName = 'auth';

export const initialUserData = {};

const userAuthenticated = (state, payload) => {
  return state.set('user', fromJS(payload));
};

const userLogout = (state) => {
  return state.set('user', fromJS(initialUserData));
};


const userSuccess = (state, payload) => {
  return state.set('user', payload);
};

const setUserAunthenticated = (state, value) => {
  return state.set('isUserAuthenticated', value);
};

const initialState = fromJS({
  user: initialUserData,
  isUserAuthenticated: false
});

export default {
  [storeName]: (state = initialState, action) => {
    switch (action.type) {
      case USER_AUTHENTICATED:
        return userAuthenticated(state, action.payload);
      case USER_LOGOUT_COMPLETED:
        return userLogout(state);
      case FETCH_USER_SUCCESS:
        return userSuccess(state, action.payload);
      case SET_USER_AUNTHENTICATED:
        return setUserAunthenticated(state, action.isUserAuthenticated);
      default:
        return state;
    }
  }
};
