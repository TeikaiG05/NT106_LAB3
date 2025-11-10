using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bai4
{
    public partial class StartupForm : Form
    {
        private Server serverControl;
        private Client clientControl;

        public StartupForm()
        {
            InitializeComponent();
            serverControl = new Server() { Dock = DockStyle.Fill };
            clientControl = new Client() { Dock = DockStyle.Fill };
            PanelMain.Controls.Add(serverControl);
            serverControl.BringToFront();
        }

        private void BtnServer_Click(object sender, EventArgs e)
        {
            if (!PanelMain.Controls.Contains(serverControl)) PanelMain.Controls.Add(serverControl);
            serverControl.BringToFront();
        }

        private void BtnClient_Click(object sender, EventArgs e)
        {
            if (!PanelMain.Controls.Contains(clientControl)) PanelMain.Controls.Add(clientControl);
            clientControl.BringToFront();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Drag form support
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void PanelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
