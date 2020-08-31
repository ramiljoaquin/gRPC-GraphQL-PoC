import signIn from './Account/signIn';
import signUp from './Account/signUp';
import updateProfile from './Profile/updateProfile';
import getProfile from './Profile/getProfile';
import getProfilePage from './Profile/getProfilePage';

const resolvers: any = {
  Mutation: {
    signUp,
    signIn,
    updateProfile
  },
  Query: {
    getProfile,
    getProfilePage
  },
};

export default resolvers;
