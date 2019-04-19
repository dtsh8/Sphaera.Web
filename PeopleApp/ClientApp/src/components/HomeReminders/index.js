import React from 'react';
import { Grid, Col, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import ButtonComponent from '../controls/Button';
import bem from '../../lib/bem';
import { ROUTE_REMINDERS } from '../../constants/routes';
import './style.css';
import { TREE_ITEM_TYPE_FOLDER } from '../../helpers/reminders';

const block = 'reminders';

export class HomeReminders extends React.PureComponent {
  getFirstReminder = () => {
    const { reminders } = this.props;

    if (reminders && reminders.length > 0) {
      return reminders[0].list[0].Id;
    }

    return [];
  };

  render() {
    const { reminders } = this.props;

    const topLevelReminders = Object.values(reminders)
      .filter(treeItem => treeItem.level === 1 && treeItem.reminder.DocumentTreeItemType === TREE_ITEM_TYPE_FOLDER);

    return (
      <Grid>
        <div className={bem({ block })}>
          <span className={bem({ block, elem: 'title' })}>
          Памятки и инструкции
          </span>
          {topLevelReminders && !!topLevelReminders.length && (
            <React.Fragment>
              <Row>
                {topLevelReminders.map((treeItem) => {
                  return (
                    <Col xs={12} md={4} key={treeItem.reminder.Id}>
                      <div className={bem({ block, elem: 'block' })}>
                        <span className={bem({ block, elem: 'block-title' })}>
                          {treeItem.reminder.Name}
                        </span>
                        {treeItem.descendants.map((item) => {
                          return (
                            <Link
                              to={`${ROUTE_REMINDERS}?id=${item.reminder.Id}`}
                              className={bem({ block, elem: 'block-item' })}
                              key={item.reminder.Id}
                            >
                              {item.reminder.Name}
                            </Link>
                          );
                        })}
                      </div>
                    </Col>
                  );
                })}
              </Row>

              <Row>
                <div className={bem({ block, elem: 'button-wrapper' })}>
                  <Link
                    to={`${ROUTE_REMINDERS}?id=${this.getFirstReminder()}`}
                  >
                    <ButtonComponent
                      text="Все памятки и инструкции"
                      type="default"
                      className={bem({ block, elem: 'button' })}
                    />
                  </Link>
                </div>
              </Row>
            </React.Fragment>
          )}
        </div>
      </Grid>
    );
  }
}
