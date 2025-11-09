using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class Dashboard : Form
    {
        private UDP_Client client = new UDP_Client();
        private UDP_Server server = new UDP_Server();
        public Dashboard()
        {
            InitializeComponent();
            client.Dock = DockStyle.Fill;
            server.Dock = DockStyle.Fill;
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // variables to follow state 
        private bool isDragging = false;
        private Point dragCursorPoint; // mouse position when dragging
        private Point dragFormPoint; // form position when dragging

        // when drag panel
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragCursorPoint = Cursor.Position; // save current mouse position
                dragFormPoint = this.Location; // save current form positon
            }
        }
        // when move panel 
        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point point = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(point));
            }
        }

        // when drop panel
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            panel4.Controls.Add(client);
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            panel4.Controls.Add(server);
        }
    }
}