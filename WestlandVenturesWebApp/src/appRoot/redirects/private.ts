import { RedirectProps } from 'react-router-dom';

const privateRedirects: RedirectProps[] = [
  {
    from: '/(|sign-in|sign-up|forgot-password)',
    to: '/manage',
    exact: true
  }
];

export default privateRedirects;
