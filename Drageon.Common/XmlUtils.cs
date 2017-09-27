using System.IO;
using System.Xml.Serialization;

namespace Drageon.Common
{
    public static class XmlUtils
    {
        public static T Deserialize<T>(string xml)
        {
            //初始化 XmlSerializer 时会报错，但并不影响运行——引发的异常:“System.IO.FileNotFoundException”(位于 mscorlib.dll 中)
            //我不清楚是为什么，如果您知道的话，希望能够为我解惑，谢谢(:3)。
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            T temp = (T)serializer.Deserialize(reader);

            reader.Close();
            reader.Dispose();

            return temp;
        }
        
        public static string Serializer<T>(T t)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, t, namespaces);
            string temp = writer.ToString();

            writer.Close();
            writer.Dispose();

            return temp;
        }
    }
}
