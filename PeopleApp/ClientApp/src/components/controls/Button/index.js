import React from 'react';
import { Button } from 'devextreme-react';

export default class ButtonComponent extends React.PureComponent {
  static defaultProps = {
    type: 'normal',
    text: '',
    disabled: false
  };

  render() {
    return (
      <Button {...this.props} />
    );
  }
}
