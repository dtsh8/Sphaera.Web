import { connect } from 'react-redux';
import Container from './Container';
import UserDataHOC from '../HOC/UserData';
import {
  onSubmitForm,
  getAddress as getAddressAction,
  setAddress as setAddressAction,
  getAutocompleteAddress as getAutocompleteAddressAction
} from './actions';
import { storeName } from './reducer';
import DictionariesHOC from '../HOC/dictionaries';
import { convertFromImmutableToJS } from '../../helpers/common';

const mapStateToProps = (state) => {
  return convertFromImmutableToJS(state.get(storeName));
};

const mapDispatchToProps = (dispatch) => {
  return {
    onSubmitForm: (data) => {
      dispatch(onSubmitForm(data));
    },
    getAddress: (location) => {
      dispatch(getAddressAction(location));
    },
    setAddress: (address) => {
      dispatch(setAddressAction(address));
    },
    getAutocompleteAddress: (cityName, location) => {
      dispatch(getAutocompleteAddressAction(cityName, location));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(DictionariesHOC(UserDataHOC(Container)));
