import React, { createContext, useReducer } from 'react';
import AppContextActions from './app_context_actions';

import _ from 'lodash';

const initial_state = {
    sessionToken: null,
    user_name: null,
    isLoggedIn: false,    
};

const stateReducer = (state, action) => {
    switch (action.type) {
        case AppContextActions.Logout:
            return { ...state, sessionToken: null,
                displayName: null, 
                userId: null, 
                isLoggedIn: false };
        case AppContextActions.Login:
            debugger;
            let session = action.payload.session;
            let s = { ...state, sessionToken: session.sessionToken, 
                    displayName: session.displayName, 
                    email: session.emailAddress, 
                    userId: session.userId, 
                    isLoggedIn: true };
            console.log(s);
            return s;
    }

    return state;
}

export const AppContext = createContext(initial_state);

export const AppContextProvider = (props) => {

    const [state, dispatch] = useReducer(stateReducer, initial_state);

    return (<AppContext.Provider value={{ state, dispatch }}>
        {props.children}
    </AppContext.Provider>);
}