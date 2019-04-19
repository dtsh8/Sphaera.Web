import { connect } from 'react-redux';
import Container from './Container';
import { loadIncidents } from './actions';
import { URL_PARAM_TYPE } from '../../constants/paramsUrl';
import { changeCurrentUrlParam } from '../Router/actions';
import UserDataHOC from '../HOC/UserData';
import { storeName } from '../Router/reducer';

const mapStateToProps = (state, props) => {
  return {
    incidentType: state.getIn([storeName, 'currentType']) || props.incidentType,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    getIncidents: () => {
      dispatch(loadIncidents());
    },
    onIncidentChange: (incidentName) => {
      dispatch(changeCurrentUrlParam(URL_PARAM_TYPE, incidentName));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(UserDataHOC(Container));
