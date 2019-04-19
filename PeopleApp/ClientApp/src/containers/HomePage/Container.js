import React from 'react';
import { parse } from 'qs';
import Header from '../../containers/Header';
import Footer from '../../containers/Footer';
import Incidents from '../../containers/Incidents';
import { HomeReminders } from '../../components/HomeReminders';
import Statistics from '../../components/Statistics';
import { getRemindersTree } from '../../helpers/reminders';

export default class HomePage extends React.PureComponent {
  componentDidMount() {
    const {
      getReminderList,
      getStatistic
    } = this.props;

    getReminderList();
    getStatistic();
  }

  render() {
    const {
      reminders: {
        reminderList
      },
      statistic,
      location
    } = this.props;

    const params = parse(location.search, { ignoreQueryPrefix: true });
    const { type } = params;

    return (
      <div>
        <Header />
        <Incidents incidentType={type} />
        <HomeReminders reminders={getRemindersTree(reminderList)} />
        <Statistics statistic={statistic} />
        <Footer />
      </div>
    );
  }
}
