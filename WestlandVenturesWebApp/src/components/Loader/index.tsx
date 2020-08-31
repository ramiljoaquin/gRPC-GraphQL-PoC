import React from 'react';
interface LoaderProps {
  loading: boolean;
}
export default ({ loading }:LoaderProps) => (
  <>
    {loading && (
      <div className="loader">
        <i className="fas fa-spinner fa-pulse" /> Loading...
      </div>
    )}
  </>
);
