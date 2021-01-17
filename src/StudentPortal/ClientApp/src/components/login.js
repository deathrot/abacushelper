import React, { useState, useContext } from 'react';
import TextField from '@material-ui/core/TextField';
import Container from '@material-ui/core/Container';
import { Link } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import Button from '@material-ui/core/Button';
import LoadingOverlay from 'react-loading-overlay';
import './common.css';
import './login.css';
import _ from 'lodash';
import axios from 'axios';

const Login = (props) => {
    const location = useLocation();
    //<div>{location && location.state && <div>{location.state.email}</div>}</div>

    const [email, setEmail] = useState(location && location.state ? location.state.email : '');
    const [emailError, setEmailError] = useState(false);

    const [loginProcess, setLoginProcess] =useState(false);

    const [password, setPassword] = useState('');
    const [passwordError, setPasswordError] = useState(false);

    const handleLogin = (e) => {
        e.preventDefault();

        if (_.isEmpty(email)) {
            setEmailError(true);
        }
        else {
            setEmailError(false);
        }

        if (_.isEmpty(password)) {
            setPasswordError(true);
        }
        else {
            setPasswordError(false);
        }

        processLogin();
    }

    const processLogin = async () => {
        if (!passwordError && !emailError) {
            setLoginProcess(true);
            try {
                let result = await axios.post("login/initiatelogin", { email: email, password: password });

                console.log(result.data);
                setLoginProcess(false);
            }
            catch (error) {
                setLoginProcess(false);
            }
        }
    }

    const handleEmailChange = (value) => {
        setEmail(value);
    }

    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    return (
        <div class="panel panel-default login">
            <div>
                <h3>Login</h3>
            </div>
            <LoadingOverlay
            active={loginProcess}
            spinner
            text='Logging you in...'>
                <div class="login-body">
                    <div class="input center">
                        <TextField label="Email:"
                            margin="normal"
                            style={{ margin: 8 }}
                            InputLabelProps={{
                                shrink: true,
                            }}
                            id="email"
                            fullWidth
                            type="email"
                            size="small"
                            error={emailError ? true : false}
                            value={email} onChange={(e) => handleEmailChange(e.target.value)}
                            helperText="login email address..." variant="outlined"></TextField>
                    </div>

                    <div class="input center">
                        <TextField label="Password:"
                            margin="normal"
                            style={{ margin: 8 }}
                            InputLabelProps={{
                                shrink: true,
                            }}
                            id="password"
                            fullWidth
                            type="password"
                            size="small"
                            error={passwordError ? true : false}
                            value={password} onChange={(e) => handlePasswordChange(e.target.value)}
                            helperText="enter your password..." variant="outlined"></TextField>
                    </div>
                    <div class="input center">
                        <Button variant="contained" style={{ margin: 8 }} onClick={(e) => handleLogin(e)} color="primary">Login</Button>
                    </div>
                    <div class="login_links">
                        <Link to="/CreateAccount">Create Account</Link> <span class="spacer" /> <Link to="/ForgotPassword">Forgot Password</Link>
                    </div>
                </div>
            </LoadingOverlay>
        </div>
    );
}

export default Login;