import React from 'react';
import { Grid } from 'react-bootstrap';
import LeafletMap from '../../containers/LeafletMap';
import IncidentsLogJournal from '../../containers/IncidentsLogJournal';
import AppealsJournalContainer from '../../containers/AppealsJournal';
import Weather from '../../containers/Weather';
import bem from '../../lib/bem';
import './style.css';

const block = 'incidents';

const incidentTypes = [
  { id: 1, type: 'map', name: 'Карта происшествий' },
  { id: 2, type: 'incidents', name: 'Журнал происшествий' },
  { id: 3, type: 'appeals', name: 'Журнал обращений' },
];

export class Incidents extends React.PureComponent {
  state = {
    type: this.props.incidentType || 'map',
  };

  componentDidUpdate() {
    if (!this.props.isUserAuthenticated && this.state.type === 'appeals') {
      this.changeType('map');
    }
  }

  getIncidentItemMods = (type) => {
    return {
      'active-type': this.state.type === type
    };
  };

  getContent = () => {
    const { userLocation } = this.props;

    switch (this.state.type) {
      case 'map':
        return <LeafletMap userLocation={userLocation} />;
      case 'incidents':
        return <IncidentsLogJournal />;
      case 'appeals':
        return <AppealsJournalContainer />;
      default:
        return '';
    }
  };

  changeType = (type) => {
    const { onIncidentChange } = this.props;

    if (this.state.type !== type) {
      this.setState({
        type
      });
    }

    onIncidentChange(type);

    return type;
  };

  renderIncidentsTypeToggle = () => {
    return incidentTypes.map(({ id, type, name }) => {
      if (!this.props.isUserAuthenticated && type === 'appeals') {
        return <div key={id} />;
      }

      return (
        <span
          className={bem({ block, elem: 'incident-item', mods: this.getIncidentItemMods(type) })}
          key={id}
          onClick={() => this.changeType(type)}
        >
          {name}
        </span>
      );
    });
  };

  render() {
    const { userLocation } = this.props;

    return (
      <div className={bem({ block })}>
        <Grid fluid>
          <div className={bem({ block, elem: 'block' })}>
            <div className={bem({ block, elem: 'toggle-incidents-type' })}>
              {this.renderIncidentsTypeToggle()}
              <div className={bem({ block, elem: 'incident-type-divider' })} />
            </div>

            <div className={bem({ block, elem: 'weather-block' })}>
              <Weather userLocation={userLocation} />
            </div>

          </div>
        </Grid>

        <div className={bem({ block, elem: 'content' })}>
          {this.getContent()}
        </div>

      </div>
    );
  }
}
