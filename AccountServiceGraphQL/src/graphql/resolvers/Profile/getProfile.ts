import ProfileQueryClient from '../../services/Profile/ProfileQueryClient';

const client = ProfileQueryClient();


interface GetProfileRequest {
  profileId: string;
}

export default (root: any, params: GetProfileRequest) => {
  return new Promise((resolve: any, reject: any) => {
    client.GetProfile(params, function(err: any, response: any) {
      if (err) {
        return reject(err);
      }
      resolve(response);
    });
  });
};
