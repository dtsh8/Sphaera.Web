/* eslint-disable react/no-did-mount-set-state */
import React from 'react';
import { connect } from 'react-redux';
import routerChecker from '../../../helpers/routerChecker';
import UserDataHOC from '../UserData';

export default function (Component, routerCheckers) {
  class RouterChecker extends React.PureComponent {
    state = {
      allow: false
    };

    componentDidMount() {
      if (routerChecker(this.props, routerCheckers)) {
        this.setState({
          allow: true
        });
        window.scrollTo(0, 0);
      }
    }

    render() {
      if (!this.state.allow) {
        return <div />;
      }

      return (
        <Component {...this.props} />
      );
    }
  }

  return connect()(UserDataHOC(RouterChecker));
}
