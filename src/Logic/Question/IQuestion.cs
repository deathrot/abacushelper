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

        Enums.QuestionSubType QuestionType { get;  }

        bool IsValid();

        decimal Calculate();

    }
}
