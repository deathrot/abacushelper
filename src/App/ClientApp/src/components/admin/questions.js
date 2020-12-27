import React, { useState, useEffect, useRef } from 'react';
import Question from './question';
import { InputText } from 'primereact/inputtext';
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
    const dt = useRef(null);
    const [selectedQuestion, setSelectedQuestion] = useState(null);
    const [globalFilter, setGlobalFilter] = useState(null);
    
    useEffect(() => {
        fetchQuestions();
    }, []);

    const fetchQuestions = async () => {
        /*const response = await fetch("Questions");
        const data = await response.json();

        setState(data);*/
        //dispatch({ type: AdminActions.SetState, payload: data })

        let arr = [];

        for (var i = 0; i < 10000; i++) {
            let a = createNewQuestion();
            a.Id = uuid();

            arr.push(a);
        }
        setQuestions(arr);
    };

    const deleteQuestion = (e) => {
        e.preventDefault();

        let arr = _.remove(questions, (d) => {
            return d.Id == selectedQuestion.Id;
        });

        setQuestions(arr);
        setSelectedQuestion(_.head(arr));

        //dispatch({ type: AdminActions.Delete })
    }

    const addNewQuestion = (e) => {
        e.preventDefault();

        let newQuestion = createNewQuestion();
        let id = uuid();
        newQuestion.Id = id;

        setQuestions([...questions, newQuestion]);
        setSelectedQuestion(newQuestion);
    };


    const questionBodyTemplate = (rowData) => {
        return (
            <div class="question_row">
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
        let id = obj.Id;

        let arr = [...questions];
        let index = _.findIndex(arr, (d) => {
            return d.Id == id;
        });

        arr[index].Name = newObj.Name;
        arr[index].Level = newObj.Level;
        arr[index].SubLevel = newObj.SubLevel;
        arr[index].QuestionType = newObj.QuestionType;

        setQuestions(arr);
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
                    <div>Total {questions.length}</div>
                </div>
                <div class="content">

                    <SplitterLayout percentage="true" primaryMinSize="15" secondaryInitialSize="85">
                        <div class="grid">
                            <div className="table-header">
                                <InputText type="search" onInput={(e) => setGlobalFilter(e.target.value)} placeholder="Global Search" />
                            </div>
                            <DataTable ref={dt} value={questions} selection={selectedQuestion} onSelectionChange={(e) => setSelectedQuestion(e.value)}
                                selectionMode="single"
                                globalFilter={globalFilter} emptyMessage="No customers found."
                                dataKey="Id" paginator rows={100} rowsPerPageOptions={[5, 10, 25]}
                                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                                currentPageReportTemplate="Showing {first} to {last} of {totalRecords} questions">
                                <Column field="Name" showHeader="false" body={questionBodyTemplate}></Column>
                            </DataTable>
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