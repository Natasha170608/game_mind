using QuizeGame;
using System;

namespace WinFormsApp12
{
    public class WrongAnswerHandler
    {
        private Player _player;
        private StatusPanel _statusPanel;
        private Action _onLevelComplete;
        private Action _onGameOver;
        private Action _onGameComplete;

        public WrongAnswerHandler(Player player, StatusPanel statusPanel,
                                  Action onLevelComplete, Action onGameOver, Action onGameComplete)
        {
            _player = player;
            _statusPanel = statusPanel;
            _onLevelComplete = onLevelComplete;
            _onGameOver = onGameOver;
            _onGameComplete = onGameComplete;
        }

        public bool Handle()
        {
            if (_player.CurrentHealth <= 0) return false;

            _player.AddQuestion();
            _player.AddWrong();
            _player.CurrentHealth--;
            _statusPanel?.UpdateStatus(_player);

            if (_player.CurrentHealth <= 0)
            {
                _onGameOver?.Invoke();
                return false;
            }

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
            _player.AddWrong();
            _player.CurrentHealth--;
            _statusPanel?.UpdateStatus(_player);

            if (_player.CurrentHealth <= 0)
            {
                _onGameOver?.Invoke();
                return;
            }

            if (_player.Question >= _player.TotalAnswers && _player.CurrentHealth > 0)
            {
                _onLevelComplete?.Invoke();
            }
        }

        public bool IsPlayerAlive()
        {
            return _player.CurrentHealth > 0;
        }

        public bool IsLevelCompleted()
        {
            return _player.Question >= _player.TotalAnswers && _player.CurrentHealth > 0;
        }
    }
}