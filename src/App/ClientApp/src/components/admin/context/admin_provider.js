import React, { createContext, useReducer } from 'react';
import AdminActions from './admin_actions';

import _ from 'lodash';

const initial_state = {
    questionMap: {},
    questions: [],
    selectedQuestion: null,
    hasChanges: false,
    newQuestions: [],
    newId: -1,
    deletedQuestions: [],
    selectedQuestionId: null
};

const stateReducer = (state, action) => {
    switch (action.type) {
        case AdminActions.ClearState:
            return {
                ...state,
                questionMap: {},
                questions: [],
                newId: -1,
                selectedQuestion: null,
                selectedQuestionId: null,
                newQuestions: [],
                deletedQuestions: [],
                hasChanges: false
            };
        case AdminActions.SetState:
            
            let map = {};

            if (action.payload) {
                _.each(action.payload, (x) => {
                    map[x.Id] = x;
                });
            }

            return {
                ...state,
                questionMap: map,
                questions: action.payload,
                newId: -1,
                selectedQuestion: null,
                selectedQuestionId: null,
                newQuestions: [],
                deletedQuestions: [],
                hasChanges: false
            };
        case AdminActions.SelectedQuestion:
            return {
                ...state,
                selectedQuestion: action.payload,
                selectedQuestionId: action.payload.Id
            };
        case AdminActions.AddQuestion:
            let q = action.payload;
            let id = state.newId;
            q['Id'] = state.newId;


            return {
                ...state,
                newId: state.newId - 1,
                questions: [...state.questions, q],
                selectedQuestion: q,
                selectedQuestionId: q.Id,
                newQuestions: [...state.newQuestions, q],
                hasChanges: true
            };
        case AdminActions.DeleteQuestion:
            let questions = _.remove(state.questions, (n) => {
                return n.Id = selectedQuestion.Id
            });

            let newQuestions = _.remove(state.newQuestions, (n) => {
                return n.Id = selectedQuestion.Id
            });

            let selectedQuestion = _.first(questions);

            return {
                ...state,
                questions: questions,
                selectedQuestion: selectedQuestion,
                selectedQuestionId: selectedQuestion.Id,
                newQuestions: newQuestions,
                deletedQuestions: [...state.deletedQuestions, selectedQuestion],
                hasChanges: true
            };
        case AdminActions.NameChanged:
            let question = state.questionMap[state.selectedQuestion.Id];
            question.Name = action.payload;

            state.selectedQuestion.Name = action.payload;
            return {
                ...state,
                selectedQuestion: question
            };
    }

    return state;
}


export const AdminContext = createContext(initial_state);

export const AdminContextProvider = (props) => {

    const [state, dispatch] = useReducer(stateReducer, initial_state);

    return (<AdminContext.Provider value={{ state, dispatch }}>
        {props.children}
    </AdminContext.Provider>);
}