using QuizeGame;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public class Level2
    {
        private List<QuestionData> _questions;
        private int _currentQuestionIndex;
        private Player _player;
        private Button _optionOneButton;
        private Button _optionTwoButton;
        private Button _optionThreeButton;
        private Label _questionLabel;
        private CorrectAnswerHandler _correctHandler;
        private WrongAnswerHandler _wrongHandler;
        private Action _onLevelComplete;
        private Action _onGameOver;
        private bool _isActive;

        public Level2(Player player, StatusPanel statusPanel,
                      Button optionOne, Button optionTwo, Button optionThree,
                      Label questionLabel, Action onLevelComplete, Action onGameOver, Action onGameComplete)
        {
            _player = player;
            _optionOneButton = optionOne;
            _optionTwoButton = optionTwo;
            _optionThreeButton = optionThree;
            _questionLabel = questionLabel;
            _onLevelComplete = onLevelComplete;
            _onGameOver = onGameOver;
            _questions = RiddleQuestions.GetQuestions();
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

            _optionOneButton.Text = question.Options[0];
            _optionTwoButton.Text = question.Options[1];
            _optionThreeButton.Text = question.Options[2];

            _optionOneButton.Visible = true;
            _optionTwoButton.Visible = true;
            _optionThreeButton.Visible = true;
        }

        public void CheckAnswer(int selectedIndex)
        {
            if (!_isActive) return;
            if (_currentQuestionIndex >= _questions.Count)
            {
                return;
            }
            QuestionData currentQuestion = _questions[_currentQuestionIndex];
            bool isCorrect = (selectedIndex == currentQuestion.CorrectOptionIndex);

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

            _currentQuestionIndex++;
            if (_currentQuestionIndex >= _questions.Count)
            {
                CompleteLevel();
                return;
            }
            if (_player.CurrentHealth > 0)
            {
                ShowQuestion();
            }
        }

        private void CompleteLevel()
        {
            _isActive = false;
            _optionOneButton.Visible = false;
            _optionTwoButton.Visible = false;
            _optionThreeButton.Visible = false;
            _onLevelComplete?.Invoke();
        }

        public void Reset()
        {
            _currentQuestionIndex = 0;
        }

        public bool IsActive => _isActive;
    }
}