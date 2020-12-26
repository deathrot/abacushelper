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
    const selectedQuestion = props.question;

    const handleNameChange = (e) => {
        selectedQuestion.Name = e.target.value;
        e.preventDefault();
    }

    return (
        <div>
            { selectedQuestion &&
                <div>
                    <span>Name:</span> <TextField id="outlined-basic" variant="outlined"
                    value={selectedQuestion.Name}
                        onChange={(e) => handleNameChange(e)} />
                {selectedQuestion.question_type == "multiplication" &&
                        <div>Multiplication</div>
                    }
                </div>
            }
        </div>
    );
};

export default Question;