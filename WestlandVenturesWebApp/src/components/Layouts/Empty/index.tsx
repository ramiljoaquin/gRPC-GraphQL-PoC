import React from 'react';

export default ({ children }: any) => {
  return <div className="layout layout-empty">
      <div className="layout-content">
        {children}
      </div>
  </div>
}