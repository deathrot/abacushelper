import React, { Component, useState, useEffect } from 'react';
import { DataTable } from 'primereact/datatable';
import { createNewQuestion } from '../common/extension_methods';
import Button from '@material-ui/core/Button';
import { Column } from 'primereact/column';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';
import './questions.css';
import { makeStyles } from '@material-ui/core/styles';

const Questions = (props) => {
  const [questions, setQuestions] = useState(null);
  const [nextId, setNextId] = useState(-1);
  const [selectedQuestion, setSelectedQuestion] = useState(null);
  const [selectedQuestionKey, setSelectedQuestionKey] = useState(null);

  useEffect(() => {
    fetchQuestions();
  }, []);

  const fetchQuestions = async () => {
    const response = await fetch("Questions");
    const data = await response.json();

    setQuestions(data);
  };

  const addNewQuestion = () => {
    const newQuestion = createNewQuestion(nextId);
    setQuestions([...newQuestion]);
    setNextId(nextId - 1);
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
            <DataTable value={questions} selectionKeys={selectedQuestionKey}
                  onSelectionChange={(e) => setSelectedQuestionKey(e.value)}
                    onSelect={e => setSelectedQuestion(e.node)} selectionMode="single">
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