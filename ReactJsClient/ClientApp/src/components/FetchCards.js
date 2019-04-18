import React, { Component } from 'react';
import { UserManager, Oidc } from 'oidc-client';
import { OidcSettings } from './oidcsettings';

export class FetchCards extends Component {
    displayName = FetchCards.name

    constructor(props) {
        super(props);
        this.state = { cards: [], loading: true, _user: "undefined" };

        this.signin = this.signin.bind(this);
        this.onUserLoaded = this.onUserLoaded.bind(this);

        this.state = { isAuthenticated: false };
        //this.setState({ _user: user });
        //this.userManager = new O


        let mgr = new UserManager(OidcSettings);
        mgr.getUser().then(function (user) {
            this._user = user;
            //console.log(user);
            //this.setState({ _user: user });
            //console.log(user);
            //return fetch('https://localhost:44343/api/cards')
            //    .then(response => response.json())
            //    .then(data => {
            //        this.setState({ cards: data, loading: false });
            //    });
            var url = "https://localhost:44343/identity";

            var xhr = new XMLHttpRequest();
            xhr.open("GET", url);
            xhr.onload = function () {
                console.log(xhr.status, JSON.parse(xhr.responseText));
            };
            xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
            xhr.send();
        });  
    }

    componentWillMount() {

        this.userManager = new UserManager(OidcSettings);
        this.userManager.events.addUserLoaded(this.onUserLoaded);
        this.userManager.events.addUserUnloaded(this.onUserUnloaded);

        this.userManager.getUser().then((user) => {
            if (user !== null && user !== undefined) {
                this.onUserLoaded(user);
            } else if (window.location.href.includes("code")) {
                this.userManager.signinRedirectCallback().then(() => {
                    window.history.replaceState({}, "", "/");
                }).catch(function (err) {
                    console.log("Error signinRedirectCallback: ", err);
                });
            }
        })
    }

    onUserLoaded(user) {
        this.setState({ isAuthenticated: true });

        if (this.props.userLoaded !== undefined)
            this.props.userLoaded(user);
    }

    onUserUnloaded() {
        this.setState({ isAuthenticated: false });

        if (this.props.userUnLoaded !== undefined)
            this.props.userUnLoaded();
    }

    signin() {
        this.userManager.signinRedirect().then(function () {
            console.log('signinRedirect ok');
        }).catch(function (err) {
            console.log('signinRedirect error:', err);
        });
    }

    static renderCardsTable(cards) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>CardId</th>
                        <th>IncidentId</th>
                        <th>Archived</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {cards.map(card =>
                        <tr key={card.cardId}>
                            <td>{card.incidentId}</td>
                            <td>{card.incidentId}</td>
                            <td>{card.isArchived}</td>
                            <td>{card.description}</td>
                            
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.isAuthenticated ? FetchCards.renderCardsTable(this.state.cards) : "Loading...";

        return (
            <div>
                <h1>Cards</h1>
                <p>This component demonstrates fetching cards from the server.</p>
                {contents}
            </div>
        );
    }
}
