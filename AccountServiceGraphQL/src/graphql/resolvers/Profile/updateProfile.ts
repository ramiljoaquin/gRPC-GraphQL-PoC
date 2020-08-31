import ProfileCommandClient from '../../services/Profile/ProfileCommandClient';

const client = ProfileCommandClient();


interface UpdateProfileRequest {
  request: {
    profileId: string;
    firstName: string;
    lastName: string;
    photoThumbUrl: string;
  }
}

export default (root: any, params: UpdateProfileRequest) => {
  return new Promise((resolve: any, reject: any) => {
    client.UpdateProfile(params.request, function(err: any, response: any) {
      if (err) {
        return reject(err);
      }
      resolve(response);
    });
  });
};
