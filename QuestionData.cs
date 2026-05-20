using System.Collections.Generic;

namespace WinFormsApp12
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

    public static class QuestionBank
    {
        public static List<QuestionData> GetAllQuestions()
        {
            return new List<QuestionData>
            {
                new QuestionData("Сколько будет 2 + 2?",
                    new string[] { "3", "4", "5" }, 1),

                new QuestionData("Какая планета называется 'Красной планетой'?",
                    new string[] { "Венера", "Марс", "Юпитер" }, 1),

                new QuestionData("Кто написал 'Войну и мир'?",
                    new string[] { "Достоевский", "Толстой", "Чехов" }, 1),

                new QuestionData("Какой язык программирования используется для создания игр на Unity?",
                    new string[] { "Java", "Python", "C#" }, 2),

                new QuestionData("Что такое HTML?",
                    new string[] { "Язык программирования", "Язык разметки", "База данных" }, 1),

                new QuestionData("Какое животное известно как 'корабль пустыни'?",
                    new string[] { "Лошадь", "Верблюд", "Слон" }, 1),

                new QuestionData("Какой цвет получается при смешивании синего и желтого?",
                    new string[] { "Красный", "Зеленый", "Фиолетовый" }, 1),

                new QuestionData("Кто написал 'Гарри Поттера'?",
                    new string[] { "Дж.Р.Р. Толкин", "Дж.К. Роулинг", "Стивен Кинг" }, 1),

                new QuestionData("Столица Франции?",
                    new string[] { "Лондон", "Берлин", "Париж" }, 2),

                new QuestionData("Сколько дней в феврале в високосный год?",
                    new string[] { "28", "29", "30" }, 1)
            };
        }
    }
}