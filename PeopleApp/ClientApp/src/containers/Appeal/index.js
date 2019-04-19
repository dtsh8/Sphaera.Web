import { connect } from 'react-redux';
import { parse } from 'qs';
import Container from './Container';
import UserDataHOC from '../HOC/UserData';
import DictionariesHOC from '../HOC/dictionaries';
import { getAppeal, downloadFile, downloadAllFiles } from './actions';
import { convertFromImmutableToJS } from '../../helpers/common';
import { storeName } from './reducer';

const mapStateToProps = (state, props) => {
  return {
    appeal: convertFromImmutableToJS(state.getIn([storeName, 'appeal'])),
    incidentId: props.location.state.incidentId,
    cardId: props.location.state.cardId,
  };
};

const mapDispatchToProps = (dispatch, props) => {
  const { location } = props;
  const params = parse(location.search, { ignoreQueryPrefix: true });
  const { incidentId, cardId } = location.state;

  const incident = incidentId || params.incidentId;
  const card = cardId || params.cardId;

  return {
    getAppeal: () => {
      dispatch(getAppeal(incident, card));
    },
    downloadFile: (fileName) => {
      dispatch(downloadFile(incident, card, fileName));
    },
    downloadAllFiles: (fileNames) => {
      dispatch(downloadAllFiles(incident, card, fileNames));
    },
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(DictionariesHOC(UserDataHOC(Container)));
