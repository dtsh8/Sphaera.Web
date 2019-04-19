import React from 'react';
import { Link } from 'react-router-dom';
import { ROUTE_CREATE_REQUEST } from '../../constants/routes';
import bem from '../../lib/bem';
import { CustomDataGrid } from '../../components/CustomDataGrid';
import { EmptyBlock } from '../../components/EmptyBlock';
import ButtonComponent from '../controls/Button';
import './style.css';

const block = 'appeals-journal';

export class AppealsJournal extends React.PureComponent {
  onClickRow = (e) => {
    const { redirectToCard } = this.props;
    redirectToCard(e.data.incidentId, e.data.cardId);
  };

  render() {
    const {
      columnsData,
      onContentReady,
      columns,
      totalDataNumber,
    } = this.props;

    return (
      <div className={bem({ block })}>
        {!columnsData ?
          <EmptyBlock
            text="Нет созданных обращений"
          >
            <div className={bem({ block, elem: 'empty-button-wrapper' })}>
              <Link to={ROUTE_CREATE_REQUEST}>
                <ButtonComponent
                  text="Создать обращение"
                  type="default"
                  className={bem({ block, elem: 'button' })}
                />
              </Link>
            </div>
          </EmptyBlock> :
          <div className={bem({ block, elem: 'data-grid-wrapper' })}>
            <div className={bem({ block, elem: 'appeal-button-wrapper' })}>
              <Link to={ROUTE_CREATE_REQUEST}>
                <ButtonComponent
                  text="Создать обращение"
                  type="default"
                  className={bem({ block, elem: 'button' })}
                />
              </Link>
            </div>
            <CustomDataGrid
              columnsData={columnsData}
              onContentReady={onContentReady}
              columns={columns}
              totalDataNumber={totalDataNumber}
              onClickRow={this.onClickRow}
            />
          </div>
        }
      </div>
    );
  }
}
