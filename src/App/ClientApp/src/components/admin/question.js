import React, { Component, useState, useEffect, useContext } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import QuestionSubType from '../common/question_sub_types';
import Severity from '../common/severity';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';
import './question.css';
import { AdminContext } from "./context/admin_provider";
import { makeStyles } from '@material-ui/core/styles';
import AdminActions from './context/admin_actions';
import _ from 'lodash';
import QuestionMultiplication from "./questions/question_multiplication"
import QuestionMental from "./questions/question_mental"
import QuestionPowerExcercise from "./questions/question_powerex"
import QuestionSequential from "./questions/question_sequential"
import QuestionBeeds from "./questions/question_beeds"
import QuestionAddSub from "./questions/question_add_sub"

const Question = (props) => {

    const [question, setQuestion] = useState(props.question);

    const handleNameChange = (e) => {
        let q = { ...question, name: e.target.value };
        setQuestion(q);
        syncChanges(q);
    }

    const handleLevelChange = (e) => {
        let q = { ...question, level: e.target.value };
        setQuestion(q);
        syncChanges(q);
    }

    const handleSubLevelChange = (e) => {
        let q = { ...question, subLevel: e.target.value };
        setQuestion(q);
        syncChanges(q);
    }

    const handleQuestionSubTypeChange = (e) => {
        let q = { ...question, questionSubType: e.target.value, questionJSON: {} };
        setQuestion(q);
        syncChanges(q);
    }

    const handleUpdateQuestionJSON = (e) => {
        let q = { ...question, questionJSON: e };
        setQuestion(q);
        syncChanges(q);
    }

    const handleSeverityChange = (e) => {
        let q = { ...question, severity: e.target.value };
        setQuestion(q);
        syncChanges(q);
    }

    const syncChanges = (q) => {
        props.handleUpdate(q)
    }

    return (
        <table>
            <tr>
                <td class="problem_field">
                    <h4>Details</h4>
                    <div>
                        {question &&
                            <div>
                                {false && <div class="input-wrapper">
                                    <TextField id="outlined-basic" variant="outlined"
                                        value={question.name} label="Name" className="input"
                                        onChange={(e) => handleNameChange(e)} />
                                </div>}
                                <div class="input-wrapper">
                                    <TextField
                                        id="level"
                                        select
                                        label="Level"
                                        className="input"
                                        value={question.level || ''}
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
                                <div class="input-wrapper">
                                    <TextField
                                        id="sub-level"
                                        select
                                        className="input"
                                        label="Sub Level"
                                        value={question.subLevel || ''}
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
                                <div class="input-wrapper">
                                    <TextField
                                        id="question-severity"
                                        select
                                        className="input"
                                        label="Severity"
                                        value={question.severity || ''}
                                        onChange={handleSeverityChange}
                                        helperText="severity?">
                                        {
                                            _.map(Severity, (r) => <MenuItem key={r} value={r}>{r}</MenuItem>)
                                        }
                                    </TextField>
                                </div>
                                
                                <div class="input-wrapper">
                                    <TextField
                                        id="question-sub-type"
                                        select
                                        className="input"
                                        label="Question Sub Type"
                                        value={question.questionSubType || ''}
                                        onChange={handleQuestionSubTypeChange}
                                        helperText="question sub type?">
                                        {
                                            _.map(QuestionSubType, (r) => <MenuItem key={r} value={r}>{r}</MenuItem>)
                                        }
                                    </TextField>
                                </div>
                            </div>
                        }
                    </div >
                </td>
                <td class="spacer">
                    
                </td>
                <td class="problem_def">
                    <h4>Problem</h4>
                    {question.questionSubType == QuestionSubType.Multiplication &&
                        <QuestionMultiplication id={question.Id}   updateQuestionJSON={handleUpdateQuestionJSON} problem={question.questionJSON}  />
                    }

                    {question.questionSubType == QuestionSubType.PowerExcercise &&
                        <QuestionPowerExcercise id={question.Id}   updateQuestionJSON={handleUpdateQuestionJSON} problem={question.questionJSON}  />
                    }

                    {question.questionSubType == QuestionSubType.Sequentials &&
                        <QuestionSequential id={question.Id}   updateQuestionJSON={handleUpdateQuestionJSON} problem={question.questionJSON}  />
                    }

                    {question.questionSubType == QuestionSubType.AddSub &&
                        <QuestionAddSub id={question.Id}  updateQuestionJSON={handleUpdateQuestionJSON} problem={question.questionJSON}  />
                    }
                </td>
                <td class="filler">

                </td>
            </tr>
        </table>
    );
};

export default Question;