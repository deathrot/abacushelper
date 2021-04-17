using Logic.Engine.Constraints;

namespace Logic.Engine.QuestionCylinders
{
    interface IQuestionCylinder
    {
        
        Logic.Enums.QuestionType QuestionType { get; }

        Logic.Enums.QuestionSubType QuestionSubType { get; }

        Logic.Question.IQuestion Fire(decimal level);

    }
}