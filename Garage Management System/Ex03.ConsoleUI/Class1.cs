using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    class Class1
    {

        public static void Main()
        {
            object color = Enum.ToObject(typeof(ecolors), 2);
            ecolors e = (ecolors)color;
            Console.WriteLine(e.ToString());
        }

        public enum ecolors
        {
            black = 1,
            white = 2,
            yellow = 3,
            blue = 4
        }

    }
}
