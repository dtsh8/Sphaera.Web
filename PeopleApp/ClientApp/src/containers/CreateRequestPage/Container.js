import React, { Fragment } from 'react';
import Header from '../../containers/Header';
import Footer from '../../containers/Footer';
import { RequestPage } from '../../components/requestPage';
import { RequestPageHeader } from '../../components/requestPage/RequestPageHeader';

const defaultUserLocation = {
  coords: [53.9, 27.567],
  zoom: 10
};

export default class CreateRequest extends React.PureComponent {
  shouldComponentRender = () => {
    return !!this.props.user || (!!this.props.address && !!this.props.address.fullAddress);
  };

  render() {
    if (!this.shouldComponentRender()) {
      return <div />;
    }

    const {
      user,
      tokens,
      onSubmitForm,
      types,
      address,
      getAddress,
      setAddress,
      getAutocompleteAddress,
      autocompleteCities,
      coord,
      history,
      isAppealSending,
    } = this.props;
    const location = (user && user.MapExtent) ? JSON.parse(user.MapExtent) : [];

    return (
      <Fragment>
        <Header user={user} tokens={tokens} />
        <RequestPageHeader />
        <RequestPage
          userLocation={(user && user.MapExtent) ? [location[0], location[1]] : defaultUserLocation.coords}
          zoom={(user && user.MapExtent) ? location[2] : defaultUserLocation.zoom}
          onSubmitForm={onSubmitForm}
          types={types}
          address={address}
          getAddress={getAddress}
          setAddress={setAddress}
          getAutocompleteAddress={getAutocompleteAddress}
          autocompleteCities={autocompleteCities}
          coord={coord}
          history={history}
          isAppealSending={isAppealSending}
        />
        <Footer />
      </Fragment>
    );
  }
}
