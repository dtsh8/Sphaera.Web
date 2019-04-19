import React from 'react';
import { Grid } from 'react-bootstrap';
import Breadcrumbs from '../../../containers/Breadcrumbs';
import bem from '../../../lib/bem';
import './style.css';

const block = 'request-page-header';

//  копипаст от RemindersHeader
export class RequestPageHeader extends React.PureComponent {
  render() {
    return (
      <div>
        <Grid>
          <div className={bem({ block })}>
            <div className={bem({ block, elem: 'title' })}>
              <h1 className={bem({ block, elem: 'title-content' })}>Создание обращения</h1>
              <Breadcrumbs />
            </div>
          </div>
        </Grid>
        <div className={bem({ block, elem: 'divider' })} />
      </div>
    );
  }
}
