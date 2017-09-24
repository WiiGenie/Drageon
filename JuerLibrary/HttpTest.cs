using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JuerLibrary
{
    class HttpTest
    {
        public static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:45678/");
            listener.Start();
            Console.WriteLine("[http] listener.Start()...");
            Console.WriteLine("[http] waiting for client's connect...");
            while (true)
            {
                var context = listener.GetContext();
                Console.WriteLine("[http] Client Connecting!");
                Console.WriteLine("[http] ------------------");
                Console.WriteLine("[http] context.Request.AcceptTypes : {0}", context.Request.AcceptTypes);
                Console.WriteLine("[http] context.Request.ContentType : {0}", context.Request.ContentType);
                Console.WriteLine("[http] context.Request.ContentLength64 : {0}", context.Request.ContentLength64);
                Console.WriteLine("[http] ------------------");
                Console.WriteLine("[http] method : {0}, KeepAlive : {1}, RawUrl : {2}", context.Request.HttpMethod, context.Request.KeepAlive, context.Request.RawUrl);
                Console.WriteLine("[http] ------------------");

                byte[] bytes;
                if (context.Request.RawUrl == "/stopserver=true")
                {
                    Console.WriteLine("[http] Server Stop!");
                    bytes = Encoding.Unicode.GetBytes("<html><h1>Server Stop!</ht></html>");
                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    context.Response.OutputStream.Close();
                    context.Response.Close();
                    break;
                }
                
                string title = "", content = "";
                Console.WriteLine("[http] input title:");
                title = Console.ReadLine();
                Console.WriteLine("[http] input content:");
                content = Console.ReadLine();

                bytes = Encoding.Unicode.GetBytes(string.Format("<html><h1>{0}</h1><h2>{1}</h2></html>", title, content));
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                context.Response.OutputStream.Close();
                context.Response.Close();
            }

            Console.ReadKey();
        }
    }
}
