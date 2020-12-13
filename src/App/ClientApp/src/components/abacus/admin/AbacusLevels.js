import React, { Component, useState, useEffect } from 'react';
import AbacusSubLevels from './AbacusSubLevels';
import InputLabel from '@material-ui/core/InputLabel';
import { Dropdown } from 'primereact/dropdown';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import Input from '@material-ui/core/Input';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import AbacusQuestions from './AbacusQuestions';

const AbacusLevels = (props) => {
  const [levelData, setlevelData] = useState(null);
  const [selectedLevel, setSelectedLevel] = useState(null);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    const response = await fetch("abacuslevel");
    const data = await response.json();

    setRows(data);
  }

  const onLevelChange = (e) => {
    setSelectedLevel(e.value);
  }


  const selectedLevelTemplate = (option, props) => {
    if (option) {
      return (
        <div>{option.name}</div>
      );
    }

    return (
      <span>
        {props.placeholder}
      </span>
    );
  }


  const levelOptionTemplate = (option) => {
    return (
      <div>{option.name}</div>
    );
  }

  const loadQuestions = (e) => {
    
  }

  return (
    <div>
      <h1>Abacus Levels</h1>
      <div>
        <Dropdown value={selectedLevel} options={levelData} onChange={onLevelChange} showClear placeholder="Select a level"
          filterBy="name" valueTemplate={selectedLevelTemplate} itemTemplate={levelOptionTemplate}></Dropdown> 
            <Button label="Load" onclick={loadQuestions} />
      </div>
    </div>
  );
};

export default AbacusLevels;