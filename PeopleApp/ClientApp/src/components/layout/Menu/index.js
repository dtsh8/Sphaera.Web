import React from 'react';
import { Navbar, NavItem, Nav } from 'react-bootstrap';
import bem from '../../../lib/bem';

import './style.css';

const block = 'app-menu';

export default class Menu extends React.PureComponent {
  render() {
    return (
      <Navbar className={bem({ block })} fluid>
        <Nav className={bem({ block, elem: 'nav' })}>
          <NavItem eventKey={1} href="#" className={bem({ block, elem: 'menu-item' })}>
            Происшествия
          </NavItem>
          <NavItem eventKey={2} href="#" className={bem({ block, elem: 'menu-item' })}>
            Инструкции и памятки
          </NavItem>
          <NavItem eventKey={3} href="#" className={bem({ block, elem: 'menu-item' })}>
            Новости
          </NavItem>
          <NavItem eventKey={4} href="#" className={bem({ block, elem: 'menu-item' })}>
            Статистика
          </NavItem>
        </Nav>
      </Navbar>
    );
  }
}
