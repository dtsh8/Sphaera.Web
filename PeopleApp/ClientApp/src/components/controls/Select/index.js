import React from 'react';
import { SelectBox } from 'devextreme-react';
import './style.css';

export default class SelectComponent extends React.PureComponent {
  render() {
    return (
      <SelectBox {...this.props} />
    );
  }
}
