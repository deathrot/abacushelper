import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import CreateAccount from './components/create_account';
import Login from './components/login';
import { AppContextProvider } from './context/app_context';

import './custom.css'

const App = (props) => {

    return (
        <Layout>
            <AppContextProvider>
                <Route exact path='/' component={CreateAccount}  />
                <Route path='/login' component={Login} />
            </AppContextProvider>
        </Layout>
    );
};

export default App;

