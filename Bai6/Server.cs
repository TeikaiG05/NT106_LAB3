using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai6
{
    public partial class Server : Form
    {
        private static readonly List<Socket> clients = new List<Socket>();
        private static readonly Dictionary<Socket, string> names = new Dictionary<Socket, string>();
        private static readonly object lockx = new object();

        public Server()
        {
            InitializeComponent();
        }

        private void btListen_Click(object sender, EventArgs e)
        {
            btListen.Enabled = false;
            CheckForIllegalCrossThreadCalls = false;
            var serverThread = new Thread(StartUnsafeThread) { IsBackground = true };
            serverThread.Start();
        }

        void StartUnsafeThread()
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Any, 8080);
            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(100);
            this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem("Server started!"))));

            try
            {
                while (true)
                {
                    var clientSocket = listenerSocket.Accept();
                    var remote = (IPEndPoint)clientSocket.RemoteEndPoint;
                    this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem($"New client connected: {remote.Address}:{remote.Port}"))));

                    lock (lockx) clients.Add(clientSocket);

                    var t = new Thread(() => HandleClient(clientSocket)) { IsBackground = true };
                    t.Start();
                }
            }
            catch (SocketException) { }
            finally
            {
                try { listenerSocket.Close(); } catch { }
                this.Invoke((Action)(() =>
                {
                    lvTin.Items.Add(new ListViewItem("Server stopped"));
                    btListen.Enabled = true;
                }));
            }
        }

        private static void SendLine(Socket s, string text)
        {
            try
            {
                var data = Encoding.ASCII.GetBytes(text + "\n");
                s.Send(data);
            }
            catch { }
        }

        private static void Broadcast(string text, Socket except = null)
        {
            lock (lockx)
            {
                foreach (var c in clients.ToList())
                {
                    if (c == except) continue;
                    SendLine(c, text);
                }
            }
        }

        private static void SendUsersToAll()
        {
            lock (lockx)
            {
                var line = "USERS|" + string.Join(",", names.Values.ToArray());
                foreach (var c in clients.ToList()) SendLine(c, line);
            }
        }

        void HandleClient(Socket clientSocket)
        {
            int bytesReceived = 0;
            byte[] recv = new byte[1];
            string myName = null;

            try
            {
                while (clientSocket.Connected)
                {
                    string text = "";
                    do
                    {
                        try
                        {
                            bytesReceived = clientSocket.Receive(recv);
                        }
                        catch (SocketException ex)
                        {
                            if (ex.SocketErrorCode == SocketError.ConnectionAborted ||
                                ex.SocketErrorCode == SocketError.ConnectionReset)
                            {
                                bytesReceived = 0;
                                break;
                            }
                            throw;
                        }

                        if (bytesReceived == 0) break;
                        text += Encoding.ASCII.GetString(recv, 0, bytesReceived);
                    }
                    while (text.Length == 0 || text[text.Length - 1] != '\n');

                    if (bytesReceived == 0 || text.Length == 0) break;

                    text = text.TrimEnd('\r', '\n');
                    if (string.Equals(text, "quit", StringComparison.OrdinalIgnoreCase))
                        break;

                    if (myName == null)
                    {
                        if (!text.StartsWith("NAME|", StringComparison.OrdinalIgnoreCase))
                            continue;

                        var name = text.Substring(5).Trim();
                        if (string.IsNullOrWhiteSpace(name))
                            break; 

                        lock (lockx)
                        {
                            if (names.Values.Any(n => n.Equals(name, StringComparison.OrdinalIgnoreCase)))
                            {
                                SendLine(clientSocket, "ERROR|Name in use");
                                break;
                            }
                            names[clientSocket] = name;
                        }

                        myName = name;
                        this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem($"{myName} joined"))));
                        SendUsersToAll();        
                        continue;                 
                    }

                    if (text.StartsWith("NAME|", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (text.StartsWith("FILE|", StringComparison.OrdinalIgnoreCase))
                    {
                        var partsIn = text.Split(new[] { '|' }, 5);
                        if (partsIn.Length >= 5)
                        {
                            string toName = partsIn[1];
                            string fname = partsIn[2];
                            string mime = partsIn[3];
                            string b64 = partsIn[4];

                            if (toName.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                            {
                                string outLine = $"FILE|{myName}|{fname}|{mime}|{b64}";
                                Broadcast(outLine, except: null);
                            }
                            else
                            {
                                Socket target = null;
                                lock (lockx)
                                {
                                    target = names.FirstOrDefault(kv => kv.Value.Equals(toName, StringComparison.OrdinalIgnoreCase)).Key;
                                }

                                if (target != null)
                                {
                                    SendLine(target, $"FILEPM|{myName}|{fname}|{mime}|{b64}");
                                    if (target != clientSocket)
                                        SendLine(clientSocket, $"Me -> {toName}: (file) {fname}");
                                }
                                else
                                {
                                    SendLine(clientSocket, $"ERROR|User '{toName}' not found");
                                }
                            }
                        }
                        continue;
                    }
                    // PM
                    if (text.StartsWith("/pm ", StringComparison.OrdinalIgnoreCase))
                    {
                        var parts = text.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 3)
                        {
                            string toUser = parts[1];
                            string body = parts[2];
                            Socket target = null;

                            lock (lockx)
                            {
                                target = names.FirstOrDefault(kv => kv.Value.Equals(toUser, StringComparison.OrdinalIgnoreCase)).Key;
                            }

                            if (target != null)
                            {
                                SendLine(target, $"{myName} (pm): {body}");
                            }
                            else
                            {
                                SendLine(clientSocket, $"ERROR|User '{toUser}' not found");
                            }
                        }
                        else
                        {
                            SendLine(clientSocket, "ERROR|Usage: /pm <username> <message>");
                        }
                    }
                    else
                    {
                        Broadcast($"{myName}: {text}");
                    }

                    this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem($"From {myName}: {text}"))));
                }
            }
            finally
            {
                try
                {
                    lock (lockx)
                    {
                        clients.Remove(clientSocket);
                        if (names.TryGetValue(clientSocket, out var goneName))
                        {
                            myName = goneName;
                            names.Remove(clientSocket);
                        }
                    }
                    clientSocket.Close();

                    if (!string.IsNullOrEmpty(myName))
                    {
                        this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem($"{myName} left the group"))));
                        SendUsersToAll();
                    }
                }
                catch { }
            }
        }
    }
}
