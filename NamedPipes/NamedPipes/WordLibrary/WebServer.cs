using System;
using System.IO;
using System.Net;
using System.Text;

namespace WordLibrary
{
    class WebServer
    {
        public void startWebServices()
        {
            // Set the port number for the server.
            int port = 8080;

            // Create a listener for incoming requests on the specified port.
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(string.Format("http://localhost:{0}/", port));
            listener.
            listener.Start();

            Console.WriteLine("Server listening on port {0}...", port);

            // Handle incoming requests.
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                // Read the request body, if any.
                StreamReader reader = new StreamReader(request.InputStream);
                string body = reader.ReadToEnd();

                Console.WriteLine("{0} {1} HTTP/{2}", request.HttpMethod, request.Url, request.ProtocolVersion);
                Console.WriteLine("Content-Length: {0}", request.ContentLength64);
                Console.WriteLine(body);

                // Construct a response.
                HttpListenerResponse response = context.Response;
                string responseString = "<html><body><h1>Hello, world!</h1></body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                // Send the response.
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.Close();
            }
        }
    }
}

