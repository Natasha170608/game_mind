using QuizeGame;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public class Level1
    {
        private List<QuestionData> _questions;
        private int _currentQuestionIndex;
        private Player _player;
        private TextBox _answerTextBox;
        private Label _questionLabel;
        private CorrectAnswerHandler _correctHandler;
        private WrongAnswerHandler _wrongHandler;
        private Action _onLevelComplete;
        private Action _onGameOver;
        private bool _isActive;

        public Level1(Player player, StatusPanel statusPanel, TextBox answerTextBox, Label questionLabel,
                      Action onLevelComplete, Action onGameOver, Action onGameComplete)
        {
            _player = player;
            _answerTextBox = answerTextBox;
            _questionLabel = questionLabel;
            _onLevelComplete = onLevelComplete;
            _onGameOver = onGameOver;
            _questions = MathQuestions.GetQuestions();
            _currentQuestionIndex = 0;
            _isActive = false;

            _correctHandler = new CorrectAnswerHandler(player, statusPanel, onLevelComplete, onGameComplete);
            _wrongHandler = new WrongAnswerHandler(player, statusPanel, onLevelComplete, onGameOver, onGameComplete);
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
                CompleteLevel();
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

            if (!double.TryParse(userAnswer.Replace('.', ','), out double userValue))
            {
                MessageBox.Show("Ошибка! Введите число.", "Неверный ввод",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _answerTextBox.Clear();
                _answerTextBox.Focus();
                return;
            }

            QuestionData currentQuestion = _questions[_currentQuestionIndex];
            int correctIndex = currentQuestion.CorrectOptionIndex;
            string correctAnswerText = currentQuestion.Options[correctIndex];
            double correctValue = Convert.ToDouble(correctAnswerText.Replace('.', ','));

            bool isCorrect = (userValue == correctValue);

            if (isCorrect)
            {
                _correctHandler.HandleWithLevelComplete();
            }
            else
            {
                _wrongHandler.HandleWithLevelComplete();

                if (!_wrongHandler.IsPlayerAlive())
                {
                    _isActive = false;
                    return;
                }
            }

            _answerTextBox.Clear();
            _currentQuestionIndex++;

            if (_currentQuestionIndex < _questions.Count && _player.CurrentHealth > 0)
                ShowQuestion();
        }

        private void CompleteLevel()
        {
            _isActive = false;
            _answerTextBox.Visible = false;
            _onLevelComplete?.Invoke();
        }

        public void Reset()
        {
            _currentQuestionIndex = 0;
        }

        public bool IsActive => _isActive;
    }
}