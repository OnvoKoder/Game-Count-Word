using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TestApp.Class.DataFiles;
using TestApp.Class.Game;

namespace TestApp.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        static private int counter=10;//время для таймера на слово
        private  int player=0;//номер игрока
        private int newWord = 0;//номер слова
        private int newSentsnces = 0;//номер слова
        private bool restart = false;
        private GameLogic logic = new GameLogic();//экземпляр класса
        DispatcherTimer timer = new DispatcherTimer();//экземпляр класса 
        public GamePage()
        {
            InitializeComponent();
            History history = new History();
            List<string> list = new List<string>();
            list.Add(DateTime.Now + " игра началась");
            history.WriteTextFile(ref list);
        }
        /// <summary>
        /// Рендеринг предложения, информации о игроке
        /// </summary>
        private void RenderText()
        {
            tbText.TextWrapping = TextWrapping.Wrap;
            
            if (logic.Message==null || (logic.Message=="конец" && 0 == player))
            {
                tbText.Text = logic.GetSentances();
                if (tbText.Text == "")
                {
                    timer.Stop();
                    MessageBox.Show("У выбранного типа нет предложений - заполни предложения");
                }
                else
                    newSentsnces++;
            }
            else
            {
                tbInformation.Text = $"Введите {newWord + 1} слово из предложения";
                tbCorrect.Text = GameLogic.gamers[player].Correct.ToString();
                tbError.Text = GameLogic.gamers[player].Errors.ToString();
                tbIndex.Text = GameLogic.gamers[player].Name.ToString();
            }
   
        }
        /// <summary>
        /// Перезапуск
        /// </summary>
        private void Restart()
        {
            if (logic.Message == "конец")
            {
                if (GameLogic.gamers.Count - 1 == player)
                {
                    if (GameLogic.quantity == newSentsnces)
                        EndGame();
                    else
                        player = 0;
                }
                else player++;
                newWord = 0;
            }
            if (restart == false)
            {
                counter = 10;
                timer.Start();
                txtWord.Text = "";
                RenderText();
            }
        }
        private void EndGame()
        {
            if (logic.EndGame(player) == true)
            {
                timer.Stop();
                Result result = new Result();
                double wordPerSecond = 0;
                List<string> list = new List<string>();
                for (int i = 0; i < player + 1; i++)
                {
                    wordPerSecond = GameLogic.gamers[i].TimeOnWord < 60 ? GameLogic.gamers[i].Correct : Math.Round(GameLogic.gamers[i].Correct / 60, 2);
                    if (GameLogic.gamers[i].TimeOnWord > 60)
                        list.Add($"{GameLogic.gamers[i].Name} - {wordPerSecond} слов в минуту");
                    else
                        list.Add($"{GameLogic.gamers[i].Name} - {wordPerSecond} слов за {GameLogic.gamers[i].TimeOnWord} секунд");
                }
                result.WriteTextFile(ref list);
                MessageBox.Show("Конец игры");
                restart = true;
                logic.Restart();
            }
        }
        /// <summary>
        /// Реализация таймера
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (counter > 0) counter--;
            else
            {
                timer.Stop();
                MessageBox.Show("Вводи следующее слово");
                newWord++;
            }
            tbTime.Text = counter.ToString();
        }
        /// <summary>
        /// Реализация нажатие на кнопку ENTER. Отправка в логику игры данные
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void txtWord_PreviewKeyDown(object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool solute=logic.CheckWord(txtWord.Text.Trim(),newWord);

                if (solute == true)
                    logic.ImpoveCorrect(player);
                else
                    logic.ImpoveErrors(player);
                newWord++;
                logic.SaveTimeOnWord(player, 10 - counter);
                Restart();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            RenderText();
        }
    }
}
