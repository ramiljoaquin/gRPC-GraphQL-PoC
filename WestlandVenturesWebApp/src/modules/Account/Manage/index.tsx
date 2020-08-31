import React from 'react';
import Profile from './components/Profile';
import ChangePassword from './components/ChangePassword';

interface ManageProps {
  history?: any;
  user?: any;
}

export default ({ user }: ManageProps) => {
  const profile = user.profile || {};
  return (
    <div className="manage-account-module">
      <div className="row">
        <div className="col">
           <Profile profile={profile} />
        </div>
      </div>
      <div className="row">
        <div className="col">
           <ChangePassword profile={profile} />
        </div>
      </div>
    </div>
  );
};
