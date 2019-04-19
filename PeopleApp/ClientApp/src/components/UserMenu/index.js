import React from 'react';
import { ListGroup, ListGroupItem } from 'react-bootstrap';
import bem from '../../lib/bem';

import './style.css';

const block = 'user-menu';

export class UserMenu extends React.PureComponent {
  render() {
    const { userLogout, user, redirectToProfile } = this.props;

    return (
      <div className={bem({ block })}>
        <ListGroup>
          <ListGroupItem className="visible-sm visible-xs">
            <span className="icon-user" />
            {user.FirstName}
            &nbsp;{user.MiddleName}
            &nbsp;{user.LastName}
          </ListGroupItem>
          <ListGroupItem
            onClick={redirectToProfile}
            className={bem({ block, elem: 'menu-item' })}
          >
            Профиль
          </ListGroupItem>
          <ListGroupItem
            onClick={userLogout}
            className={bem({ block, elem: 'menu-item' })}
          >
            Выход
          </ListGroupItem>
        </ListGroup>
      </div>
    );
  }
}
