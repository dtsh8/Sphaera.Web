import { connect } from 'react-redux';
import Container from './Container';
import DictionariesHOC from '../HOC/dictionaries';
import { convertFromImmutableToJS } from '../../helpers/common';
import { storeName } from '../Incidents/reducer';

const mapStateToProps = (state) => {
  return convertFromImmutableToJS(state.get(storeName));
};

export default connect(mapStateToProps, null)(DictionariesHOC(Container));
