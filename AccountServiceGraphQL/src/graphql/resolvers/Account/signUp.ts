import AccountCommandClient from '../../services/Account/AccountCommandClient';

const client = AccountCommandClient();


interface SignUpRequest {
  request: {
    companyName?: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    roleName: string;
  }
}

export default (root: any, params: SignUpRequest) => {
  return new Promise((resolve: any, reject: any) => {
    client.SignUp(params.request, function(err: any, response: any) {
      if (err) {
        return reject(err);
      }
      resolve(response);
    });
  });
};
