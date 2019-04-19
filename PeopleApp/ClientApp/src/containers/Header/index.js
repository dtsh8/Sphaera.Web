import { connect } from 'react-redux';
import Container from './Container';
import UserDataHOC from '../HOC/UserData';
import { storeName } from './reducer';
import { loginUser, userLogout, redirectToEditProfile } from '../AuthPage/actions';
import { convertFromImmutableToJS } from '../../helpers/common';
import { loadHeaderEmergencyData } from '../Header/actions';

const mapStateToProps = (state) => {
  return convertFromImmutableToJS(state.get(storeName));
};

const mapDispatchToProps = (dispatch) => {
  return {
    userLogout: () => {
      dispatch(userLogout());
    },
    userLogin: () => {
      dispatch(loginUser());
    },
    loadHeaderEmergencyData: () => {
      dispatch(loadHeaderEmergencyData());
    },
    redirectToProfile: () => {
      dispatch(redirectToEditProfile());
    },
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(UserDataHOC(Container));
