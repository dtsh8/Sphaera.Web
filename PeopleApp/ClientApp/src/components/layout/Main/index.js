import React from 'react';
import Routes from '../../../containers/Routes';
import Notification from '../../../containers/Notification';
import './style.css';

export default class Main extends React.PureComponent {
  render() {
    return (
      <main className="main">
        <Routes />
        <Notification />
      </main>
    );
  }
}
