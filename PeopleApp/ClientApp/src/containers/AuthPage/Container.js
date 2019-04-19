import React from 'react';
import { AuthForm } from '../../components/AuthForm';
import Header from '../../components/layout/Header';

export default class AuthPage extends React.PureComponent {
  render() {
    const { onSubmitAuthForm } = this.props;

    return (
      <div>
        <Header menuIsEnabled={false} />
        <AuthForm onSubmitAuthForm={onSubmitAuthForm} />
      </div>
    );
  }
}
