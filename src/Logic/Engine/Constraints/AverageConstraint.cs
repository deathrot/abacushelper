using System;

namespace Logic.Engine.Constraints
{
    public class AverageConstraint : ICylinderConstraint
    {

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math;

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.Average;

        public decimal MaxLevel { get; }

        public decimal MinLevel {get;}

        public string ConstraintType {get; set;} = "Avg";

        public decimal Version {get; set;} = 1;

        public decimal MaxScore { 
            get
            {
                return (MaxNumber)*MaxTotalNumbers; 
            } 
        }
        
        public decimal MinScore { 
            get; private set;
        }

        public decimal MaxNumber { get; set; }

        public decimal MinNumber { get; set; }

        public int MaxTotalNumbers { get; set; }

        public int MinTotalNumbers { get; set; }

        public bool AllowDecimal { get; set; } = false;

        public AverageConstraint(decimal maxLevel, decimal minScore)
        {
            this.MaxLevel = maxLevel;
            this.MinScore = minScore;
        }

    }
}