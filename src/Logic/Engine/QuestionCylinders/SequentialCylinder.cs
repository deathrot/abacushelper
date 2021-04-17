using Logic.Engine.Constraints;
using System.Linq; 

namespace Logic.Engine.QuestionCylinders
{
    public class SequentialCylinder : IQuestionCylinder
    {
        private readonly System.Collections.Generic.Dictionary<int, SequentialConstraints> levelIds = getConstraints(); 

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

        bool constraintPasses(SequentialConstraints constraint, Logic.Question.IQuestion q)
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

        internal Question.SequentialQuestion generateQuestion(SequentialConstraints constraint)
        {
            int totalNumbers = 1;
            
            Question.SequentialQuestion q = new Question.SequentialQuestion();
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

        static System.Collections.Generic.Dictionary<int, SequentialConstraints> getConstraints()
        {
            System.Collections.Generic.Dictionary<int, SequentialConstraints> constraintsByLevelId = 
                new System.Collections.Generic.Dictionary<int, SequentialConstraints>();

            constraintsByLevelId.Add(1, new SequentialConstraints(1, 2){
                     AllowNegative = false,
                     MaxNumber = 10,
                     MinNumber = 3
                });

            constraintsByLevelId.Add(2, new SequentialConstraints(2, constraintsByLevelId[1].MaxScore){
                     AllowNegative = false,
                     MaxNumber = 10,
                     MinNumber = 3
                });

            constraintsByLevelId.Add(3, new SequentialConstraints(3, constraintsByLevelId[2].MaxScore){
                     AllowNegative = false,
                     MaxNumber = 25,
                     MinNumber = 10
                });

            constraintsByLevelId.Add(4, new SequentialConstraints(4, constraintsByLevelId[3].MaxScore){
                     AllowNegative = false,
                     MaxNumber = 40,
                     MinNumber = 20
                });

            constraintsByLevelId.Add(5, new SequentialConstraints(5, constraintsByLevelId[4].MaxScore){
                     MaxNumber = 60,
                     MinNumber = 30
                });
                
            return constraintsByLevelId;
        }

        private SequentialConstraints getConstraintByLevel(decimal level)                           
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