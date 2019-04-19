import React from 'react';
import bem from '../../../lib/bem';
import './style.css';

const block = 'status-cell';

const doneStatus = 'Реагирование завершено';
const newStatus = 'Новое';
const workStatus = 'В работе';
const rejectedStatus = 'Отклонено';

export const StatusCellWithEmergency = (cellData) => {
  const getStatusMods = () => {
    return cellData.value
      ? {
        done: cellData.value === doneStatus,
        new: cellData.value === newStatus,
        'at-work': cellData.value === workStatus,
        rejected: cellData.value === rejectedStatus
      }
      : {};
  };

  return (
    <div className={bem({ block })}>
      {cellData.data.isDanger ?
        <div className={bem({ block, elem: 'emergency' })} /> :
        ''
      }
      <div className={bem({ block, elem: 'name', mods: getStatusMods() })}>
        {cellData.value || cellData.text}
      </div>
    </div>
  );
};
