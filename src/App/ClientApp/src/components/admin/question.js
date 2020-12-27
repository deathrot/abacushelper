import React, { Component, useState, useEffect, useContext } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import QuestionType from '../common/question_types';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';
import './question.css';
import { AdminContext } from "./context/admin_provider";
import { makeStyles } from '@material-ui/core/styles';
import AdminActions from './context/admin_actions';
import _ from 'lodash';

const Question = (props) => {

    const [question, setQuestion] = useState(props.question);

    const handleNameChange = (e) => {
        setQuestion({ ...question, Name: e.target.value });
    }

    const handleLevelChange = (e) => {
        setQuestion({ ...question, Level: e.target.value });
    }

    const handleSubLevelChange = (e) => {
        setQuestion({ ...question, SubLevel: e.target.value });
    }

    const handleQuestionTypeChange = (e) => {
        setQuestion({ ...question, QuestionType: e.target.value });
    }

    return (
        <div>
            {question &&
                <div>
                <div>
                    <TextField id="outlined-basic" variant="outlined"
                        value={question.Name} label="Name"
                        
                        onChange={(e) => handleNameChange(e)} />
                </div>
                <div>
                    <TextField
                      id="level"
                      select
                      label="Level"
                    className="input"
                      value={question.level}
                      onChange={handleLevelChange}
                      helperText="level?">
                        <MenuItem key="1" value="1">1</MenuItem>
                        <MenuItem key="2" value="2">2</MenuItem>
                        <MenuItem key="3" value="3">3</MenuItem>
                        <MenuItem key="4" value="4">4</MenuItem>
                        <MenuItem key="5" value="5">5</MenuItem>
                        <MenuItem key="6" value="6">6</MenuItem>
                        <MenuItem key="7" value="7">7</MenuItem>
                        <MenuItem key="8" value="8">8</MenuItem>
                        <MenuItem key="9" value="9">9</MenuItem>
                        <MenuItem key="10" value="10">10</MenuItem>
                    </TextField>
                </div>
                <div>
                    <TextField
                        id="sub-level"
                        select
                        className="input"
                        label="Sub Level"
                        value={question.SubLevel}
                        onChange={handleSubLevelChange}
                        helperText="sub level?">
                        <MenuItem key="1" value="1">1</MenuItem>
                        <MenuItem key="2" value="2">2</MenuItem>
                        <MenuItem key="3" value="3">3</MenuItem>
                        <MenuItem key="4" value="4">4</MenuItem>
                        <MenuItem key="5" value="5">5</MenuItem>
                        <MenuItem key="6" value="6">6</MenuItem>
                        <MenuItem key="7" value="7">7</MenuItem>
                        <MenuItem key="8" value="8">8</MenuItem>
                        <MenuItem key="9" value="9">9</MenuItem>
                        <MenuItem key="10" value="10">10</MenuItem>
                    </TextField>
                </div>

                <div>
                    <TextField
                        id="question-type"
                        select
                        className="input"
                        label="Question Type"
                        value={question.QuestionType}
                        onChange={handleQuestionTypeChange}
                        helperText="question type?">
                        {
                            _.map(QuestionType, (r) => <MenuItem key={r} value={r}>{r}</MenuItem>)
                        }
                    </TextField>
                </div>

                {question.QuestionType == QuestionType.Multiplication &&
                    <div>Multiplication</div>
                }

            <Button variant="contained" color="secondary" onClick={(e) => props.handleUpdate(question)}>Update</Button>
        </div>
            }
        </div >);
};

export default Question;