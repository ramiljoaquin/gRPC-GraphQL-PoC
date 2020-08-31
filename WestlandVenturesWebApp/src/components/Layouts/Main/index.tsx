import React from 'react';
import Header from './Header';

interface LayoutMainProps {
  history?: any;
  user: any;
  children: any;
}

export default ({ history, children }: LayoutMainProps) => {
  return (
    <div className="layout">
      <div className="layout-navs">
        <div className="navs-header">
          <span></span>
        </div>
        <div className="navs-content">
          <ul className="rs-navs nav">
             <li></li>
          </ul>
        </div>
      </div>
      <div className="layout-content">
        <div className="layout-header">
          <Header />
        </div>
        <div className="layout-body">
           {children}
        </div>
      </div>
    </div>
  );
};
