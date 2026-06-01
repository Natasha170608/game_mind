using System;
using System.Collections.Generic;
using System.Text;

namespace QuizeGame
{
    public class Player
    {

        public int MaxHealth { get; set; } = 100;
        public int CurrentHealth { get; set; } = 100;
        public int Experience { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int MaxLevel { get; set; } = 3;
        public int CorrectAnswers { get; set; } = 0;
        public int TotalAnswers { get; set; } = 30;
        public int WrongAnswer { get; set; } = 0;
        public int Question { get; set; } = 0;

       
       
        public void newMaxlevel()
        {
            MaxLevel += 10;
            CurrentHealth = MaxHealth;
        }
        public void AddAnswers()
        {
            CorrectAnswers++;
        }
        public void AddWrong()
        {
            WrongAnswer++;
        }
        public void AddQuestion()
        {
            Question++;
        }
    }
}
