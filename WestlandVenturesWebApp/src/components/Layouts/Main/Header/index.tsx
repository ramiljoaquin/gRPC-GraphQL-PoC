import React from 'react';
import AddDropDown from './components/AddDropDown';
import RightNavigation from './components/RightNavigation';

export default () => {
  return (
    <nav id="rs-navbar" className="navbar">
      <div className="navbar-grid">
        <div className="navbar-grid-col">
          <AddDropDown />
        </div>
        <div className="navbar-grid-col">
          <RightNavigation />
        </div>
      </div>
    </nav>
  );
};
