import { push } from 'react-router-redux';
import { ROUTE_AUTH, ROUTE_HOME } from '../constants/routes';

export const checkUserShouldBeAuthenticated = (props, dispatch) => {
  if (!props.isUserAuthenticated) {
    dispatch(push({
      pathname: ROUTE_AUTH,
      state: {
        backUrl: props.location.pathname
      }
    }));
    return false;
  }

  return true;
};

export const checkUserShouldNotBeAuthenticated = (props, dispatch) => {
  if (props.isUserAuthenticated) {
    dispatch(push(ROUTE_HOME));
    return false;
  }

  return true;
};


export default (props, routerCheckers) => {
  const {
    dispatch,
    ...restProps
  } = props;

  let result = true;

  if (Array.isArray(routerCheckers)) {
    routerCheckers.forEach((currentCheckerFunction) => {
      if (result) {
        result = currentCheckerFunction(restProps, dispatch);
      }
    });
  } else if (typeof routerCheckers === 'function') {
    result = routerCheckers(restProps, dispatch);
  } else {
    throw new Error('Argument routerCheckers should be function or array');
  }

  return result;
};
