import React from 'react';
import { connect } from 'react-redux';
import { fetchPeriods } from './actions';
import { storeName } from './reducer';

const mapStateToProps = (state) => {
  return {
    periods: state.getIn([storeName, 'periods'])
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadPeriods: () => {
      dispatch(fetchPeriods());
    }
  };
};

export default function (Component) {
  class PeriodsDictionary extends React.PureComponent {
    componentDidMount() {
      this.props.loadPeriods();
    }

    render() {
      return (
        <Component {...this.props} />
      );
    }
  }

  return connect(mapStateToProps, mapDispatchToProps)(PeriodsDictionary);
}
