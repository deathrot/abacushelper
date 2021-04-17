using Logic.Engine.Constraints;
using System.Linq; 

namespace Logic.Engine.QuestionCylinders
{
    public class SimpleDividisionCylinder : IQuestionCylinder
    {
        private readonly System.Collections.Generic.Dictionary<int, SimpleDivisionConstraint> levelIds = getConstraints(); 

        public Logic.Enums.QuestionType QuestionType { get; } = Logic.Enums.QuestionType.Math; 

        public Logic.Enums.QuestionSubType QuestionSubType { get; } = Logic.Enums.QuestionSubType.SimpleDivision;

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

        bool constraintPasses(SimpleDivisionConstraint constraint, Logic.Question.IQuestion q)
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

        internal Question.SimpleDivisionQuestion generateQuestion(SimpleDivisionConstraint constraint)
        {
            Question.SimpleDivisionQuestion q = new Question.SimpleDivisionQuestion();

            while(true)
            {
                
                int denominator = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt((int)(constraint.MinDivisor),
                                                                                            (int)(constraint.MaxDivisor));

                int numerator = PeanutButter.RandomGenerators.RandomValueGen.GetRandomInt((int)(denominator*constraint.MinNumberOfMulitple),
                                                                                            (int)(denominator*constraint.MaxNumberOfMulitple));


                if ( !constraint.AllowRemainder && numerator%denominator != 0)
                {
                    continue;
                }

                q.Numbers.Add(new Question.SignedNumber{ SortOrder = 1, Number = numerator });
                q.Numbers.Add(new Question.SignedNumber{ SortOrder = 2, Number = denominator });
                q.AllowRemainder = constraint.AllowRemainder;
                break;
            }

            return q;
        }

        static System.Collections.Generic.Dictionary<int, SimpleDivisionConstraint> getConstraints()
        {
            System.Collections.Generic.Dictionary<int, SimpleDivisionConstraint> constraintsByLevelId = 
                new System.Collections.Generic.Dictionary<int, SimpleDivisionConstraint>();

            constraintsByLevelId.Add(1, new SimpleDivisionConstraint(1){
                     AllowRemainder = false,
                     MaxDivisor = 5,
                     MinDivisor = 1,
                     MaxNumberOfMulitple = 10,
                     MinNumberOfMulitple = 1
                });

            constraintsByLevelId.Add(2, new SimpleDivisionConstraint(2){
                     AllowRemainder = false,
                     MaxDivisor = 10,
                     MinDivisor = 1,
                     MaxNumberOfMulitple = 10,
                     MinNumberOfMulitple = 1
                });

            constraintsByLevelId.Add(3, new SimpleDivisionConstraint(3){
                     AllowRemainder = true,
                     MaxDivisor = 10,
                     MinDivisor = 1,
                     MaxNumberOfMulitple = 15,
                     MinNumberOfMulitple = 4
                });
                
            constraintsByLevelId.Add(4, new SimpleDivisionConstraint(4){
                     AllowRemainder = true,
                     MaxDivisor = 10,
                     MinDivisor = 4,
                     MaxNumberOfMulitple = 30,
                     MinNumberOfMulitple = 8
                });
                
            constraintsByLevelId.Add(5, new SimpleDivisionConstraint(5){
                     AllowRemainder = true,
                     MaxDivisor = 20,
                     MinDivisor = 8,
                     MaxNumberOfMulitple = 100,
                     MinNumberOfMulitple = 8
                });

                
            return constraintsByLevelId;
        }

        private SimpleDivisionConstraint getConstraintByLevel(decimal level)                           
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