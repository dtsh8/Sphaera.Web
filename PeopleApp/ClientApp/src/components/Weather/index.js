import React from 'react';
import '../../lib/owfont/css/owfont-regular.min.css';
import bem from '../../lib/bem';
import './style.css';

const block = 'weather-widget';

export default class Weather extends React.PureComponent {
  render() {
    const {
      cityName,
      forecast,
      loading
    } = this.props;

    if (forecast.size === 0 || loading) {
      return <div>Loading...</div>;
    }

    const tempSymbol = (forecast.temp >= 1) ? '+' : '';

    return (
      <div className={bem({ block })}>
        <div className={bem({ block, elem: 'temperature' })}>
          <span>
            {`${tempSymbol}${Math.round(forecast.temp)}`}&deg; C
          </span>
        </div>
        <div className={bem({ block, elem: 'icon' })}>
          <div className={`owf owf-${forecast.weather.id}`} />
        </div>
        <div className={bem({ block, elem: 'info' })}>
          <div className={bem({ block, elem: 'city' })}>
            {cityName}
          </div>
          <div className={bem({ block, elem: 'description' })}>
            <span>
              {forecast.weather.description}
            </span>
          </div>
          <div className={bem({ block, elem: 'other' })}>
            <span>
              {Math.round(forecast.wind)} м/с,&nbsp;
              сз {Math.round(forecast.pressure)} мм рт. ст.,&nbsp;
              {Math.round(forecast.humidity)}% влажн.
            </span>
          </div>
        </div>
      </div>
    );
  }
}
