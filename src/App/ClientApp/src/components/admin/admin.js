import React, { Component } from 'react';
import Questions from './questions'
import { AdminContextProvider } from './context/admin_provider'
const Admin = (props) => {

    return (
        <div>
            <h1>Admin</h1>
            <AdminContextProvider>
                <Questions />
            </AdminContextProvider>
        </div>
    );
};

export default Admin;