import React, { useState, useContext, useEffect, useRef } from 'react';
import QuestionSubType from '../../common/question_sub_types';
import AddSub from './questions/add_sub';
import Sequentials from './questions/sequentials';
import Multiplication from './questions/multiplication';
import SimpleDivision from './questions/simple_division';
import Average from './questions/average';
import LoadingOverlay from 'react-loading-overlay';
import { ToastContainer, toast } from 'react-toastify';
import LinearProgress from '@material-ui/core/LinearProgress';
import Button from '@material-ui/core/Button';
import Paper from '@material-ui/core/Paper';
import axios from 'axios';
import _ from 'lodash';
import './quiz.css';

const getNextQuestion = (questions, lastQuestionSortOrder) => {
    if (_.isNumber(lastQuestionSortOrder)) {
        return _.first(_.sortBy(_.filter(questions, (d) => {
            return d.sortOrder > lastQuestionSortOrder
        }), "sortOrder"));
    }

    return _.first(_.sortBy(questions, 'sortOrder'));
}

const Quiz = (props) => {
    const [quizObj, setQuizObj] = useState({});
    const [currentQuestion, setCurrentQuestion] = useState(null);

    const [quizStatus, setQuizStatus] = useState(0);
    const [totalTimeTaken, setTotalTimeTaken] = useState(0);
    const [totalCorrect, setTotalCorrect] = useState(0);
    const [totalQuestion, setTotalQuestion] = useState(0);
    const [totalQuestionAnswered, setTotalQuestionAnswered] = useState(0);

    const testQuestion = {
        "level": 0,
        "questionType": 0,
        "questionSubType": 0,
        "sortOrder": 8,
        "question": {
            "questionSubType": 1,
            "numbers": [
                {
                    "number": 5,
                    "sortOrder": 0
                },
                {
                    "number": 2,
                    "sortOrder": 1
                },
                {
                    "number": 17,
                    "sortOrder": 2
                },
                {
                    "number": 18,
                    "sortOrder": 3
                }
            ]
        }
    };

    const [fetchingQuiz, setFetchingQuiz] = useState(false);

    useEffect(() => {
        fetchQuiz(false);
    }, []);

    const fetchQuiz = async (start) => {
        setFetchingQuiz(true);

        resetState();

        let rawData = await axios.get('quiz/fetchQuiz', {});
        setQuizObj(rawData.data);
        setFetchingQuiz(false);
        if (start) {
            handleStartQuiz();
        }
    }

    const handleStartQuiz = () => {
        if (quizObj) {
            let question = getNextQuestion(quizObj.questions);
            setCurrentQuestion(question);
            setTotalQuestion(quizObj.questions.length);
            setTotalQuestionAnswered(0);
            setQuizStatus(1);
        }
    }

    const resetState = () => {
        setQuizStatus(0);
        setQuizObj({});
        setCurrentQuestion(null);
        setInterval(null);
        setTotalTimeTaken(0);
        setTotalCorrect(0);
        setTotalQuestion(0);
        setTotalQuestionAnswered(0);
    }

    const getTimerDisplay = () => {
        if ((totalTimeTaken / 60) > 0) {
            let min = parseInt((totalTimeTaken / 60));
            let sec = parseInt((totalTimeTaken % 60));
            if (min > 0)
                return `${min} min, ${sec} sec`;
        }

        return `${totalTimeTaken} sec`;
    }

    const handleQuestionAnswered = (e) => {

        let totalA = totalQuestionAnswered + 1;

        setTotalQuestionAnswered(totalA);
        setTotalTimeTaken(totalTimeTaken + e.totalSeconds);

        if (e.result) {
            setTotalCorrect(totalCorrect + 1);
        }
        if (totalA < totalQuestion) {
            let question = getNextQuestion(quizObj.questions, currentQuestion.sortOrder);
            console.log(question);
            setCurrentQuestion(question);
        }
        else {
            setQuizStatus(2);
        }
    }

    const handleTryAgain = (e) => {
        fetchQuiz(true);
        e.preventDefault();
    }

    const getProgress = () => {
        return (totalQuestionAnswered / totalQuestion) * 100;
    }

    return (<LoadingOverlay
        active={fetchingQuiz}
        spinner
        text='Fetching quiz...'>
        <h1 class="display-4">Quiz</h1>

        <div class="container-fluid">
            {quizStatus == 1 &&
                <div>
                    {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.AddSub &&
                        <AddSub data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

                    {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.Multiplication &&
                        <Multiplication data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

                    {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.SimpleDivision &&
                        <SimpleDivision data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

                    {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.Average &&
                        <Average data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

                    {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.Sequentials &&
                        <Sequentials data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

                    <div class="question_list">
                        <p>&nbsp;</p>
                        <div class="timer_progress">
                            <LinearProgress variant="determinate" color="primary" value={getProgress()} />
                        </div>
                    </div>
                </div>
            }
            {quizStatus == 0 &&
                <div class="card">
                    <div class="card-body">
                        <p class="card-text">
                            <div class="card-title"><strong>Questions:</strong></div>
                            <ul>
                                <li>The math quiz is composed of 10 questions</li>
                                <li>The questions range from addition, subtraction, division, multiplication and average</li>
                            </ul>

                            <div class="card-title"><strong>Answering:</strong></div>
                            <ul>
                                <li>Your aim should be to finish the questions as soon as possible</li>
                                <li>Enter your answer in the "Answer" box and hit "Enter" to go to the next question</li>
                                <li>For questions with multiple answer hit "Tab" key to move to the next answer</li>
                                <li>After finishing all the questions the results will appear at the end including total time taken by you</li>
                            </ul>
                        </p>
                        <p class="card-text">Good luck! When ready click the button below to start the quiz</p>
                        <Button color="primary" variant="contained" onClick={(e) => handleStartQuiz(e)}>Start Quiz</Button>
                    </div>
                </div>
            }
                
            {quizStatus == 2 &&
                <div class="card">
                    <div class="card-body">
                        <p class="card-text">
                            <div class="card-title"><strong>Summary:</strong></div>
                            <ul>
                                <li>Total Questions: {totalQuestion}</li>
                                <li>Total Answered: {totalQuestionAnswered}</li>
                                <li>Total Correct: {totalCorrect}</li>
                                <li>Total Time: {getTimerDisplay()}</li>
                            </ul>
                            
                            <Button color="primary" variant="contained" onClick={(e) => handleTryAgain(e)}>Try Again</Button>
                        </p>
                    </div>
                </div>
            }
        </div>
        <ToastContainer
            position="top-center"
            autoClose={5000}
            hideProgressBar
            newestOnTop={false}
            closeOnClick
            rtl={false}
            pauseOnFocusLoss={false}
            draggable={false}
            pauseOnHover />

    </LoadingOverlay>);
}

export default Quiz;