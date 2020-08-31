import { RedirectProps } from 'react-router-dom';

const publicRedirects: RedirectProps[] = [
  {
    from: '/(|manage|request-service|request-services)',
    to: '/sign-in',
    exact: true
  }
];

export default publicRedirects;
