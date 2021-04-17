using System;

namespace Logic.Engine.Constraints
{
    public class AddSubConstraint : ICylinderConstraint
    {

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math;

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.AddSub;

        public int Level { get; }

        public decimal MaxScore { 
            get
            {
                return Math.Max((MaxNumber)*MaxTotalNumbers, Math.Abs(MinNumber)*2*MaxTotalNumbers); 
            } 
        }
        
        public decimal MinScore { 
            get;
        }

        public decimal MaxNumber { get; set; }

        public decimal MinNumber { get; set; }

        public int MaxTotalNumbers { get; set; }

        public bool AllowDecimal { get; set; } = false;

        public bool AllowNegative {get; set;} = false;

        public AddSubConstraint(int level, decimal minScore)
        {
            this.Level = level;
            this.MinScore = minScore;
        }

    }
}