import React, { createContext, useReducer } from 'react';
import AdminActions from './admin_actions';

import _ from 'lodash';

const initial_state = {
    questions: {},
    selectedQuestion: null,
    hasChanges: false,
    newQuestions: [],
    newId: -1,
    deletedQuestions: []
};

export const AdminContext = createContext();

export const AdminContextProvider = (props) => {

    const [state, dispatch] = useReducer((state, action) => {
        debugger;
        switch (action.type) {
            case AdminActions.ClearState:
                state.questions = null;
                state.selectedQuestion = null;
            case AdminActions.SetState:
                state.questions = action.payload;
                state.selectedQuestion = null;
            case AdminActions.SelectedQuestion:
                state.selectedQuestion = action.payload;
            case AdminActions.AddQuestion:
                let q = action.payload;
                q['id'] = state.newId;
                state.newId -= 1;
                state.questions[q.id] = q;
                state.newQuestions.push(q);
            case AdminActions.DeleteQuestion:
                if (!_.isNull(state.selectedQuestion)) {
                    delete state.questions[q.id];
                    state.newQuestions.pop(q);
                    state.deletedQuestions.push(q);
                }
        }

        return state;
    }, initial_state);

    return (<AdminContext.Provider value={{
        state: state,
        dispatch: dispatch
    }} >
        {props.children}
    </AdminContext.Provider>);
}