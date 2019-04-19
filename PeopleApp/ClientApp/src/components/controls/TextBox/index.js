import React from 'react';
import { TextBox } from 'devextreme-react';
import './style.css';

export class TextBoxComponent extends React.PureComponent {
  static defaultProps = {
    value: '',
    disabled: false,
    placeholder: '',
    readOnly: false
  };

  render() {
    return (
      <TextBox {...this.props} />
    );
  }
}
