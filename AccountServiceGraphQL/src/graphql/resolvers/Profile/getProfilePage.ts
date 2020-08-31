import ProfileQueryClient from '../../services/Profile/ProfileQueryClient';

const client = ProfileQueryClient();


interface GetProfilePageRequest {
  request: {
    keywords?: string;
    orderBy?: string;
    page: number;
    pageSize: number;
  }
}

export default (root: any, params: GetProfilePageRequest) => {
  return new Promise((resolve: any, reject: any) => {
    client.GetProfile(params.request, function(err: any, response: any) {
      if (err) {
        return reject(err);
      }
      resolve(response);
    });
  });
};
