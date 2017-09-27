using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Drageon.Server
{
    class PluginCore
    {
        List<Assembly> PluginList;




        public PluginCore()
        {
            PluginList = new List<Assembly>();
            string[] path = Directory.GetFiles("./Plugin/", "*.*.plug");
            foreach(string s in path)
            {
                PluginList.Add(Assembly.LoadFile("./Plugin/" + s));
            }
        }
    }
}