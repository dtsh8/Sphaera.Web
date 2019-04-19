import React from 'react';
import { Modal } from 'react-bootstrap';
import './style.css';
import bem from '../../../lib/bem';

const block = 'modal-component';

export class ModalComponent extends React.Component {
  constructor(props, context) {
    super(props, context);

    this.state = {
      show: false
    };
  }

  handleClose = () => {
    this.setState({ show: false });
  };

  handleShow = () => {
    this.setState({ show: true });
  };

  render() {
    return (
      <div>
        <div onClick={this.handleShow}>
          {this.props.children}
        </div>

        <Modal
          bsSize="large"
          show={this.state.show}
          onHide={this.handleClose}
          backdropClassName="backdrop-modal"
          dialogClassName={`${bem({ block })} ${this.props.className}`}
        >
          <Modal.Header closeButton>
            <Modal.Title className={bem({ block, elem: 'title' })}>
              <span>
                {this.props.titleContent}
              </span>
            </Modal.Title>
          </Modal.Header>
          <Modal.Body className={bem({ block, elem: 'body' })} dangerouslySetInnerHTML={{ __html: this.props.bodyContent }} />
        </Modal>
      </div>
    );
  }
}
