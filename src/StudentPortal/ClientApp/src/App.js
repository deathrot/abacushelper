import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import CreateAccount from './components/create_account';
import Login from './components/login';
import Home from './components/home';
import Page404 from './components/page_404'
import ForgotPassword from './components/forgot_password'
import { AppContextProvider } from './context/app_context';

import './custom.css'

const App = (props) => {

    return (
        <Layout>
            <AppContextProvider>
                <Switch>
                    <Route exact path="/" component={Home} />
                    <Route path='/CreateAccount' component={CreateAccount}  />
                    <Route path='/login' component={Login} />
                    <Route path='/forgotpassword' component={ForgotPassword} />
                    <Route component={Page404} />
                </Switch>
            </AppContextProvider>
        </Layout>
    );
};

export default App;

