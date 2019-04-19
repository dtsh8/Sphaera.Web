import React from 'react';
import { Marker, Popup } from 'react-leaflet';
import { divIcon } from 'leaflet';
import moment from 'moment';
import bem from '../../../lib/bem';
import { STATUS_DONE, STATUS_NEW, STATUS_WORK } from '../../../constants/statuses';

const block = 'leaflet-map';

export class MapMarker extends React.PureComponent {
  getMarkerMods = () => {
    const {
      marker: {
        isEmergency,
      }
    } = this.props;
    return { emergency: isEmergency, status: this.getStatusClass() };
  };

  getStatusClass = () => {
    const {
      marker: {
        stateId,
      }
    } = this.props;

    switch (stateId) {
      case STATUS_DONE:
        return 'done';
      case STATUS_NEW:
        return 'new';
      case STATUS_WORK:
        return 'work';
      default:
        return '';
    }
  };

  getMarkerIcon = () => {
    return divIcon({
      html: `<i class="${bem({ block, elem: 'leaflet-custom-marker', mods: this.getMarkerMods() })}" />`
    });
  };

  render() {
    const {
      marker: {
        latitude,
        longitude,
        stateName,
        serviceTypeName,
        shortAddress,
        created,
        cardIndexName,
      },
      center,
    } = this.props;

    return (
      <Marker position={[latitude || center[0], longitude || center[1]]} icon={this.getMarkerIcon()}>
        <Popup>
          <div className={bem({ block })}>
            <div>
              <span className={bem({ block, elem: 'title' })}>{ serviceTypeName }</span>
              <span className={bem({ block, elem: `status ${this.getStatusClass()}` })}>{ stateName }</span>
            </div>
            <div className={bem({ block, elem: 'reason' })}>
              <span>{ cardIndexName }</span>
            </div>
            <div className={bem({ block, elem: 'date' })}>
              <i className="fa fa-clock-o" />
              <span>Дата и время регистрации:</span> { moment(created).format('DD.MM.YYYY hh:mm') }
            </div>
            <div className={bem({ block, elem: 'address' })}>
              <i className="fa fa-map-marker" />
              <span>Адрес:</span> { shortAddress }
            </div>
          </div>
        </Popup>
      </Marker>
    );
  }
}
