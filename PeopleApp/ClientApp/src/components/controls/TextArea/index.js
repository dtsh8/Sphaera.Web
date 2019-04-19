import React from 'react';
import { TextArea } from 'devextreme-react';
import './style.css';

export class TextAreaComponent extends React.PureComponent {
  static defaultProps = {
    value: '',
    disabled: false,
    placeholder: '',
    readOnly: false,
    height: 100
  };

  render() {
    return (
      <TextArea {...this.props} />
    );
  }
}
