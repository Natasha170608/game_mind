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
                new QuestionData("Сколько будет 3 * 4?", new string[] { "12", "10", "14" }, 0),
                new QuestionData("Сколько будет 24 + 16 : 4 – 7?", new string[] { "21", "3", "17" }, 0),
                new QuestionData("Сколько будет 15 : 3?", new string[] { "5", "6", "4" }, 0),
                new QuestionData("Сколько будет 5 * 8 – 6 * 3?", new string[] { "30", "22", "42" }, 1),
                new QuestionData("Сколько будет 30 : 3?", new string[] { "5", "1", "10" }, 2),
                new QuestionData("Сколько будет 64 – 48 : 8 + 2?", new string[] { "60", "6", "18" }, 0),
                new QuestionData("Сколько будет 9 + 6?", new string[] { "14", "15", "16" }, 1),
                new QuestionData("Сколько будет 18 : 3 + 7 * 2?", new string[] { "20", "32", "14" }, 0),
                new QuestionData("Сколько будет 12 * 2?", new string[] { "24", "34", "26" }, 0),
                new QuestionData("Сколько будет 90 – (30 + 20) : 5?", new string[] { "80", "84", "76" }, 0)
            };
        }
    }
}
