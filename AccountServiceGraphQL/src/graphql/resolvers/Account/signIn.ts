import AccountCommandClient from '../../services/Account/AccountCommandClient';

const client = AccountCommandClient();

interface SignInRequest {
  userName: string;
  password: string;
}

export default (root: any, params: SignInRequest) => {
  return new Promise((resolve: any, reject: any) => {
    client.SignIn(params, function(err: any, response: any) {
      if (err) {
        return reject(err.details);
      }
      resolve(response);
    });
  });
};
