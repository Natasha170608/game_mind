using QuizeGame;
using System;
using System.Drawing;
using System.Windows.Forms;
using static WinFormsApp12.Program;

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
        private Button _player1;
        private Button _player2;
        private TextBox _answerTextBox;

        private Player _player;
        private bool _isGameActive = true;
        private int _currentLevelNumber;

        private Level1 _level1;
        private Level2 _level2;
        private Level3 _level3;

        private GameResultPresenter _resultPresenter;

        public QuizeGame()
        {
            AppState.MainForm = this;
            InitializeComponent();
            InitializeCustomUI();
            InitializeLevels();

            _optionOneButton.Click += OptionClicked;
            _optionTwoButton.Click += OptionClicked;
            _optionThreeButton.Click += OptionClicked;
            _newGameButton.Click += OnNewGameButtonClicked;
            _startButton.Click += StartButtonClicked;
            _player1.Click += Player1Clicked;
            _player2.Click += Player2Clicked;
            _answerTextBox.KeyDown += AnswerTextBoxKeyDown;
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

            _player1 = new Button
            {
                Location = new Point(100, 200),
                Size = new Size(200, 60),
                Text = "1 игрок",
                BackColor = Color.FromArgb(40, 40, 50),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            _player2 = new Button
            {
                Location = new Point(500, 200),
                Size = new Size(200, 60),
                Text = "2 игрока",
                BackColor = Color.FromArgb(40, 40, 50),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            Controls.Add(_titleLabel);
            Controls.Add(_startButton);
            Controls.Add(_player1);
            Controls.Add(_player2);

            _player1.Visible = false;
            _player2.Visible = false;

            int buttonWidth = 220;
            int buttonHeight = 60;
            int startX = (this.Width - (buttonWidth * 3 + 60)) / 2;
            int startY = 400;
            int spacing = 30;

            _statusPanel = new StatusPanel
            {
                Location = new Point(20, 20),
                Size = new Size(740, 100),
            };

            _questionLabel = new Label
            {
                Location = new Point(50, 150),
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
                Location = new Point(300, 350),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12)
            };
            Controls.Add(_answerTextBox);
            _answerTextBox.Visible = false;

            _newGameButton = new Button
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
            Controls.Add(_questionLabel);

            _statusPanel.Visible = false;
            _questionLabel.Visible = false;
            _optionOneButton.Visible = false;
            _optionTwoButton.Visible = false;
            _optionThreeButton.Visible = false;
            _newGameButton.Visible = false;
        }

        private void InitializeLevels()
        {
            _player = new Player();
            _resultPresenter = new GameResultPresenter(_player, ReturnToMainMenu, () => Application.Exit());

            _level1 = new Level1(_player, _statusPanel, _answerTextBox, _questionLabel,
                                OnLevel1Complete, OnGameOver, OnGameComplete);

            _level2 = new Level2(_player, _statusPanel, _optionOneButton, _optionTwoButton,
                                _optionThreeButton, _questionLabel, OnLevel2Complete, OnGameOver, OnGameComplete);

            _level3 = new Level3(_player, _statusPanel, _optionOneButton, _optionTwoButton,
                                _optionThreeButton, _questionLabel, OnGameComplete, OnGameOver);
        }

        private void StartNewGame()
        {
            _player = new Player();
            _isGameActive = true;
            _currentLevelNumber = 1;

            _resultPresenter = new GameResultPresenter(_player, ReturnToMainMenu, () => Application.Exit());

            _level1 = new Level1(_player, _statusPanel, _answerTextBox, _questionLabel,
                                OnLevel1Complete, OnGameOver, OnGameComplete);

            _level2 = new Level2(_player, _statusPanel, _optionOneButton, _optionTwoButton,
                                _optionThreeButton, _questionLabel, OnLevel2Complete, OnGameOver, OnGameComplete);

            _level3 = new Level3(_player, _statusPanel, _optionOneButton, _optionTwoButton,
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
            UpdateUIFromPlayer(_player);
        }

        private void StartLevel1()
        {
            _currentLevelNumber = 1;
            _level1.Start();
        }

        private void StartLevel2()
        {
            _player.CurrentHealth = _player.MaxHealth;
            _currentLevelNumber = 2;
            _level2.Start();
        }

        private void StartLevel3()
        {
            _player.CurrentHealth = _player.MaxHealth;
            _currentLevelNumber = 3;
            _level3.Start();
        }

        private void OnLevel1Complete()
        {
            if (_player.CurrentHealth > 0)
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
            if (_player.CurrentHealth > 0)
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
            _resultPresenter.ShowWinResult();
        }

        private void OnGameOver()
        {
            _isGameActive = false;
            _optionOneButton.Enabled = false;
            _optionTwoButton.Enabled = false;
            _optionThreeButton.Enabled = false;
            _answerTextBox.Enabled = false;
            _resultPresenter.ShowLoseResult();
        }

        private void OptionClicked(object sender, EventArgs e)
        {
            if (!_isGameActive || _player.CurrentHealth <= 0) return;

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

        private void StartButtonClicked(object sender, EventArgs e)
        {
            _titleLabel.Text = "Выберите режим игры";
            _startButton.Visible = false;
            _player1.Visible = true;
            _player2.Visible = true;
        }

        private void Player1Clicked(object sender, EventArgs e)
        {
            _titleLabel.Visible = false;
            _player1.Visible = false;
            _player2.Visible = false;
            _statusPanel.Visible = true;
            _questionLabel.Visible = true;
            _newGameButton.Visible = true;
            StartNewGame();
        }

        private void Player2Clicked(object sender, EventArgs e)
        {
            player_2 player2Form = new player_2();
            player2Form.Show();
            this.Hide();
        }

        private void OnNewGameButtonClicked(object sender, EventArgs e)
        {
            ReturnToMainMenu();
        }

        public void ReturnToMainMenu()
        {
            _isGameActive = false;

            _level1?.Stop();
            _level2?.Stop();
            _level3?.Stop();

            _statusPanel.Visible = false;
            _questionLabel.Visible = false;
            _optionOneButton.Visible = false;
            _optionTwoButton.Visible = false;
            _optionThreeButton.Visible = false;
            _answerTextBox.Visible = false;
            _newGameButton.Visible = false;

            _titleLabel.Text = "Игра ума";
            _titleLabel.Visible = true;
            _startButton.Visible = true;
            _player1.Visible = false;
            _player2.Visible = false;

            _player = new Player();

            Refresh();
        }

        public void UpdateUIFromPlayer(Player player)
        {
            if (player == null) return;
            _statusPanel?.UpdateStatus(player);
            Refresh();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }
}