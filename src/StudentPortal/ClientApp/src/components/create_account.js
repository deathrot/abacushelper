import React, {useState} from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import './create_account.css';
import _ from 'lodash';
import './common.css';

const CreateAccount = (props) => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleRegister = (e) => {

        e.preventdefault();
    }

    return (
        <div>
            <h3>Create account</h3>
            <div class="input">
                <TextField label="Name:"
                margin="normal"
                style={{ margin: 8 }}
                InputLabelProps={{
                    shrink: true,
                  }}
                fullWidth
                value={email} onChange={(e) => setEmail(e.target.value)} 
                helperText="John Smith" variant="outlined"></TextField>
            </div>
            <div class="input">
                <TextField label="Email:"
                margin="normal"
                type="email"
                style={{ margin: 8 }}
                InputLabelProps={{
                    shrink: true,
                  }}
                fullWidth
                value={email} onChange={(e) => setEmail(e.target.value)} 
                helperText="john@gmail.com" variant="outlined"></TextField>
            </div>
            <div class="input">
                <TextField label="Password:"
                margin="normal"
                type="password"
                style={{ margin: 8 }}
                InputLabelProps={{
                    shrink: true,
                  }}
                fullWidth
                value={name} onChange={(e) => setName(e.target.value)} 
                helperText="minimum length 6, atleast 1 uppercase" variant="outlined"></TextField>
            </div>
            <div class="input">
                <Button variant="contained" style={{margin: 8}} onClick={(e) => handleRegister(e)} color="primary">Register</Button>
            </div>
        </div>
    );
}

export default CreateAccount;