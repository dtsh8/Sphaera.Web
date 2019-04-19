import { createStore, applyMiddleware, compose } from 'redux';
import { routerMiddleware } from 'react-router-redux';
import createSagaMiddleware from 'redux-saga';
import reducers from './reducers';
import sagas from './sagas';

export default function storeConfigure(history) {
  const sagaMiddleware = createSagaMiddleware();

  /* eslint-disable no-underscore-dangle */
  const composeEnhancers =
    typeof window === 'object' &&
    window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ ?
      window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__({
        // Specify extension’s options like name, actionsBlacklist, actionsCreators, serialize...
      }) : compose;
  /* eslint-enable */

  const enhancer = composeEnhancers(
    applyMiddleware(
      routerMiddleware(history),
      sagaMiddleware
    )
  );
  const store = createStore(reducers, enhancer);

  sagaMiddleware.run(sagas);

  return store;
}