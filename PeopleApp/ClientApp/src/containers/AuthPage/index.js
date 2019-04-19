import { connect } from 'react-redux';
import Container from './Container';
import { sendUserAuthForm } from './actions';

const mapDispatchToProps = (dispatch) => {
  return {
    onSubmitAuthForm: (formData, callback) => {
      dispatch(sendUserAuthForm(formData, callback));
    }
  };
};

export default connect(null, mapDispatchToProps)(Container);
