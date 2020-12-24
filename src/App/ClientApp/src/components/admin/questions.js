import React, { Component, useState, useEffect, useContext } from 'react';
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

const Questions = (props) => {
    const { state, dispatch } = useContext(AdminContext);
    //const [selectedQuestion, setSelectedQuestion] = useState(null);
    //const [selectedQuestionKey, setSelectedQuestionKey] = useState(null);

    useEffect(() => {
        fetchQuestions();
    }, []);

    const fetchQuestions = async () => {
        const response = await fetch("Questions");
        const data = await response.json();

        dispatch(state, { type: AdminActions.SetState, payload: data })
    };

    const addNewQuestion = () => {
        const newQuestion = createNewQuestion();

        dispatch(state, { type: AdminActions.AddQuestion, payload: newQuestion })
    };

    return (
        <div class="admin_question">
            <div class="admin_question_header">Questions</div>
            <div class="container">
                <div class="toolbar">
                    <Button variant="contained" color="secondary" click={(e) => addNewQuestion()}>Add</Button>
                </div>
                <div>
                    <div class="grid">
                        <DataTable value={state.questions} selectionKeys={state.selectedQuestionId}
                            onSelect={(e) => dispatch({ type: AdminActions.SelectedQuestion, payload: e.value })}
                            selectionMode="single">
                            <Column field="Id" header="Name" expander></Column>
                            <Column field="RecordName" header="Question"></Column>
                        </DataTable>
                    </div>
                    <div class="question_instance">

                    </div>
                    <div class="spacer">
                        &nbsp;
          </div>
                </div>
            </div>
        </div>
    );
};

export default Questions;