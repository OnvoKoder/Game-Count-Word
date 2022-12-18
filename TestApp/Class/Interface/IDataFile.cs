using System.Collections.Generic;

namespace TestApp.Class.Interface
{
    interface IDataFile
    {
        void ReadTextFile(out List<string> output);
        void WriteTextFile(ref List<string> input);
    }
}
