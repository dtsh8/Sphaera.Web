import React from 'react';
import { Grid, Col, Row } from 'react-bootstrap';
import bem from '../../../lib/bem';
import './style.css';

const block = 'footer';

export default class Footer extends React.PureComponent {
  getFooterContent = () => {
    const { footerData } = this.props;

    return footerData.map(({ id, title, elements }) => {
      return (
        <Col xs={6} md={3} key={id}>
          <div className={bem({ block, elem: 'block' })}>
            <span className={bem({ block, elem: 'item-title' })}>
              {title}
            </span>
            {elements.map(({ id: itemId, name, link }) => {
              return (
                <a
                  className={bem({ block, elem: 'item' })}
                  key={itemId}
                  href={link && link}
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  {name}
                </a>
              );
            })}
          </div>
        </Col>
      );
    });
  };

  shouldComponentRender = () => {
    const { footerData } = this.props;

    return footerData && !!footerData.length;
  };

  render() {
    if (!this.shouldComponentRender()) {
      return <div />;
    }

    return (
      <footer className={bem({ block })}>
        <Grid>
          <Row>
            {this.getFooterContent()}
          </Row>
        </Grid>
      </footer>
    );
  }
}
