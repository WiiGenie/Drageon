using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JuerLibrary.FileManager
{
    public class INIManager
    {
        private string path;

        public INIManager(string path)
        {
            this.path = path;
            if (!File.Exists(path))
                File.Create(path);
        }
        /// <summary>
        /// 读取指定节点中的指定键值。
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <param name="defaultValue">默认返回值，可空</param>
        /// <returns></returns>
        public T Read<T>(string section, string key, string defaultValue = null)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentException("section参数不能为空！请提供一个节点名", "section");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("key参数不能为空！请提供一个键名", "key");
            if (string.IsNullOrWhiteSpace(defaultValue))
                defaultValue = string.Empty;

            StringBuilder value = new StringBuilder(1024);
            API.GetPrivateProfileString(section, key, defaultValue, value, 1024, path);

            return (T)Convert.ChangeType(value, typeof(T));
        }
        /// <summary>
        /// 将值写到指定节点中的指定键
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <param name="value">欲写入值</param>
        /// <returns></returns>
        public bool Write(string section, string key, string value)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentException("参数不能为空！请提供一个节点名", "section");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("参数不能为空！请提供一个键名", "key");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("参数不能为空！请提供一个写入值", "value");

            return API.WritePrivateProfileString(section, key, value, path);
        }
        /// <summary>
        /// 删除指定节点中的指定键值，如果不指定键值，则清空该节点
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键名
        /// <para>如果该值为空，则清空节点下的所有键</para>
        /// </param>
        /// <returns></returns>
        public bool Delect(string section, string key = null)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentException("参数不能为空！请提供一个节点名", "section");

            return API.WritePrivateProfileString(section, key, null, path);
        }
        /// <summary>
        /// 节点名称列表
        /// </summary>
        public string[] Section
        {
            get
            {
                string[] value = null;
                IntPtr memory = Marshal.AllocCoTaskMem(65535);
                uint length = API.GetPrivateProfileSectionNames(memory, 65535, path);
                
                if (length != 65533 && length != 0)
                {
                    value = Marshal.PtrToStringAuto(memory, (int)length).Split(new char[] { '\0' });
                }

                Marshal.FreeCoTaskMem(memory);
                return value;
            }
        }
        /// <summary>
        /// 取指定节点下的所有键值对，键值对的形式为 key=value
        /// </summary>
        /// <param name="section">节点名</param>
        /// <returns>如果获取失败则返回 null</returns>
        public string[] GetItem(string section)
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentException("参数不能为空！请提供一个节点名", "section");
            string[] value = null;
            IntPtr memory = Marshal.AllocCoTaskMem(65535);
            uint length = API.GetPrivateProfileSection(section, memory, 65535, path);

            if (length != 65533 && length != 0)
            {
                value = Marshal.PtrToStringAuto(memory, (int)length).Split(new char[] { '\0' });
            }

            Marshal.FreeCoTaskMem(memory);
            return value;
        }

        public static class API
        {
            #region 引用API

            /// <summary>  
            /// 获取所有节点名称(Section)  
            /// </summary>  
            /// <param name="lpszReturnBuffer">存放节点名称的内存地址,每个节点之间用\0分隔</param>  
            /// <param name="nSize">内存大小(characters)</param>  
            /// <param name="lpFileName">Ini文件</param>  
            /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

            /// <summary>  
            /// 获取某个指定节点(Section)中所有KEY和Value  
            /// </summary>  
            /// <param name="lpAppName">节点名称</param>  
            /// <param name="lpReturnedString">返回值的内存地址,每个之间用\0分隔</param>  
            /// <param name="nSize">内存大小(characters)</param>  
            /// <param name="lpFileName">Ini文件</param>  
            /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

            /// <summary>  
            /// 读取INI文件中指定的Key的值  
            /// </summary>  
            /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>  
            /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>  
            /// <param name="lpDefault">读取失败时的默认值</param>  
            /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>  
            /// <param name="nSize">内容缓冲区的长度</param>  
            /// <param name="lpFileName">INI文件名</param>  
            /// <returns>实际读取到的长度</returns>  
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);

            //另一种声明方式,使用 StringBuilder 作为缓冲区类型的缺点是不能接受\0字符，会将\0及其后的字符截断,  
            //所以对于lpAppName或lpKeyName为null的情况就不适用  
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

            //再一种声明，使用string作为缓冲区的类型同char[]  
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

            /// <summary>  
            /// 将指定的键值对写到指定的节点，如果已经存在则替换。  
            /// </summary>  
            /// <param name="lpAppName">节点，如果不存在此节点，则创建此节点</param>  
            /// <param name="lpString">Item键值对，多个用\0分隔,形如key1=value1\0key2=value2  
            /// <para>如果为string.Empty，则删除指定节点下的所有内容，保留节点</para>  
            /// <para>如果为null，则删除指定节点下的所有内容，并且删除该节点</para>  
            /// </param>  
            /// <param name="lpFileName">INI文件</param>  
            /// <returns>是否成功写入</returns>  
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]     //可以没有此行  
            public static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

            /// <summary>  
            /// 将指定的键和值写到指定的节点，如果已经存在则替换  
            /// </summary>  
            /// <param name="lpAppName">节点名称</param>  
            /// <param name="lpKeyName">键名称。如果为null，则删除指定的节点及其所有的项目</param>  
            /// <param name="lpString">值内容。如果为null，则删除指定节点中指定的键。</param>  
            /// <param name="lpFileName">INI文件</param>  
            /// <returns>操作是否成功</returns>  
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

            #endregion
        }
    }
}
