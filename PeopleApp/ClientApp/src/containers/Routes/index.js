import { connect } from 'react-redux';
import Container from './Container';
import { convertFromImmutableToJS } from '../../helpers/common';
import { storeName } from '../Router/reducer';

const mapStateToProps = (state) => {
  return convertFromImmutableToJS(state.get(storeName));
};

export default connect(mapStateToProps)(Container);
