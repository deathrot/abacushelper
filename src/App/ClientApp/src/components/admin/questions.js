import React, { Component, useState, useEffect, useContext } from 'react';
import Question from './question';
import { DataTable } from 'primereact/datatable';
import { createNewQuestion } from '../common/extension_methods';
import Button from '@material-ui/core/Button';
import { Column } from 'primereact/column';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';
import './questions.css';
import AdminActions from "./context/admin_actions"
import { makeStyles } from '@material-ui/core/styles';
import { AdminContext } from "./context/admin_provider";
import SplitterLayout from 'react-splitter-layout';
import 'react-splitter-layout/lib/index.css';
import uuid from 'react-uuid'

const Questions = (props) => {
    //const { state, dispatch } = useContext(AdminContext);
    //const selectedQuestion = state && state.selectedQuestion;
    //const [selectedQuestion, setSelectedQuestion] = useState(null);
    //const [selectedQuestionKey, setSelectedQuestionKey] = useState(null);
    const [state, setState] = useState({ questions: [], selectedQuestion: null, selectedQuestionId: null });

    useEffect(() => {
        fetchQuestions();
    }, []);

    const fetchQuestions = async () => {
        const response = await fetch("Questions");
        const data = await response.json();

        setState({...state, questions: data});
        //dispatch({ type: AdminActions.SetState, payload: data })
    };

    const deleteQuestion = (e) => {
        e.preventDefault();

        //dispatch({ type: AdminActions.Delete })
    }

    const addNewQuestion = (e) => {
        e.preventDefault();
        const newQuestion = createNewQuestion();
        newQuestion.Id = uuid();

;        setState({
            ...state,
            questions: [...state.questions, newQuestion],
            selectedQuestion: newQuestion,
            selectedQuestionId: newQuestion.Id
        });
        ///dispatch({ type: AdminActions.AddQuestion, payload: newQuestion })
    };

    const questionBodyTemplate = (rowData) => {
        return (
            <div>
                <h4>{rowData.Name}</h4>
                <div>L: {rowData.Level}, SL: {rowData.SubLevel}</div>
                <div>Tags: {rowData.Tags.length}</div>
            </div>
        );
    }

    return (
        <div class="admin_question">
            <div class="admin_question_header">Questions</div>
            <div class="container">
                <div class="toolbar">
                    <Button variant="contained" color="secondary" onClick={(e) => addNewQuestion(e)}>Add</Button>
                    <span class="spacer" />
                    {state.selectedQuestion && 
                        <Button variant="contained" color="secondary" onClick={(e) => deleteQuestion(e)}>Delete</Button>
                    }
                </div>
                <div class="content">
                    <SplitterLayout percentage="true" primaryMinSize="10" secondaryInitialSize="80">
                        <div class="grid">
                            <DataTable value={state.questions} selectionKeys={state.selectedQuestionId}
                                selection={state.selectedQuestion}
                                onSelectionChange={(e) => setState({...state, selectedQuestion: e.value, selectedQuestionId: e.value.Id})}
                                selectionMode="single">
                                <Column header="Question" body={questionBodyTemplate}></Column>
                            </DataTable>
                        </div>
                        <div class="question_instance">
                            <Question question={state.selectedQuestion} />
                        </div>
                    </SplitterLayout>
                </div>
            </div>
        </div>
    );
};

export default Questions;