using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai5
{
    public partial class Bai5_lap3 : Form
    {
        // Khai báo socket và các biến dùng chung
        Socket client;
        IPEndPoint serverEP;
        Thread listenThread;
        bool isConnected = false;

        // Dùng lưu đường dẫn ảnh tạm thời khi chọn món
        string currentImagePath = "";
        public Bai5_lap3()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        void ProcessServerMessage(string message)
        {
            // Giả sử server gửi dạng: TYPE|data
            string[] parts = message.Split('|');
            string type = parts[0];

            switch (type)
            {
                case "USER_OK":
                    MessageBox.Show("Thêm người thành công!");
                    break;

                case "FOOD_OK":
                    MessageBox.Show("Đã thêm món thành công!");
                    break;

                case "DATA":
                    dataGridView1.Rows.Clear();

                    // Nếu chưa có cột, tạo tự động
                    if (dataGridView1.Columns.Count == 0)
                    {
                        dataGridView1.Columns.Add("TenMonAn", "Tên món ăn");
                        dataGridView1.Columns.Add("HinhAnh", "Đường dẫn ảnh");
                        dataGridView1.Columns.Add("NguoiCungCap", "Người cung cấp");
                    }

                    string[] items = parts[1].Split(';');
                    foreach (var item in items)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            string[] info = item.Split(',');
                            // Đảm bảo số phần tử đúng
                            if (info.Length >= 3)
                                dataGridView1.Rows.Add(info[0], info[1], info[2]);
                        }
                    }
                    break;


                case "RANDOM":
                case "RANDOM_PERSONAL":
                case "RANDOM_GLOBAL":
                    if (parts.Length >= 4)
                    {
                        ngaunhienten.Text = parts[1];
                        ngaunhienmon.Text = parts[2];
                        try
                        {
                            if (!string.IsNullOrEmpty(parts[3]))
                            {
                                byte[] imgBytes = Convert.FromBase64String(parts[3]);
                                using (var ms = new MemoryStream(imgBytes))
                                {
                                    picmonanngaunhien.Image = Image.FromStream(ms);
                                    picmonanngaunhien.SizeMode = PictureBoxSizeMode.Zoom;
                                }
                            }
                            else
                            {
                                picmonanngaunhien.Image = null;
                            }
                        }
                        catch
                        {
                            picmonanngaunhien.Image = null;
                        }
                    }
                    break;


                case "NEWMON":
                    if (parts.Length >= 4)
                    {
                        string nguoi = parts[1];
                        string tenmon = parts[2];
                        string base64 = parts[3];

                        try
                        {
                            if (!string.IsNullOrEmpty(base64))
                            {
                                byte[] imgBytes = Convert.FromBase64String(base64);
                                using (var ms = new MemoryStream(imgBytes))
                                {
                                    picanhmon.Image = Image.FromStream(ms);
                                    picanhmon.SizeMode = PictureBoxSizeMode.Zoom;
                                }
                            }

                            texttennguoi.Text = nguoi;
                            texttenmon.Text = tenmon;

                            MessageBox.Show($"Món mới từ {nguoi}: {tenmon}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi nhận ảnh: " + ex.Message);
                        }
                    }
                    break;



                default:
                    break;
            }
        }


        void ReceiveData()
        {
            try
            {
                while (true)
                {
                    // đọc 4 byte length
                    byte[] lenBuf = ReadExact(4);
                    if (lenBuf == null) break;
                    int len = BitConverter.ToInt32(lenBuf, 0); // match server endianness

                    // đọc payload len bytes
                    byte[] payload = ReadExact(len);
                    if (payload == null) break;

                    string data = Encoding.UTF8.GetString(payload, 0, payload.Length);
                    this.Invoke(new Action(() =>
                    {
                        ProcessServerMessage(data);
                    }));
                }
            }
            catch
            {
                this.Invoke(new Action(() =>
                {
                    labeltrangthai.Text = "Mất kết nối với server";
                    isConnected = false;
                }));
            }
        }

        private byte[] ReadExact(int count)
        {
            byte[] buffer = new byte[count];
            int offset = 0;
            while (offset < count)
            {
                int rec = client.Receive(buffer, offset, count - offset, SocketFlags.None);
                if (rec == 0) return null; // connection closed
                offset += rec;
            }
            return buffer;
        }


        private void butketnoi_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy IP và Port từ textbox
                string ip = textiipserver.Text.Trim();
                int port = int.Parse(textportipserver.Text.Trim());

                // Tạo socket TCP
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverEP = new IPEndPoint(IPAddress.Parse(ip), port);
                client.Connect(serverEP);

                isConnected = true;
                labeltrangthai.Text = "Đã kết nối với server";

                // Bắt đầu thread lắng nghe dữ liệu từ server
                listenThread = new Thread(ReceiveData);
                listenThread.IsBackground = true;
                listenThread.Start();

                MessageBox.Show("Kết nối thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến server: " + ex.Message);
            }
        }

        private void themnguoi_Click(object sender, EventArgs e)
        {
            if (!isConnected) { MessageBox.Show("Chưa kết nối server!"); return; }

            string msg = $"USER|{texttennguoi.Text}|{textquyenhan.Text}";
            client.Send(Encoding.UTF8.GetBytes(msg));
        }

        private void butthemanh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = ofd.FileName;
                picanhmon.Image = Image.FromFile(currentImagePath);
            }
        }
        private void SendMessage(string msg)
        {
            if (!isConnected) return;
            byte[] payload = Encoding.UTF8.GetBytes(msg);
            byte[] lenPrefix = BitConverter.GetBytes(payload.Length);
            byte[] packet = new byte[4 + payload.Length];
            Array.Copy(lenPrefix, 0, packet, 0, 4);
            Array.Copy(payload, 0, packet, 4, payload.Length);
            client.Send(packet);
        }


        private void butthemmon_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Chưa kết nối server!");
                return;
            }

            if (string.IsNullOrWhiteSpace(texttennguoi.Text))
            {
                MessageBox.Show("Vui lòng nhập tên người cung cấp trước!");
                return;
            }

            if (string.IsNullOrWhiteSpace(texttenmon.Text) || string.IsNullOrWhiteSpace(currentImagePath))
            {
                MessageBox.Show("Vui lòng nhập tên món và chọn ảnh!");
                return;
            }

            try
            {
                // Đọc file ảnh → mã hóa sang base64
                byte[] imgBytes = File.ReadAllBytes(currentImagePath);
                string base64 = Convert.ToBase64String(imgBytes);

                // Gửi sang server dạng: FOOD|TênNgười|TênMón|Base64
                string msg = $"FOOD|{texttennguoi.Text}|{texttenmon.Text}|{base64}";
                client.Send(Encoding.UTF8.GetBytes(msg));

                MessageBox.Show("Đã gửi món ăn kèm ảnh thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi ảnh: " + ex.Message);
            }
        }


        private void buthienthi_Click(object sender, EventArgs e)
        {
            client.Send(Encoding.UTF8.GetBytes("SHOW"));
        }

        private void butngaunhiencanhan_Click(object sender, EventArgs e)
        {
            client.Send(Encoding.UTF8.GetBytes($"RANDOM_PERSONAL|{texttennguoi.Text}"));
        }

        private void butngaunhiencongdong_Click(object sender, EventArgs e)
        {
            client.Send(Encoding.UTF8.GetBytes("RANDOM_GLOBAL"));
        }

        private void butxoamon_Click(object sender, EventArgs e)
        {
            client.Send(Encoding.UTF8.GetBytes($"DELETE_PERSONAL|{texttennguoi.Text}"));
        }
    }
}
