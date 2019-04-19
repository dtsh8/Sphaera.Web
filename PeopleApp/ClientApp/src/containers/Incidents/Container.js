import React from 'react';
import { Incidents } from '../../components/Incidents';

export default class IncidentsContainer extends React.PureComponent {
  componentDidMount() {
    this.props.getIncidents();
  }

  render() {
    const {
      user,
      onIncidentChange,
      incidentType,
      isUserAuthenticated,
    } = this.props;

    return (
      <Incidents
        userLocation={user && user.MapExtent}
        onIncidentChange={onIncidentChange}
        incidentType={incidentType}
        isUserAuthenticated={isUserAuthenticated}
      />
    );
  }
}
