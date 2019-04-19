import React from 'react';
import Weather from '../../components/Weather';

export default class WeatherContainer extends React.PureComponent {
  componentDidMount() {
    const { userLocation, getWeather } = this.props;

    getWeather(userLocation);
  }

  componentWillUpdate(nextProps) {
    const { userLocation, getWeather } = this.props;
    if (userLocation !== nextProps.userLocation) {
      getWeather(nextProps.userLocation);
    }
  }

  render() {
    const {
      forecast,
      loading,
      city,
      cityName
    } = this.props;

    return (
      <Weather
        city={city}
        cityName={cityName}
        forecast={forecast}
        loading={loading}
      />
    );
  }
}
