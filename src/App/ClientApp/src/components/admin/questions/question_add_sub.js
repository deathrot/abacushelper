import React, { useState } from 'react';
import TextField from '@material-ui/core/TextField';
import _ from 'lodash';
import './style.css';

const QuestionAddSub = (props) => {

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

        let nextOrder = _.max(prob.Numbers, (d) => {
          return d.SortOrder;
        });

        let maxOrder = nextOrder ? nextOrder.SortOrder : 0;

        debugger;
        prob.Numbers.push({ Number: n, SortOrder: maxOrder});
        
        setProblem(prob);

        updateQuestionJSON(prob);
      }
      e.target.value = '';
      e.preventDefault();
    }
  }

  return (
    <div class="problem">
      <div class="problem_input">
        <TextField onKeyDown={handleKeyDown} />
      </div>
      <div class="problem_output_container">
        {problem && problem.Numbers && _.map(problem.Numbers, (r) => <div class="problem_output">{r.Number}</div>)}
      </div>
      <div class="clear">

      </div>
    </div>
  );
};

export default QuestionAddSub;