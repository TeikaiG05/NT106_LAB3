using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai4
{
    public partial class Server : UserControl
    {
        private static Dictionary<string, Movie> movies = new Dictionary<string, Movie>();
        private static Dictionary<int, Dictionary<string, Seat>> phong = new Dictionary<int, Dictionary<string, Seat>>();
        private static object lockObj = new object();
        private TcpListener listener;

        public Server()
        {
            InitializeComponent();
            InitializeData();
            StartServer();
        }

        private void InitializeData()
        {
            movies["Đào, phở và piano"] = new Movie { phim = "Đào, phở và piano", gia = 45000, phong = new List<int> { 1, 2, 3 } };
            movies["Mai"] = new Movie { phim = "Mai", gia = 100000, phong = new List<int> { 2, 3 } };
            movies["Gặp lại chị bầu"] = new Movie { phim = "Gặp lại chị bầu", gia = 70000, phong = new List<int> { 1 } };
            movies["Tarot"] = new Movie { phim = "Tarot", gia = 90000, phong = new List<int> { 3 } };

            for (int room = 1; room <= 3; room++)
            {
                phong[room] = new Dictionary<string, Seat>();
                for (int i = 1; i <= 5; i++)
                {
                    phong[room]["A" + i] = new Seat { vitri = "A" + i, loai = GetSeatloai("A" + i) };
                    phong[room]["B" + i] = new Seat { vitri = "B" + i, loai = GetSeatloai("B" + i) };
                    phong[room]["C" + i] = new Seat { vitri = "C" + i, loai = GetSeatloai("C" + i) };
                }
            }
        }

        private string GetSeatloai(string vitri)
        {
            if (vitri == "A1" || vitri == "A5" || vitri == "C1" || vitri == "C5") return "Vớt";
            if (vitri == "B2" || vitri == "B3" || vitri == "B4") return "VIP";
            return "Thường";
        }

        private void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();
            Log("Máy chủ đã khởi động trên cổng 8888");

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Log("Đã nhận: " + request);

                string response = ProcessRequest(request);
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
                client.Close();
            }
            catch (Exception ex)
            {
                Log("Lỗi: " + ex.Message);
            }
        }

        private string ProcessRequest(string request)
        {
            string[] parts = request.Split('|');
            if (parts.Length != 4) return "Định dạng yêu cầu không hợp lệ.";

            string name = parts[0];
            string movieName = parts[1];
            if (!int.TryParse(parts[2], out int room)) return "Phòng không hợp lệ.";
            string[] seats = parts[3].Split(',');

            lock (lockObj)
            {
                if (!movies.ContainsKey(movieName) || !movies[movieName].phong.Contains(room))
                    return "Phim không có sẵn trong phòng này";

                if (seats.Length > 2) return "Không thể chọn nhiều hơn 2 chỗ ngồi.";

                List<string> selectedSeats = new List<string>();
                int totalPrice = 0;
                Movie movie = movies[movieName];

                foreach (string seat in seats)
                {
                    if (!phong[room].ContainsKey(seat) || !phong[room][seat].cosan)
                        return $"Chỗ ngồi {seat} không có sẵn.";

                    selectedSeats.Add(seat);
                    int price = movie.gia;
                    if (phong[room][seat].loai == "Vớt") price /= 4;
                    else if (phong[room][seat].loai == "VIP") price *= 2;
                    totalPrice += price;
                    phong[room][seat].cosan = false;
                }

                string result = $"Người mua: {name}\n đã chọn: {string.Join(", ", selectedSeats)}\n Phim: {movieName}\n Phòng: {room}\n Tổng giá: {totalPrice} VND";
                return result;
            }
        }

        private void Log(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Log), message);
            }
            else
            {
                txtLog.AppendText(message + Environment.NewLine);
            }
        }

        public class Seat
        {
            public string vitri { get; set; }
            public string loai { get; set; }
            public bool cosan { get; set; } = true;
        }

        public class Movie
        {
            public string phim { get; set; }
            public int gia { get; set; }
            public List<int> phong { get; set; }
        }
    }
}
