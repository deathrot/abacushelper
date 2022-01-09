import React, { useState, useContext, useEffect, useRef } from 'react';
import Button from '@material-ui/core/Button';
import Slider from '@material-ui/core/Slider';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import { makeStyles } from '@material-ui/core/styles';
import axios from 'axios';
import _ from 'lodash';
import './quiz.css';
import { TextField, Typography } from '@material-ui/core';

const useStyles = makeStyles({
    root: {
        width: 300,
    },
});

const ConfigurePractice = (props) => {
    const { level, handleLevelChange } = useState(1);

    const classes = useStyles();

    return (<div>
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <p class="card-text">
                        <div class="card-title"><strong>Practice:</strong></div>
                        <div className={classes.root}>
                            <Typography id="level" gutterBottom>
                                Level:
                            </Typography>
                            <Slider label="# of questions:"
                                margin="normal"
                                aria-labelledby="normal"
                                valueLabelDisplay="auto"
                                step={1}
                                value={level}
                                onChange={handleLevelChange}
                                marks
                                min={1}
                                max={5}></Slider>
                        </div>
                        <div className={classes.root}>
                            <Typography id="level" gutterBottom>
                                # of questions:
                            </Typography>
                            <Slider label="# of questions:"
                                margin="normal"
                                aria-labelledby="normal"
                                valueLabelDisplay="auto"
                                step={5}
                                value={level}
                                onChange={handleLevelChange}
                                marks
                                min={5}
                                max={40}></Slider>
                        </div>
                        <div>
                            <div class="row">
                                <div class="col-md">
                                    <Typography gutterBottom>
                                        Types:
                                    </Typography>
                                    <div>
                                        <FormControlLabel
                                            control={<Checkbox name="checkedH" />}
                                            label="Add Sub"
                                        />
                                        <FormControlLabel
                                            control={<Checkbox name="checkedH" />}
                                            label="Multiplication"
                                        />
                                    </div>
                                    <div>
                                        <FormControlLabel
                                            control={<Checkbox name="checkedH" />}
                                            label="Division"
                                        />

                                        <FormControlLabel
                                            control={<Checkbox name="checkedH" />}
                                            label="Averages"
                                        />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </p>
                    <Button color="primary" variant="contained">Start Quiz</Button>
                </div>
            </div>
        </div>
    </div>);
}

export default ConfigurePractice;