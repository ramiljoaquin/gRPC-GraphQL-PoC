import React from 'react';
import { Formik, Form, Field } from 'formik';
import { ChangePasswordSchema } from '@common/helpers/Account/Validations';


interface ChangePasswordProps {
  profile: any;
}

export default ({ profile }: ChangePasswordProps) => {
  return (
    <div className="form-card">
      <div className="form-card-body">
        <Formik
          initialValues={{
            newPassword: '',
            confirmPassword: '',
          }}
          validationSchema={ChangePasswordSchema}
          onSubmit={(values: any) => {
            // same shape as initial values
            debugger;
            console.log(values);
          }}
          handleInputChange
        >
          {({ values, errors, touched }: any) => (
            <Form className="form-horizontal">
              <div className="form-group">
                <label htmlFor="currentPassword">Current password</label>
                <Field
                  id="currentPassword"
                  name="currentPassword"
                  type="password"
                  placeholder="Current Password"
                  className={`form-control ${
                    errors.currentPassword &&
                    touched.currentPassword &&
                    'is-invalid'
                  }`}
                />
              </div>
              <div className="form-group">
                <label htmlFor="newPassword">New password</label>
                <Field
                  id="newPassword"
                  name="newPassword"
                  type="password"
                  placeholder="Enter 6 characters or more"
                  className={`form-control ${
                    errors.newPassword && touched.newPassword && 'is-invalid'
                  }`}
                />
              </div>
              <div className="form-group">
                <label htmlFor="confirmPassword">Confirm new password</label>
                <Field
                  id="confirmPassword"
                  name="confirmPassword"
                  type="password"
                  placeholder="Re-enter new password"
                  className={`form-control ${
                    errors.confirmPassword &&
                    touched.confirmPassword &&
                    'is-invalid'
                  }`}
                />
              </div>
              <div className="form-group">
                <br />
                <button type="submit" className="btn btn-primary rs-btn">
                  Update Password
                </button>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};
