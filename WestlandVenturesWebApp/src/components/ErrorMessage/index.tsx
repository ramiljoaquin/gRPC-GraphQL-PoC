import React, { Fragment } from 'react';
import isEmpty from 'lodash/isEmpty';

interface ErrorProps {
  loading: boolean;
  errorMessage?: string;
}
export default ({ loading = false, errorMessage }: ErrorProps) => (
  <Fragment>
    {!loading && !isEmpty(errorMessage) && (
      <div className="alert alert-danger" role="alert">
        {errorMessage}
      </div>
    )}
  </Fragment>
);
