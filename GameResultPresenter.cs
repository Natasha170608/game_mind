using QuizeGame;
using System;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public class GameResultPresenter
    {
        private Player _player;
        private Action _onNewGame;
        private Action _onExit;

        public GameResultPresenter(Player player, Action onNewGame, Action onExit)
        {
            _player = player;
            _onNewGame = onNewGame;
            _onExit = onExit;
        }

        public void ShowWinResult()
        {
            string message = $"ПОБЕДА! \nВерных ответов: {_player.CorrectAnswers}\n" +
                             $"Неверных ответов: {_player.WrongAnswer}\n" +
                             $"Всего вопросов: {_player.TotalAnswers}\n\n" +
                             $"Вы успешно прошли викторину!\n\n" +
                             $"Да - Новая игра\n" +
                             $"Нет - Главное меню\n" +
                             $"Отмена - Выход";

            ShowResultDialog(message, "ПОБЕДА!");
        }

        public void ShowLoseResult()
        {
            string message = $"ИГРА ОКОНЧЕНА\n" +
                             $"Вы проиграли!\n" +
                             $"Верных ответов: {_player.CorrectAnswers}\n" +
                             $"Неверных ответов: {_player.WrongAnswer}\n\n" +
                             $"Да - Новая игра\n" +
                             $"Нет - Главное меню\n" +
                             $"Отмена - Выход";

            ShowResultDialog(message, "ИГРА ЗАВЕРШЕНА");
        }

        private void ShowResultDialog(string message, string title)
        {
            DialogResult result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    _onNewGame?.Invoke();
                    break;
                case DialogResult.No:
                    _onExit?.Invoke();
                    break;
                case DialogResult.Cancel:
                    Application.Exit();
                    break;
            }
        }
    }
}