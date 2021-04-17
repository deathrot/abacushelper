namespace Logic.Engine.Constraints
{
    public class SequentialConstraints : ICylinderConstraint
    {

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math;

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.AddSub;

        public int Level { get; }

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

        public SequentialConstraints(int level, decimal minScore)
        {
            this.Level = level;
            this.MinScore = minScore;
        }

    }
}