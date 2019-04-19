import React from 'react';
import { Alert } from 'react-bootstrap';
import { WARNING_NOTIFICATION } from '../../containers/Notification/constants';
import bem from '../../lib/bem';
import './style.css';

const block = 'notification';

export class Notification extends React.PureComponent {
  static defaultProps = {
    type: WARNING_NOTIFICATION,
    show: false,
    text: ''
  };

  handleDismiss = () => {
    this.props.onDismiss();
  };

  render() {
    const { show, type, text } = this.props;

    if (!show) {
      return <div />;
    }

    return (
      <div className={bem({ block })}>
        <Alert bsStyle={type} onDismiss={this.handleDismiss}>
          {text}
        </Alert>
      </div>
    );
  }
}