import React, { Component, useContext } from 'react';
import './portal.css';
import { AppContext } from '../../context/app_context';
import _ from 'lodash';
import axios from 'axios';
import './portal.css';
import AddSub from './questions/add_sub';
import Multiplication from './questions/multiplication';
import Sequentials from './questions/sequentials';
import Quiz from './quiz';

const Portal = (props) => {
    const { state, dispatch } = useContext(AppContext);
    return (
        <div>
            <h3>Portal</h3>
            <p>
                Student portal for {state.displayName}
            </p>
            <div>
                <Quiz />
            </div>
        </div>
    );
}

export default Portal;