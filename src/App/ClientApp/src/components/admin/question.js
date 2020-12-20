import React, { Component, useState, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';
import './question.css';
import { makeStyles } from '@material-ui/core/styles';

const Question = (props) => {
  
  return (
      <div>
          {props.question_type == "multiplication" &&
            <div>Multiplication</div>
          }
      </div>
  );
};

export default Question;