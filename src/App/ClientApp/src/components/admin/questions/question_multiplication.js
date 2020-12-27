import React, { useState } from 'react';
import TextField from '@material-ui/core/TextField';
import _ from 'lodash';
import './style.css';

const QuestionMultiplication = (props) => {

  const [problem, setProblem] = useState(props.problem);
  const updateQuestionJSON = props.updateQuestionJSON;
  
  const handleStartKeyDown = (e) => {
    if (e.keyCode == 13) {
      
      let n = _.toNumber(e.target.value)
      if(_.isNumber(n)){
        let prob = problem ? {...problem} : {Numbers: []};
        if(!prob.Numbers)
        {
          prob.Numbers = [];
        }

        prob.Numbers[0] = {Number: n, SortOrder: 0};
        
        setProblem(prob);

        updateQuestionJSON(prob);
      }
      e.preventDefault();
    }
  }

  return (
    <div class="problem">
      <div class="problem_input">
        <TextField label="Multiplication Table" onKeyDown={handleStartKeyDown} />
      </div>
      <div class="problem_output_container">
        {problem && problem.Numbers && problem.Numbers.length == 1 && problem.Numbers[0] && 
        _.map(new Array(10), (r, m) => <div class="problem_output">{problem.Numbers[0].Number} * {m+1} = {problem.Numbers[0].Number * (m+1)}</div>)}
      </div>
      <div class="clear">

      </div>
    </div>
  );
};


export default QuestionMultiplication;