using System;
using System.Collections.Generic;
using System.Text;
using WinFormsApp12;

namespace QuizeGame
{
    public class MathQuestions
    {
        public static List<QuestionData> GetQuestions()
        {
            return new List<QuestionData>
            {
                new QuestionData("Сколько будет 4,5 * 5,4 – 6,1?", new string[] { "18.2", "4", "5" }, 0),
                new QuestionData("Сколько будет (4.8*0.4)/0.6?", new string[] { "3.1", "4.1", "3,2" }, 2),
                new QuestionData("Сколько будет 5.4*0.8+0.08?", new string[] { "4.1", "4.4", "4.9" }, 1),
                new QuestionData("Сколько будет 3 * 4?", new string[] { "12", "10", "14" }, 0),
                new QuestionData("Сколько будет 15 : 3?", new string[] { "5", "6", "4" }, 0),
                new QuestionData("Сколько будет 30 : 3?", new string[] { "5", "1", "10" }, 2),
                new QuestionData("Сколько будет 9 + 6?", new string[] { "14", "15", "16" }, 1),
                new QuestionData("Сколько будет 15 - 7?", new string[] { "7", "9", "8" }, 2),
                new QuestionData("Сколько будет 12 * 2?", new string[] { "24", "34", "26" }, 0),
                new QuestionData("Сколько будет 52 : 4?", new string[] { "17", "19", "13" }, 2)
            };
        }
    }
}
