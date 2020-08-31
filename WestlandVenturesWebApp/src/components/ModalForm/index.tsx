import React from 'react';
import { Formik, Form } from 'formik';
import Loader from '@components/Loader';
import ErrorMessage from '@components/ErrorMessage';

interface ModalProps {
  title?: any;
  initialValues: any;
  validationSchema: any;
  loading: boolean;
  errorMessage: any;
  children: any;
  footer?: any;
  onClose?: () => void;
  onSubmit: (values: any) => void;
}

export default ({
  title,
  initialValues,
  validationSchema,
  loading,
  errorMessage,
  children,
  footer,
  onSubmit,
  onClose,
}: ModalProps) => {
  return (
    <div className="modal fade show">
      <div className="modal-dialog modal-lg">
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={onSubmit}
        >
          {(props: any) => (
            <Form className="modal-content">
              <div className="modal-header">
                <span className="modal-title medium">{title}</span>
                <button type="button" className="close" onClick={onClose}>
                  &times;
                </button>
              </div>
              <div className="modal-body">
                <div className="form-card form-card-fix">
                  <div className="form-card-head">
                    <Loader loading={loading} />
                    <ErrorMessage
                      loading={loading}
                      errorMessage={errorMessage}
                    />
                  </div>
                  <div className="form-card-body">
                    {React.cloneElement(children, { ...props })}
                  </div>
                </div>
              </div>
              {footer && <div className="modal-footer">{footer}</div>}
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};
