import {
  FETCH_USER,
  FETCH_USER_ERROR,
  FETCH_USER_SUCCESS,
  LOGIN_USER,
  USER_AUTHENTICATED,
  USER_LOGOUT,
  USER_LOGOUT_COMPLETED,
  SET_USER_AUNTHENTICATED,
  CHECK_AUTH,
  REDIRECT_TO_PROFILE,
} from './constants';

export const loginUser = () => {
  return {
    type: LOGIN_USER
  };
};

export const userAuthenticated = (payload) => {
  return {
    type: USER_AUTHENTICATED,
    payload
  };
};

export const userLogout = () => {
  return {
    type: USER_LOGOUT
  };
};

export const userLogoutCompleted = () => {
  return {
    type: USER_LOGOUT_COMPLETED
  };
};

export const fetchUser = () => {
  return { type: FETCH_USER };
};

export const fetchUserSuccess = (payload) => {
  return {
    type: FETCH_USER_SUCCESS,
    payload
  };
};

export const fetchUserError = () => {
  return { type: FETCH_USER_ERROR };
};

export const checkAuth = () => {
  return {
    type: CHECK_AUTH
  };
};

export const setIsUserAuthenticated = (value) => {
  return {
    type: SET_USER_AUNTHENTICATED,
    isUserAuthenticated: value
  };
};

export const redirectToEditProfile = () => {
  return {
    type: REDIRECT_TO_PROFILE
  };
};
