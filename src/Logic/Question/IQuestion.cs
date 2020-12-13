using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Question
{
    /// <summary>
    /// IQuestion type
    /// </summary>
    public interface IQuestion
    {

        QuestionType QuestionType { get;  }

        bool IsValid();

        decimal Calculate();

    }
}
