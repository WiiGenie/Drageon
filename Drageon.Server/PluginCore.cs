using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Drageon.Server
{
    class PluginCore
    {
        Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "/Plugin/");
    }
}
