import React from 'react';
import ReactDOM from 'react-dom';
import {
  ApolloClient,
  ApolloProvider,
  InMemoryCache,
  HttpLink,
  ApolloLink,
  concat,
} from '@apollo/client';
import App from './appRoot/index';
import * as serviceWorker from './serviceWorker';
import '@css/style.scss';
import resolvers from '@src/store/resolvers';

const cache = new InMemoryCache({
  addTypename: false,
});


const authMiddleware = new ApolloLink((operation, forward) => {
  operation.setContext({
    headers: {
      'client-name': 'Account Service',
      'client-version': '1.0.0',
    },
  });
  return forward(operation);
});

const httpLink = new HttpLink({
  uri: 'http://localhost:4001/graphql',
});

const init = async () => {
  const client = new ApolloClient({
    connectToDevTools: true,
    link: concat(authMiddleware, httpLink),
    queryDeduplication: false,
    cache,
    ssrMode: typeof window === 'undefined',
    resolvers,
  });

  const ApolloApp = () => {
    return (
      <ApolloProvider client={client}>
        <App />
      </ApolloProvider>
    );
  };

  ReactDOM.render(<ApolloApp />, document.getElementById('root'));
};

init();

serviceWorker.unregister();
