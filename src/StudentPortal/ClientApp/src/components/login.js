import React, { useState, useContext } from 'react';
import { useHistory } from "react-router-dom";
import TextField from '@material-ui/core/TextField';
import Container from '@material-ui/core/Container';
import { Link } from 'react-router-dom';
import { AppContext } from '../context/app_context';
import AppContextActions from '../context/app_context_actions';
import { useLocation } from 'react-router-dom';
import Button from '@material-ui/core/Button';
import LoadingOverlay from 'react-loading-overlay';
import './common.css';
import './login.css';
import _ from 'lodash';
import axios from 'axios';
import LoginResultEnum from '../common/login_enum';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Login = (props) => {
    const location = useLocation();
    const history = useHistory();
    const { context, dispatch } = useContext(AppContext);
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
                let rawResult = await axios.post("login/initiatelogin", { email: email, password: password });
                let result = rawResult.data;
                if (result.resultType == LoginResultEnum.Success) {

                    toast.success("Login successful...");
                    dispatch({type: AppContextActions.Login, payload: result });
                    history.push({ pathname: '/' });
                }
                else {
                    
                    if (result.resultType == LoginResultEnum.UserDoesNotExists || 
                        result.resultType == LoginResultEnum.PasswordDoesNotMatch) {
                        toast.error("No user could not be found by the email and password supplied...");
                    }
                    if (result.resultType == LoginResultEnum.UnknownError) {
                        toast.error("There was no error procesing your request on the server please try again...");
                    }
                    if (result.resultType == LoginResultEnum.UserIsLockedOut) {
                        toast.error("The user account is locked out because of too many incorrect password attempts...");
                    }
                    if (result.resultType == LoginResultEnum.UserIsDeleted) {
                        toast.error("The user has been marked for deletion and login is not allowed...");
                    }
                }
                
                setLoginProcess(false);
            }
            catch (error) {
                console.error(error);
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
                        <Link to="/register">Create Account</Link> <span class="spacer" /> <Link to="/ForgotPassword">Forgot Password</Link>
                    </div>
                </div>
            </LoadingOverlay>
            <ToastContainer
                position="top-center"
                autoClose={5000}
                hideProgressBar
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss={false}
                draggable={false}
                pauseOnHover />
        </div>
    );
}

export default Login;