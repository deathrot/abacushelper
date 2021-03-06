import _ from 'lodash';

const reSpecialChar = /^[\@\#\$\%\^\&\*]{1,}$/;
const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const reLowerCaseCheckChar = /[*a-z]{1,}/;
const reUpperCaseChar = /[A-Z]{1,}/;
const reNumberCaseChar = /[0-9]{1,}/;
const reDisplayNameChar = /^[a-z0-9]{3,16}$/;


export function isValidEmail(strToTest) {

    if (_.isUndefined(strToTest) || strToTest.length == 0) {
        return false;
    }

    return re.test(String(strToTest).toLowerCase());
}

export function sanitzieString(strToTest) {

    if (_.isUndefined(strToTest)) {
        return strToTest;
    }

    return strToTest.toLowerCase();
}

export function isValidName(strToTest) {

    if (_.isUndefined(strToTest) || strToTest.length > 512 || strToTest.length == 0) {
        return false;
    }

    return true;
}

export function isValidDisplayName(strToTest) {

    if (_.isUndefined(strToTest)) {
        return false;
    }

    return reDisplayNameChar.test(strToTest);
}

export function passwordChecker(strToTest) {

    if (_.isUndefined(strToTest) || strToTest.length > 128 || strToTest.length <= 6) {
        return 0;
    }

    let result = 0;
    if (reSpecialChar.test(strToTest)) {
        result++;
    }

    if (reLowerCaseCheckChar.test(strToTest)) {
        result++;
    }
    
    if (reNumberCaseChar.test(strToTest)) {
        result++;
    }

    if (reUpperCaseChar.test(strToTest)) {
        result++;
    }

    return result;
}