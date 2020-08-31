import { Resolver } from '@apollo/client';
import axios from 'axios';

interface QueryResolvers {
  readAsText: Resolver;
  getIpAddress: Resolver;
}

const Queries: QueryResolvers = {
  readAsText: (_obj, variables) => {
    const { path } = variables;
    if (path) {
      return axios.get(path);
    }
    return _obj;
  },
  getIpAddress: (_obj, variables) => {
    return axios
      .get('https://geolocation-db.com/json/');
  }
}

export default {
  ...Queries
}

