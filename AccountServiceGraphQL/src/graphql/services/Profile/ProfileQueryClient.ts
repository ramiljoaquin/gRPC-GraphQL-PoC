import * as grpc from 'grpc';
import * as protoLoader from '@grpc/proto-loader';
const {
  grpcAccountApiEndpoint,
  grpcAccountApiPort,
} = require('./../../../config');
const packageDefinition: any = protoLoader.loadSync(
  __dirname + '/profile_query.proto',
);

const proto: any = grpc.loadPackageDefinition(packageDefinition)
  .Profile;
  
const credentials: any = grpc.credentials.createInsecure();

const interceptorAuth: any = (options: any, nextCall: any) =>
  new grpc.InterceptingCall(nextCall(options), {
    start: function(metadata, listener, next) {
      metadata.add('x-api-key', 'myapikey');
      next(metadata, listener);
    },
  });

const options: any = {
  'grpc.ssl_target_name_override': grpcAccountApiEndpoint,
  interceptors: [interceptorAuth],
};

export default () =>
  new proto.ProfileQuery(
    `${grpcAccountApiEndpoint}:${grpcAccountApiPort}`,
    credentials,
    options,
  );
