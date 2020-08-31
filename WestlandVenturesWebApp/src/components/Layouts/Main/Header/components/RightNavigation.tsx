import React from 'react';
import Avatar from './Avatar';
import MessageBadge from '@components/MessageBadge';

export default () => {
  return (
    <div className="rs-right-navbar">
      <span className="navbar-item message-badge">
        <MessageBadge />
      </span>
      <span className="navbar-item">
        <i className="icon fa fa-bell"></i>
      </span>
      <span className="navbar-item">
        <Avatar />
      </span>
    </div>
  );
};
