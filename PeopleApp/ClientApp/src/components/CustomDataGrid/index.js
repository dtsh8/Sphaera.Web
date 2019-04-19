import React from 'react';
import DataGrid, { Pager, Paging, Column, FilterPanel } from 'devextreme-react/ui/data-grid';
import bem from '../../lib/bem';
import { getFilterParams } from '../../helpers/common';
import './style.css';

const block = 'custom-data-grid';

const pageSizes = [10, 25, 50, 100];
const defaultPageSize = pageSizes[0];

export class CustomDataGrid extends React.PureComponent {
  state = {
    filterEnabled: true
  };

  onOptionChanged = ({ name, value }) => {
    if (name === 'filterPanel') {
      this.setState({
        filterEnabled: value
      });
    }
  };

  pagingInfoMods = () => {
    const { totalDataNumber } = this.props;

    return {
      'several-pages': totalDataNumber > defaultPageSize
    };
  };

  customizeFilterPanelText = ({ filterValue }) => {
    return `Выбраны параметры фильтрации: ${getFilterParams(filterValue, this.props.columns)}`;
  };

  rowClick = (e) => {
    const { onClickRow } = this.props;
    if (onClickRow) {
      onClickRow(e);
    }
  };

  renderFilterPanel = () => {
    const { filterEnabled } = this.state;
    const {
      withoutFilterPanel
    } = this.props;

    if (withoutFilterPanel) {
      return <div />;
    }

    return (
      <FilterPanel
        visible
        filterEnabled={filterEnabled}
        customizeText={this.customizeFilterPanelText}
        texts={{
          clearFilter: 'Очистить',
          filterEnabledHint: 'Применить фильтр',
          createFilter: '',
        }}
      />
    );
  };

  render() {
    const {
      columnsData,
      onContentReady,
      columns,
      totalDataNumber,
    } = this.props;

    const wordWrapEnabled = true;
    const filterRowOptions = {
      visible: true,
    };
    const headerFilter = {
      visible: true,
      texts: {
        cancel: 'Отмена',
        ok: 'Применить',
      },
    };

    return (
      <div className={bem({ block })}>
        <div className={bem({ block, elem: 'wrapper' })}>
          <DataGrid
            dataSource={columnsData}
            ordering
            showBorders
            onContentReady={onContentReady}
            wordWrapEnabled={wordWrapEnabled}
            filterRow={filterRowOptions}
            headerFilter={headerFilter}
            noDataText="По вашему запросу ничего не найдено"
            onOptionChanged={this.onOptionChanged}
            onRowClick={this.rowClick}
          >
            {this.renderFilterPanel()}
            {columns.map(column => (
              <Column
                key={column.dataField}
                dataField={column.dataField}
                caption={column.caption}
                dataType={column.dataType}
                width={column.width}
                cellComponent={column.cellComponent}
                allowSorting={column.allowSorting}
                cssClass={column.cssClass}
                allowHeaderFiltering={column.allowHeaderFiltering}
                allowFiltering={column.allowFiltering}
              />
            ))}
            <Pager
              allowedPageSizes={pageSizes}
              showNavigationButtons
            />
            <Paging defaultPageSize={defaultPageSize} />
          </DataGrid>
          <div className={bem({ block, elem: 'paging-info', mods: this.pagingInfoMods() })}>
            Показано
            <span className={bem({ block, elem: 'bold-info' })}>
              &nbsp;{totalDataNumber < defaultPageSize ? totalDataNumber : defaultPageSize}&nbsp;
            </span>
            записей из &nbsp;
            <span className={bem({ block, elem: 'bold-info' })}>
              {totalDataNumber}
            </span>
          </div>
        </div>
      </div>
    );
  }
}
