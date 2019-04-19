import React from 'react';
import { CheckBox } from 'devextreme-react';
import './style.css';

export class CheckBoxComponent extends React.PureComponent {
  static defaultProps = {
    disabled: false,
    text: ''
  };

  render() {
    return (
      <CheckBox {...this.props} />
    );
  }
}
