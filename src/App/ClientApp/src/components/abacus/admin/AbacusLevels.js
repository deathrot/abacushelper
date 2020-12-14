import React, { Component, useState, useEffect } from 'react';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';
import 'primeicons/primeicons.css';
import 'primereact/resources/themes/fluent-light/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';

const AbacusLevels = (props) => {
    const [levelData, setlevelData] = useState(null);
    const [selectedLevel, setSelectedLevel] = useState(null);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        const response = await fetch("abacuslevel");
        const data = await response.json();

        setlevelData(data);
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
            <h2>Abacus Levels</h2>
            <div>
                <Dropdown value={selectedLevel} options={levelData} onChange={onLevelChange} optionLabel="Levels" filter filterBy="name"
                    showClear placeholder="Select a level" filterBy="name" valueTemplate={selectedLevelTemplate}
                        itemTemplate={levelOptionTemplate}></Dropdown>
                &nbsp;&nbsp;<Button label="Load Questions" onclick={loadQuestions} />
            </div>
        </div>
    );
};

export default AbacusLevels;