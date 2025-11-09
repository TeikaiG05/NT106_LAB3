using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai4
{
    public partial class Client : UserControl
    {
        private Dictionary<string, Server.Movie> movies = new Dictionary<string, Server.Movie>();

        public Client()
        {
            InitializeComponent();
            InitializeMovies();
            cmbMovie.SelectedIndexChanged += CmbMovie_SelectedIndexChanged;
        }

        private void InitializeMovies()
        {
            movies["Đào, phở và piano"] = new Server.Movie { phim = "Đào, phở và piano", gia = 45000, phong = new List<int> { 1, 2, 3 } };
            movies["Mai"] = new Server.Movie { phim = "Mai", gia = 100000, phong = new List<int> { 2, 3 } };
            movies["Gặp lại chị bầu"] = new Server.Movie { phim = "Gặp lại chị bầu", gia = 70000, phong = new List<int> { 1 } };
            movies["Tarot"] = new Server.Movie { phim = "Tarot", gia = 90000, phong = new List<int> { 3 } };

            cmbMovie.Items.AddRange(new string[] { "Đào, phở và piano", "Mai", "Gặp lại chị bầu", "Tarot" });
        }

        private void CmbMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMovie = cmbMovie.SelectedItem?.ToString();
            if (selectedMovie != null && movies.ContainsKey(selectedMovie))
            {
                cmbRoom.Items.Clear();
                foreach (int room in movies[selectedMovie].phong)
                {
                    cmbRoom.Items.Add(room);
                }
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string movie = cmbMovie.SelectedItem?.ToString();
            string phongtr = cmbRoom.SelectedItem?.ToString();
            List<string> selectedSeats = new List<string>();
            foreach (var item in clbSeats.CheckedItems)
                selectedSeats.Add(item.ToString());

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(movie) || string.IsNullOrEmpty(phongtr) || selectedSeats.Count == 0 || selectedSeats.Count > 2)
            {
                MessageBox.Show("Chọn đầy đủ và chọn nhiều nhất 2 chỗ ngồi.");
                return;
            }

            string request = $"{name}|{movie}|{phongtr}|{string.Join(",", selectedSeats)}";
            Thread thread = new Thread(() => SendRequest(request));
            thread.IsBackground = true;
            thread.Start();
        }

        private void SendRequest(string request)
        {
            string serverIP = txtServerIP.Text.Trim(); 
            try
            {
                TcpClient client = new TcpClient(serverIP, 8888);
                NetworkStream stream = client.GetStream();
                byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                stream.Write(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Invoke(new Action(() => txtResult.Text = response));
                client.Close();
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => MessageBox.Show("Lỗi: " + ex.Message)));
            }
        }
    }
}
