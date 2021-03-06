import _ from 'lodash';

export function getEllapsedSeconds(dt) {

    if (!dt) {
        return 0;
    }

    const millis = Date.now() - dt;
    return Math.floor(millis / 1000);
}