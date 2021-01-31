import React, { useState, useContext, useEffect } from 'react';
import QuestionSubType from '../../common/question_sub_types';
import AddSub from './questions/add_sub';
import Sequentials from './questions/sequentials';
import Multiplication from './questions/multiplication';
import LoadingOverlay from 'react-loading-overlay';
import { ToastContainer, toast } from 'react-toastify';
import axios from 'axios';

const getNextQuestion = (questions, lastQuestionSortOrder) => {
    if ( _.isNumber(lastQuestionSortOrder)) {
        return _.first(_.sortBy(_.filter(questions, (d) => {
            return d.sortBy > lastQuestionSortOrder
        }), "sortBy"));
    }

    return _.first(_.sortBy(quiz.questions, 'sortOrder'));
}

const createInitialState = (quiz) => {
    return {
        quiz: quiz,
        currentQuestion: getNextQuestion(quiz.questions)
    };
}

//MaxLevel
//MinLevel
//Questions
// QuestionType
// QuestionSubType
// SortOrder
// Questions
//   Numbers

const Quiz = (props) => {
    const [quizObj, setQuizObj] = useState();
    const [fetchingQuiz, setFetchingQuiz] = useState(false);

    const handleQuestionAnswered = (e) => {
        setQuizObj(...quizObj, currentQuestion: getNextQuestion(quizObj, quizObj.currentQuestion.sortBy));
    }

    useEffect(() => {
        fetchQuiz();
    }, []);

    const fetchQuiz = async () => {
        setFetchingQuiz(true);
        
        let rawData = await axios.post('quiz/fetchQuiz', {});
        setQuizObj(createInitialState(rawData.data));
        
        setFetchingQuiz(false);
    }
    
    return (<LoadingOverlay
        active={fetchingQuiz}
        spinner
        text='Fetching quiz...'>
        <h4>Quiz</h4>
        <div>
            {quizObj.currentQuestion && quizObj.currentQuestion.questionSubType == QuestionSubType.AddSub &&
                <AddSub data={quizObj.currentQuestion.numbers} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}
            
            {quizObj.currentQuestion && quizObj.currentQuestion.questionSubType == QuestionSubType.Multiplication &&
                <Multiplication data={quizObj.currentQuestion.numbers} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}    
            
                {quizObj.currentQuestion && quizObj.currentQuestion.questionSubType == QuestionSubType.Sequentials &&
                <Sequentials data={quizObj.currentQuestion.numbers} onQuestionAnswered={(e) => handleQuestionAnswered(e)} />}    
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