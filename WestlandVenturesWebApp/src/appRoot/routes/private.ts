import { RouteProps } from 'react-router-dom';
import Manage from '@modules/Account/Manage';

export default [{
  path: '/account',
  component: Manage,
  exact: true
}] as RouteProps[]