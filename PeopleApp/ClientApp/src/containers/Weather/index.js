import { connect } from 'react-redux';
import Container from './Container';
import { fetchWeather } from './actions';
import { storeName } from './reducer';

const mapStateToProps = (state) => {
  return {
    loading: state.getIn([storeName, 'loading']),
    forecast: state.getIn([storeName, 'forecast']),
    city: state.getIn([storeName, 'city']),
    cityName: state.getIn([storeName, 'cityName'])
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    getWeather: (userLocation) => {
      dispatch(fetchWeather(userLocation));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Container);
