import React, { useState } from 'react';
import TextField from '@material-ui/core/TextField';
import _ from 'lodash';
import './style.css';

const QuestionPowerExcercise = (props) => {

  const [problem, setProblem] = useState(props.problem);
  const updateQuestionJSON = props.updateQuestionJSON;

  const handleKeyDown = (e) => {
    if (e.keyCode == 13) {
      
      let n = _.toNumber(e.target.value)
      if(_.isNumber(n)){
        let prob = problem ? {...problem} : {Numbers: []};
        if(!prob.Numbers)
        {
          prob.Numbers = [];
        }

        let maxOrder = _.max(prob.Numbers, (d) => {
          return d.SortOrder;
        }) || 0;

        prob.Numbers.push({Number: n, SortOrder: maxOrder++});
        
        setProblem(prob);

        updateQuestionJSON(prob);
      }
      e.preventDefault();
    }
  }

  return (
    <div class="problem">
      <div class="problem_input">
        <TextField label="Number" onKeyDown={handleKeyDown} />
      </div>
      <div class="problem_output_container">
        {problem && problem.Numbers && problem.Numbers.length == 1 && problem.Numbers[0].Number && 
          <div class="problem_output">Keep adding {problem.Numbers[0].Number} for 1 minute</div>}
      </div>
      <div class="clear">

      </div>
    </div>
  );
};

export default QuestionPowerExcercise;