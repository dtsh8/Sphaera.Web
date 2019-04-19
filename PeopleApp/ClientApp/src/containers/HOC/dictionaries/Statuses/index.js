import React from 'react';
import { connect } from 'react-redux';
import { fetchStatuses } from './actions';
import { storeName } from './reducer';
import { convertFromImmutableToJS } from '../../../../helpers/common';

const mapStateToProps = (state) => {
  return {
    statuses: convertFromImmutableToJS(state.getIn([storeName, 'statuses']))
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadStatuses: () => {
      dispatch(fetchStatuses());
    }
  };
};

export default function (Component) {
  class StatusesDictionary extends React.PureComponent {
    componentDidMount() {
      this.props.loadStatuses();
    }

    render() {
      return (
        <Component {...this.props} />
      );
    }
  }

  return connect(mapStateToProps, mapDispatchToProps)(StatusesDictionary);
}
