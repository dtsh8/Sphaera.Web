import React from 'react';
import L from 'leaflet';
import { parse } from 'qs';
import get from 'lodash/get';
import LeafletMap from '../../components/leafletMap';

const DefaultIcon = L.divIcon({
  html: '<i class="leaflet-custom-marker"/>'
});

L.Marker.prototype.options.icon = DefaultIcon;

export const defaultLocation = [47.2, 39.75, 15];

export default class LeafletMapContainer extends React.PureComponent {
  static defaultProps = {
    withoutIncidents: false,
    getIncidents: () => {},
    changeIncidentsFilter: () => {}
  };

  state = {
    lat: defaultLocation[0],
    lng: defaultLocation[1],
    zoom: defaultLocation[2],
  };

  componentDidMount() {
    this.setLatLng();
  }

  componentDidUpdate() {
    this.setLatLng();
  }

  setLatLng = () => {
    const { userLocation, isUserAuthenticated, historyLocation } = this.props;
    let params = {};

    if (historyLocation && historyLocation.search) {
      params = parse(historyLocation.search, { ignoreQueryPrefix: true });
    }

    if (params && params.mapPosition) {
      const locationParams = parse(params.mapPosition, { ignoreQueryPrefix: true });

      this.setState({
        lat: parseFloat(locationParams.lat),
        lng: parseFloat(locationParams.lon),
        zoom: parseFloat(locationParams.zoom),
      });
    } else if (isUserAuthenticated && userLocation) {
      const location = Array.isArray(userLocation) ? userLocation : JSON.parse(userLocation);

      this.setState({
        lat: location[0],
        lng: location[1],
        zoom: location[2]
      });
    } else {
      this.setState({
        lat: defaultLocation[0],
        lng: defaultLocation[1],
        zoom: defaultLocation[3]
      });
    }
  };

  render() {
    const {
      lat,
      lng,
      zoom,
    } = this.state;

    const {
      withoutIncidents,
      types,
      incidents,
      changeIncidentsFilter,
      filters,
      saveToUrlMapPosition,
    } = this.props;

    return (
      <LeafletMap
        lat={lat}
        lng={lng}
        zoom={zoom}
        markers={get(incidents, 'data', [])}
        withoutIncidents={withoutIncidents}
        types={types}
        filters={filters}
        onChangeIncidentsFilter={changeIncidentsFilter}
        saveToUrlMapPosition={saveToUrlMapPosition}
      />
    );
  }
}
