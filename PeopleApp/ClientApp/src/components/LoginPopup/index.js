import React from 'react';
import Form from 'devextreme-react/ui/form';

import bem from '../../lib/bem';
import FormControl from '../FormControl';
import { CheckBoxComponent as CheckBox } from '../controls/CheckBox';
import Button from '../controls/Button';
import './style.css';

const block = 'login-popup';

const formDataValues = {
  email: '',
  password: ''
};

export class LoginPopup extends React.PureComponent {
  getCityIncidentsMods = () => {
    return {
      'max-content': !!this.props.cityIncidentMaxContent
    };
  };

  handleSubmit = (e) => {
    this.props.onSubmitAuthForm(formDataValues);

    formDataValues.email = '';
    formDataValues.password = '';

    e.preventDefault();
  };

  render() {
    const formItems = [{
      itemType: 'group',
      cssClass: 'first-group',
      items: [{
        itemType: 'group',
        items: [{
          dataField: 'email',
          editorType: 'dxTextBox',
          editorOptions: {
            placeholder: 'Введите логин'
          },
          validationRules: [{
            type: 'required',
            message: 'Введите почту или номер телефона'
          }],
          cssClass: 'username',
          label: {
            visible: false
          }
        }, {
          dataField: 'password',
          editorType: 'dxTextBox',
          editorOptions: {
            placeholder: 'Введите пароль',
            mode: 'password'
          },
          validationRules: [{
            type: 'required',
            message: 'Введите ваш пароль'
          }],
          cssClass: 'password',
          label: {
            visible: false
          }
        }]
      }]
    }];

    return (
      <div className={bem({ block, elem: 'wrapper', mods: this.getCityIncidentsMods() })}>
        <div className={bem({ block })}>
          <form onSubmit={this.handleSubmit}>
            <Form
              items={formItems}
              validationGroup={block}
              formData={formDataValues}
            />

            <FormControl>
              <CheckBox
                text="Запомнить меня"
                height={12}
              />
            </FormControl>

            <Button
              text="Войти"
              height={40}
              className={bem({ block, elem: 'button' })}
              useSubmitBehavior
              validationGroup={block}
            />
          </form>

          <div className={bem({ block, elem: 'labels' })}>
            <span>Забыли пароль?</span>
            <span>Регистрация</span>
          </div>
        </div>
      </div>
    );
  }
}
