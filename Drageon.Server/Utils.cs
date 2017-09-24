using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drageon.Server
{
    static class Utils
    {
        static ColorConverter converter = new ColorConverter();
        static Utils()
        {

        }
        public static Color GetColor(string colorName)
        {
            return (Color)converter.ConvertFromString(colorName);
        }
    }
}