import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { FetchCards } from './components/FetchCards';
import { Counter } from './components/Counter';
import { AuthProvider } from './providers/authProvider';
import { BrowserRouter } from "react-router-dom";
import { Routes } from "./routes/routes";
import { Auth } from "./components/Auth";

export default class App extends Component {
  displayName = App.name

   
    constructor(props) {
        super(props);
        
    }

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} />
        <Route path='/fetchcards' component={FetchCards} />
        <Route path='/auth' component={Auth} />
            <AuthProvider>
                <BrowserRouter children={Routes} basename={"/"} />
            </AuthProvider>
      </Layout>
    );
  }
    
}
