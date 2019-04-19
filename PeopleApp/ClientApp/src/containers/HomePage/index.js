import { connect } from 'react-redux';
import Container from './Container';
import { getReminderList } from '../RemindersPage/actions';
import { convertFromImmutableToJS } from '../../helpers/common';
import { getStatistic } from './actions';
import { storeName } from './reducer';

const mapStateToProps = (state) => {
  return {
    reminders: convertFromImmutableToJS(state.get('reminders')),
    statistic: convertFromImmutableToJS(state.getIn([storeName, 'statistic']))
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    getReminderList: () => {
      dispatch(getReminderList());
    },
    getStatistic: () => {
      dispatch(getStatistic());
    }
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Container);
