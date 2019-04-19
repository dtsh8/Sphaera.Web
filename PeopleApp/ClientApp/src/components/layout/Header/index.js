import React from 'react';
import { Grid, Row, Col, Glyphicon } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { UserMenu } from '../../UserMenu';
import { ROUTE_HOME } from '../../../constants/routes';
import bem from '../../../lib/bem';
import logo from '../../../images/logotype.svg';
import loginImg from '../../../images/log-in.svg';
import cs from '../../../images/cs.svg';
import { ModalComponent } from '../../controls/Modal';
import { DropMenu } from '../DropMenu';
import './style.css';

const block = 'app-header';

export default class Header extends DropMenu {
  getLoginPopupMods = () => {
    return {
      active: this.state.show
    };
  };

  getDropdownOverlayMods = () => {
    return {
      active: this.state.show && this.props.isUserAuthenticated
    };
  };

  setDropdownButtonRef = (node) => {
    this.dropdownButton = node;
  };

  setDropdownRef = (node) => {
    this.dropdown = node;
  };

  render() {
    const {
      user,
      userLogout,
      menuIsEnabled,
      emergencyData,
      userLogin,
      isUserAuthenticated,
      redirectToProfile,
    } = this.props;
    const { show } = this.state;

    // TODO: ???
    const isMenuEnabled = (menuIsEnabled !== undefined) ? menuIsEnabled : true;

    return (
      <React.Fragment>
        <Grid fluid className={bem({ block })}>
          {isMenuEnabled ? (
            <Row>
              <Col
                xs={emergencyData.isEnabled ? 6 : 9}
                sm={emergencyData.isEnabled ? 6 : 11}
                md={emergencyData.isEnabled ? 4 : 8}
                xsHidden
                className={bem({ block, elem: 'logo' })}
              >
                <Link to={ROUTE_HOME} className={bem({ block, elem: 'logo-link' })}>
                  <img src={logo} alt="Logotype" className={bem({ block, elem: 'logo-img' })} />
                </Link>
              </Col>
              {emergencyData.isEnabled
                ? (
                  <Col xs={7} sm={5} md={4} className={bem({ block, elem: 'center_block' })}>
                    <Row>
                      <ModalComponent
                        bodyContent={emergencyData.content}
                        titleContent={emergencyData.title}
                        className="emergency"
                      >
                        <div className={bem({ block, elem: 'center' })}>
                          <img src={cs} alt="cs" className={bem({ block, elem: 'cs_img' })} />
                          <span>{emergencyData.title}</span>
                        </div>
                      </ModalComponent>
                    </Row>
                  </Col>
                ) : ''}
              {isUserAuthenticated && user && user.Id
                ? (
                  <Col xs={1} sm={1} md={4} className={bem({ block, elem: 'lk-enter' })}>
                    <div
                      ref={this.setDropdownButtonRef}
                      className={bem({ block, elem: 'lk-enter-button', mods: this.getLoginPopupMods() })}
                      onClick={this.showDropMenu}
                    >
                      <span className={`${show ? 'icon-close-button' : 'icon-user'} log-in-icon visible-sm visible-xs`} />
                      <span className="icon-user log-in-icon hidden-sm hidden-xs" />
                      <span className={bem({ block, elem: 'username' })}>{user.FirstName} {user.MiddleName} {user.LastName}</span>
                      <Glyphicon glyph="triangle-bottom" className={bem({ block, elem: 'triangle-menu-down' })} />
                    </div>
                    <div className={bem({ block, elem: 'lk-login-form', mods: this.getLoginPopupMods() })}>
                      <div ref={this.setDropdownRef} className={bem({ block, elem: 'lk-login-form-col' })}>
                        <UserMenu userLogout={userLogout} user={user} redirectToProfile={redirectToProfile} />
                      </div>
                    </div>
                  </Col>
                ) : (
                  <Col xs={2} sm={1} md={4} className={bem({ block, elem: 'lk-enter authorization' })}>
                    <div
                      className={bem({ block, elem: 'lk-enter-button', mods: this.getLoginPopupMods() })}
                      onClick={() => userLogin()}
                    >
                      <img src={loginImg} alt="Log in" className={bem({ block, elem: 'log-in' })} />
                      <Row>
                        <Col xsHidden smHidden md={12}>
                          Вход в личный кабинет
                        </Col>
                      </Row>
                    </div>
                  </Col>
                )}
            </Row>
          ) : (
            <Row>
              <Col xs={12} className={bem({ block, elem: 'logo-full' })}>
                <Link to={ROUTE_HOME}>
                  <img src={logo} alt="Logotype" />
                </Link>
              </Col>
            </Row>
          )}
        </Grid>
        <div className={bem({ block, elem: 'dropdown-overlay', mods: this.getDropdownOverlayMods() })} />
      </React.Fragment>
    );
  }
}
