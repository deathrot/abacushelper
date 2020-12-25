import React, { Component, useState, useEffect, useContext } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';
import './question.css';
import { AdminContext } from "./context/admin_provider";
import { makeStyles } from '@material-ui/core/styles';
import AdminActions from './context/admin_actions';

const Question = (props) => {  
  const {state, dispatch} = useContext(AdminContext);
  const selectedQuestion = state.selectedQuestion;

  const handleNameChange = (e) => {
    e.preventDefault();
    console.log(e.target.value);
    dispatch({type: AdminActions.NameChanged, payLoad: e.target.value});
  }

  return (
      <div>
          Name: <TextField id="outlined-basic" label="Name" variant="outlined" 
                  value={selectedQuestion && selectedQuestion.Name} 
                    onChange={(e) => handleNameChange(e)} />
          Question: {selectedQuestion && selectedQuestion['id']}
          {selectedQuestion && selectedQuestion.question_type == "multiplication" &&
            <div>Multiplication</div>
          }
      </div>
  );
};

export default Question;