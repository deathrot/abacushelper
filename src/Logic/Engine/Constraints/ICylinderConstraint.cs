namespace Logic.Engine.Constraints
{
    public interface ICylinderConstraint
    {
        Logic.Enums.QuestionType QuestionType { get; }

        Logic.Enums.QuestionSubType QuestionSubType { get; }
        
        decimal MaxLevel { get; }

        decimal MinLevel {get;}

        string ConstraintType {get; set;}

        decimal Version {get; set;}
    }
}