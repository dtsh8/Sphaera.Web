import React from 'react';
import { Grid, Col, Row } from 'react-bootstrap';
import bem from '../../lib/bem';

import stat1 from '../../images/stat-1.svg';
import stat2 from '../../images/stat-2.svg';
import './style.css';

const block = 'statistics';

// const INCIDENTS_COUNT = 1;
const INCIDENTS_OPEN_COUNT = 2;
const INCIDENTS_FINISHED_COUNT = 4;
// const INCIDENTS_CREATED_COUNT = 8;
// const DANGER_INCIDENTS_CREATED_COUNT = 16;

export default class Statistics extends React.PureComponent {
  getStatistic = (type) => {
    const { statistic } = this.props;
    if (statistic && !!statistic.length) {
      const statisticType = statistic.find(stat => stat.type === type);
      if (statisticType) {
        return statisticType.value;
      }
    }
    return 0;
  };

  render() {
    return (
      <div className={bem({ block })}>
        <Grid>
          <Row>
            <Col xs={12} lg={6} className={bem({ block, elem: 'block' })}>
              <span className={bem({ block, elem: 'description' })}>
                Статистика работы
                <br />
                <b>АПК Безопасный Город</b>
                &nbsp;за сутки
              </span>
            </Col>
            <Col xs={6} sm={4} lg={3} className={bem({ block, elem: 'block' })}>
              <div className={bem({ block, elem: 'number', mods: { 'stat-1': true } })}>
                <img src={stat1} alt="Stat 1" />
                <span>
                  {this.getStatistic(INCIDENTS_OPEN_COUNT)} {this.getStatistic(INCIDENTS_OPEN_COUNT) > 1000 && 'тыс.'}
                </span>
              </div>
              <span className={bem({ block, elem: 'label' })}>
                заявок в работе
              </span>
            </Col>
            <Col xs={6} sm={4} lg={3} className={bem({ block, elem: 'block' })}>
              <div className={bem({ block, elem: 'number', mods: { 'stat-2': true } })}>
                <img src={stat2} alt="Stat 2" />
                <span>
                  {this.getStatistic(INCIDENTS_FINISHED_COUNT)} {this.getStatistic(INCIDENTS_FINISHED_COUNT) > 1000 && 'тыс.'}
                </span>
              </div>
              <span className={bem({ block, elem: 'label' })}>
                заявок выполнено
              </span>
            </Col>
          </Row>
        </Grid>
      </div>
    );
  }
}
