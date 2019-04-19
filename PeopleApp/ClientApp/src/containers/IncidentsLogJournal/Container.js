import React from 'react';
import { IncidentsJournal } from '../../components/IncidentsJournal';
import context from '../../api/api';
import { EmergencyCell } from '../../components/customTableCells/EmergencyCell';
import { StatusCellWithEmergency } from '../../components/customTableCells/StatusCellWithEmergency';

const columns = [{
  dataField: 'stateName',
  caption: 'Статус',
  dataType: 'string',
  width: '15%',
  cellComponent: StatusCellWithEmergency,
  allowSorting: false,
  cssClass: undefined,
  allowHeaderFiltering: true,
  allowFiltering: true,
}, {
  dataField: 'isDanger',
  caption: 'ЧС',
  dataType: 'string',
  width: '5%',
  cellComponent: EmergencyCell,
  allowSorting: false,
  cssClass: undefined,
  allowHeaderFiltering: false,
  allowFiltering: false,
}, {
  dataField: 'serviceTypeName',
  caption: 'Тип происшествия',
  dataType: 'string',
  width: '20%',
  cellComponent: null,
  allowSorting: false,
  cssClass: 'bold',
  allowHeaderFiltering: false,
  allowFiltering: true,
}, {
  dataField: 'created',
  caption: 'Дата и время регистрации',
  dataType: 'datetime',
  width: '20%',
  cellComponent: null,
  allowSorting: false,
  cssClass: undefined,
  allowHeaderFiltering: true,
  allowFiltering: true,
}, {
  dataField: 'shortAddress',
  caption: 'Адрес происшествия',
  dataType: 'string',
  width: '40%',
  cellComponent: null,
  allowSorting: false,
  cssClass: 'bold',
  allowHeaderFiltering: true,
  allowFiltering: true,
}];
const defaultTotalDataNumber = 0;

export default class IncidentsLogJournal extends React.PureComponent {
  constructor(props) {
    super(props);

    this.state = {
      collapsed: false
    };
  }

  onContentReady = (e) => {
    if (!this.state.collapsed) {
      e.component.expandRow(['EnviroCare']);
      this.setState({
        collapsed: true
      });
    }
  };

  render() {
    const { incidents } = this.props;

    return (
      <IncidentsJournal
        onContentReady={this.onContentReady}
        columnsData={context.card.dataSource || []}
        columns={columns}
        totalDataNumber={(incidents && incidents.count) || defaultTotalDataNumber}
      />
    );
  }
}
