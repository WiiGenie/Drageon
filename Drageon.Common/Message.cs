using System;
using System.Xml;
using System.Xml.Serialization;

namespace Drageon.Common
{
    [Serializable]
    public class Message
    {
        /// <summary>
        /// 消息来源
        /// </summary>
        [XmlElement(Order = 1)]
        public string FromUser { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [XmlElement(Order = 2)]
        public string MsgText { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [XmlElement(Order = 3)]
        public MsgType Type { get; set; }

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
        public Message(string fromUser, string msgText, MsgType type)
        {
            FromUser = fromUser;
            MsgText = msgText;
            Type = type;
        }
        public Message(string msgXml)
        {
            Message temp = XmlUtils.Deserialize<Message>(msgXml);
            FromUser = temp.FromUser;
            MsgText = temp.MsgText;
            Type = temp.Type;
        }
        public Message()
        {
            FromUser = "";
            MsgText = "";
            Type = MsgType.Basic;
        }
        public override string ToString()
        {
            return XmlUtils.Serializer(this);
        }
    }
}
