import React from 'react';
import { Autocomplete } from 'devextreme-react';

export class AutocompleteComponent extends React.PureComponent {
  static defaultProps = {
    value: null,
    placeholder: '',
    dataSource: []
  };

  render() {
    return (
      <Autocomplete {...this.props} />
    );
  }
}
