using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class UDP_Server : UserControl
    {
        private UdpClient server;
        private bool listening = false;

        public UDP_Server()
        {
            InitializeComponent();
        }
        private async void btnListen_Click_1(object sender, EventArgs e)
        {
            if (listening) return;

            try
            {
                int port = int.Parse(Port.Text.Trim());
                server = new UdpClient(port);
                listening = true;
                btnListen.Enabled = false;

                while (listening)
                {
                    var result = await server.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(result.Buffer);
                    string displayedMessage = $"{result.RemoteEndPoint.Address} : {message}";

                    this.Invoke((MethodInvoker)delegate
                    {
                        txtRev.AppendText(displayedMessage + Environment.NewLine);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lắng nghe: " + ex.Message);
                btnListen.Enabled = true;
                listening = false;
            }
        }
    }
}
