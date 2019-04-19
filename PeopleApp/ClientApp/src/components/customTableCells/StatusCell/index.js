import React from 'react';
import bem from '../../../lib/bem';
import './style.css';

const block = 'status-cell';

const doneStatus = 'Реагирование завершено';
const newStatus = 'Новое';
const workStatus = 'В работе';
const rejectedStatus = 'Отклонено';

export const StatusCell = (cellData) => {
  const getStatusMods = () => {
    return cellData.data.status ? {
      done: cellData.value === doneStatus,
      new: cellData.value === newStatus,
      'at-work': cellData.value === workStatus,
      rejected: cellData.value === rejectedStatus
    } : {};
  };

  return (
    <div className={bem({ block })}>
      <div className={bem({ block, elem: 'name', mods: getStatusMods() })}>
        {cellData.data.status ? cellData.data.status.name : ''}
      </div>
    </div>
  );
};
