using System.Xml;
using System.Collections.Generic;

namespace TestApp.Class.DataFiles
{
    /// <summary>
    /// Класс записывает и читает информацию о последней игре
    /// </summary>
    class LastGameHistory:DefaultData
    {
        private string file = @"/Settings/History.xml";
        /// <summary>
        /// Метод читает последнюю игру
        /// </summary>
        /// <param name="output">Выводит список: уровень, язык и кол-во людей</param>
        public override void ReadTextFile(out List<string> output)
        {
            output = new List<string>();
            XmlDocument xml = new XmlDocument();
            xml.Load(GetPath()+file);
            XmlElement xRoot = xml.DocumentElement;
            if (xRoot != null)
            {
                foreach(XmlElement element in xRoot)
                {
                    for (int i = 0; i < element.ChildNodes.Count; i++)
                        output.Add(element.ChildNodes[i].InnerText);
                }
            }
        }
        /// <summary>
        /// Метод записывает последнюю игру
        /// </summary>
        /// <param name="input">Вводит список: уровень, язык и кол-во людей</param>
        public override void WriteTextFile(ref List<string> input)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(GetPath() + file);
            XmlElement xRoot = xml.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement element in xRoot)
                {
                    for (int i = 0; i < element.ChildNodes.Count; i++)
                        element.ChildNodes[i].InnerText = input[i];
                }
            }
            xml.Save(GetPath() + file);
            input.Clear();
        }
    }
}
