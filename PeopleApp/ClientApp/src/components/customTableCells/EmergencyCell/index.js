import React from 'react';
import bem from '../../../lib/bem';
import emergencyIcon from '../../../images/emergency.svg';
import './style.css';

const block = 'emergency-cell';

export const EmergencyCell = (cellData) => {
  return (
    <div className={bem({ block })}>
      {cellData.data.isDanger ?
        <img src={emergencyIcon} alt="" /> :
        ''
      }
    </div>
  );
};
