import React from 'react';
import Footer from '../../components/layout/Footer';

export default class FooterContainer extends React.PureComponent {
  componentDidMount() {
    this.props.getFooterData();
  }

  render() {
    const { footerData } = this.props;

    return (
      <Footer footerData={footerData} />
    );
  }
}