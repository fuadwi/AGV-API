using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public static HttpListener listener;
        public static string url = "http://+:8000/";
     


        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                //CORS - KIRIM DATA REPLY LANGSUNG KE WMS
                if (req.HttpMethod == "OPTIONS")
                {
                    resp.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
                    resp.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                    resp.AddHeader("Access-Control-Max-Age", "1728000");
                }
                resp.AppendHeader("Access-Control-Allow-Origin", "*");

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                {
                    Console.WriteLine(req.Url.ToString());
                    Console.WriteLine(req.HttpMethod);
                    Console.WriteLine(req.UserHostName);
                    Console.WriteLine("Request to route " + req.QueryString["route"]);
                    Console.WriteLine();
                }
             
                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data;
                //data = Encoding.UTF8.GetBytes("Casun AGV API");
                //Console.WriteLine(req.QueryString["route"]);
                if (String.IsNullOrEmpty(req.QueryString["route"]))
                {
                    data = Encoding.UTF8.GetBytes("Casun AGV API");
                }
                else
                {
                    try
                    {
                        int route = Convert.ToInt32(req.QueryString["route"]);
                        data = Encoding.UTF8.GetBytes("Accepted, route=" + route.ToString());
                    }
                    catch
                    {
                        data = Encoding.UTF8.GetBytes("Wrong data type");
                    }
                }
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }


        public static void Main(string[] args)
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
    }
 
}
