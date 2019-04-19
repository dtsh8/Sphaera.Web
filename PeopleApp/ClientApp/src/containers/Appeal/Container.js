import React, { Fragment } from 'react';
import Header from '../../containers/Header';
import Footer from '../../containers/Footer';
import { Appeal } from '../../components/appeal/AppealContent';
import { AppealHeader } from '../../components/appeal/AppealHeader';

export default class AppealContainer extends React.PureComponent {
  componentDidMount() {
    const { getAppeal } = this.props;
    getAppeal();
  }

  render() {
    const {
      user,
      tokens,
      appeal,
      downloadFile,
      downloadAllFiles,
      types,
    } = this.props;
    const { cardIndexCode } = appeal;

    return (
      <Fragment>
        <Header user={user} tokens={tokens} />
        <AppealHeader
          title={appeal && appeal.cardIndexName ? appeal.cardIndexName : ''}
          types={types}
          cardIndexCode={cardIndexCode}
        />
        <Appeal
          appeal={appeal}
          downloadFile={downloadFile}
          downloadAllFiles={downloadAllFiles}
        />
        <Footer />
      </Fragment>
    );
  }
}
