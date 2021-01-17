import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import PasswordStrengthBar from 'react-password-strength-bar';
import LoadingOverlay from 'react-loading-overlay';
import { ToastContainer, toast } from 'react-toastify';
import AvailiabilityComponent from '../common/availiability_component';
import AvailiabilityEnum from '../common/availability_enum';
import {checkEmailAvailiability, checkDisplayNameAvailiability} from './avaliability_checker';
import { useHistory } from "react-router-dom";
import 'react-toastify/dist/ReactToastify.css';
import './create_account.css';
import _ from 'lodash';
import './common.css';
import axios from 'axios';
import { isValidName, isValidDisplayName, isValidEmail, passwordChecker, sanitzieString } from '../common/validation_utils';

const CreateAccount = (props) => {
    const history = useHistory();
    const [name, setName] = useState('');
    const [nameError, setNameError] = useState(false);

    const [email, setEmail] = useState('');
    const [emailError, setEmailError] = useState(false);
    const [lastEmailCheckedForAvailability, setLastEmailCheckedForAvailability] = useState('');
    const [checkingEmailAvailability, setCheckingEmailAvailability] = useState(false);
    const [emailAvailability, setEmailAvailability] = useState(0);

    const [password, setPassword] = useState('');
    const [passwordError, setPasswordError] = useState(false);
    const [repassword, setRePassword] = useState('');
    const [repasswordError, setRePasswordError] = useState(false);

    const [displayName, setDisplayName] = useState('');
    const [displayNameAvailability, setDisplayNameAvailability] = useState(0);
    const [lastDisplayNameCheckedForAvailability, setLastDisplayNameCheckedForAvailability] = useState('');
    const [checkingDisplayNameAvailability, setCheckingDisplayNameAvailability] = useState(false);
    const [displayNameError, setDisplayNameError] = useState(false);

    const [creatingAccount, setCreatingAccount] = useState(false);

    const handleNameChange = (value) => {
        setName(value);
        setNameError(!isValidName(value));
    }

    const handleDisplayNameChange = (value) => {
        let v = sanitzieString(value);
        setDisplayName(v);
        setDisplayNameError(!isValidDisplayName(v));
    }

    const handlePasswordChange = (value) => {
        setPassword(value);
        let p = passwordChecker(value);
        setPasswordError(!(p > 0));
    }

    const handleRePasswordChange = (value) => {
        setRePassword(value);
        if (value != password) {
            setRePasswordError(true);
        }
        else {
            setRePasswordError(false);
        }
    }

    const handleEmailChange = (value) => {
        setEmail(value);
        setEmailError(!isValidEmail(value));
    }

    const handleChangeScore = (e) => {
        setPasswordError(e < 1);
    }

    const handleRegister = (e) => {
        e.preventDefault();

        
        debugger;
        setCreatingAccount(true);
        navigateToLogin();

        /*if (repasswordError || passwordError || displayNameError || emailError || nameError || _.isEmpty(repassword)) {
            return;
        }

        setCreatingAccount(true);
        sendCreateAccountRequest();*/
    }

    const sendCreateAccountRequest = async () => {
        axios.post("createaccount/createaccount",
            getAccountPayLoad())
        .then((result) => {
            let data = result.data;
            
            setDisplayNameError(data.displayNameError);
            setEmailError(data.emailError);
            setPasswordError(data.passwordError);
            setNameError(data.nameError);
            
            setDisplayNameAvailability(data.displayNameAvaliability);
            setEmailAvailability(data.emailAvailiability);
            
            if (data.validationSuccess) {
                toast.success('Student profile created...navigating to the login page');
                navigateToLogin();
            }
            else {
                toast.error('Student profile could be created because of errors');
                setCreatingAccount(false);
            }
        })
        .catch((error) => {
            toast.error('Unexpected error on server...please try again');
            setCreatingAccount(false);
        });
    }

    const navigateToLogin = () => {
        const timerId = setTimeout(() => {
            history.push({ pathname: '/Login', state: {email: email} });
            clearTimeout(timerId);
        }, 2000);
    }

    const getAccountPayLoad = () => {
        return {
            studentEmail: email,
            studentName: name,
            studentDisplayName: displayName,
            password: password
        };
    }

    const handleEmailFocusOut = (value) => {
        if (email.length > 0 && !emailError) {
            setCheckingEmailAvailability(true);
            checkEmailAvaliability();
        }
        else {
            setEmailAvailability(0);
        }
    }

    const handleDisplayNameFocusOut = (e) => {
        if (displayName.length > 0 && !displayNameError) {
            setCheckingDisplayNameAvailability(true);
            checkDisplayNameAvaliability();
        }
        else {
            setDisplayNameAvailability(0);
        }
    }

    const checkEmailAvaliability = async () => {
        if (email.trim() == lastEmailCheckedForAvailability) {
            setCheckingEmailAvailability(false);
            return;
        }

        if (email.length > 0 && !emailError) {
            setLastEmailCheckedForAvailability(email);
            let value = await checkEmailAvailiability(email);
            setEmailAvailability(value);
        }
        else {
            setEmailAvailability(AvailiabilityEnum.None);
        }

        setCheckingEmailAvailability(false);
    }

    const checkDisplayNameAvaliability = async () => {
        if (displayName.trim() == lastDisplayNameCheckedForAvailability) {
            setCheckingDisplayNameAvailability(false);
            return;
        }

        if (displayName.length > 0 && !displayNameError) {
            setLastDisplayNameCheckedForAvailability(displayName);
            let value = await checkDisplayNameAvailiability(displayName);

            setDisplayNameAvailability(value);
        }
        else {
            setDisplayNameAvailability(AvailiabilityEnum.Unknown);
        }

        setCheckingDisplayNameAvailability(false);
    }

    return (
        <LoadingOverlay
            active={creatingAccount}
            spinner
            text='Saving...'>
            <div>
                <h3>Create account</h3>
                <div class="input">
                    <TextField label="Name:"
                        margin="normal"
                        style={{ margin: 8 }}
                        InputLabelProps={{
                            shrink: true,
                        }}
                        error={nameError ? true : false}
                        fullWidth
                        value={name} onChange={(e) => handleNameChange(e.target.value)}
                        helperText="John Smith" variant="outlined"></TextField>
                </div>
                <div class="input">
                    <TextField label="Display Name:"
                        margin="normal"
                        style={{ margin: 8 }}
                        InputLabelProps={{
                            shrink: true,
                        }}
                        onBlur={(e) => handleDisplayNameFocusOut(e)}
                        error={displayNameError ? true : false}
                        value={displayName} onChange={(e) => handleDisplayNameChange(e.target.value)}
                        helperText="friendly display name that others can see...should not contain spaces or special characters" variant="outlined"></TextField>
                        <AvailiabilityComponent availabilityFlag={displayNameAvailability} checkingAvailability={checkingDisplayNameAvailability} />
                </div>
                <div class="input">
                    <TextField label="Email:"
                        margin="normal"
                        type="email"
                        style={{ margin: 8 }}
                        InputLabelProps={{
                            shrink: true,
                        }}
                        onBlur={(e) => handleEmailFocusOut(e)}
                        error={emailError ? true : false}
                        value={email} onChange={(e) => handleEmailChange(e.target.value)}
                        width="75%"
                        helperText="enter your email address...this will be your login ad as well" variant="outlined"></TextField>
                        <AvailiabilityComponent availabilityFlag={emailAvailability} checkingAvailability={checkingEmailAvailability} />
                </div>
                <div class="input">
                    <TextField label="Password:"
                        margin="normal"
                        type="password"
                        style={{ margin: 8 }}
                        InputLabelProps={{
                            shrink: true,
                        }}
                        error={passwordError ? true : false}
                        fullWidth
                        value={password} onChange={(e) => handlePasswordChange(e.target.value)}
                        helperText="minimum length 6, atleast 1 uppercase" variant="outlined"></TextField>
                    <div>
                        <PasswordStrengthBar password={password} minLength="6" onChangeScore={(e) => handleChangeScore(e)} />
                    </div>
                </div>
                <div class="input">
                    <TextField label="Re-Enter Password:"
                        margin="normal"
                        type="password"
                        style={{ margin: 8 }}
                        InputLabelProps={{
                            shrink: true,
                        }}
                        error={repasswordError ? true : false}
                        fullWidth
                        value={repassword} onChange={(e) => handleRePasswordChange(e.target.value)}
                        helperText="confirm the password entered in the field above" variant="outlined"></TextField>
                </div>
                <div class="input">
                    <Button variant="contained" style={{ margin: 8 }} onClick={(e) => handleRegister(e)} color="primary">Register</Button>
                </div>
            </div>
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
        </LoadingOverlay>
    );
}

export default CreateAccount;