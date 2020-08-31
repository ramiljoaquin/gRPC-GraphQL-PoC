import { RouteProps } from 'react-router-dom';
import SignIn from '@modules/Account/SignIn';
import SignUp from '@modules/Account/SignUp';
import ForgotPassword from '@modules/Account/ForgotPassword';
import NotFound from '@modules/Shared/NotFound';

export default [{
  path: '/sign-in',
  component: SignIn,
  exact: true
}, {
  path: '/sign-up',
  component: SignUp,
  exact: true
}, {
  path: '/forgot-password',
  component: ForgotPassword,
  exact: true
}, {
  path: '*',
  component: NotFound,
  exact: true
}] as RouteProps[]