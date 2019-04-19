import React from 'react';
import { Grid, Row, Col } from 'react-bootstrap';
import Breadcrumbs from '../../containers/Breadcrumbs';
import LeafletMapContainer from '../../containers/LeafletMap/Container';
import { InfoBlock } from '../InfoBlock';
import { Divider } from '../Divider';
import ButtonComponent from '../controls/Button';
import bem from '../../lib/bem';
import data from './mock';
import './style.css';

const block = 'profile';

export class ProfileComponent extends React.PureComponent {
  render() {
    return (
      <div className={bem({ block })}>
        <Grid>
          <div className={bem({ block, elem: 'header' })}>
            <div className={bem({ block, elem: 'title' })}>
              <h1 className={bem({ block, elem: 'title-content' })}>
                Профиль
              </h1>
              <Breadcrumbs />
            </div>
          </div>
        </Grid>

        <div className={bem({ block, elem: 'divider' })} />
        <Grid>
          <div className={bem({ block, elem: 'info-wrapper' })}>
            <div className={bem({ block, elem: 'info-header' })}>
              <span className={bem({ block, elem: 'name' })}>
                {data.fio}
              </span>
              <ButtonComponent text="Редактировать" className={bem({ block, elem: 'button' })} />
            </div>

            <Row>
              <Col xs={9}>
                <div className={bem({ block, elem: 'info-block' })}>
                  <InfoBlock label="Логин в системе" info={data.login} />

                  <InfoBlock label="Номер телефона" info={data.phone} />

                  <InfoBlock label="Электронная почта" info={data.email.address} />
                </div>

                <Divider dark lessMargin infoDivider />

                <div className={bem({ block, elem: 'info-block' })}>
                  <InfoBlock label="Организация" info={data.organization} />

                  <InfoBlock label="Роль" info={data.role} />
                </div>

                <InfoBlock label="Организации в подчинении" info={data.subordinateOrganization} />

                <Divider dark lessMargin infoDivider />
              </Col>
            </Row>

            <InfoBlock
              label="Область карты по умолчанию"
              info={<LeafletMapContainer withoutIncidents />}
              marginBetween
            />

          </div>
        </Grid>
      </div>
    );
  }
}