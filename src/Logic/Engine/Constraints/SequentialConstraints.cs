namespace Logic.Engine.Constraints
{
    public class SequentialConstraints : ICylinderConstraint
    {

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math;

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.AddSub;
        
        public decimal MaxLevel { get; }

        public decimal MinLevel {get;}

        public string ConstraintType {get; set;} = "Seq";

        public decimal Version {get; set;} = 1;

        public decimal MaxScore { 
            get
            {
                return MaxNumber; 
            } 
        }
        
        public decimal MinScore { 
            get;
        }

        public int MinNumber { get; set; }

        public int MaxNumber { get; set; }

        public bool AllowNegative {get; set;}

        public SequentialConstraints(decimal maxLevel, decimal minScore)
        {
            this.MaxLevel = maxLevel;
            this.MinScore = minScore;
        }

    }
}