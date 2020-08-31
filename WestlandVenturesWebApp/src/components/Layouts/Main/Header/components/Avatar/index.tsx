import React from 'react';
import { Link } from 'react-router-dom';
import { useCookies } from 'react-cookie';
import { useApolloClient } from '@apollo/client';

interface AvatarDropdownProps {
  profile?: any;
}

export default ({ profile }: AvatarDropdownProps ) => {
  const [,,removeCookie] = useCookies(['user_id'])
  const client = useApolloClient();
  const handleSignOut = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
  ) => {
    event.preventDefault();
    client.resetStore();
    client.clearStore();
    
    removeCookie('user_id');
    sessionStorage.clear();
    localStorage.clear();
    window.location.href = '/';
  };

  return (
    <div id="rs-collapse-dropdown" className="profile-dropdown dropdown">
      <button
        className="profile-dropdown-btn"
        data-toggle="dropdown"
        aria-haspopup="true"
        aria-expanded="false"
      >
        <span className="profile-pic">
           {}
        </span>
      </button>
      <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <Link className="dropdown-item nav-link" to="/profile">
          Profile
        </Link>
        <Link className="dropdown-item nav-link" to="/settings">
          Settings
        </Link>
        <Link className="dropdown-item nav-link" to="/account">
          Account
        </Link>
        <div className="dropdown-divider"></div>
        <button className="dropdown-item nav-link" onClick={handleSignOut}>
          Sign Out
        </button>
      </div>
    </div>
  );
};
