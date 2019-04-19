import { connect } from 'react-redux';
import { stringify } from 'qs';
import Container from './Container';
import DictionariesHOC from '../HOC/dictionaries';
import UserDataHOC from '../HOC/UserData';
import { changeFilter } from '../Incidents/actions';
import { convertFromImmutableToJS } from '../../helpers/common';
import { storeName } from '../Incidents/reducer';
import { changeCurrentUrlParam } from '../Router/actions';
import { URL_PARAM_MAP_POSITION } from '../../constants/paramsUrl';

const mapStateToProps = (state) => {
  return {
    ...convertFromImmutableToJS(state.get(storeName)),
    historyLocation: convertFromImmutableToJS(state.getIn(['router', 'location']))
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    changeIncidentsFilter: (filterData) => {
      dispatch(changeFilter(filterData));
    },
    saveToUrlMapPosition: (position) => {
      if (position && position.zoom && position.center && position.center.length) {
        const params = stringify({ lat: position.center[0], lon: position.center[1], zoom: position.zoom }, { addQueryPrefix: true });
        dispatch(changeCurrentUrlParam(URL_PARAM_MAP_POSITION, params));
      }
    },
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(UserDataHOC(DictionariesHOC(Container)));
