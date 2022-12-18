using System.Xml;
using System.Collections.Generic;

namespace TestApp.Class.DataFiles
{
    /// <summary>
    /// Класс, в котором проимходит чтение и запись предложений по типу
    /// </summary>
    class Content:DefaultData
    {
        private string file = @"/Settings/Content.xml";
        private static string name;
        /// <summary>
        /// Метод Устанавливает тип
        /// </summary>
        /// <param name="value"> тип(уровень + язык)</param>
        internal void SetLanguageLevel(string value) => name = value;
        /// <summary>
        /// Метод производит чтение из файла, где хранятся предложения и предоставляет список опредленного типа предложений
        /// </summary>
        /// <param name="output">Лист типа string</param>
        public override void ReadTextFile(out List<string> output)
        {
            output = new List<string>();
            XmlDocument xml = new XmlDocument();
            xml.Load(GetPath() + file);
            XmlElement xRoot = xml.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement element in xRoot)
                {
                    var attrubute =element.Attributes.GetNamedItem("name");
                    for(int i = 0; i < element.ChildNodes.Count; i++)
                    {
                        if (attrubute.Value.StartsWith(name))
                            output.Add(element.ChildNodes[i].InnerText);
                    }
                }
            }
        }
        /// <summary>
        /// Метод считает количества предложений опредленного типа
        /// </summary>
        /// <param name="name">Тип предложения</param>
        /// <returns>Количество предложений</returns>
      internal int Counting(string name)
        {
            int counter = 0;
            XmlDocument xml = new XmlDocument();
            xml.Load(GetPath() + file);
            XmlElement xRoot = xml.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement element in xRoot)
                {
                    var attrubute = element.Attributes.GetNamedItem("name");
                    for (int i = 0; i < element.ChildNodes.Count; i++)
                    {
                        if (attrubute.Value.StartsWith(name))
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
        /// <summary>
        /// Метод добавляет предложения для каждого типа(уровень+язык)
        /// </summary>
        /// <param name="input">Список типа string</param>
        public override void WriteTextFile(ref List<string> input)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(GetPath() + file);
            XmlElement xRoot = xml.DocumentElement;
            XmlElement personElem = xml.CreateElement("LevelLanguage");
            XmlAttribute nameAttr = xml.CreateAttribute("name");
            XmlElement companyElem = xml.CreateElement("Sentence");
            XmlText nameText = xml.CreateTextNode(name);
            XmlText companyText = xml.CreateTextNode(input[0]);
            nameAttr.AppendChild(nameText);
            companyElem.AppendChild(companyText);
            personElem.Attributes.Append(nameAttr);
            personElem.AppendChild(companyElem);
            xRoot?.AppendChild(personElem);
            xml.Save(GetPath() + file);
            input.Clear();
        }
    }
}
