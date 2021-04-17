import React, { Component, useContext, useState, useEffect } from 'react';
import TextField from '@material-ui/core/TextField';
import _ from 'lodash';
import axios from 'axios';
import './question.css';
import CheckIcon from '@material-ui/icons/Check';
import ClearIcon from '@material-ui/icons/Clear';
import { green, red } from '@material-ui/core/colors';
import { getEllapsedSeconds } from '../utility_methods';

const calculateAnswer = (data) => {
    if (!_.isUndefined(data) && !_.isUndefined(data.numbers)){
        return data.numbers[0].number * data.numbers[1].number;
    }

    return null;
}

const Multiplication = ({onQuestionAnswered, data}) => {
    const onQuestionAnsweredEvent = onQuestionAnswered;
    const [answer, setAnswer] = useState();
    const [answerValid, setAnswerValid] = useState(false);
    const [start, setStart] = useState(null);
    const [totalSeconds, setTotalSeconds] = useState(0);
    const correctAnswer = calculateAnswer(data);

    useEffect(() => {
        setTotalSeconds(0);
        setAnswerValid(false);
        setStart(Date.now());
        setAnswer('');
    }, [data]);
    
    const handleAnswerKeyDown = (e) => {
        if(e.key === 'Enter'){

            let totalSecondsTaken = getEllapsedSeconds(start);

            setTotalSeconds(totalSecondsTaken);

            if ( answer == correctAnswer ) {
                onQuestionAnsweredEvent({result: true, totalSeconds: totalSecondsTaken});
                setAnswerValid(true);
            }
            else {
                onQuestionAnsweredEvent({result: false, totalSeconds: totalSecondsTaken});
                setAnswerValid(false);
            }
        }
    }

    return (
        <React.Fragment>
        {data && data.numbers && 
            <div class="container question_container">   
            <div class="row">
                <div class="col-xs sign">
                </div>
                <div class="col-sm number">
                    <span>{data.numbers[0].number}</span> <span>X</span> <span>{data.numbers[1].number}</span>
                </div>
                <div class="col-xxl">
                    &nbsp;
                </div>
            </div>
            <div class="row">
                <div class="col-xs sign">
                    Answer:
                </div>
                <div class="col-sm total number">
                    <TextField type="number" value={answer} size="small" InputProps={{autoFocus: true}} onChange={(e) => setAnswer(e.target.value)} 
                        onKeyPress={(e) => handleAnswerKeyDown(e)} />
                    {answerValid && <CheckIcon style={{ color: green[500] }} /> }
                    {!answerValid && <ClearIcon style={{ color: red[500] }} /> }
                    {answerValid && <div>You took {totalSeconds} sec</div>}
                </div>
                <div class="col-xxl">
                    &nbsp;
                </div>
            </div>
        </div>
        }
        </React.Fragment>
    );
}

export default Multiplication;