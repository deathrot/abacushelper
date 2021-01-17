import React from 'react';
import CheckIcon from '@material-ui/icons/Check';
import ClearIcon from '@material-ui/icons/Clear';
import HelpIcon from '@material-ui/icons/Help';
import { green, red } from '@material-ui/core/colors';
import AvailiabilityEnum from './availability_enum';
import CircularProgress from '@material-ui/core/CircularProgress';


const AvailiabilityComponent = (props) => {

    return (
        <div class="middle">
            {props.checkingAvailability && <CircularProgress size="1.5rem" />}
            {!props.checkingAvailability && props.availabilityFlag == AvailiabilityEnum.Available && <CheckIcon style={{ color: green[500] }} />}
            {!props.checkingAvailability && props.availabilityFlag == AvailiabilityEnum.NotAvailable && <ClearIcon style={{ color: red[500] }} />}
            {!props.checkingAvailability && props.availabilityFlag == AvailiabilityEnum.Unknown && <HelpIcon />}
        </div>
    );
}

export default AvailiabilityComponent;