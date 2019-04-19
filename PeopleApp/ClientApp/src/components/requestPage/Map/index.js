import React from 'react';
import { Map as LeafletMap, Marker, WMSTileLayer, ZoomControl } from 'react-leaflet';
import { AutocompleteComponent } from '../../controls/AutoComplete';
import context from '../../../api/api';
import bem from '../../../lib/bem';
import './style.css';

const block = 'coordsMap';

export class Map extends React.PureComponent {
  static defaultProps = {
    onMapChange: () => {}
  };

  static getDerivedStateFromProps(nextProps, prevState) {
    let stateObj = {};
    if (
      nextProps.center &&
      nextProps.center.length > 0 &&
      nextProps.center[0] !== prevState.map.center[0]
    ) {
      stateObj = {
        ...stateObj,
        map: {
          center: nextProps.center,
          zoom: nextProps.zoom
        },
      };
    }
    return stateObj;
  }

  constructor(props) {
    super(props);

    this.state = {
      map: {
        center: props.center,
        zoom: props.zoom
      },
      marker: [],
      searchField: '',
      gisServerUri: '',
      gisServerLayers: ''
    };
    context.settings.getGisServerSettings()
    .then((settings) => 
      this.setState({gisServerUri: settings.Uri, gisServerLayers: settings.Layers})
    );
  }

  componentDidUpdate(prevProps) {
    if (
      (prevProps.address && this.props.address && prevProps.address.fullAddress !== this.props.address.fullAddress) ||
      (!prevProps.address && this.props.address)
    ) {
      this.changeAddress(this.props.address.fullAddress);
    }

    if (
      (
        !!prevProps.coord.length &&
        !!this.props.coord.length &&
        prevProps.coord[0] !== this.props.coord[0] &&
        prevProps.coord[1] !== this.props.coord[1]
      ) ||
      (
        !prevProps.coord.length > 0 &&
        this.props.coord.length
      )
    ) {
      this.changeMapPosition(this.props.coord);
    }
  }

  onClickMap = (e) => {
    this.setState({
      marker: [e.latlng.lat, e.latlng.lng]
    });

    this.props.onMapChange([e.latlng.lat, e.latlng.lng]);
    this.props.getAddress([e.latlng.lat, e.latlng.lng]);
  };

  changeAddress = (searchField) => {
    this.setState({ searchField });
  };

  changeMapPosition = (coord) => {
    this.setState({
      marker: [coord[0], coord[1]],
      map: {
        center: [coord[0], coord[1]],
        zoom: 17
      }
    });

    this.props.onMapChange([coord[0], coord[1]]);
  };

  handleValueChanged = (e) => {
    this.props.getAutocompleteAddress(e.value);
  };

  handleOptionSelected = (e) => {
    this.setState({ searchField: e.itemData });
    this.props.setAddress(e.itemData);
  };

  render() {
    const { map, marker, searchField } = this.state;

    const { autocompleteCities } = this.props;

    return (
      <div className={bem({ block })}>
        <AutocompleteComponent
          className={bem({ block, elem: 'address' })}
          onInput={e => this.setState({ searchField: e.event.target.value })}
          value={searchField}
          onValueChanged={this.handleValueChanged}
          onItemClick={this.handleOptionSelected}
          dataSource={autocompleteCities && autocompleteCities}
        />
        <LeafletMap
          center={map.center}
          zoom={map.zoom}
          onClick={this.onClickMap}
          zoomControl={false}
          scrollWheelZoom={false}
        >
          <WMSTileLayer
            url={this.state.gisServerUri}
            layers={this.state.gisServerLayers}
          />
          <ZoomControl
            zoomInTitle="Увеличить"
            position="topright"
            zoomOutTitle="Уменьшить"
          />
          {marker.length > 0 && <Marker position={marker} />}
        </LeafletMap>
      </div>
    );
  }
}
