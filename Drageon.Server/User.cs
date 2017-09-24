using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Drageon.Server
{
    public class User
    {
        string Name;
        List<Message> MsgBox;
        
        public User(string name)
        {
            Name = name;
            MsgBox = new List<Message>();
        }

        public void SendMsg(HttpListenerResponse response)
        {
            StreamWriter builder = new StreamWriter(response.OutputStream);
        }
    }
}