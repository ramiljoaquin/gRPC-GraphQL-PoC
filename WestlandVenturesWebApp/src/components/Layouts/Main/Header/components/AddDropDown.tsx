import React from 'react';
import { Link } from 'react-router-dom';

export default () => {
  return (
    <div id="rs-collapse-dropdown" className="add-dropdown dropdown">
      <button
        className="btn"
        type="button"
        id="dropdownMenuButton"
        data-toggle="dropdown"
        aria-haspopup="true"
        aria-expanded="false"
      >
        <i className="fas fa-plus"></i> Add
      </button>
      <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <Link className="dropdown-item" to="/messages/new">
          New 
        </Link>
        <div className="dropdown-divider"></div>
        <Link className="dropdown-item" to="/messages">
          Compose Message
        </Link>
        <Link className="dropdown-item" to="/job-application">
          Job Application
        </Link>
        <div className="dropdown-divider"></div>
        <Link className="dropdown-item" to="/account">
          Email
        </Link>
      </div>
    </div>
  );
};
