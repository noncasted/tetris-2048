using System;
using System.Collections.Generic;

namespace Tools.EditorTools
{
    [Serializable]
    public class AsmdefFile
    {
        public string name;
        public List<string> references = new();
    }
}