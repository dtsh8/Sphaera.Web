import { connect } from 'react-redux';
import Container from './Container';
import { getFooterData as loadFooterData } from './actions';
import { storeName } from './reducer';
import { convertFromImmutableToJS } from '../../helpers/common';

const mapStateToProps = (state) => {
  return convertFromImmutableToJS(state.get(storeName));
};

const mapDispatchToProps = (dispatch) => {
  return {
    getFooterData: () => {
      dispatch(loadFooterData());
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Container);