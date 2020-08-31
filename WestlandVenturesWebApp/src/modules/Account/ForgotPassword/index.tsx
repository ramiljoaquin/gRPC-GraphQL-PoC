import React from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import { ForgotPasswordSchema } from '@common/helpers/Account/Validations';
import classNames from 'classnames';

export default () => {
  return (
    <div className="rs-account-forgot-password">
      <div className="form-card">
        <div className="form-card-header">
          <h3>Forgot Password</h3>
          <p>Enter email address to reset your password.</p>
          <p />
        </div>
        <div className="form-card-body">
          <Formik
            initialValues={{
              email: '',
            }}
            validationSchema={ForgotPasswordSchema}
            onSubmit={(values: any) => {
              console.log(values);
            }}
          >
            {({ errors, touched }: any) => (
              <Form className="form-horizontal">
                <div className="form-group">
                  <label>Email address</label>
                  <Field
                    name="email"
                    type="email"
                    placeholder="youremail@example.com"
                    className={classNames('form-control', {
                      'is-invalid': errors.email && touched.email,
                    })}
                  />
                </div>
                <div className="form-group text-center">
                  <button type="submit" className="btn btn-primary submit-btn">
                    Send Email
                  </button>
                </div>
              </Form>
            )}
          </Formik>
        </div>
        <div className="form-card-footer">
          <p />
          <p>
            Return to{' '}
            <Link className="link" to="/sign-in">
              Sign In
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};
