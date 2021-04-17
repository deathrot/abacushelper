using System;

namespace Logic.Engine.Constraints
{
    public class SimpleDivisionConstraint : ICylinderConstraint
    {

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math;

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.SimpleDivision;

        public int Level { get; }

        public bool AllowRemainder { get; set; } = false;

        public decimal MaxNumberOfMulitple { get; set; }
        
        public decimal MinNumberOfMulitple { get; set; }

        public decimal MaxDivisor {get; set;}

        public decimal MinDivisor {get; set;}

        public decimal MaxScore 
        {
            get
            {
                return MaxDivisor*MaxNumberOfMulitple;
            }
        }
        
        public decimal MinScore 
        {
            get
            {
                return MinDivisor*MinNumberOfMulitple;
            }
        }

        public SimpleDivisionConstraint(int level)
        {
            this.Level = level;
        }

    }
}