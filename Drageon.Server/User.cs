using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drageon.Server
{
    public class User
    {
        string Name;
        List<Message> MsgBox;
        HttpListenerContext Context;
        
        public User(string name)
        {
            Name = name;
            MsgBox = new List<Message>();
        }

        public void SetContext(HttpListenerContext context)
        {
            Context = context;
        }
        public void SetMsg(Message msg)
        {
            lock (this)
            {
                MsgBox.Add(msg);
                MsgUpdata();
            }
        }

        private void GetNewMsg()
        {
            if (MsgBox.Count != 0)
            {
                StreamWriter writer = new StreamWriter(Context.Response.OutputStream);
                lock (this)
                {
                    for (int i = 0; i < MsgBox.Count; i++)
                    {
                        writer.WriteLine(MsgBox[i].ToString());
                        writer.Close();
                    }
                    MsgBox.Clear();
                    Context.Response.Close();
                }
            }
        }
        #region    未读消息盒子更新事件
        private delegate void UserMsgUpdataHandler();
        private event UserMsgUpdataHandler UserMsgUpdata;
        private void MsgUpdata()
        {
            UserMsgUpdata?.Invoke();
        }
        #endregion
    }
}
