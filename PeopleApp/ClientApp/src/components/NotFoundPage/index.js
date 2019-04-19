import React from 'react';
import bem from '../../lib/bem';
import './style.css';

const block = 'not-found-page';

export class NotFoundPage extends React.PureComponent {
  render() {
    return (
      <div className={bem({ block })}>
        Content #404
      </div>
    );
  }
}