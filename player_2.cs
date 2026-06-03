using QuizeGame;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public partial class player_2 : Form
    {
        private StatusPanel _statusPanel;
        private Button _optionOneButton;
        private Button _optionTwoButton;
        private Button _optionThreeButton;
        private Button _mainMenuButton;
        private Label _questionLabel;
        private Label _titleLabel;
        private TextBox _answerTextBox;
        private TextBox _playerNameTextBox;
        private Label _instructionLabel;

        private Player _currentPlayer;
        private Player _player1;
        private Player _player2;

        private string _player1Name;
        private string _player2Name;

        private bool _isGameActive = true;
        private int _currentLevelNumber;
        private int _currentPlayerTurn; // 1 или 2
        private int _inputStep; // 0 - имя игрока 1, 1 - имя игрока 2

        private Level1 _level1;
        private Level2 _level2;
        private Level3 _level3;

        public player_2()
        {
            InitializeCustomUI();

            _optionOneButton.Click += OptionClicked;
            _optionTwoButton.Click += OptionClicked;
            _optionThreeButton.Click += OptionClicked;
            _answerTextBox.KeyDown += AnswerTextBoxKeyDown;
            _playerNameTextBox.KeyDown += PlayerNameTextBoxKeyDown;
            _mainMenuButton.Click += MainMenuButtonClicked;
        }

        private void InitializeCustomUI()
        {
            this.Size = new Size(800, 550);
            this.MinimumSize = new Size(700, 500);
            this.BackColor = Color.Black;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += Player2_FormClosing;

            // ========== ЭКРАН ВВОДА ИМЁН ==========
            _titleLabel = new Label
            {
                Location = new Point(250, 80),
                Size = new Size(300, 60),
                Text = "Игра для двоих",
                ForeColor = Color.White,
                BackColor = Color.Black,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _instructionLabel = new Label
            {
                Location = new Point(200, 160),
                Size = new Size(400, 40),
                Text = "Введите имя первого игрока и нажмите Enter:",
                ForeColor = Color.LightGray,
                BackColor = Color.Black,
                Font = new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleCenter
            };

            _playerNameTextBox = new TextBox
            {
                Location = new Point(250, 220),
                Size = new Size(300, 35),
                Font = new Font("Segoe UI", 14),
                TextAlign = HorizontalAlignment.Center
            };

            Controls.Add(_titleLabel);
            Controls.Add(_instructionLabel);
            Controls.Add(_playerNameTextBox);

            // ========== ИГРОВЫЕ ЭЛЕМЕНТЫ ==========
            int buttonWidth = 220;
            int buttonHeight = 60;
            int startX = (this.Width - (buttonWidth * 3 + 60)) / 2;
            int startY = 400;
            int spacing = 30;

            _statusPanel = new StatusPanel
            {
                Location = new Point(20, 20),
                Size = new Size(740, 80),
            };

            _questionLabel = new Label
            {
                Location = new Point(50, 120),
                Size = new Size(700, 180),
                BackColor = Color.FromArgb(30, 30, 40),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };

            _answerTextBox = new TextBox
            {
                Location = new Point(300, 320),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12)
            };
            Controls.Add(_answerTextBox);
            _answerTextBox.Visible = false;

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

            // ========== КНОПКА ГЛАВНОГО МЕНЮ ==========
            _mainMenuButton = new Button
            {
                Location = new Point(20, 460),
                Size = new Size(200, 40),
                Text = "Главное меню",
                BackColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            _mainMenuButton.FlatAppearance.BorderSize = 0;
            Controls.Add(_mainMenuButton);
            _mainMenuButton.Visible = false;

            Controls.Add(_statusPanel);
            Controls.Add(_questionLabel);

            _statusPanel.Visible = false;
            _questionLabel.Visible = false;
            _optionOneButton.Visible = false;
            _optionTwoButton.Visible = false;
            _optionThreeButton.Visible = false;
            _answerTextBox.Visible = false;
        }

        private void PlayerNameTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string name = _playerNameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Пожалуйста, введите имя!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_inputStep == 0)
                {
                    // Сохраняем имя первого игрока
                    _player1Name = name;
                    _inputStep = 1;
                    _instructionLabel.Text = "Введите имя второго игрока и нажмите Enter:";
                    _playerNameTextBox.Clear();
                    _playerNameTextBox.Focus();
                }
                else
                {
                    // Сохраняем имя второго игрока и начинаем игру
                    _player2Name = name;
                    StartGame();
                }
            }
        }

        private void StartGame()
        {
            // Скрываем элементы ввода имён
            _titleLabel.Visible = false;
            _instructionLabel.Visible = false;
            _playerNameTextBox.Visible = false;

            // Показываем игровые элементы
            _statusPanel.Visible = true;
            _questionLabel.Visible = true;
            _mainMenuButton.Visible = true;

            // Создаём игроков (жизни = 100, по сути бесконечные)
            _player1 = new Player();
            _player1.MaxHealth = 100;
            _player1.CurrentHealth = 100;
            _player1.TotalAnswers = 30;

            _player2 = new Player();
            _player2.MaxHealth = 100;
            _player2.CurrentHealth = 100;
            _player2.TotalAnswers = 30;

            _currentPlayerTurn = 1;
            _currentPlayer = _player1;

            // Запускаем игру для первого игрока
            StartNewGameForCurrentPlayer();
        }

        private void StartNewGameForCurrentPlayer()
        {
            _isGameActive = true;
            _currentLevelNumber = 1;

            // Сбрасываем здоровье текущего игрока (если нужно)
            if (_currentPlayer.CurrentHealth <= 0)
            {
                _currentPlayer.CurrentHealth = _currentPlayer.MaxHealth;
            }

            // Обновляем статус панель с именем текущего игрока
            string currentPlayerName = (_currentPlayerTurn == 1) ? _player1Name : _player2Name;
            _statusPanel.UpdateStatus(_currentPlayer, currentPlayerName);

            // Создаём уровни для текущего игрока
            _level1 = new Level1(_currentPlayer, _statusPanel, _answerTextBox, _questionLabel,
                                OnLevel1Complete, OnGameOver, OnGameComplete);

            _level2 = new Level2(_currentPlayer, _statusPanel, _optionOneButton, _optionTwoButton,
                                _optionThreeButton, _questionLabel, OnLevel2Complete, OnGameOver, OnGameComplete);

            _level3 = new Level3(_currentPlayer, _statusPanel, _optionOneButton, _optionTwoButton,
                                _optionThreeButton, _questionLabel, OnGameComplete, OnGameOver);

            _answerTextBox.Visible = false;
            _optionOneButton.Visible = false;
            _optionTwoButton.Visible = false;
            _optionThreeButton.Visible = false;

            _optionOneButton.Enabled = true;
            _optionTwoButton.Enabled = true;
            _optionThreeButton.Enabled = true;
            _answerTextBox.Enabled = true;

            StartLevel1();
        }

        private void StartLevel1()
        {
            _currentLevelNumber = 1;
            _level1.Start();
        }

        private void StartLevel2()
        {
            _currentLevelNumber = 2;
            _level2.Start();
        }

        private void StartLevel3()
        {
            _currentLevelNumber = 3;
            _level3.Start();
        }

        private void OnLevel1Complete()
        {
            if (_currentPlayer.CurrentHealth > 0)
            {
                StartLevel2();
            }
            else
            {
                OnGameOver();
            }
        }

        private void OnLevel2Complete()
        {
            if (_currentPlayer.CurrentHealth > 0)
            {
                StartLevel3();
            }
            else
            {
                OnGameOver();
            }
        }

        private void OnGameComplete()
        {
            _isGameActive = false;
            _optionOneButton.Enabled = false;
            _optionTwoButton.Enabled = false;
            _optionThreeButton.Enabled = false;
            _answerTextBox.Enabled = false;

            // Сохраняем результат текущего игрока и переключаемся
            SwitchToNextPlayer();
        }

        private void OnGameOver()
        {
            _isGameActive = false;
            _optionOneButton.Enabled = false;
            _optionTwoButton.Enabled = false;
            _optionThreeButton.Enabled = false;
            _answerTextBox.Enabled = false;

            // Даже при "смерти" переключаемся на следующего игрока
            SwitchToNextPlayer();
        }

        private void SwitchToNextPlayer()
        {
            if (_currentPlayerTurn == 1)
            {
                // Первый игрок закончил, начинаем второго
                _currentPlayerTurn = 2;
                _currentPlayer = _player2;

                MessageBox.Show($"Игрок {_player1Name} завершил игру!\n" +
                               $"Правильных ответов: {_player1.CorrectAnswers}\n" +
                               $"Неверных ответов: {_player1.WrongAnswer}\n\n" +
                               $"Теперь очередь игрока {_player2Name}!",
                               "Смена игрока",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);

                StartNewGameForCurrentPlayer();
            }
            else
            {
                // Оба игрока закончили, показываем результат
                ShowFinalResult();
            }
        }

        private void ShowFinalResult()
        {
            _optionOneButton.Visible = false;
            _optionTwoButton.Visible = false;
            _optionThreeButton.Visible = false;
            _answerTextBox.Visible = false;

            string winner;
            int winnerScore;
            int loserScore;
            string winnerName;
            string loserName;

            if (_player1.CorrectAnswers > _player2.CorrectAnswers)
            {
                winner = _player1Name;
                winnerScore = _player1.CorrectAnswers;
                loserScore = _player2.CorrectAnswers;
                winnerName = _player1Name;
                loserName = _player2Name;
            }
            else if (_player2.CorrectAnswers > _player1.CorrectAnswers)
            {
                winner = _player2Name;
                winnerScore = _player2.CorrectAnswers;
                loserScore = _player1.CorrectAnswers;
                winnerName = _player2Name;
                loserName = _player1Name;
            }
            else
            {
                // Ничья
                string message = $"НИЧЬЯ!\n\n" +
                               $"{_player1Name}: {_player1.CorrectAnswers} правильных ответов\n" +
                               $"{_player2Name}: {_player2.CorrectAnswers} правильных ответов\n\n" +
                               $"Хотите вернуться в главное меню?";

                DialogResult result = MessageBox.Show(message, "НИЧЬЯ!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ReturnToMainMenu();
                }
                else
                {
                    Close();
                }
                return;
            }

            string resultMessage = $"ПОБЕДИТЕЛЬ: {winner}!\n\n" +
                                  $"Результаты:\n" +
                                  $"{winnerName}: {winnerScore} правильных ответов\n" +
                                  $"{loserName}: {loserScore} правильных ответов\n\n" +
                                  $"{winnerName} ответил правильно на {winnerScore - loserScore} " +
                                  $"вопросов больше!\n\nХотите вернуться в главное меню?";

            DialogResult finalResult = MessageBox.Show(resultMessage, "ИГРА ЗАВЕРШЕНА",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (finalResult == DialogResult.Yes)
            {
                ReturnToMainMenu();
            }
            else
            {
                Close();
            }
        }

        private void ReturnToMainMenu()
        {
            // Останавливаем текущую игру
            _isGameActive = false;

            // Останавливаем все уровни
            _level1?.Stop();
            _level2?.Stop();
            _level3?.Stop();

            // Закрываем форму двух игроков
            this.Close();

            // Показываем главную форму
            var mainForm = Program.AppState.MainForm;
            if (mainForm != null)
            {
                mainForm.Show();

                // Сбрасываем главную форму в главное меню
                if (mainForm is QuizeGame quizeGame)
                {
                    quizeGame.ReturnToMainMenu();
                }
            }
        }

        private void MainMenuButtonClicked(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите вернуться в главное меню?\nПрогресс текущего игрока будет потерян!",
                "Выход в главное меню",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ReturnToMainMenu();
            }
        }

        private void OptionClicked(object sender, EventArgs e)
        {
            if (!_isGameActive || _currentPlayer.CurrentHealth <= 0) return;

            Button button = (Button)sender;
            int selectedIndex = (int)button.Tag;

            if (_currentLevelNumber == 2)
            {
                _level2?.CheckAnswer(selectedIndex);
            }
            else if (_currentLevelNumber == 3)
            {
                _level3?.CheckAnswer(selectedIndex);
            }
        }

        private void AnswerTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _currentLevelNumber == 1 && _isGameActive)
            {
                _level1?.CheckAnswer(_answerTextBox.Text);
            }
        }

        private void Player2_FormClosing(object sender, FormClosingEventArgs e)
        {
            var mainForm = Program.AppState.MainForm;
            if (mainForm != null && !mainForm.Visible)
            {
                mainForm.Show();
            }
        }
    }
}