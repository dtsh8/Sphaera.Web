import { connect } from 'react-redux';
import Container from './Container';
import { 
  getRemindersContent,
  getReminderList,
  setReminderNodeExpanded,
  setReminderNodeCollapsed
} from './actions';
import { convertFromImmutableToJS } from '../../helpers/common';
import { getRemindersTree } from './../../helpers/reminders';

const mapStateToProps = (state) => {
  const store = convertFromImmutableToJS(state.get('reminders'));
  store.reminderTree = getRemindersTree(store.reminderList);

  const items = Object.values(store.reminderTree);
  const {
    expandedNodes,
    remindersData
  } = store;
  const activeAccordionItemId = remindersData && remindersData.Id;

  items.forEach(item => item.expanded = expandedNodes[item.reminder.Id]);
  let activeItem = items.find(i => i.reminder.Id === activeAccordionItemId);
  while (activeItem) {
    activeItem.expanded = true;
    activeItem = store.reminderTree[activeItem.reminder.ParentId];
  }

  return store;
};

const mapDispatchToProps = (dispatch) => {
  return {
    getRemindersContent: (id) => {
      dispatch(getRemindersContent(id));
    },
    getReminderList: () => {
      dispatch(getReminderList());
    },
    setReminderNodeExpanded: (id) => {
      dispatch(setReminderNodeExpanded(id));
    },
    setReminderNodeCollapsed: (id) => {
      dispatch(setReminderNodeCollapsed(id));
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Container);
