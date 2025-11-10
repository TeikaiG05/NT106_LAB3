using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Client : Form
    {
        private TcpClient tcpClient;
        public Client()
        {
            InitializeComponent();
           

        }
        private void btConnect_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient();
                IPAddress ipAddress = IPAddress.Parse("25.5.134.104");
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8080);
                tcpClient.Connect(ipEndPoint);
                MessageBox.Show("Connected");
                btConnect.Enabled = false;
                btSend.Enabled = true;
                btDisconnect.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect error: " + ex.Message);
            }
        }
        private void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                {
                    MessageBox.Show("Not connected to server.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbChat.Text))
                {
                    MessageBox.Show("Please enter a message to send.");
                    return;
                }
                string message = tbChat.Text + "\n";
                NetworkStream ns = tcpClient.GetStream();
                byte[] data = Encoding.ASCII.GetBytes(message);
                ns.Write(data, 0, data.Length);
                tbChat.Clear();
                tbChat.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send error: " + ex.Message);
            }
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient != null)
                {
                    if (tcpClient.Connected)
                    {
                        NetworkStream ns = tcpClient.GetStream();
                        byte[] data = Encoding.ASCII.GetBytes("quit\n");
                        ns.Write(data, 0, data.Length);
                        ns.Close();
                    }
                    tcpClient.Close();
                    tcpClient = null;
                }
            }
            finally
            {
                btConnect.Enabled = true;
                btSend.Enabled = false;
                btDisconnect.Enabled = false;
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            btSend.Enabled = false;
            btDisconnect.Enabled = false;
        }
    }
}

