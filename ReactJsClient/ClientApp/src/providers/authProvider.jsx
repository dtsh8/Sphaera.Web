/* /src/providers/authProvider.jsx */

import React, { Component } from "react";
import AuthService from "../services/authService";

const AuthContext = React.createContext({
    signinRedirectCallback: () => ({}),
    logout: () => ({}),
    signoutRedirectCallback: () => ({}),
    isAuthenticated: () => ({}),
    signinRedirect: () => ({}),
    signinSilentCallback: () => ({}),
    createSigninRequest: () => ({})
});

export const AuthConsumer = AuthContext.Consumer;

export class AuthProvider extends Component {
    
    constructor(props) {
        super(props);
        this.authService = new AuthService();
    }
    authService;
    render() {
        return <AuthContext.Provider value={this.authService}>{this.props.children}</AuthContext.Provider>;
    }
}