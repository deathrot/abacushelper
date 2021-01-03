import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/home';
import { Login } from './components/login';
import { AppContextProvider } from './context/app_context';

import './custom.css'

export default App = (props) => {
    const displayName = App.name;

    return (
        <Layout>
            <AppContextProvider>
                <Route exact path='/' component={Home} />
                <Route path='/login' component={Login} />
            </AppContextProvider>
        </Layout>
    );
}