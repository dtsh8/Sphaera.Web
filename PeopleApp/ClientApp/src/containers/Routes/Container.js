import React from 'react';
import { Switch, Route } from 'react-router-dom';
import HomePage from '../HomePage';
import { NotFoundPage } from '../../components/NotFoundPage';
import RemindersPage from '../RemindersPage';
import Appeal from '../Appeal';
import {
  ROUTE_HOME,
  ROUTE_REMINDERS,
  ROUTE_CREATE_REQUEST,
  ROUTE_ONE_APPEAL,
} from '../../constants/routes';
import CreateRequestPage from '../CreateRequestPage';

export default function Routes({ location }) {
  return (
    <Switch location={location}>
      <Route
        exact
        path={ROUTE_HOME}
        component={HomePage}
      />
      <Route
        exact
        path={ROUTE_CREATE_REQUEST}
        component={CreateRequestPage}
      />
      <Route
        exact
        path={ROUTE_REMINDERS}
        component={RemindersPage}
      />
      <Route
        exact
        path={ROUTE_ONE_APPEAL}
        component={Appeal}
      />
      <Route
        component={NotFoundPage}
      />
    </Switch>
  );
}
