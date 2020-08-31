import React, { useState } from 'react';
import { useMutation } from '@apollo/client';
import { Link } from 'react-router-dom';
import { Formik, Form, Field } from 'formik';
import { useCookies } from 'react-cookie';
import decode from 'jwt-decode';
import classNames from 'classnames';
import { SIGN_IN } from '@common/helpers/Account/gqlTags';
import { SignInShema } from '@common/helpers/Account/Validations';
import Loader from '@components/Loader';
import ErrorMessage from '@components/ErrorMessage';


export default () => {
  const [errorMessage, setErrorMessage] = useState('');
  const [, setCookie] = useCookies(['user_id']);
  const [signIn, { loading }] = useMutation(SIGN_IN, {
    onError: (error: any) => {
      setErrorMessage(error.message);
    },
    onCompleted: ({ user }: any) => {
      const token: any = decode(user.accessToken);
      var d = new Date();
      d.setTime(d.getTime() + (7*24*60*60*1000));
      setCookie('user_id', token.id, {
        path: '/',
        expires: d,
      });
      window.location.href = '/';
    },
    fetchPolicy: 'no-cache',
  });
  return (
    <div className="rs-account-sign-in">
      <div className="form-card">
        <div className="form-card-header">
          <h3>Sign In</h3>
          <p>Enter email address and password to sign in.</p>
        </div>
        <Loader loading={loading} />
        <ErrorMessage loading={loading} errorMessage={errorMessage} />
        <div className="form-card-body">
          <Formik
            initialValues={{
              email: '',
              password: '',
            }}
            validationSchema={SignInShema}
            onSubmit={async (values: any) => {
              await signIn({
                variables: {
                  userName: values.email,
                  password: values.password,
                },
              });
            }}
          >
            {({ errors, touched }: any) => (
              <Form className="form-horizontal">
                <div className="form-group">
                  <label htmlFor="email">Email address</label>
                  <Field
                    id="email"
                    name="email"
                    type="email"
                    placeholder="you@example.com"
                    className={classNames('form-control', { 'is-invalid': errors.email && touched.email })}
                  />
                </div>
                <div className="form-group">
                  <label htmlFor="password">Password</label>
                  <Field
                    id="password"
                    name="password"
                    type="password"
                    placeholder="Enter Password"
                    className={classNames('form-control', { 'is-invalid': errors.password && touched.password })}
                  />
                </div>
                <div className="form-group">
                  <label className="forgot-password-link">
                    <Link to="/forgot-password">Forgot password?</Link>
                  </label>
                </div>
                <div className="form-group text-center">
                  <button type="submit" className="btn btn-primary submit-btn">
                    Sign In
                  </button>
                </div>
              </Form>
            )}
          </Formik>
        </div>
        <div className="form-card-footer">
          <span />
          <p>
            Don't you have an account yet?{' '}
            <Link className="link" to="/sign-up">
              Create Account
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};
