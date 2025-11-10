using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void btListen_Click(object sender, EventArgs e)
        {
            btListen.Enabled = false;
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.Start();
            serverThread.IsBackground = true;
        }
        void StartUnsafeThread()
        {
            int bytesReceived = 0;
            byte[] recv = new byte[1];

            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Any, 8080);
            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(10);
            this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem("Server started!"))));

            Socket clientSocket = listenerSocket.Accept();
            var remote = (IPEndPoint)clientSocket.RemoteEndPoint;
            string clientInfo = $"New client connected: {remote.Address}:{remote.Port}";
            this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem(clientInfo))));

            while (clientSocket.Connected)
            {
                string text = "";
                do
                {
                    bytesReceived = clientSocket.Receive(recv);
                    if (bytesReceived == 0)
                        break;
                    text += Encoding.ASCII.GetString(recv, 0, bytesReceived);
                }
                while (text.Length == 0 || text[text.Length - 1] != '\n');

                if (text.Length == 0)
                    break;

                this.Invoke((Action)(() => lvTin.Items.Add(new ListViewItem($"From client: {text}"))));
            }

            listenerSocket.Close();
            this.Invoke((Action)(() =>
            {
                lvTin.Items.Add(new ListViewItem("Client disconnected"));
                btListen.Enabled = true;
            }));
        }
    }
    }
