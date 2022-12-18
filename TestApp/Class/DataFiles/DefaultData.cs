using System;
using System.Collections.Generic;
using System.IO;
using TestApp.Class.Interface;

namespace TestApp.Class.DataFiles
{
    /// <summary>
    /// Класс реализует интерфейс и дает переопределять методы
    /// </summary>
    class DefaultData : IDataFile
    {
        /// <summary>
        /// Чтнение текстового файла
        /// </summary>
        /// <param name="output">Список типа string</param>
        /// <exception cref="NotImplementedException">ничего не делать</exception>
        virtual public void ReadTextFile(out List<string> output)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Запись в текстовый файл
        /// </summary>
        /// <param name="input">Список типа string</param>
        /// <exception cref="NotImplementedException">ничего не делать</exception>

        virtual public void WriteTextFile(ref List<string> input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Получение пути
        /// </summary>
        /// <returns>Путь</returns>
        protected internal string GetPath()
        {
            string[] path = Directory.GetCurrentDirectory().Split(new string[] { "bin" }, StringSplitOptions.None);
            return path[0];
        } 
    }
}
