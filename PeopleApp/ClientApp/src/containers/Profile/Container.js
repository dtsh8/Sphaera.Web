import React from 'react';
import Header from '../../components/layout/Header';
import Footer from '../../containers/Footer';
import { ProfileComponent } from '../../components/Profile';

export default class Profile extends React.PureComponent {
  render() {
    const { user } = this.props;

    return (
      <div>
        <Header user={user} />
        <ProfileComponent />
        <Footer />
      </div>
    );
  }
}
