using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _123Updater
{
    class Program
    {
        private static string IP = "83.249.251.177";
        private static int PORT = 12345;

        private static Socket updateSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] buffer = new byte[200000];
        private static string fileName = "";
        private static bool updating = true;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            updateSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), PORT), new AsyncCallback(onBeginConnect), null);
            sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Update, ""));
            while(updating)
            {

            }
        }

        private static void onBeginConnect(IAsyncResult ar)
        {
            try
            {
                updateSocket.EndConnect(ar);

                updateSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(onBeginRecieve), null);
                sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Update, ""));
            }
            catch (SocketException)
            {
                updateSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), PORT), new AsyncCallback(onBeginConnect), null);
            }
        }

        private static void onBeginRecieve(IAsyncResult ar)
        {
            try
            {
                int size = updateSocket.EndReceive(ar);
                string message = (Encoding.UTF8.GetString(buffer, 0, size));
                MessageProtocol.MessageType messageType = MessageProtocol.getMessageType(message);

                switch (messageType)
                {
                    case MessageProtocol.MessageType.Version:
                        updateConsole(message);
                        FileManager.saveVersion(MessageProtocol.getMessage(message));
                        Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "123ClickGUI.exe"))
                        {
                            Process.Start("123ClickGUI.exe");
                            updating = false;
                        }

                        break;
                    case MessageProtocol.MessageType.Update:
                        sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.NextFile, ""));
                        break;
                    case MessageProtocol.MessageType.NextFile:
                        break;
                    case MessageProtocol.MessageType.SendFile:
                        break;
                    case MessageProtocol.MessageType.FileName:
                        fileName = MessageProtocol.getMessage(message);
                        if(fileName != "123Updater.exe")
                            updateConsole(fileName + ": 0%");
                        sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.SendFile, ""));
                        break;
                    default:
                        if (fileName != "123Updater.exe")
                        {
                            byte[] file = new byte[size];
                            Array.Copy(buffer, 0, file, 0, size);
                            File.WriteAllBytes(AppContext.BaseDirectory + "\\" + fileName, file);
                            updateConsole(fileName + ": 100%");
                            
                        }
                        sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.NextFile, ""));
                        break;
                }
                if(messageType != MessageProtocol.MessageType.Disconnect)
                    updateSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(onBeginRecieve), null);
            }
            catch (SocketException)
            {
                updateSocket.Disconnect(true);
                updateSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), PORT), new AsyncCallback(onBeginConnect), null);
            }
        }

        private static void OnBeginSend(IAsyncResult ar)
        {
            try
            {
                updateSocket.EndSend(ar);
            }
            catch (SocketException)
            {

            }
        }

        public static void sendMessage(byte[] message)
        {
            try
            {
                updateSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnBeginSend), null);
            }
            catch
            {

            }
        }

        private static void updateConsole(string message)
        {
            Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + message);
        }
    }
}
