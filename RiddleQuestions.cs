using System;
using System.Collections.Generic;
using System.Text;
using WinFormsApp12;

namespace QuizeGame
{
    public class RiddleQuestions
    {
        public static List<QuestionData> GetQuestions()
        {
            return new List<QuestionData>
            {
                new QuestionData("У какого преступника есть совесть?",
                    new string[] { "вор", "убийца", "мошенник" }, 1),

                new QuestionData("Что делает сторож, когда у него на шапке сидит воробей?",
                    new string[] { "спит", "кричит", "машет" }, 0),

                new QuestionData("Чей голос звучит громче всего в лесу?",
                    new string[] { "дятла", "волка", "эха" }, 2),

                new QuestionData("Что можно сломать, даже не прикасаясь?",
                    new string[] { "палку", "обещание", "тишину" }, 1),

                new QuestionData("Что служит для обмена информации между людьми, но не имеет ни звука, ни текста?",
                    new string[] { "взгляд", "интернет", "жесты" }, 0),

                new QuestionData("Чем можно поделиться только с тем, у кого этого нет?",
                    new string[] { "деньги", "секрет", "еда" }, 1),

                new QuestionData("Что нельзя купить ни за какие деньги, но можно подарить?",
                    new string[] { "любовь", "совет", "счастье" }, 1),

                new QuestionData("Что не имеет веса, но может заставить человека упасть?",
                    new string[] { "ветер", "мысль", "сон" }, 2),

                new QuestionData("Что есть у каждого человека, но у каждого своё, и это нельзя потрогать?",
                    new string[] { "имя", "прошлое", "характер" }, 0),

                new QuestionData("У кого есть спинка, но нет живота?",
                    new string[] { "кресло", "диван", "стул" }, 2)
            };
        }
    }
}
