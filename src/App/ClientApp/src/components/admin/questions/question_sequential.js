import React, { useState } from 'react';
import TextField from '@material-ui/core/TextField';
import _ from 'lodash';
import './style.css';

const QuestionSequential = (props) => {

  const [problem, setProblem] = useState(props.problem);
  const updateQuestionJSON = props.updateQuestionJSON;

  const handleStartKeyDown = (e) => {
    if (e.keyCode == 13) {

      let n = _.toNumber(e.target.value)
      if (_.isNumber(n)) {
        let prob = problem ? { ...problem } : { Numbers: [] };
        if (!prob.Numbers) {
          prob.Numbers = [];
        }

        prob.Numbers[0] = { Number: n, SortOrder: 0 };

        setProblem(prob);

        updateQuestionJSON(prob);
      }
      e.preventDefault();
    }
  }

  const handleEndKeyDown = (e) => {
    if (e.keyCode == 13) {

      let n = _.toNumber(e.target.value)
      if (_.isNumber(n)) {
        let prob = problem ? { ...problem } : { Numbers: [] };
        if (!prob.Numbers) {
          prob.Numbers = [];
        }

        prob.Numbers[1] = { Number: n, SortOrder: 0 };

        setProblem(prob);

        updateQuestionJSON(prob);
      }
      e.preventDefault();
    }
  }

  return (
    <div class="problem">
      <div class="problem_input">
        <TextField label="Start" onKeyDown={handleStartKeyDown} /> <span> - </span> <TextField label="End" onKeyDown={handleEndKeyDown} />
      </div>
      <div class="problem_output_container">
        {problem && problem.Numbers && problem.Numbers.length == 2 && problem.Numbers[0] && problem.Numbers[1] && problem.Numbers[0].Number && problem.Numbers[1].Number && 
        <div class="problem_output">Add the numbers in the order</div>}
        {problem && problem.Numbers && problem.Numbers.length == 2 && problem.Numbers[0] && problem.Numbers[1] && problem.Numbers[0].Number && problem.Numbers[1].Number
          && _.map(new Array(problem.Numbers[1].Number - problem.Numbers[0].Number), (r, m) => <span class="problem_output">{problem.Numbers[0].Number + m}, </span>)}
        {problem && problem.Numbers && problem.Numbers.length == 2 && problem.Numbers[0] && problem.Numbers[1] && problem.Numbers[0].Number && problem.Numbers[1].Number
          && <span class="problem_output">{problem.Numbers[1].Number}</span>}
      </div>
      <div class="clear">

      </div>
    </div>
  );
};

export default QuestionSequential;