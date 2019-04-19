import React from 'react';
import { Breadcrumb } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { breadcrumbsTranslate } from '../../../constants/breadcrumbs';
import './style.css';

export class BreadcrumbComponent extends React.PureComponent {
  getBreadcrumbsItems = () => {
    const { breadcrumbs, title } = this.props;

    return breadcrumbs.map((crumb, index) => {
      const breadcrumbObject = {
        href: `/${crumb}`,
        active: index === breadcrumbs.length - 1,
        name: breadcrumbsTranslate[crumb]
      };

      return (
        <li className={breadcrumbObject.active ? 'active' : ''} key={crumb}>
          {breadcrumbObject.active ?
            <span>{title || breadcrumbObject.name}</span> :
            <Link
              to={breadcrumbObject.href}
            >
              {breadcrumbObject.name}
            </Link>
          }
        </li>
      );
    });
  };

  render() {
    return (
      <Breadcrumb>
        {this.getBreadcrumbsItems()}
      </Breadcrumb>
    );
  }
}