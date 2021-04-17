using Logic.Engine.Constraints;
using System.Linq;

namespace Logic.Engine.QuestionCylinders
{
    public class AddSubQuestionCylinder : IQuestionCylinder
    {
        private readonly System.Collections.Generic.Dictionary<int, AddSubConstraint> levelIds = getConstraints(); 

        
        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math; 

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.AddSub;

        public Logic.Question.IQuestion Fire(decimal level)
        {
            var constraint = getConstraintByLevel(level);
            
            System.Random random = new System.Random(System.Guid.NewGuid().GetHashCode());

            int totalTries = 0;
            while(true)
            {
                Logic.Question.IQuestion q = generateNumber(constraint);
                if ( q.IsValid() && constraintPasses(constraint, q))
                {
                    return q;
                }

                totalTries++;

                if ( totalTries > Constants.Constants.MAX_CYLINDER_TRIES)
                {
                    throw new System.Exception($"Could create a question within a constraint for level {level}");
                }
            }
            
        }

        bool constraintPasses(AddSubConstraint constraint, Logic.Question.IQuestion q)
        {
           decimal score = getScore(q);

           return score <= constraint.MaxScore || score >= constraint.MinScore; 
        }

        decimal getScore(Logic.Question.IQuestion q)
        {
          decimal totalScore = 0;
          foreach(var n in q.Numbers)
          {
              totalScore += System.Math.Abs(n.Number) * (n.Number < 0 ? 1 : 2);
          } 
          return totalScore;
        }

        internal Question.AddSubQuestion generateNumber(AddSubConstraint constraint)
        {
            int totalNumbers = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt(2, constraint.MaxTotalNumbers);

            Question.AddSubQuestion q = new Question.AddSubQuestion();
            int totalNumberGenerated = 0;
            while(true)
            {   
                int number = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt((int)constraint.MinNumber,
                                                                                        (int)constraint.MaxNumber);

                q.Numbers.Add(new Question.SignedNumber() { SortOrder = totalNumberGenerated, Number = number});
                totalNumberGenerated++;

                if ( totalNumberGenerated >= totalNumbers)
                 break;
            }

            return q;
        }

        static System.Collections.Generic.Dictionary<int, AddSubConstraint> getConstraints()
        {
            System.Collections.Generic.Dictionary<int, AddSubConstraint> constraintsByLevelId = 
                new System.Collections.Generic.Dictionary<int, AddSubConstraint>();

            constraintsByLevelId.Add(1, new AddSubConstraint(1, 10){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 10,
                     MinNumber = 1,
                     MaxTotalNumbers = 3
                });

            constraintsByLevelId.Add(2, new AddSubConstraint(2, constraintsByLevelId[1].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 19,
                     MinNumber = 1,
                     MaxTotalNumbers = 3
                });

            constraintsByLevelId.Add(3, new AddSubConstraint(3, constraintsByLevelId[2].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 19,
                     MinNumber = -10,
                     MaxTotalNumbers = 4
                });

            constraintsByLevelId.Add(4, new AddSubConstraint(4, constraintsByLevelId[3].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 100,
                     MinNumber = -50,
                     MaxTotalNumbers = 5
                });

            constraintsByLevelId.Add(5, new AddSubConstraint(5, constraintsByLevelId[4].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 500,
                     MinNumber = -500,
                     MaxTotalNumbers = 6
                });
                
            return constraintsByLevelId;
        }

        private AddSubConstraint getConstraintByLevel(decimal level)                           
        {
            if ( level <= 1 )
            {
                return levelIds[1];
            }

            if ( level > 1 && level <= 2 )
            {
                return levelIds[2];
            }
            if ( level > 2 && level <= 3 )
            {
                return levelIds[3];
            }
            if ( level > 3 && level <= 4 )
            {
                return levelIds[4];
            }
            
            return levelIds[5];
        }

    }
}