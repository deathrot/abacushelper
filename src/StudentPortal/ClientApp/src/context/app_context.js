import React, { createContext, useReducer } from 'react';
import AppContextActions from './app_context_actions';

import _ from 'lodash';

const initial_state = {
    sesison_token: null,
    user_name: null,
    isLoggedIn: null
};

const stateReducer = (state, action) => {
    switch (action.type) {
        case AppContextActions.Logout:
            break;
        case AppContextActions.Login:
            break;
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