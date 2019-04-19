import React from 'react';
import { connect } from 'react-redux';
import { fetchUser, checkAuth } from '../../AuthPage/actions';
import { storeName as authStoreName } from '../../AuthPage/reducer';
import { convertFromImmutableToJS } from '../../../helpers/common';

const mapStateToProps = (state) => {
  return {
    user: convertFromImmutableToJS(state.getIn([authStoreName, 'user'])),
    isUserAuthenticated: convertFromImmutableToJS(state.getIn([authStoreName, 'isUserAuthenticated'])),
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadUserInfo: () => {
      dispatch(fetchUser());
    },
    checkUserAutorized: () => {
      dispatch(checkAuth());
    }
  };
};

export default function (Component) {
  class UserData extends React.PureComponent {
    componentDidMount() {
      const {
        loadUserInfo,
        checkUserAutorized,
        isUserAuthenticated,
        user
      } = this.props;
      checkUserAutorized();

      if (isUserAuthenticated && (!user || (user && !user.Id))) {
        loadUserInfo();
      }
    }

    componentDidUpdate(prevProps) {
      const { loadUserInfo, isUserAuthenticated } = this.props;

      if (isUserAuthenticated && (!prevProps.user || (prevProps.user && !prevProps.user.Id))) {
        loadUserInfo();
      }
    }

    render() {
      return (
        <Component {...this.props} />
      );
    }
  }

  return connect(mapStateToProps, mapDispatchToProps)(UserData);
}
