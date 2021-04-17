using System;

namespace Logic.Engine.Constraints
{
    public class AverageConstraint : ICylinderConstraint
    {

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math;

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.Average;

        public int Level { get; }

        public decimal MaxScore { 
            get
            {
                return (MaxNumber)*MaxTotalNumbers; 
            } 
        }
        
        public decimal MinScore { 
            get
            {
                return Math.Abs(MinNumber)*MinTotalNumbers; 
            }
        }

        public decimal MaxNumber { get; set; }

        public decimal MinNumber { get; set; }

        public int MaxTotalNumbers { get; set; }

        public int MinTotalNumbers { get; set; }

        public bool AllowDecimal { get; set; } = false;

        public AverageConstraint(int level)
        {
            this.Level = level;
        }

    }
}