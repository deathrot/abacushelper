import axios from 'axios';
import AvailiabilityEnum from '../common/availability_enum';

const emailAvailiabilityUrl = "createaccount/CheckEmailAvailiability";
const displayNameAvailiabilityUrl = "createaccount/CheckDisplayNameAvaliability";

export const checkEmailAvailiability = async (emailAddressToCheck) => {
    try {
        let result = await axios.post(emailAvailiabilityUrl, {entityToCheck: emailAddressToCheck});
        return result.data;
    }
    catch{ 
        return AvailiabilityEnum.Unknown;
    }
}

export const checkDisplayNameAvailiability = async (displayName) => {
    try {
        let result = await axios.post(displayNameAvailiabilityUrl,{entityToCheck: displayName});
        return result.data;
    }
    catch{ 
        return AvailiabilityEnum.Unknown;
    }
}