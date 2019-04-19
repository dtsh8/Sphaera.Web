import React from 'react';
import { connect } from 'react-redux';
import { fetchTypes } from './actions';
import { storeName } from './reducer';

const mapStateToProps = (state) => {
  return {
    types: state.getIn([storeName, 'types'])
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadTypes: () => {
      dispatch(fetchTypes());
    }
  };
};

export default function (Component) {
  class TypesDictionary extends React.PureComponent {
    componentDidMount() {
      this.props.loadTypes();
    }

    render() {
      return (
        <Component {...this.props} />
      );
    }
  }

  return connect(mapStateToProps, mapDispatchToProps)(TypesDictionary);
}
