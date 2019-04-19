import React from 'react';
import { Lookup } from 'devextreme-react';

export default class LookupComponent extends React.PureComponent {
  static defaultProps = {
    value: null,
    placeholder: '',
    dataSource: []
  };

  render() {
    return (
      <Lookup {...this.props} />
    );
  }
}
