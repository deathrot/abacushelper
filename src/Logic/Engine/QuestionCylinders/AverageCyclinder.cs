using Logic.Engine.Constraints;
using System.Linq;

namespace Logic.Engine.QuestionCylinders
{
    public class AverageCylinder : IQuestionCylinder
    {
        private readonly System.Collections.Generic.Dictionary<int, AverageConstraint> levelIds = getConstraints(); 

        
        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math; 

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.Average;

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

        bool constraintPasses(AverageConstraint constraint, Logic.Question.IQuestion q)
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

        internal Question.AvergaQuestion generateQuestion(AverageConstraint constraint)
        {
            bool questionCreated = false;
            Question.AvergaQuestion q = null;
            while(true)
            {
                int totalNumbers = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt(constraint.MinTotalNumbers, 
                                    constraint.MaxTotalNumbers);
            
                q = new Question.AvergaQuestion();
                int totalNumberGenerated = 0;
                while(true)
                {   
                    int number = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt((int)constraint.MinNumber,
                                                                                            (int)constraint.MaxNumber);

                    q.Numbers.Add(new Question.SignedNumber() { SortOrder = totalNumberGenerated, Number = number});
                    totalNumberGenerated++;

                    decimal v = q.Calculate();
                    if ( totalNumberGenerated >= totalNumbers)
                    {
                        if(constraint.AllowDecimal || v == System.Math.Round(v))
                        {
                            questionCreated = true;
                            break;
                        }
                    }
                }

                if (questionCreated)
                    break;
            }

            return q;
        }

        static System.Collections.Generic.Dictionary<int, AverageConstraint> getConstraints()
        {
            System.Collections.Generic.Dictionary<int, AverageConstraint> constraintsByLevelId = 
                new System.Collections.Generic.Dictionary<int, AverageConstraint>();

            constraintsByLevelId.Add(1, new AverageConstraint(1){
                     AllowDecimal = false,
                     MaxNumber = 5,
                     MinNumber = 1,
                     MaxTotalNumbers = 4,
                     MinTotalNumbers = 2
                });

            constraintsByLevelId.Add(2, new AverageConstraint(1){
                     AllowDecimal = false,
                     MaxNumber = 10,
                     MinNumber = 3,
                     MaxTotalNumbers = 6,
                     MinTotalNumbers = 2
                });

            constraintsByLevelId.Add(3, new AverageConstraint(1){
                     AllowDecimal = false,
                     MaxNumber = 12,
                     MinNumber = 3,
                     MaxTotalNumbers = 6,
                     MinTotalNumbers = 2
                });

            constraintsByLevelId.Add(4, new AverageConstraint(1){
                     AllowDecimal = true,
                     MaxNumber = 15,
                     MinNumber = 5,
                     MaxTotalNumbers = 6,
                     MinTotalNumbers = 2
                });

            constraintsByLevelId.Add(5, new AverageConstraint(1){
                     AllowDecimal = true,
                     MaxNumber = 20,
                     MinNumber = 5,
                     MaxTotalNumbers = 5,
                     MinTotalNumbers = 2
                });
                
            return constraintsByLevelId;
        }

        private AverageConstraint getConstraintByLevel(decimal level)                           
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