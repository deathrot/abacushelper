using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LogicTests
{
    public class SimpleDivisionCylinderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        public void SmokeTests_1()
        {
            Logic.Engine.QuestionCylinders.SimpleDividisionCylinder cylinder = new Logic.Engine.QuestionCylinders.SimpleDividisionCylinder();
            var q = cylinder.Fire(1);

            NUnit.Framework.Assert.Greater(q.Calculate(), 0);
            NUnit.Framework.Assert.IsTrue(q.GetType() == typeof(Logic.Question.SimpleDivisionQuestion));

            System.Console.WriteLine(q.ToString());
        }

        [Test]
        public void SmokeTests_2()
        {
            Logic.Engine.QuestionCylinders.SimpleDividisionCylinder cylinder = new Logic.Engine.QuestionCylinders.SimpleDividisionCylinder();
            
            int i = 0;
            while(i<1)
            {
                var q = cylinder.Fire(4);

                NUnit.Framework.Assert.Greater(q.Calculate(), 0);
                NUnit.Framework.Assert.IsTrue(q.GetType() == typeof(Logic.Question.SimpleDivisionQuestion));

                System.Console.WriteLine(q.ToString());
                
                i++;
            }
        }

        [Test]
        public void SmokeTests_Level1()
        {
            Logic.Engine.QuestionCylinders.SimpleDividisionCylinder cylinder = 
                new Logic.Engine.QuestionCylinders.SimpleDividisionCylinder();
            
            int i = 0;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            System.Collections.Generic.Dictionary<int, int> totalCounts = new System.Collections.Generic.Dictionary<int, int>();

            while(i<100000)
            {
                var q = cylinder.Fire(1);

                int hc = q.GetHashCode();
                if(totalCounts.ContainsKey(hc))
                {
                    totalCounts[hc] = totalCounts[hc] + 1;
                }
                else{
                    totalCounts[hc] = 1;
                }
                
                i++;
            }

            sw.Stop();

            int totalDuplicate = 0;
            foreach(var key in totalCounts)
            {
                if ( key.Value > 1)
                {
                    totalDuplicate++;
                }
            }

            System.Console.WriteLine($"Total questions created {totalCounts.Count} duplicates {totalDuplicate}");
            System.Console.WriteLine($"Took total {sw.ElapsedMilliseconds}ms");
            NUnit.Framework.Assert.IsTrue(true);
        }

        [Test]
        public void SmokeTests_Level5()
        {
            Logic.Engine.QuestionCylinders.SimpleDividisionCylinder cylinder = 
                new Logic.Engine.QuestionCylinders.SimpleDividisionCylinder();
            
            int i = 0;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            System.Collections.Generic.Dictionary<int, int> totalCounts = new System.Collections.Generic.Dictionary<int, int>();

            while(i<100000)
            {
                var q = cylinder.Fire(5);
                int hc = q.GetHashCode();

                if(totalCounts.ContainsKey(hc))
                {
                    totalCounts[hc] = totalCounts[hc] + 1;
                }
                else{
                    totalCounts[hc] = 1;
                }
                
                i++;
            }

            sw.Stop();

            int totalDuplicate = 0;
            foreach(var key in totalCounts)
            {
                if ( key.Value > 1)
                {
                    totalDuplicate++;
                }
            }

            System.Console.WriteLine($"Total questions created {totalCounts.Count} duplicates {totalDuplicate}");
            System.Console.WriteLine($"Took total {sw.ElapsedMilliseconds}ms");
            NUnit.Framework.Assert.IsTrue(true);
        }

        [Test]
        public void SmokeTests_Level4()
        {
            Logic.Engine.QuestionCylinders.SimpleDividisionCylinder cylinder = 
                new Logic.Engine.QuestionCylinders.SimpleDividisionCylinder();
            
            int i = 0;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            System.Collections.Generic.Dictionary<int, int> totalCounts = new System.Collections.Generic.Dictionary<int, int>();

            while(i<100000)
            {
                var q = cylinder.Fire(4);
                int hc = q.GetHashCode();

                if(totalCounts.ContainsKey(hc))
                {
                    totalCounts[hc] = totalCounts[hc] + 1;
                }
                else{
                    totalCounts[hc] = 1;
                }
                
                i++;
            }

            sw.Stop();

            int totalDuplicate = 0;
            foreach(var key in totalCounts)
            {
                if ( key.Value > 1)
                {
                    totalDuplicate++;
                }
            }

            System.Console.WriteLine($"Total questions created {totalCounts.Count} duplicates {totalDuplicate}");
            System.Console.WriteLine($"Took total {sw.ElapsedMilliseconds}ms");
            NUnit.Framework.Assert.IsTrue(true);
        }

    }
}