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

        Enums.QuestionSubType QuestionSubType { get; }

        List<SignedNumber> Numbers { get; }

        bool IsValid();

        decimal Calculate();

    }
}
