import React, { MouseEventHandler } from 'react';

interface ModalProps {
  id?: string;
  title?: any;
  children: any;
  footer?: any;
  onClose?: MouseEventHandler<HTMLButtonElement>;
}

export default ({ title, children, footer, onClose }: ModalProps) => {
  return (
    <div className="modal fade show">
      <div className="modal-dialog modal-lg">
        <div className="modal-content">
          <div className="modal-header">
            <span className="modal-title medium">
              {title}
            </span>
            <button type="button" className="close" onClick={onClose}>
              &times;
            </button>
          </div>
          <div className="modal-body">{children}</div>
          {footer && <div className="modal-footer">{footer}</div>}
        </div>
      </div>
    </div>
  );
};
