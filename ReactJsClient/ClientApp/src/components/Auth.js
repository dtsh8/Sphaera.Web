import React, { Component } from 'react';
import Authenticate from 'react-openidconnect';
import OidcSettings from './oidcsettings';

export class Auth extends Component {
    displayName = "Authenticate"
    //this.mgr = new Oidc.UserManager(OidcSettings);
    constructor(props) {
        super(props);
    }

    login() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }

    render() {
        //mgr.signinRedirect();
        return (
            <div>
                <button id="login" onClick={this.login}>Login</button>
                <button id="api">Call API</button>
                <button id="logout">Logout</button>
                <button id="cards">Cards</button>
            </div>
            );
    }
}