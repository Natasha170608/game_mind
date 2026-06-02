using QuizeGame;
using System;

namespace WinFormsApp12
{
    public class CorrectAnswerHandler
    {
        private Player _player;
        private StatusPanel _statusPanel;
        private Action _onLevelComplete;
        private Action _onGameComplete;

        public CorrectAnswerHandler(Player player, StatusPanel statusPanel,
                                    Action onLevelComplete, Action onGameComplete)
        {
            _player = player;
            _statusPanel = statusPanel;
            _onLevelComplete = onLevelComplete;
            _onGameComplete = onGameComplete;
        }

        public bool Handle()
        {
            if (_player.CurrentHealth <= 0) return false;

            _player.AddQuestion();
            _player.AddAnswers();
            _statusPanel?.UpdateStatus(_player);

            if (_player.Question >= _player.TotalAnswers && _player.CurrentHealth > 0)
            {
                _onGameComplete?.Invoke();
                return true;
            }

            return true;
        }

        public void HandleWithLevelComplete()
        {
            if (_player.CurrentHealth <= 0) return;

            _player.AddQuestion();
            _player.AddAnswers();
            _statusPanel?.UpdateStatus(_player);

            if (_player.Question >= _player.TotalAnswers && _player.CurrentHealth > 0)
            {
                _onLevelComplete?.Invoke();
            }
        }
    }
}