using System;
using System.Collections.Generic;
using TestApp.Class.DataFiles;
using TestApp.Class.Managment;

namespace TestApp.Class.Game
{
    /// <summary>
    ///В классе происходит логика игры
    /// </summary>
    class GameLogic : Information
    {
        internal int counters { get; private set; } //предоставляет счет слов в предложении для чтения
        internal static List<GameItems> gamers { get; private set; } //список игроков
        internal static int quantity { get; private set; } //количество предложений
        private List<int> indexSentance { get; set; } //список для рандома
        private bool installization = false; //флаг на инициализацию списка
        private bool generated = false; //флаг на инициализацию списка
        private static int number; //номер предложения
        private Content content = new Content(); //экземпляр класса
        private static LastGameHistory history = new LastGameHistory(); //экземпляр класса
        /// <summary>
        /// Метод увеличивает счет неправильных слов
        /// </summary>
        /// <param name="player">Индекс игрока</param>
        internal void ImpoveErrors(in int player)=>gamers[player].Errors++;
        /// <summary>
        /// Метод увеличивает счет правильных слов
        /// </summary>
        /// <param name="player">Индекс игрока</param>
        internal void ImpoveCorrect(in int player)=>gamers[player].Correct++;
        /// <summary>
        /// Метод прибавляет время, затраченное на слово
        /// </summary>
        /// <param name="player">Индекс игрока</param>
        /// <param name="time">Затраченное время</param>
        internal void SaveTimeOnWord(in int player, int time) => gamers[player].TimeOnWord += time;
        /// <summary>
        /// Метод инициализтрует список, если надо
        /// </summary>
        private void CheckNotNull()
        {
            if (installization == false)
            {
                gamers = new List<GameItems>();
                history.ReadTextFile(out List<string> list);
                for (int i = 0; i < Convert.ToInt32(list[2]); i++)
                    gamers.Add(new GameItems($"Игрок {i+1}")) ;
                installization = true;
            }
        }
        internal void Restart()
        {
            for (int i = 0; i < gamers.Count; i++)
            {
                gamers[i].Correct = 0;
                gamers[i].Errors = 0;
                gamers[i].TimeOnWord = 0;
                Message = null;
            }
        }
        /// <summary>
        /// Метод проверяет введенное слово совпадает с ожидаемым введенным словом
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="index">Индекс слова</param>
        /// <returns>Совпало или нет</returns>
        internal bool CheckWord(in string word,in int index)
        {
            content.ReadTextFile(out List<string> sentance);
            string[] words = sentance[number].Trim().Split(' ');
            counters=words.Length;  
            Message = index == words.Length-1 ?"конец": "нет";
            return words[index].ToLower()==word.ToLower()? true : false;
        }
        /// <summary>
        /// Метод выводит тип для программы
        /// </summary>
        /// <returns>Тип</returns>
        private string GetType()
        {
            history.ReadTextFile(out List<string> output);
            string type = output[0] == "0" ? "RU" : "EN";
            type += output[1] == "0" ? "_EASY_" : output[1] == "1" ? "_MIDDLE_" : "_HARD_";
            return type;
        }
        /// <summary>
        /// Метод генерирует случайное число от 0 до количества предлодений по типу.
        /// </summary>
        /// <returns>Выводит случайное число</returns>
        private int RandomSentances()
        {
            int output=0;
            Random rnd = new Random();
            while (true)
            {
                output = rnd.Next(0, quantity);
                if (indexSentance.Count < 1)
                {
                    indexSentance.Add(output);
                    break;
                }
                else
                {
                    for(int i = 0; i < indexSentance.Count; i++)
                    {
                        if(indexSentance[i] != output)
                        {
                            indexSentance.Add(output);
                            break;
                        }    
                    }
                    if (indexSentance[indexSentance.Count - 1] == output && indexSentance.Count>1)
                        break;
                }
            }
            return output;
        }
        /// <summary>
        /// Метод проверяет игра закончена или нет
        /// </summary>
        /// <param name="player">Индекс игрока</param>
        /// <returns>Игра завершена или нет</returns>
        internal bool EndGame(in int player)
        {
            if ((player == gamers.Count - 1) && Message == "конец")
                return true;
            else
                return false;
        }
        /// <summary>
        /// Метод, который выводит предложение, которое надо показать
        /// </summary>
        /// <returns>Предложение</returns>
        internal string GetSentances()
        {
            CheckNotNull();
            content.SetLanguageLevel(GetType());
            content.ReadTextFile(out List<string> sentance);
            if(sentance.Count > 0)
            {
                quantity = sentance.Count;
                if (generated == false)
                {
                    indexSentance = new List<int>(quantity);
                    generated = true;
                }
                if(quantity>1)
                    number = RandomSentances();
                Message = "нет";
                return sentance[number];
            }
            else
                return null;
        }
    }
}
