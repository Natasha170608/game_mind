using QuizeGame;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public class Level3
    {
        private List<QuestionData> _questions;
        private int _currentQuestionIndex;
        private Player _player;
        private TextBox _answerTextBox;
        private Label _questionLabel;
        private CorrectAnswerHandler _correctHandler;
        private WrongAnswerHandler _wrongHandler;
        private Action _onGameComplete;
        private Action _onGameOver;
        private bool _isActive;

        public Level3(Player player, StatusPanel statusPanel,
                      TextBox answerTextBox,
                      Label questionLabel, Action onGameComplete, Action onGameOver)
        {
            _player = player;
            _answerTextBox = answerTextBox;
            _questionLabel = questionLabel;
            _onGameComplete = onGameComplete;
            _onGameOver = onGameOver;
            _questions = TrueFalseQuestions.GetQuestions();
            _currentQuestionIndex = 0;
            _isActive = false;
            _correctHandler = new CorrectAnswerHandler(player, statusPanel, onGameComplete, onGameComplete);
            _wrongHandler = new WrongAnswerHandler(player, statusPanel, onGameComplete, onGameOver, onGameComplete);
        }

        public void Start()
        {
            _isActive = true;
            _currentQuestionIndex = 0;
            ShowQuestion();
        }

        public void Stop()
        {
            _isActive = false;
        }

        public void ShowQuestion()
        {
            if (!_isActive) return;

            if (_currentQuestionIndex >= _questions.Count)
            {
                CompleteGame();
                return;
            }

            QuestionData question = _questions[_currentQuestionIndex];
            _questionLabel.Text = question.Text;
            _answerTextBox.Clear();
            _answerTextBox.Visible = true;
            _answerTextBox.Focus();
        }

        public void CheckAnswer(string userAnswer)
        {
            if (!_isActive) return;
            if (_currentQuestionIndex >= _questions.Count)
            {
                return;
            }
            if (userAnswer != "1" && userAnswer != "2" && userAnswer != "3")
            {
                MessageBox.Show("Введите число 1, 2 или 3", 
                    "Неверный ввод", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                _answerTextBox.Clear();
                _answerTextBox.Focus();
                return;
            }
            int selected = Convert.ToInt32(userAnswer);
            selected--;
            QuestionData currentQuestion = _questions[_currentQuestionIndex];
            bool isCorrect = (selected == currentQuestion.CorrectOptionIndex);

            if (isCorrect)
            {
                _correctHandler.Handle();
            }
            else
            {
                _wrongHandler.Handle();

                if (!_wrongHandler.IsPlayerAlive())
                {
                    _isActive = false;
                    return;
                }
            }
            _answerTextBox.Clear();
            _currentQuestionIndex++;
            if (_currentQuestionIndex < _questions.Count && _player.CurrentHealth > 0)
            {
                ShowQuestion();
            }
            else
            {
                CompleteGame();
            }
        }

        private void CompleteGame()
        {
            _isActive = false;
            _answerTextBox.Visible = false;
            _onGameComplete?.Invoke();
        }

        public void Reset()
        {
            _currentQuestionIndex = 0;
        }

        public bool IsActive => _isActive;
    }
}