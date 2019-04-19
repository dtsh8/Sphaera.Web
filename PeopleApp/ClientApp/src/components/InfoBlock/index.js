import React from 'react';
import bem from '../../lib/bem';
import './style.css';

const block = 'info-block';

export class InfoBlock extends React.PureComponent {
  static defaultProps = {
    marginBetween: false
  };

  getLabelMods = () => {
    return {
      'margin-between': this.props.marginBetween
    };
  };

  render() {
    const { label, info } = this.props;

    return (
      <div className={bem({ block })}>
        <span className={bem({ block, elem: 'info-label', mods: this.getLabelMods() })}>
          {label}
        </span>

        <span className={bem({ block, elem: 'info' })}>
          {info}
        </span>
      </div>
    );
  }
}