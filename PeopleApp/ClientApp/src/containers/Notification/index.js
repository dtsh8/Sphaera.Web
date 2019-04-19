import { connect } from 'react-redux';
import Container from './Container';
import { convertFromImmutableToJS } from '../../helpers/common';
import { storeName } from './reducer';
import { hideNotification } from './actions';

const mapStateToProps = (state) => {
  return convertFromImmutableToJS(state.get(storeName));
};

const mapDispatchToProps = (dispatch) => {
  return {
    onDismiss: () => {
      dispatch(hideNotification());
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Container);
