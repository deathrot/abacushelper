import React, { useReducer, useContext, setState, useEffect } from React;
import AdminActions from './admin_actions';

var _ = require("loadash");

const initial_state = {questions: {},
selectedQuestion: null,
hasChanges: false,
newQuestions: [],
deletedQuestions: []};

export const AdminContext = createContext();

export const AdminContextProvider = (props) => {
    
    const [state, dispatch] = useReducer((state, action) => {
        switch(action.type) {
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
                state.questions[q.id] = q;
                state.newQuestions.push(q);
            case AdminActions.DeleteQuestion:
                if (!_.isNull(state.selectedQuestion)){
                    delete state.questions[q.id];
                    state.newQuestions.pop(q);
                    state.deletedQuestions.push(q);
                }
            }

          return state;
        }, initial_state);

    return (<AdminContext.Provider value={state, dispatch}>
        {props.children}
    </AdminContext.Provider>);
}