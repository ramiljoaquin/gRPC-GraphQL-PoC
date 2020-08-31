import React, { useState } from 'react';
import { useMutation } from '@apollo/client';
import { Link } from 'react-router-dom';
import { Formik, Form, Field } from 'formik';
import { SIGN_UP } from '@common/helpers/Account/gqlTags';
import { SignUpSchema } from '@common/helpers/Account/Validations';
import Loader from '@components/Loader';
import ErrorMessage from '@components/ErrorMessage';
import classNames from 'classnames';

interface SignUpProps {
  history?: any;
}

// eslint-disable-next-line no-empty-pattern
export default ({}: SignUpProps) => {
  const [errorMessage, setErrorMessage] = useState('');

  const [signUp, { loading }] = useMutation(SIGN_UP, {
    onError: (error: any) => {
      setErrorMessage(error.message);
    },
    onCompleted: ({ signUp: res }: any) => {
      console.log(res);
      setErrorMessage('');
      window.location.href = '/';
    },
    fetchPolicy: 'no-cache',
  });
  return (
    <div className="rs-account-sign-up">
      <div className="form-card">
        <div className="form-card-header">
          <h3>Create Account</h3>
          <p>Enter your details to create an account.</p>
          <p />
        </div>
        <Loader loading={loading} />
        <ErrorMessage loading={loading} errorMessage={errorMessage} />
        <div className="form-card-body">
          <Formik
            initialValues={{
              firstName: '',
              lastName: '',
              email: '',
              password: '',
              confirmPassword: '',
            }}
            validationSchema={SignUpSchema}
            onSubmit={async (values: any) => {
              await signUp({
                variables: {
                  signUpRequest: {
                    companyName: '',
                    firstName: values.firstName,
                    lastName: values.lastName,
                    email: values.email,
                    password: values.password,
                    roleName: 'User',
                  },
                },
              });
            }}
          >
            {({ errors, touched }: any) => (
              <Form className="form-horizontal">
                <div className="form-group row">
                  <div className="col col-6">
                    <label htmlFor="firstName">First Name</label>
                    <Field
                      id="firstName"
                      name="firstName"
                      type="text"
                      placeholder="Your first name"
                      className={classNames('form-control', {
                        'is-invalid': errors.firstName && touched.firstName,
                      })}
                    />
                  </div>
                  <div className="col col-6">
                    <label htmlFor="lastName">Last Name</label>
                    <Field
                      id="lastName"
                      name="lastName"
                      type="text"
                      placeholder="Your last name"
                      className={classNames('form-control', {
                        'is-invalid': errors.lastName && touched.lastName,
                      })}
                    />
                  </div>
                </div>
                <div className="form-group">
                  <label htmlFor="email">
                    Email address (Make sure it's correct)
                  </label>
                  <Field
                    id="email"
                    name="email"
                    type="email"
                    placeholder="youremail@example.com"
                    className={classNames('form-control', {
                      'is-invalid': errors.email && touched.email,
                    })}
                  />
                </div>
                <div className="form-group row">
                  <div className="col col-6">
                    <label htmlFor="password">Password</label>
                    <Field
                      id="password"
                      name="password"
                      type="password"
                      placeholder="Enter 6 characters or more"
                      className={classNames('form-control', {
                        'is-invalid': errors.password && touched.password,
                      })}
                    />
                  </div>
                  <div className="col col-6">
                    <label htmlFor="password">Password</label>
                    <Field
                      id="confirmPassword"
                      name="confirmPassword"
                      type="password"
                      placeholder="Confirm Password"
                      className={classNames('form-control', {
                        'is-invalid':
                          errors.confirmPassword && touched.confirmPassword,
                      })}
                    />
                  </div>
                </div>
                <div className="form-group text-center">
                  <p />
                  <button type="submit" className="btn btn-primary submit-btn">
                    Create Account
                  </button>
                </div>
              </Form>
            )}
          </Formik>
        </div>
        <div className="form-card-footer">
          <p />
          <p>
            By creating an account, you agree to our{' '}
            <Link className="link" to="/terms-and-conditions">
              Terms and Conditions
            </Link>
            .
            <br />
            Already have an account?{' '}
            <Link className="link" to="/sign-in">
              Sign In
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};
