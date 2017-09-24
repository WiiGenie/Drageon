using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JuerLibrary.HttpHelper
{
    public class WebHelper
    {
        HttpClient client = new HttpClient();

        public string Url { get; }

        public WebHelper(string url)
        {
            Url = url;
        }

        public async Task<HttpResponseMessage> Post(string uri, HttpContent content)
        {
            return await client.PostAsync(uri, content);
        }
        public async Task<HttpResponseMessage> Get(string uri)
        {
            return await client.GetAsync(uri);
        }
    }
    /// <summary>
    /// Get方法中的参数节点
    /// </summary>
    public class Section
    {
        private Hashtable hashtable = new Hashtable();
        public string this[string Index]
        {
            get
            {
                return (string)hashtable[Index];
            }
        }

        public override string ToString()
        {
            string temp = string.Empty;
            foreach (string key in hashtable.Keys)
            {
                temp += "&" + key + hashtable[key];
            }
            return temp;
        }
    }
}
