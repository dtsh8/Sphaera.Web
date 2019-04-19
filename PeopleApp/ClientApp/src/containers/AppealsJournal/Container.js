import React from 'react';
import { union } from 'lodash';
import { AppealsJournal } from '../../components/AppealsJournal';
import { StatusCell } from '../../components/customTableCells/StatusCell';
import { initResultsWithDictionaries } from '../../helpers/dictionaries';

const columns = [{
  dataField: 'stateName',
  caption: 'Статус',
  dataType: 'string',
  width: '15%',
  cellComponent: StatusCell,
  allowSorting: false,
  cssClass: undefined,
  allowHeaderFiltering: true,
  allowFiltering: true,
}, {
  dataField: 'type.name',
  caption: 'Тип происшествия',
  dataType: 'string',
  width: undefined,
  cellComponent: null,
  allowSorting: false,
  cssClass: 'bold',
  allowHeaderFiltering: false,
  allowFiltering: true,
}, {
  dataField: 'created',
  caption: 'Дата и время регистрации',
  dataType: 'datetime',
  width: '15%',
  cellComponent: null,
  allowSorting: false,
  cssClass: undefined,
  allowHeaderFiltering: true,
  allowFiltering: true,
}, {
  dataField: 'shortAddress',
  caption: 'Адрес',
  dataType: 'string',
  width: undefined,
  cellComponent: null,
  allowSorting: false,
  cssClass: undefined,
  allowHeaderFiltering: false,
  allowFiltering: true,
}, {
  dataField: 'comment',
  caption: 'Краткое описание',
  dataType: 'string',
  width: '30%',
  cellComponent: null,
  allowSorting: false,
  cssClass: undefined,
  allowHeaderFiltering: false,
  allowFiltering: false,
}];

const defaultTotalDataNumber = 0;

export default class AppealsJournalContainer extends React.PureComponent {
  constructor(props) {
    super(props);

    this.state = {
      collapsed: false
    };
  }

  componentDidMount() {
    this.props.getAppeals();
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
    const {
      statuses,
      types,
      redirectToCard,
      appealsData,
    } = this.props;
    const appeals = initResultsWithDictionaries({ statuses, types, dataMap: { data: union(appealsData.data) } });

    return (
      <AppealsJournal
        onContentReady={this.onContentReady}
        columnsData={(appeals && appeals.data) || []}
        columns={columns}
        totalDataNumber={(appeals && appeals.data.length) || defaultTotalDataNumber}
        redirectToCard={redirectToCard}
      />
    );
  }
}
