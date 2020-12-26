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
import uuid from 'react-uuid';
import _ from 'lodash';


const Questions = (props) => {
    //const { state, dispatch } = useContext(AdminContext);
    //const selectedQuestion = state && state.selectedQuestion;
    //const [selectedQuestion, setSelectedQuestion] = useState(null);
    //const [selectedQuestionKey, setSelectedQuestionKey] = useState(null);
    const [questions, setQuestions] = useState([]);
    const [selectedQuestion, setSelectedQuestion] = useState(null);
    const [selectedQuestionId, setSelectedQuestionId] = useState(-1);

    useEffect(() => {
        fetchQuestions();
    }, []);

    const fetchQuestions = async () => {
        /*const response = await fetch("Questions");
        const data = await response.json();

        setState(data);*/
        //dispatch({ type: AdminActions.SetState, payload: data })

        let arr = [];
        for (var i = 0; i < 10; i++) {
            let a = createNewQuestion();
            a.Id = uuid();

            arr.push(a);
        }

        setQuestions(arr);
    };

    const deleteQuestion = (e) => {
        e.preventDefault();

        //dispatch({ type: AdminActions.Delete })
    }

    const addNewQuestion = (e) => {
        e.preventDefault();
        const newQuestion = createNewQuestion();
        newQuestion.Id = uuid();

        setQuestions([...questions, newQuestion]);
        setSelectedQuestion(newQuestion);
        setSelectedQuestionId(newQuestion.Id);
        ///dispatch({ type: AdminActions.AddQuestion, payload: newQuestion })
    };

    const questionBodyTemplate = (rowData) => {
        return (
            <div>
                {rowData &&
                    <div>
                        <h4>{rowData.Name}</h4>
                        <div>L: {rowData.Level}, SL: {rowData.SubLevel}</div>
                        <div>Tags: {rowData.Tags.length}</div>
                    </div>
                }
            </div>
        );
    }

    const handleUpdate = (newObj) => {
        let obj = newObj;

        let arr = _.filter(questions, (d) => {
            return d.Id != obj.Id;
        });

        setQuestions([...arr, obj]);
        setSelectedQuestion(obj);
        setSelectedQuestionId(obj.Id);
    }

    const handleSelectionChange = (e) => {
        setSelectedQuestion(e);
    }

    return (
        <div class="admin_question">
            <div class="admin_question_header">Questions</div>
            <div class="container">
                <div class="toolbar">
                    <Button variant="contained" color="secondary" onClick={(e) => addNewQuestion(e)}>Add</Button>
                    <span class="spacer" />
                    {selectedQuestion && 
                        <Button variant="contained" color="secondary" onClick={(e) => deleteQuestion(e)}>Delete</Button>
                    }
                </div>
                <div class="content">
                    <SplitterLayout percentage="true" primaryMinSize="10" secondaryInitialSize="80">
                        <div class="grid">
                            {questions.map(d => {
                                return (<div key={d.Id} onClick={(e) => handleSelectionChange(d)}>
                                    {d.Id} -- {d.Name}
                                </div>);
                            })}
                        </div>
                        <div class="question_instance">
                            {selectedQuestion &&
                                <Question key={selectedQuestion.Id} question={selectedQuestion} handleUpdate={handleUpdate} />
                            }
                        </div>
                    </SplitterLayout>
                </div>
            </div>
        </div>
    );
};

export default Questions;