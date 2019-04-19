import '@babel/polyfill';
import React from 'react';
import ReactDOM from 'react-dom';
import { ConnectedRouter } from 'react-router-redux';
import { Provider } from 'react-redux';
import createHistory from 'history/createBrowserHistory';
import 'normalize.css';
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.compact.css';
import 'leaflet/dist/leaflet.css';
import 'font-awesome/css/font-awesome.css';
import 'font-awesome/css/font-awesome.min.css';
import App from './components/App';
import configureStore from './configureStore';
import './lib/bootstrap/bootstrap.min.css';
import './lib/bootstrap/bootstrap-theme.min.css';
import './style.css';
import context from './api/api';

const history = createHistory({ basename: 'PeoplePublicArea' });
//const history = createHistory();
export const store = configureStore(history);

const appRootDomElement = document.getElementById('root');

let wasAuthenticated = false;
const checkAuth = () => {
  setTimeout(async () => {
    try {
      const isAuthenticated = await context.account.isAuthenticated();
      if (wasAuthenticated && !isAuthenticated) {
        window.location.reload();
      }
      wasAuthenticated = isAuthenticated;
    } finally {
      checkAuth();
    }
  }, 10000);
};
checkAuth();

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  appRootDomElement
);
