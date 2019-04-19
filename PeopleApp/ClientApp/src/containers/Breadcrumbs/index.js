import { connect } from 'react-redux';
import Container from './Container';

const mapStateToProps = (state) => {
  return {
    breadcrumbs: state.getIn(['router', 'breadcrumbs'])
  };
};

export default connect(mapStateToProps)(Container);
