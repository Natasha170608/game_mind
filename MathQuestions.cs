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
                new QuestionData("Сколько будет 2 + 2?", new string[] { "3", "4", "5" }, 1),
                new QuestionData("Сколько будет 5 + 7?", new string[] { "10", "12", "13" }, 1),
                new QuestionData("Сколько будет 9 - 4?", new string[] { "5", "6", "4" }, 0),
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
