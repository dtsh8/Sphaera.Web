import React from 'react';
import bem from '../../lib/bem';
import './style.css';

const block = 'empty';

export class EmptyBlock extends React.PureComponent {
  render() {
    const { text, children } = this.props;

    return (
      <div className="container-fluid">
        <div className={bem({ block })}>
          <span className={bem({ block, elem: 'empty-text' })}>{text}</span>
          {children}
        </div>
      </div>
    );
  }
}