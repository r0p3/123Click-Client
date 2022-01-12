using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace _123ClickGUI
{
    public class Communication
    {
        #region Events
        public delegate void Connect();
        public static event Connect onConnected;
        public static event Connect onDisconnect;
        public static event Connect onCountDownStart;
        public static event Connect onUsersChanged;
        public static event Connect onCountDownCancel;
        public static event Connect onUpdateRequest;
        public static event Connect onRewind;
        public static event Connect onForward;
        #endregion

        private static string IP = "83.249.251.177";
        private static int PORT = 12345;

        private Socket serverConnection;

        private byte[] buffer = new byte[1024];

        public static List<string> usersOnline = new List<string>();

        private GUI gui;

        public Communication(GUI gui)
        {
            this.gui = gui;
            serverConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverConnection.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), PORT), new AsyncCallback(onConnect), null);
        }

        public bool isConnected()
        {
            return serverConnection.Connected;
        }

        private void onConnect(IAsyncResult ar)
        {
            try
            {
                serverConnection.EndConnect(ar);
                onConnected?.Invoke();
                serverConnection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(onBeginRecieve), null);
                sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Name, FileManager.name));
                sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Version, FileManager.getVersion().ToString()));
            }
            catch (SocketException)
            {
                onDisconnect?.Invoke();
                serverConnection.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), PORT), new AsyncCallback(onConnect), null);
            }
        }

        private void onBeginRecieve(IAsyncResult ar)
        {
            try
            {
                int size = serverConnection.EndReceive(ar);
                handleMessage(Encoding.UTF8.GetString(buffer, 0, size));
                serverConnection.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(onBeginRecieve), null);
            }
            catch (SocketException)
            {
                onDisconnect?.Invoke();
                serverConnection.Disconnect(true);
                serverConnection.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), PORT), new AsyncCallback(onConnect), null);
            }
        }

        private void OnBeginSend(IAsyncResult ar)
        {
            try
            {
                serverConnection.EndSend(ar);
            }
            catch (SocketException){}
        }

        public void sendMessage(byte[] message)
        {
            try
            {
                serverConnection.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnBeginSend), null);
            }
            catch{}
        }

        private void handleMessage(string message)
        {
            MessageProtocol.MessageType messageType = MessageProtocol.getMessageType(message);
            message = MessageProtocol.getMessage(message);
            switch (messageType)
            {
                case MessageProtocol.MessageType.Ping:
                    sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Ping, ""));
                    break;
                case MessageProtocol.MessageType.Name:
                    break;
                case MessageProtocol.MessageType.StartCountDown:
                    gui.backLog.Add(message + " ▶");
                    onCountDownStart?.Invoke();
                    break;
                case MessageProtocol.MessageType.CancelCountDown:
                    gui.backLog.Add(message + " ❚❚");
                    onCountDownCancel?.Invoke();
                    break;
                case MessageProtocol.MessageType.Rewind:
                    gui.backLog.Add(message + " «");
                    onRewind?.Invoke();
                    break;
                case MessageProtocol.MessageType.Forward:
                    gui.backLog.Add(message + " »");
                    onForward?.Invoke();
                    break;
                case MessageProtocol.MessageType.ForceUpdate:
                    onUpdateRequest?.Invoke();
                    break;
                case MessageProtocol.MessageType.Disconnect:
                    Application.Exit();
                    break;
                case MessageProtocol.MessageType.OnlineUsers:
                    usersOnline = message.Split(',').ToList();
                    onUsersChanged?.Invoke();
                    break;
                case MessageProtocol.MessageType.Version:
                    break;
                case MessageProtocol.MessageType.Update:
                    onUpdateRequest?.Invoke();
                    break;
                case MessageProtocol.MessageType.None:
                    break;
                default:
                    break;
            }
        }


        public bool changeName(string name)
        {
            try
            {
                sendMessage(MessageProtocol.createMessage(MessageProtocol.MessageType.Name, name));
                FileManager.saveName(name);
                return true;
            }
            catch
            {
                return false;
            }
        }        

    }
}
