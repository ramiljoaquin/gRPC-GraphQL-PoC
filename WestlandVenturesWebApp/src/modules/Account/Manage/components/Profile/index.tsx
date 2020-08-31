import React from 'react';
import { Formik, Form, Field } from 'formik';
import classNames from 'classnames';

interface ProfileProps {
  history?: any;
  profile?: any;
}

export default ({ profile }: ProfileProps) => {
  
  return (
    <div className="container form-panel">
      <div className="form-panel-header"></div>
      <div className="form-panel-body">
        <Formik
          initialValues={{
            firstName: profile.firstName,
            lastName: profile.lastName,
          }}
          onSubmit={({ values }: any) => {
            console.log(values);
          }}
        >
          {({ errors, touched }) => (
            <Form className="form-horizontal">
              <div className="form-group">
                <label htmlFor="firstName">Firstname</label>
                <Field
                  id="firstName"
                  name="firstName"
                  type="text"
                  placeholder="Firstname"
                  className={classNames('form-control', {
                    'is-invalid': errors.firstName && touched.firstName,
                  })}
                />
              </div>
              <div className="form-group">
                <label htmlFor="lastName">Lastname</label>
                <Field
                  id="lastName"
                  name="lastName"
                  type="text"
                  placeholder="Lastname"
                  className={classNames('form-control', {
                    'is-invalid': errors.lastName && touched.lastName,
                  })}
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Email</label>
                <input
                  id="email"
                  name="email"
                  type="text"
                  defaultValue={profile.email}
                  placeholder="Email"
                  className="form-control"
                  disabled={true}
                />
              </div>
              <div className="form-group">
                 <button type="submit" className="btn btn-default">Save</button>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};
