import React from 'react';
import Header from '../../containers/Header';
import Footer from '../../containers/Footer';
import { RemindersHeader } from '../../components/reminders/RemindersHeader';
import { RemindersContent } from '../../components/reminders/RemindersContent';
import { getRemindersTree } from '../../helpers/reminders';
import { ROUTE_REMINDERS } from '../../constants/routes';

export default class RemindersPage extends React.PureComponent {
  constructor(props) {
    super(props);

    const {
      history: {
        location: {
          pathname, search
        }
      }
    } = props;

    const searchParams = new URLSearchParams(search);

    this.state = {
      resultParams: {
        id: searchParams.get('id')
      },
      path: pathname + search
    };
  }

  componentDidMount() {
    window.scrollTo(0, 0);

    this.props.getReminderList();
    this.props.getRemindersContent(this.state.resultParams.id);
  }

  componentDidUpdate() {
    const {
      history: {
        location: {
          pathname, search
        }
      }
    } = this.props;

    const path = pathname + search;
    if (path !== this.state.path) {
      window.scrollTo(0, 0);

      const searchParams = new URLSearchParams(this.props.location.search);
      this.setResultParams(searchParams, path);
      this.props.getRemindersContent(searchParams.get('id'));
    }
  }

  setResultParams = (searchParams, path) => {
    this.setState({
      resultParams: {
        id: searchParams.get('id')
      },
      path
    });
  };

  shouldComponentRender = () => {
    return !!this.props.remindersData && this.props.reminderList.length;
  };

  changeReminderPage = (id) => {
    const { history } = this.props;

    if (id) {
      history.push({
        pathname: `${ROUTE_REMINDERS}`,
        search: `id=${id}`
      });
    }
  };

  render() {
    if (!this.shouldComponentRender()) {
      return <div />;
    }

    const {
      reminderList,
      reminderTree,
      remindersData,
      expandedNodes,
      setReminderNodeExpanded,
      setReminderNodeCollapsed
    } = this.props;

    return (
      <div>
        <Header />
        <RemindersHeader />
        <RemindersContent
          data={remindersData}
          list={reminderTree}
          searchParams={this.state.resultParams}
          changeReminderPage={this.changeReminderPage}
          expandedNodes={expandedNodes}
          setReminderNodeExpanded={setReminderNodeExpanded}
          setReminderNodeCollapsed={setReminderNodeCollapsed}
        />
        <Footer />
      </div>
    );
  }
}
