using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static Drageon.Server.Server;

namespace Drageon.Server
{
    public class Message
    {
        static JavaScriptSerializer Serializer = new JavaScriptSerializer();
        string FromUser;
        string MsgText;
        MsgType Type;

        public Message(string fromUser, string msgText, MsgType type)
        {
            FromUser = fromUser;
            MsgText = msgText;
            Type = type;
        }
        public Message(string msg)
        {
            Message temp = (Message)Serializer.Deserialize(msg, typeof(Message));
            FromUser = temp.FromUser;
            MsgText = temp.MsgText;
            Type = temp.Type;
        }
        public override string ToString()
        {
            return Serializer.Serialize(this);
        }
    }
}
