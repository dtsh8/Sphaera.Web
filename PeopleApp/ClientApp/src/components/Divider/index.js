import React from 'react';
import bem from '../../lib/bem';
import './style.css';

const block = 'divider';

export class Divider extends React.PureComponent {
  static defaultProps = {
    dark: false,
    infoDivider: false
  };

  getDividerMods = () => {
    const { dark, infoDivider } = this.props;

    return {
      'dark-divider': dark,
      'info-divider': infoDivider
    };
  };

  render() {
    return (
      <div className={bem({ block, mods: this.getDividerMods() })} />
    );
  }
}
