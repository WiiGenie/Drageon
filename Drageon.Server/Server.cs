using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Drageon.Server
{
    public partial class Server : Form
    {
        BackgroundWorker worker;
        HttpListener httpListener;
        Hashtable client;
        
        public Server()
        {
            InitializeComponent();
            httpListener = new HttpListener();
            client = new Hashtable();
            
            worker = new BackgroundWorker();
            worker.DoWork += Init;
            worker.RunWorkerAsync();
        }

        public enum MsgType
        {
            /// <summary>
            /// 基本信息
            /// </summary>
            Basic,
            /// <summary>
            /// 系统日志信息
            /// </summary>
            System,
            /// <summary>
            /// 用户发送信息
            /// </summary>
            User,
            /// <summary>
            /// 插件反馈信息
            /// </summary>
            Plugin,
            /// <summary>
            /// 严重错误信息
            /// </summary>
            Error,
            /// <summary>
            /// 错误警告信息
            /// </summary>
            Warnning
        }
        private void Output(string text, MsgType msgType)
        {
            Action action = () =>
            {
                logBox.Select();
                logBox.SelectionColor = GetColor(msgType);
                logBox.SelectedText = "\r\n[" + Enum.GetName(typeof(MsgType), msgType) + "]" + text;
            };

            logBox.Invoke(action);
        }
        private void Output(string text)
        {
            Output(text, MsgType.Basic);
        }

        private void Init(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(200);
            Output("服务器初始化中......", MsgType.System);
            Thread.Sleep(800);
            Output("服务器初始化完成！", MsgType.System);
            Output("服务器开始监听用户链接...", MsgType.System);
            worker.DoWork += ListenLoop;
            worker.DoWork -= Init;
            worker.RunWorkerAsync();
        }
        private void ListenLoop(object sender, DoWorkEventArgs e)
        {
            while (httpListener.IsListening)
            {
                httpListener.GetContext();
            }
        }

        private Color GetColor(MsgType colorCode)
        {
            Color color = Color.Black;

            switch (colorCode)
            {
                case MsgType.System: color = Color.DarkSlateBlue; break;
                case MsgType.User: color = Color.Purple; break;
                case MsgType.Plugin: color = Color.LimeGreen; break;
                case MsgType.Error: color = Color.Crimson; break;
                case MsgType.Warnning: color = Color.Maroon; break;
            }

            return color;
        }

        private void Interpreter(string text)
        {
            if (text.Substring(0, 1) == "/")
                Command(text.Substring(1));
        }

        private void Command(string command)
        {
            string[] args = command.Split(' ');

        }

        [STAThread]
        public static void Main(string[] args)
        {
            Server start = new Server();
            Application.Run(start);
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(inputBox.Text))
                Interpreter(inputBox.Text);
        }
    }
}
