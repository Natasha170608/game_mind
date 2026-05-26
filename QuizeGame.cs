using QuizeGame;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public partial class QuizeGame : Form
    {
        private StatusPanel _statusPanel;
        private Button _newGameButton;
        private Button _optionOneButton;
        private Button _optionTwoButton;
        private Button _optionThreeButton;
        private Label _questionLabel;
        private Label _titleLabel;
        private Button _startButton;
        private Player _player;
        private bool _isGameActive = true;
        private List<QuestionData> _questions;
        private int _currentQuestion;
        public QuizeGame()
        {
            InitializeComponent();
            InitializeCustomUI();
            _optionOneButton.Click += OptionClicked;
            _optionTwoButton.Click += OptionClicked;
            _optionThreeButton.Click += OptionClicked;
            _newGameButton.Click += OnNewGameButtonClicked;
            _startButton.Click += StartButtonClicked;
        }

        private void StartNewGame()
        {
            _player = new Player();
            _isGameActive = true;
            _questions = MathQuestions.GetQuestions();
            _currentQuestion = 0;
            ShowQuestion();
            UpdateUIFromPlayer(_player);
        }
        private void ShowQuestion()
        {
            QuestionData question = _questions[_currentQuestion];
            _questionLabel.Text = question.Text;
            _optionOneButton.Text = question.Options[0];
            _optionTwoButton.Text = question.Options[1];
            _optionThreeButton.Text = question.Options[2];
        }
        private void InitializeCustomUI()
        {
            this.Size = new Size(800, 550);
            this.MinimumSize = new Size(700, 500);
            this.BackColor = Color.Black;
            this.StartPosition = FormStartPosition.CenterScreen;
            _titleLabel = new Label
            {
                Location = new Point(250, 100),
                Size = new Size(300, 70),
                Text = "Игра ума",
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            _startButton = new Button
            {
                Location = new Point(300, 220),
                Size = new Size(200, 60),
                Text = "Начать игру",
                BackColor = Color.FromArgb(40, 40, 50),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            Controls.Add(_titleLabel);
            Controls.Add(_startButton);
            int buttonWidth = 220;
            int buttonHeight = 60;
            int startX = (this.Width - (buttonWidth * 3 + 60)) / 2;
            int startY = 280;
            int spacing = 30;
            _statusPanel = new StatusPanel
            {
                Location = new Point(20, 20),
                Size = new Size(740, 100),

            };

            _questionLabel = new Label
            {
                Location = new Point(50, 140),
                Size = new Size(700, 80),
                BackColor = Color.FromArgb(30, 30, 40),
                FlatStyle = FlatStyle.Flat,
                ForeColor =Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            _newGameButton = new Button
            {
                Location = new Point(20, 460),
                Size = new Size(200, 40),
                Text = "🔄 Новая игра",
                BackColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _newGameButton.FlatAppearance.BorderSize = 0;

            _optionOneButton = new Button
            {
                Location = new Point(startX, startY),
                Size = new Size(buttonWidth, buttonHeight),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = 0
            };
            _optionTwoButton = new Button
            {
                Location = new Point(startX + buttonWidth + spacing, startY),
                Size = new Size(buttonWidth, buttonHeight),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = 1
            };
            _optionThreeButton = new Button
            {
                Location = new Point(startX + (buttonWidth + spacing) * 2, startY),
                Size = new Size(buttonWidth, buttonHeight),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 40, 50),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = 2
            };
            foreach (var btn in new[] { _optionOneButton, _optionTwoButton, _optionThreeButton })
            {
                btn.MouseEnter += (s, e) => ((Button)s).BackColor = Color.FromArgb(60, 60, 70);
                btn.MouseLeave += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 40, 50);
                Controls.Add(btn);
            }

            Controls.Add(_statusPanel);
            Controls.Add(_newGameButton);
            Controls.Add(_optionOneButton);
            Controls.Add(_optionTwoButton);
            Controls.Add(_optionThreeButton);
            Controls.Add(_questionLabel);
            _statusPanel.Visible = false;
            _questionLabel.Visible = false;
            _optionOneButton.Visible = false;
            _optionTwoButton.Visible = false;
            _optionThreeButton.Visible = false;
            _newGameButton.Visible = false;
        }

        private void OnNewGameButtonClicked(object sender, EventArgs e)
        {
            StartNewGame();
        }

        public event Action OnNewGameRequested;

        public void CorrectAnswer(Player player)
        {
            if (!_isGameActive) return;
            if (player.CurrentHealth <= 0) return;
            player.AddQuestion();
            player.AddAnswers();
            if (player.Question > player.MaxLevel)
            {
                player.Level++;
                player.newMaxlevel();
            }
            _statusPanel?.UpdateStatus(player);

            if (player.Question >= player.TotalAnswers && player.CurrentHealth > 0)
            {
                _isGameActive = false;
                _optionOneButton.Enabled = false;
                ShowGameResult(true); 
                return;
            }
            
        }
        public void WrongAnswer(Player player)
        {
            if (!_isGameActive) return;
            if (player.CurrentHealth <= 0) return;
            player.AddQuestion();
            player.AddWrong();
            player.CurrentHealth--;
            if (player.Question > player.MaxLevel)
            {
                player.Level++;
                player.newMaxlevel();
            }
            _statusPanel?.UpdateStatus(player);

            if (player.Question >= player.TotalAnswers && player.CurrentHealth > 0)
            {
                _isGameActive = false;
                _optionOneButton.Enabled = false;
                ShowGameResult(true);
                return;
            }
            if (player.CurrentHealth <= 0)
            {
                _isGameActive = false;
                _optionOneButton.Enabled = false;
                ShowGameResult(false);
            }
        }

        private void OptionClicked(object sender, EventArgs e)
        {
            Button buttonNumber = (Button)sender;
            int index = (int)buttonNumber.Tag;
            if (index == _questions[_currentQuestion].CorrectOptionIndex)
                CorrectAnswer(_player);
            else
                WrongAnswer(_player);
            _currentQuestion++;
            if (_currentQuestion < _questions.Count)
            {
                ShowQuestion();
            }
            else
            {
                ShowGameResult(true);
            }
        }
        private void StartButtonClicked(object sender, EventArgs e)
        {
            _titleLabel.Visible = false;
            _startButton.Visible = false;
            _statusPanel.Visible = true;
            _questionLabel.Visible = true;
            _optionOneButton.Visible = true;
            _optionTwoButton.Visible = true;
            _optionThreeButton.Visible = true;
            _newGameButton.Visible = true;
            StartNewGame();
        }
        public void UpdateUIFromPlayer(Player player)
        {
            if (player == null) return;
            _statusPanel?.UpdateStatus(player);
            Refresh();
        }

        private void ShowGameResult(bool isWin)
        {
            string message;
            string title;
            MessageBoxIcon icon;

            if (isWin)
            {
                message = $" ПОБЕДА! \n Верных ответов:{_player.CorrectAnswers}\nВы успешно прошли викторину!\n\nХотите начать новую игру?";
                title = "ПОБЕДА!";
                icon = MessageBoxIcon.None;
            }
            else
            {
                message = " ИГРА ОКОНЧЕНА \nВы проиграли!\n\nХотите начать новую игру?";
                title = "ИГРА ЗАВЕРШЕНА";
                icon = MessageBoxIcon.None;
            }

            DialogResult result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                icon);

            if (result == DialogResult.Yes)
            {
                StartNewGame(); 
            }
            else
            {
                Application.Exit(); 
            }
        }

        public void SetUIEnabled(bool enabled)
        {
            
            _newGameButton.Enabled = enabled;
        }

        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isGameActive && _player.Question < _player.TotalAnswers && _player.CurrentHealth > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Вы уверены, что хотите выйти из игры?\nПрогресс будет потерян!",
                    "Подтверждение выхода",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }
    }
}