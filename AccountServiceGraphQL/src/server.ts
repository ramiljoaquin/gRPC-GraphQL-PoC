import * as fs from 'fs';
import resolvers from './graphql/resolvers';
import * as bodyParser from 'body-parser';
const express = require('express');
const { ApolloServer, gql } = require('apollo-server-express');
const { buildFederatedSchema } = require('@apollo/federation');
const typeDefs = gql(
  fs.readFileSync(__dirname.concat('/graphql/schema/schema.graphql'), 'utf8')
);
const { graphqlAccountApiEndpoint, port } = require('./config');

const app = express();

const server = new ApolloServer({
  schema: buildFederatedSchema([
    {
      typeDefs,
      resolvers
    },
  ]),
  engine: false,
});
app.use(bodyParser.json({ limit: '10mb' }));
server.applyMiddleware({ app });

app.listen({ port: port }, () =>
  console.log(
    `🚀 Server ready at ${graphqlAccountApiEndpoint}:${port}${server.graphqlPath}ql`
  )
);
