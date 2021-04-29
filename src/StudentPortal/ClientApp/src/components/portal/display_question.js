import React, { Component, useContext } from 'react';
import _ from 'lodash';
import AddSub from './questions/add_sub';
import Sequentials from './questions/sequentials';
import Multiplication from './questions/multiplication';
import SimpleDivision from './questions/simple_division';
import Average from './questions/average';

const DisplayQuestion = (props) => {
    const currentQuestion = props.currentQuestion;
    
    const handleQuestionAnswered = (e) => {
        if (props.handleQuestionAnswered){
            props.onQuestionAnswered(e);
        }
    }

    return (
        <React.Fragment>
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

        </React.Fragment>);
}

export default DisplayQuestion;