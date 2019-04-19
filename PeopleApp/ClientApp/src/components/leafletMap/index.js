import React from 'react';
import { Grid, Row } from 'react-bootstrap';
import { LayersControl, Map, WMSTileLayer, ZoomControl } from 'react-leaflet';
import MarkerClusterGroup from 'react-leaflet-markercluster';
import filter from 'lodash/filter';
import moment from 'moment';
import 'react-leaflet-markercluster/dist/styles.min.css';
import * as L from 'leaflet';
import { renderToStaticMarkup } from 'react-dom/server';
import CityIncidents from '../../components/CityIncidents';
import { MapMarker } from './MapMarker';
import context from '../../api/api';
import './style.css';
import bem from '../../lib/bem';

const block = 'incidents-leaflet';

// генерация иконки в зависимости от вхождения в кластер маркера с чс
const customIconCreateFunction = (cluster) => {
  let markerClassName = '';
  let clusterClassName = '';

  cluster.getAllChildMarkers().some((marker) => {
    // если содержит хоть 1 чс - весь кластер становится чс
    if (marker.options.icon.options.html.includes('emergency')) {
      clusterClassName = 'marker-cluster-has-emergency';
      markerClassName = 'leaflet-map__leaflet-custom-marker leaflet-map__leaflet-custom-marker_emergency';
      return true;
    }
    return false;
  });

  const childCount = cluster.getChildCount();

  return new L.DivIcon({
    html: `<div><span class='${markerClassName}'>${childCount}</span></div>`,
    className: `marker-cluster marker-cluster-medium ${clusterClassName}`,
    iconSize: new L.Point(40, 40)
  });
};

// сбор и открытие всех попапов маркеров в одном
const customOnClusterClick = (cluster) => {
  const popupHtml = cluster.getAllChildMarkers().map((value) => {
    return renderToStaticMarkup(value.getPopup().options.children);
  });

  cluster.bindPopup(popupHtml.join('')).openPopup();
};

export default class LeafletMap extends React.PureComponent {
  constructor(props) {
    super(props);
    this.state = {
      gisServerUri: '',
      gisServerLayers: ''
    };
    context.settings.getGisServerSettings()
    .then((settings) => 
      this.setState({gisServerUri: settings.Uri, gisServerLayers: settings.Layers})
    );
  }
  getMarkersByFilters = () => {
    const { markers, filters } = this.props;

    if (markers
      && markers.length > 0
      && (!filters.type.id || filters.type.id > -1)) {
      return filter(markers, (marker) => {
        let res = true;

        if (filters.type && filters.type.caseTypeId) {
          res = res && (marker.serviceTypeId === filters.type.caseTypeId);
        }

        return res;
      });
    }

    return markers;
  };

  setRefToMap = (node) => {
    this.mapRef = node;
  };

  handleMoveEnd = () => {
    const { saveToUrlMapPosition } = this.props;

    saveToUrlMapPosition(this.mapRef.viewport);
  };

  render() {
    const {
      lat,
      lng,
      zoom,
      withoutIncidents,
      types,
      filters,
      onChangeIncidentsFilter,
    } = this.props;

    const rootTypes = {
      ...types,
      results: types.results.filter(type => type.parentCode === null)
    };

    const center = [lat, lng];

    const markersByFilters = this.getMarkersByFilters();

    return (
      <div className={bem({ block })}>
        {withoutIncidents
          ? <div />
          : (
            <Grid fluid>
              <Row>
                <CityIncidents
                  types={rootTypes}
                  filters={filters}
                  onChangeIncidentsFilter={onChangeIncidentsFilter}
                />
              </Row>
            </Grid>
          )}
        <Map
          center={center}
          zoom={zoom}
          zoomControl={false}
          scrollWheelZoom={false}
          maxZoom={15}
          ref={this.setRefToMap}
          onMoveEnd={this.handleMoveEnd}
        >
          <ZoomControl
            zoomInTitle="Увеличить"
            position="topright"
            zoomOutTitle="Уменьшить"
          />
          <LayersControl position="topright">
            <LayersControl.BaseLayer checked name="Streets">
              <WMSTileLayer
                attribution="&amp;copy <a href=&quot;http://osm.org/copyright&quot;>OpenStreetMap</a> contributors"
                url={this.state.gisServerUri}
                layers={this.state.gisServerLayers}
              />
            </LayersControl.BaseLayer>
          </LayersControl>
          <MarkerClusterGroup
            maxClusterRadius={30}
            iconCreateFunction={customIconCreateFunction}
            onClusterClick={customOnClusterClick}
            spiderfyOnMaxZoom={false}
            zoomToBoundsOnClick={false}
            removeOutsideVisibleBounds
          >
            {markersByFilters.map((marker, index) => {
              return (
                <MapMarker key={`${marker.cardId + index}`} marker={marker} center={center} />
              );
            })}
          </MarkerClusterGroup>
        </Map>
        <div className={bem({ block, elem: 'legend-status' })}>
          <span className={bem({ block, elem: 'status', mods: { new: true } })}>Новое</span>
          <span className={bem({ block, elem: 'status', mods: { work: true } })}>В работе</span>
          <span className={bem({ block, elem: 'status', mods: { done: true } })}>Реагирование завершено</span>
          <span className={bem({ block, elem: 'status', mods: { declined: true } })}>Отклонено</span>
        </div>
      </div>
    );
  }
}
