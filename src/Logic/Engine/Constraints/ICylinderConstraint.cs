namespace Logic.Engine.Constraints
{
    public interface ICylinderConstraint
    {
        
        Logic.Enums.QuestionType QuestionType { get; }

        Logic.Enums.QuestionSubType QuestionSubType { get; }
        
        int Level { get; }

    }
}