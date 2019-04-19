import { connect } from 'react-redux';
import Container from './Container';
import UserDataHOC from '../HOC/UserData';

export default connect()(UserDataHOC(Container));
