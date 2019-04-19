import React from 'react';
import Header from '../../components/layout/Header';

export default class HeaderContainer extends React.PureComponent {
  componentDidMount() {
    this.props.loadHeaderEmergencyData();
  }

  render() {
    const {
      user,
      userLogout,
      userLogin,
      emergencyData,
      isUserAuthenticated,
      redirectToProfile,
    } = this.props;

    return (
      <Header
        user={user}
        userLogout={userLogout}
        userLogin={userLogin}
        emergencyData={emergencyData}
        isUserAuthenticated={isUserAuthenticated}
        redirectToProfile={redirectToProfile}
      />
    );
  }
}
