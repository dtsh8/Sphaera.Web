import React from 'react';
import { Col } from 'react-bootstrap';
import Form from 'devextreme-react/ui/form';
import ButtonComponent from '../controls/Button';
import { CheckBoxComponent } from '../controls/CheckBox';
import bem from '../../lib/bem';
import './style.css';

const block = 'auth-form';

const formDataValues = {
  email: '',
  password: ''
};

export class AuthForm extends React.PureComponent {
  handleSubmit = (e) => {
    this.props.onSubmitAuthForm(formDataValues, () => e.preventDefault());
  };

  render() {
    const formItems = [{
      itemType: 'group',
      cssClass: 'first-group',
      items: [{
        itemType: 'group',
        items: [{
          dataField: 'email',
          editorOptions: {
            placeholder: 'Имя пользователя'
          },
          cssClass: 'username',
          label: {
            visible: false
          }
        }, {
          dataField: 'password',
          editorOptions: {
            placeholder: 'Ваш пароль',
            mode: 'password'
          },
          cssClass: 'password',
          label: {
            visible: false
          }
        }]
      }]
    }];

    return (
      <div className={bem({ block, elem: 'wrapper' })}>
        <div className={bem({ block, elem: 'block' })}>
          <div className="container">
            <Col lg={4} md={5} sm={7} xs={12}>
              <div className={bem({ block })}>
                <div className={bem({ block, elem: 'title-wrapper' })}>
                  <span className={bem({ block, elem: 'title' })}>
                  вход
                  </span>

                  <span className={bem({ block, elem: 'subtitle' })}>
                  для портала «Безопасный город»
                  </span>
                </div>

                <form onSubmit={this.handleSubmit}>
                  <Form items={formItems} validationGroup={block} formData={formDataValues} />

                  <div className={bem({ block, elem: 'remember' })}>
                    <CheckBoxComponent text="Запомнить меня" />
                  </div>

                  <div className={bem({ block, elem: 'button-wrapper' })}>
                    <ButtonComponent
                      text="Войти"
                      type="default"
                      className={bem({ block, elem: 'button' })}
                      useSubmitBehavior
                      validationGroup={block}
                    />
                  </div>
                </form>

                <div className={bem({ block, elem: 'forgot-password' })}>
                  <a href="https://app.zeplin.io/project/5b7fd8f278369947a8c74820/dashboard">
                    Забыли пароль?
                  </a>
                </div>
              </div>
            </Col>
          </div>
        </div>
        <div className={bem({ block, elem: 'registration' })}>
          <a href="https://app.zeplin.io/project/5b7fd8f278369947a8c74820/dashboard">
            Зарегистрируйтесь&nbsp;
          </a>
          <span>
            для полного доступа к сервисам
          </span>
        </div>
      </div>
    );
  }
}