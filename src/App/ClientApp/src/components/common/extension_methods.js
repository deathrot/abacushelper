import EntityState from './entity_states';

export function createNewQuestion() {
    const newQuestion = {
        Id: '',
        Name: "_",        
        Description: '',
        Tags: [],
        QuestionJSON: '',
        Level: 0,
        SubLevel: 0,
        QuestionType: '',
        EntityState: EntityState.New
    };

    return newQuestion;
}