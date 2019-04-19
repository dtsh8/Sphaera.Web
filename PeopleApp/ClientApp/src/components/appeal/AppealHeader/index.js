import React from 'react';
import { Grid } from 'react-bootstrap';
import Breadcrumbs from '../../../containers/Breadcrumbs';
import bem from '../../../lib/bem';
import './style.css';

const block = 'reminders-header';

export class AppealHeader extends React.PureComponent {
  getFullTitle = () => {
    const { cardIndexCode } = this.props;
    const types = this.props.types.results;
    return types.find(type => type.code === cardIndexCode).name;
  };

  render() {
    const { title, cardIndexCode } = this.props;
    const fullTitle = cardIndexCode ? this.getFullTitle() : title;

    return (
      <div>
        <Grid>
          <div className={bem({ block })}>
            <div className={bem({ block, elem: 'title' })}>
              <h1 className={bem({ block, elem: 'title-content' })}>{fullTitle}</h1>
              <Breadcrumbs title={fullTitle} />
            </div>
          </div>
        </Grid>
        <div className={bem({ block, elem: 'divider' })} />
      </div>
    );
  }
}
