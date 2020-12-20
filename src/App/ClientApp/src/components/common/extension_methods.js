export function createNewQuestion(id) {
    const newQuestion = {
        Id: '',
        DisplayName: "New pod " + id,
        Description: ''
    };

    return newQuestion;
}