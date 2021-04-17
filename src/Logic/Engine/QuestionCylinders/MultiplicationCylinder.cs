using Logic.Engine.Constraints;
using System.Linq; 

namespace Logic.Engine.QuestionCylinders
{
    public class MultiplicationCylinder : IQuestionCylinder
    {
        private readonly System.Collections.Generic.Dictionary<int, MultiplicationConstraints> levelIds = getConstraints(); 

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math; 

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.Multiplication;

        public Logic.Question.IQuestion Fire(decimal level)
        {
            var constraint = getConstraintByLevel(level);
            
            System.Random random = new System.Random(System.Guid.NewGuid().GetHashCode());

            int totalTries = 0;
            while(true)
            {
                Logic.Question.IQuestion q = generateQuestion(constraint);
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

        bool constraintPasses(MultiplicationConstraints constraint, Logic.Question.IQuestion q)
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

        internal Question.MultiplicationQuestion generateQuestion(MultiplicationConstraints constraint)
        {
            int totalNumbers = 0;
            
            if ( 2 == constraint.MaxTotalNumbers)
            {
                totalNumbers = 2;
            }
            else 
            {
                totalNumbers = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt(2, constraint.MaxTotalNumbers);
            }

            Question.MultiplicationQuestion q = new Question.MultiplicationQuestion();
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

        static System.Collections.Generic.Dictionary<int, MultiplicationConstraints> getConstraints()
        {
            System.Collections.Generic.Dictionary<int, MultiplicationConstraints> constraintsByLevelId = 
                new System.Collections.Generic.Dictionary<int, MultiplicationConstraints>();

            constraintsByLevelId.Add(1, new MultiplicationConstraints(1, 2){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 5,
                     MinNumber = 1,
                     MaxTotalNumbers = 2
                });

            constraintsByLevelId.Add(2, new MultiplicationConstraints(2, constraintsByLevelId[1].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 10,
                     MinNumber = 1,
                     MaxTotalNumbers = 2
                });

            constraintsByLevelId.Add(3, new MultiplicationConstraints(3, constraintsByLevelId[2].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 20,
                     MinNumber = 1,
                     MaxTotalNumbers = 3
                });

            constraintsByLevelId.Add(4, new MultiplicationConstraints(4, constraintsByLevelId[3].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 50,
                     MinNumber = 1,
                     MaxTotalNumbers = 3
                });

            constraintsByLevelId.Add(5, new MultiplicationConstraints(5, constraintsByLevelId[4].MaxScore){
                     AllowDecimal = false,
                     AllowNegative = false,
                     MaxNumber = 100,
                     MinNumber = 1,
                     MaxTotalNumbers = 3
                });
                
            return constraintsByLevelId;
        }

        private MultiplicationConstraints getConstraintByLevel(decimal level)                           
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