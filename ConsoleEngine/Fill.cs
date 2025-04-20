using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Fill
    {
        public static List<Fill> fill = new List<Fill>();

        public int X;
        public int Y;
        public string ColorFG;
        public string ColorBG;
        public Fill(int x, int y, string colorFG, string colorBG)
        {
            X = x;
            Y = y;
            ColorFG = colorFG;
            ColorBG = colorBG;
        }
    }
}
