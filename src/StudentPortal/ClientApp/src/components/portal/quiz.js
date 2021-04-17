import React, { useState, useContext, useEffect, useRef } from 'react';
import QuestionSubType from '../../common/question_sub_types';
import AddSub from './questions/add_sub';
import Sequentials from './questions/sequentials';
import Multiplication from './questions/multiplication';
import SimpleDivision from './questions/simple_division';
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

    const [quizInProgress, setQuizInProgress] = useState(false);
    const [totalTimeTaken, setTotalTimeTaken] = useState(0);
    const [totalCorrect, setTotalCorrect] = useState(0);
    const [totalQuestion, setTotalQuestion] = useState(0);
    const [totalQuestionAnswered, setTotalQuestionAnswered] = useState(0);

    const [fetchingQuiz, setFetchingQuiz] = useState(false);

    useEffect(() => {
        fetchQuiz();
    }, []);

    const fetchQuiz = async () => {
        setFetchingQuiz(true);

        resetState();

        let rawData = await axios.get('quiz/fetchQuiz', {});
        let quiz = rawData.data;

        setQuizObj(rawData.data);
        if (quiz) {
            let question = getNextQuestion(rawData.data.questions);
            setCurrentQuestion(question);
            setTotalQuestion(rawData.data.questions.length);
            setTotalQuestionAnswered(0);
            setQuizInProgress(true);
        }

        setFetchingQuiz(false);
    }

    const resetState = () => {
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
            setQuizInProgress(false);
        }
    }

    const handleTryAgain = (e) => {
        fetchQuiz();
        e.preventDefault();
    }

    const getProgress = () => {
        return (totalQuestionAnswered / totalQuestion) * 100;
    }

    return (<LoadingOverlay
        active={fetchingQuiz}
        spinner
        text='Fetching quiz...'>
        <h4>Quiz</h4>

        <div class="quiz_container">
            <div class="quiz_row">
                <div class="question_wrapper">
                    {quizInProgress &&
                        <div>
                            {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.AddSub &&
                                <AddSub data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

                            {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.Multiplication &&
                                <Multiplication data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

                            {currentQuestion && currentQuestion.question.questionSubType == QuestionSubType.SimpleDivision &&
                                <SimpleDivision data={currentQuestion.question} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}

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
                    {!quizInProgress &&
                        <div>
                            <Paper square variant="elevation">
                                <div class="total_questions">
                                    <span class="totals">Total Questions: {totalQuestion}</span>
                                </div>
                                <div class="total_questions">
                                    <span class="totals">Total Answered: {totalQuestionAnswered}</span>
                                </div>
                                <div class="total_questions">
                                    <span class="totals">Total Correct: {totalCorrect}</span>
                                </div>
                                <div class="total_questions">
                                    <span class="totals">Total Time: {getTimerDisplay()}</span>
                                </div>
                                <div class="total_action">
                                    <Button color="primary" size="larg" onClick={(e) => handleTryAgain(e)}>Try Again</Button>
                                </div>
                            </Paper>
                        </div>
                    }
                </div>
            </div>
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