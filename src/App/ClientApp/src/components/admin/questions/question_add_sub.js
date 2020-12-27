import React, { useState } from 'react';
import TextField from '@material-ui/core/TextField';
import Box from '@material-ui/core/Box';
import _ from 'lodash';

const QuestionAddSub = (props) => {

  const [question, setQuestion] = useState(props.question);
  const updateQuestionJSON = props.updateQuestionJSON;

  const handleKeyDown = (e) => {
    if (e.keyCode == 13) {

      e.target.value = '';
      e.preventDefault();
      // put the login here
    }


  }

  return (
    <div>
      <div class="question_input">
        <TextField onKeyDown={handleKeyDown} />
      </div>
      <div class="question_output">
      </div>
      <div class="clear">

      </div>
    </div>
  );
};

export default QuestionAddSub;