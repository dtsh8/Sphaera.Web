import React from 'react';
import bem from '../../lib/bem';
import { CustomDataGrid } from '../../components/CustomDataGrid';
import { EmptyBlock } from '../../components/EmptyBlock';
import './style.css';

const block = 'incidents-journal';

export class IncidentsJournal extends React.PureComponent {
  render() {
    const {
      columnsData,
      onContentReady,
      columns,
      totalDataNumber,
    } = this.props;

    return (
      <div className={bem({ block })}>
        {columnsData && columnsData.length === 0 ?
          <EmptyBlock
            text="Происшествий нет"
          /> :
          <div>
            <CustomDataGrid
              columnsData={columnsData}
              onContentReady={onContentReady}
              columns={columns}
              totalDataNumber={totalDataNumber}
            />
          </div>
        }
      </div>
    );
  }
}
