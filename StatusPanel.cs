using QuizeGame;
using System;
using System.Drawing;
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
        private Label _playerNameLabel;

        public StatusPanel()
        {
            InitializeControls();
        }

        private void InitializeControls()
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
                Text = "Здоровье: 100"
            };

            _maxhealthLabel = new Label
            {
                Location = new Point(10, 45),
                Size = new Size(160, 25),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 40, 50),
                Text = "Макс здоровье: 100"
            };

            _levelLabel = new Label
            {
                Location = new Point(200, 5),
                Size = new Size(100, 25),
                ForeColor = Color.Gold,
                Text = "Уровень: 1"
            };

            _totalAnswersLabel = new Label
            {
                Location = new Point(200, 45),
                Size = new Size(120, 25),
                ForeColor = Color.White,
                Text = "0/30"
            };

            _playerNameLabel = new Label
            {
                Location = new Point(400, 25),
                Size = new Size(200, 30),
                ForeColor = Color.Cyan,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Text = "",
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false  
            };

            _correctLabel = new Label
            {
                Location = new Point(580, 5),
                Size = new Size(150, 25),
                ForeColor = Color.LightGreen,
                Text = "Правильные: 0"
            };

            _wrongAnswerLabel = new Label
            {
                Location = new Point(580, 45),
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
            Controls.Add(_playerNameLabel);
        }

        public void UpdateStatus(Player player)
        {
            if (player == null) return;
            _healthLabel.Text = $"Здоровье: {player.CurrentHealth}";
            _maxhealthLabel.Text = $"Макс здоровье: {player.MaxHealth}";
            _levelLabel.Text = $"Уровень: {player.Level}";
            if (player.Question < player.TotalAnswers)
            {
                _totalAnswersLabel.Text = $"{player.Question + 1}/{player.TotalAnswers}";
            }
            else
            {
                _totalAnswersLabel.Text = $"{player.Question}/{player.TotalAnswers}";
            }
            _correctLabel.Text = $"Правильные: {player.CorrectAnswers}";
            _wrongAnswerLabel.Text = $"Неверные: {player.WrongAnswer}";
        }

        public void UpdateStatus(Player player, string playerName)
        {
            UpdateStatus(player);

            if (!string.IsNullOrEmpty(playerName))
            {
                _playerNameLabel.Text = $"Игрок: {playerName}";
                _playerNameLabel.Visible = true;  
            }
            else
            {
                _playerNameLabel.Visible = false;
            }
        }

        public void SetPlayerName(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                _playerNameLabel.Visible = false;
            }
            else
            {
                _playerNameLabel.Text = $"Игрок: {playerName}";
                _playerNameLabel.Visible = true;
            }
        }

        public void HidePlayerName()
        {
            _playerNameLabel.Visible = false;
        }
    }
}