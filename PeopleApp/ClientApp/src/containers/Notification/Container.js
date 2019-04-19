import React from 'react';
import { Notification } from '../../components/Notification';

export default class NotificationContainer extends React.PureComponent {
  render() {
    return (
      <Notification {...this.props} />
    );
  }
}