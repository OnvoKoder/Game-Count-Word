namespace TestApp.Class.Game
{
    /// <summary>
    /// Класс, в котором описаны поля для игрока
    /// </summary>
    class GameItems
    {
        internal string Name { get;set; }
        internal double Correct { get; set; }
        internal int Errors { get; set; }
        internal double TimeOnWord { get; set; }
        internal GameItems(string name)
        {
            Name = name;
            Correct = 0;
            Errors = 0;
            TimeOnWord = 0;
        }
    }
}
