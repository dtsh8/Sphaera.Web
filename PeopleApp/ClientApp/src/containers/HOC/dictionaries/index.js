import React from 'react';
import { connect } from 'react-redux';
import { fetchPeriods } from './Periods/actions';
import { fetchStatuses } from './Statuses/actions';
import { fetchTypes } from './Types/actions';
import { storeName as periodsStoreName } from './Periods/reducer';
import { storeName as statusesStoreName } from './Statuses/reducer';
import { storeName as typesStoreName } from './Types/reducer';
import { convertFromImmutableToJS } from '../../../helpers/common';

const mapStateToProps = (state) => {
  return {
    periods: state.getIn([periodsStoreName, 'periods']),
    statuses: convertFromImmutableToJS(state.getIn([statusesStoreName, 'statuses'])),
    types: state.getIn([typesStoreName, 'types']),
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadDictionaries: () => {
      dispatch(fetchStatuses());
      dispatch(fetchPeriods());
      dispatch(fetchTypes());
    }
  };
};

export default function (Component) {
  class Dictionaries extends React.PureComponent {
    componentDidMount() {
      this.props.loadDictionaries();
    }

    shouldComponentRender = () => {
      const { periods, statuses, types } = this.props;

      if (periods && statuses && types) {
        return !periods.loading && periods.count
          && !statuses.loading && statuses.count
          && !types.loading && types.count;
      }

      return false;
    };

    render() {
      if (!this.shouldComponentRender()) {
        return <div />;
      }

      return (
        <Component {...this.props} />
      );
    }
  }

  return connect(mapStateToProps, mapDispatchToProps)(Dictionaries);
}
