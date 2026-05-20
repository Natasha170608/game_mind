using QuizeGame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public class StatusPanel : UserControl
    {
        private Label _healthLabel;
        private Label _maxhealthLabel;
        private Label _levelLabel;
        private Label _totalAnswersLabel;
        private Label _correctLabel;
        private Label _wrongAnswerLabel;
        

        public StatusPanel()
        {
            Size = new Size(740, 80);
            BackColor = Color.FromArgb(40, 40, 50);

            _healthLabel = new Label
            {
                Location = new Point(10, 5),
                Size = new Size(120, 25),
  
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 40, 50),
                ForeColor = Color.Red,
                Text ="Здоровье 5"
            };
            _maxhealthLabel = new Label
            {
                Location = new Point(10, 50),
                Size = new Size(160, 40),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 40, 50),
                Text ="Макс здоровье: 5"
            };
            _levelLabel = new Label
            {
                Location = new Point(300, 5),
                Size = new Size(100, 25),
                ForeColor = Color.Gold,
                Text= "Уровень 1"
            };
            
            _totalAnswersLabel = new Label
            {
                Location = new Point(300, 50),
                Size = new Size(120, 25),
                ForeColor = Color.White,
                Text="0/30"
            };
            _correctLabel = new Label
            {
                Location = new Point(600, 5),
                Size = new Size(150, 25),
                ForeColor = Color.LightGreen,
                Text ="Правильные: 0"
            };
            _wrongAnswerLabel = new Label
            {
                Location = new Point(600, 50), 
                Size = new Size(150, 25),
                ForeColor = Color.Red,
                Text = "Неверные: 0"
            };
            Controls.Add(_healthLabel);
            Controls.Add(_maxhealthLabel);
            Controls.Add(_levelLabel);
            Controls.Add(_totalAnswersLabel);
            Controls.Add(_correctLabel);
            Controls.Add(_wrongAnswerLabel);
        }
        public void UpdateStatus(Player player)
        {
            _healthLabel.Text = $"Здоровье:{player.CurrentHealth}";
            _maxhealthLabel.Text = $"Всего здоровья:{player.MaxHealth}";
            _levelLabel.Text = $"Ур.:{player.Level}";
            _totalAnswersLabel.Text = $"{player.Question}/{player.TotalAnswers}";
            _correctLabel.Text = $"Правильные: {player.CorrectAnswers}";
            _wrongAnswerLabel.Text = $"Неверных: {player.WrongAnswer}";


        }

    }

}
