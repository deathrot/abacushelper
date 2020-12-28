import React, { useState, useEffect, useRef } from 'react';
import Question from './question';
import EntityState from '../common/entity_states';
import { InputText } from 'primereact/inputtext';
import { DataTable } from 'primereact/datatable';
import { createNewQuestion, transformQuestionsForSave, transformQuestionsFromServer } from '../common/extension_methods';
import Button from '@material-ui/core/Button';
import { Column } from 'primereact/column';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';
import './questions.css';
import SplitterLayout from 'react-splitter-layout';
import 'react-splitter-layout/lib/index.css';
import uuid from 'react-uuid';
import _ from 'lodash';
import axios from 'axios';


const Questions = (props) => {
    const [questions, setQuestions] = useState([]);
    const [hasChanges, setHasChanges] = useState(false);

    const [newQuestions, setNewQuestions] = useState([]);
    const [deletedQuestions, setDeletedQuestions] = useState([]);
    const [modifiedQuestions, setModifiedQuestions] = useState([]);

    const dt = useRef(null);
    const [selectedQuestion, setSelectedQuestion] = useState(null);
    const [globalFilter, setGlobalFilter] = useState(null);

    useEffect(() => {
        fetchQuestions();
    }, []);

    const fetchQuestions = async () => {
        const response = await fetch("Questions/get");
        const data = await response.json();

        const transformedData = transformQuestionsFromServer(data);
        
        debugger;

        setHasChanges(false);
        setSelectedQuestion(null);
        setGlobalFilter('');
        setNewQuestions([]);
        setDeletedQuestions([]);
        setModifiedQuestions([]);
        setQuestions(transformedData);
    };

    const deleteQuestion = (e) => {
        e.preventDefault();

        let newArr = newQuestions;
        if (_.remove(newArr, (d) => {
            return d.id == selectedQuestion.id
        }).length > 0)
        {
            setNewQuestions(newArr);
        }

        let delArr = [...deletedQuestions];
        let modArr = [...modifiedQuestions];
        if (selectedQuestion.entityState != EntityState.New) {
            setDeletedQuestions([...delArr, selectedQuestion]);
            
            let removedItems = _.remove(modArr, (d) => {
                return d.id == selectedQuestion.id;
            });

            if (removedItems && removedItems.length > 0){ 
                setModifiedQuestions([...modArr, selectedQuestion]);
            }
        }

        let arr = [...questions];
        _.remove(arr, (d) => {
            return d.id == selectedQuestion.id;
        });
        setQuestions([...arr]);
        setSelectedQuestion(_.head(arr));

        setHasChanges((newArr.length > 0 || delArr.length > 0 || modArr.length > 0));
    }

    const addNewQuestion = (e) => {
        e.preventDefault();

        let newQuestion = createNewQuestion();
        let id = uuid();
        newQuestion.id = id;

        setNewQuestions([...newQuestions, newQuestion]);
        
        setQuestions([...questions, newQuestion]);
        setSelectedQuestion(newQuestion);
        
        setHasChanges(true);
    };


    const questionBodyTemplate = (rowData) => {
        return (
            <div class="question_row">
                {rowData &&
                    <div>
                        <h4>{rowData.name}</h4>
                        <div>L: {rowData.level}, SL: {rowData.subLevel}</div>
                        <div>QT: {rowData.questionType}</div>
                    </div>
                }
            </div>
        );
    }

    const handleUpdate = (newObj) => {
        let obj = newObj;
        let id = obj.id;

        let arr = [...questions];
        let index = _.findIndex(arr, (d) => {
            return d.id == id;
        });

        arr[index].name = newObj.name;
        arr[index].level = newObj.level;
        arr[index].subLevel = newObj.subLevel;
        arr[index].questionType = newObj.questionType;
        arr[index].questionJSON = newObj.questionJSON;
        arr[index].entityState = EntityState.Modified;
        arr[index].severity = newObj.severity;

        setQuestions(arr);

        if(obj.entityState != EntityState.New) {
            if (_.findIndex(modifiedQuestions, (r) => {
                return r.id == newObj.id;
            }) == -1) {
                setModifiedQuestions([...modifiedQuestions, arr[index]]);
            }
        }

        setHasChanges(true);
    }

    const handleSave = async (e) => {
        
        let response = await axios({ method: "post", 
        url: "questions/save",
        data: {
            entitesToUpdate: transformQuestionsForSave(modifiedQuestions),
            entitesToDelete: transformQuestionsForSave(deletedQuestions),
            entitesToInsert: transformQuestionsForSave(newQuestions)
        }});

        e.preventDefault();
    }

    return (
        <div class="admin_question">
            <div class="admin_question_header">Questions</div>
            <div class="container">
                <div class="toolbar">

                    <span class="count">Total {questions.length}</span> 
                    <span class="action_button">
                    <Button variant="contained" color="secondary" onClick={(e) => addNewQuestion(e)}>Add</Button>
                    <span class="spacer" />
                    {selectedQuestion &&
                        <Button variant="contained" color="secondary" onClick={(e) => deleteQuestion(e)}>Delete</Button>
                    }
                    </span>
                    <span class="save_button">
                    {hasChanges &&
                        <Button variant="contained" color="primary" onClick={(e) => handleSave(e)}>Save</Button>
                    }
                    </span>
                </div>
                <div class="content">

                    <SplitterLayout percentage="true" primaryMinSize="15" secondaryInitialSize="85">
                        <div class="grid">
                            <div classname="table-header">
                                <InputText type="search" onInput={(e) => setGlobalFilter(e.target.value)} placeholder="Global Search" />
                            </div>
                            <DataTable ref={dt} value={questions} selection={selectedQuestion} onSelectionChange={(e) => setSelectedQuestion(e.value)}
                                selectionMode="single"
                                globalFilter={globalFilter} emptyMessage="No customers found."
                                dataKey="id" paginator rows={100} rowsPerPageOptions={[5, 10, 25]}
                                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                                currentPageReportTemplate="Showing {first} to {last} of {totalRecords} questions">
                                <Column field="name" showHeader="false" body={questionBodyTemplate}></Column>
                            </DataTable>
                        </div>
                        <div class="question_instance">
                            {selectedQuestion &&
                                <Question key={selectedQuestion.id} question={selectedQuestion} handleUpdate={handleUpdate} />
                            }
                        </div>
                    </SplitterLayout>
                </div>
            </div>
        </div>
    );
};

export default Questions;