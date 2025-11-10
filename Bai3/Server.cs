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
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.Start();
        }
        void StartUnsafeThread()
        {
            int bytesReceived = 0;
            byte[] recv = new byte[1];

            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080); // sửa “ ” -> " "
            listenerSocket.Bind(ipepServer);
            listenerSocket.Listen(10);                        // sửa backlog: không dùng -1

            Socket clientSocket = listenerSocket.Accept();    // khai báo biến clientSocket cục bộ
            lvTin.Items.Add(new ListViewItem("New client connected"));

            while (clientSocket.Connected)
            {
                string text = "";
                do
                {
                    bytesReceived = clientSocket.Receive(recv);
                    if (bytesReceived == 0)                   // client đóng kết nối -> thoát vòng
                        break;
                    text += Encoding.ASCII.GetString(recv, 0, bytesReceived);
                }
                // tránh IndexOutOfRange khi chuỗi rỗng
                while (text.Length == 0 || text[text.Length - 1] != '\n');

                if (text.Length == 0)                         // đóng kết nối
                    break;

                lvTin.Items.Add(new ListViewItem(text));
            }

            listenerSocket.Close();
        }
    }
    }
