import React from 'react';
import bem from '../../lib/bem';
import './style.css';

const block = 'form-control-element';

export default class FormControl extends React.PureComponent {
  getFormControlMods = () => {
    return {
      i: this.props.iconClass
    };
  };

  render() {
    const {
      children,
      label,
      name,
      icon
    } = this.props;

    return (
      <React.Fragment>
        <div className={bem({ block })}>
          <label className={bem({ block, elem: 'label-element' })} htmlFor={name}>
            {label ? <span className={bem({ block, elem: 'label-title' })}>{label}:</span> : ''}
          </label>
          <div className={bem({ block, elem: 'icon', mods: this.getFormControlMods() })}>
            {icon ? <img src={icon} alt="icon" /> : ''}
          </div>
          {children}
        </div>
      </React.Fragment>
    );
  }
}
