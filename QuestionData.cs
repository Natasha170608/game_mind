using System;
using System.Collections.Generic;
using System.Text;

namespace QuizeGame
{
    public class QuestionData
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectOptionIndex { get; set; }

        public QuestionData(string text, string[] options, int correctOptionIndex)
        {
            Text = text;
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
        }
    }
}
