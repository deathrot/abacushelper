import EntityState from './entity_states';
import _ from 'lodash';

export function createNewQuestion() {
    const newQuestion = {
        id: '',
        name: "_",        
        description: '',
        tags: [],
        questionJSON: '',
        level: 0,
        subLevel: 0,
        questionType: 'Math',
        questionSubType: '',
        entityState: EntityState.New,
        severity: ''
    };

    return newQuestion;
}

export function transformQuestionsForSave(entities) {
    
    if(entities && entities.length > 0) {
        return _.map(entities, (r)=> {
            return transformedQuestionForSave(r);
        });
    }
    
    return null;
}

export function transformQuestionsFromServer(entities) {
    
    if(entities && entities.length > 0) {
        return _.map(entities, (r)=> {
            return transformedQuestionFromServer(r);
        });
    }
    
    return [];
}

const transformedQuestionFromServer = (d) => {
    let newObj = {...d};
    newObj.questionJSON = JSON.parse(newObj.questionJSON);
    return newObj;
}

const transformedQuestionForSave = (d) => {
    let newObj = {...d};
    newObj.questionJSON = JSON.stringify(newObj.questionJSON);
    newObj.level = _.toNumber(newObj.level);
    newObj.subLevel = _.toNumber(newObj.subLevel);
    return newObj;
}