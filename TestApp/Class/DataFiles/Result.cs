using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestApp.Class.DataFiles
{
    /// <summary>
    /// Результаты игр(прогресс)
    /// </summary>
    class Result :DefaultData
    {
        private string file = @"/Settings/result.log";
        internal Result()
        {
            if (!File.Exists(GetPath() + file))
                File.Create(GetPath() + file).Close();
        }
        /// <summary>
        /// Метод читает из файла данных
        /// </summary>
        /// <param name="output">Список типа string</param>
        public override void ReadTextFile(out List<string> output)
        {
            output = new List<string>();
            for(int i = 0; i < File.ReadLines(GetPath()+file).Count(); i++)
                output.Add(File.ReadLines(GetPath()+file).ElementAt(i).Trim());
        }
        /// <summary>
        /// Записывает данные в тектосвый файл
        /// </summary>
        /// <param name="input">Список типа string</param>
        public override void WriteTextFile(ref List<string> input)
        {
            using (StreamWriter writer = new StreamWriter(GetPath() + file, true))
            {
                for(int i = 0; i < input.Count; i++)
                {
                    writer.WriteLine(input[i]);
                }
            }
            input.Clear();
        }
    }
}
