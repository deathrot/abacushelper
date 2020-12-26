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

    const [question, setQuestion] = useState(props.question);

    const handleNameChange = (e) => {
        setQuestion({ ...question, Name: e.target.value });
    }

    return (
        <div>
            {question &&
                <div>
                    <div>{question.Id}</div>
                    <span>Name:</span> <TextField id="outlined-basic" variant="outlined"
                        value={question.Name}
                        onChange={(e) => handleNameChange(e)} />

                    {question.question_type == "multiplication" &&
                        <div>Multiplication</div>
                    }

                    <Button variant="contained" color="secondary" onClick={(e) => props.handleUpdate(question)}>Update</Button>
                </div>
            }
    </div>);
};

export default Question;