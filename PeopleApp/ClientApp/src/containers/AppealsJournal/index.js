import { connect } from 'react-redux';
import Container from './Container';
import DictionariesHOC from '../HOC/dictionaries';
import { loadAppeals, redirectToCard as redirectToCardAction } from './actions';
import { convertFromImmutableToJS } from '../../helpers/common';

const mapStateToProps = (state) => {
  return convertFromImmutableToJS(state.get('appeals'));
};

const mapDispatchToProps = (dispatch) => {
  return {
    getAppeals: () => {
      dispatch(loadAppeals());
    },
    redirectToCard: (incidentId, cardId) => {
      dispatch(redirectToCardAction(incidentId, cardId));
    },
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(DictionariesHOC(Container));
