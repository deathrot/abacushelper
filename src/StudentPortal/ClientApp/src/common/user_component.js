import React, { useContext } from 'react';
import { useHistory } from "react-router-dom";
import Dropdown from 'react-bootstrap/Dropdown';
import DropDownButton from 'react-bootstrap/DropdownButton';
import { AppContext } from '../context/app_context';
import _ from 'lodash';
import axios from 'axios';
import AppContextActions from '../context/app_context_actions';

const UserComponent = () => {

    const {state, dispatch} = useContext(AppContext);
    const history = useHistory();

    const handleLogout = async (e) => {
        await axios.post("login/logout", { sessionToken: state.sessionToken });
        dispatch({type: AppContextActions.Logout });
        history.push({pathname: '/login', state: {email: state.email}});
    }

    const handleDetails = (e) => {
        history.push({pathname: '/loginDetails', state: {email: state.email}});
    }

    return (
        <DropDownButton title={'Welcome ' + state.displayName}>
        <Dropdown.Item onClick={(e) => handleDetails(e)}>Details</Dropdown.Item>
            <Dropdown.Item onClick={(e) => handleLogout(e)}>Logout</Dropdown.Item>
        </DropDownButton>
    );

}

export default UserComponent;