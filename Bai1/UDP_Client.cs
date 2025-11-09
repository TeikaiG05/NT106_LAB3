using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class UDP_Client : UserControl
    {
        private UdpClient client;

        public UDP_Client()
        {
            InitializeComponent();
            client = new UdpClient();
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            try
            {
                string ip = txtIP.Text.Trim();
                int port = int.Parse(txtPort.Text.Trim());
                string message = txtMess.Text.Trim();

                byte[] data = Encoding.UTF8.GetBytes(message);

                client.Send(data, data.Length, ip, port);

                MessageBox.Show("Message sent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending: " + ex.Message);
            }
        }
    }
}
