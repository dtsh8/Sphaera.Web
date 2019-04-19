import React from 'react';
import { BreadcrumbComponent } from '../../components/layout/Breadcrumbs';

export default class BreadCrumbs extends React.PureComponent {
  render() {
    const { breadcrumbs, title } = this.props;

    return (
      <BreadcrumbComponent breadcrumbs={breadcrumbs} title={title} />
    );
  }
}