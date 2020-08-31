import { Resolvers } from '@apollo/client';
// import Mutations from './Mutations/index';
import Queries from './Queries/index';

const resolvers: Resolvers = {
  Query: Queries,
  // Mutation: ,
};

export default resolvers;
