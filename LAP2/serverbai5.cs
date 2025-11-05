using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;

namespace LAP2
{
    public partial class serverbai5 : Form
    {
        private string dbFilePath;
        private string filedb;
        private Socket server;
        private List<Socket> clients = new List<Socket>();
        private Thread listenThread;
        private volatile bool serverRunning = false;

        private string base64Image = ""; // ảnh đang chọn (lưu dạng base64)

        public serverbai5()
        {
            InitializeComponent();
            dbFilePath = Path.Combine(Application.StartupPath, "monan.db");
            filedb = $"Data Source={dbFilePath};Version=3;";

            this.themng.Click += new System.EventHandler(this.themng_Click);
            this.themanh.Click += new System.EventHandler(this.themanh_Click);
            this.themmon.Click += new System.EventHandler(this.themmon_Click);
            this.hienthimon.Click += new System.EventHandler(this.hienthimon_Click);
            this.xoamon.Click += new System.EventHandler(this.xoamon_Click);
            this.ngaunhien.Click += new System.EventHandler(this.ngaunhien_Click);

            this.Load += Serverbai5_Load;
            this.FormClosing += Serverbai5_FormClosing;
        }

        private void Serverbai5_Load(object sender, EventArgs e)
        {
            try
            {
                EnsureDatabase();
                LoadNguoiDung();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo CSDL: " + ex.Message);
            }

            StartServer();
        }

        private void Serverbai5_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        #region Database
        private void EnsureDatabase()
        {
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();

                    string sqlNguoiDung = @"
                        CREATE TABLE IF NOT EXISTS NguoiDung (
                            IDNCC INTEGER PRIMARY KEY AUTOINCREMENT,
                            HoVaTen TEXT NOT NULL,
                            QuyenHan TEXT
                        );";

                    string sqlMonAn = @"
                        CREATE TABLE IF NOT EXISTS MonAn (
                            IDMA INTEGER PRIMARY KEY AUTOINCREMENT,
                            TenMonAn TEXT NOT NULL,
                            HinhAnh TEXT,
                            IDNCC INTEGER,
                            FOREIGN KEY(IDNCC) REFERENCES NguoiDung(IDNCC)
                        );";

                    using (var cmd = new SQLiteCommand(sqlNguoiDung, conn)) cmd.ExecuteNonQuery();
                    using (var cmd = new SQLiteCommand(sqlMonAn, conn)) cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        private void LoadNguoiDung()
        {
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "SELECT IDNCC, HoVaTen FROM NguoiDung";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        boxngcc.DataSource = dt;
                        boxngcc.DisplayMember = "HoVaTen";
                        boxngcc.ValueMember = "IDNCC";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load người dùng: " + ex.Message);
            }
        }
        #endregion

        #region Nút xử lý DB (UI)
        private void themng_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxtennguoi.Text))
            {
                MessageBox.Show("Nhập tên người dùng!");
                return;
            }

            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES (@HoVaTen, @QuyenHan)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoVaTen", boxtennguoi.Text.Trim());
                        cmd.Parameters.AddWithValue("@QuyenHan", boxquyen.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thêm người dùng thành công.");
                boxtennguoi.Clear();
                boxquyen.Clear();
                LoadNguoiDung();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm người dùng: " + ex.Message);
            }
        }

        private void themanh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Hiển thị ảnh
                        pictureBox1.Image?.Dispose();
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                        // Chuyển sang base64
                        byte[] bytes = File.ReadAllBytes(ofd.FileName);
                        base64Image = Convert.ToBase64String(bytes);
                        boxanh.Text = Path.GetFileName(ofd.FileName); // chỉ hiển thị tên ảnh
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message);
                        pictureBox1.Image = null;
                        base64Image = "";
                    }
                }
            }
        }

        private void themmon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxtenmon.Text) || boxngcc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập tên món ăn và chọn người cung cấp.");
                return;
            }

            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES (@TenMonAn, @HinhAnh, @IDNCC)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenMonAn", boxtenmon.Text.Trim());
                        cmd.Parameters.AddWithValue("@HinhAnh", base64Image ?? "");
                        cmd.Parameters.AddWithValue("@IDNCC", boxngcc.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Gửi qua tất cả client
                string nguoi = boxngcc.Text.Trim();

                // Làm sạch chuỗi Base64 (tránh xuống dòng gây lỗi)
                string safeBase64 = (base64Image ?? "")
                    .Replace("\r", "")
                    .Replace("\n", "")
                    .Trim();

                // Tăng tính ổn định khi gửi ảnh (base64 có thể dài)
                string message = $"NEWMON|{nguoi}|{boxtenmon.Text.Trim()}|{safeBase64}";
                SendToAllClients(message);

                MessageBox.Show("Thêm món thành công!");
                boxtenmon.Clear();
                boxanh.Clear();
                pictureBox1.Image = null;
                base64Image = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message);
            }
        }

        private void hienthimon_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "SELECT MonAn.IDMA, MonAn.TenMonAn, NguoiDung.HoVaTen FROM MonAn LEFT JOIN NguoiDung ON MonAn.IDNCC = NguoiDung.IDNCC";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader, LoadOption.OverwriteChanges);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị món: " + ex.Message);
            }
        }

        private void xoamon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món để xóa.");
                return;
            }

            try
            {
                var idCell = dataGridView1.SelectedRows[0].Cells["IDMA"];
                if (idCell == null) return;

                int id = Convert.ToInt32(idCell.Value);
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "DELETE FROM MonAn WHERE IDMA = @ID";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Xóa món thành công.");
                hienthimon_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa món: " + ex.Message);
            }
        }
        #endregion

        #region Socket server
        private void StartServer()
        {
            try
            {
                if (serverRunning) return;
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 9000);
                server.Bind(ep);
                server.Listen(10);
                serverRunning = true;

                listenThread = new Thread(ListenClient);
                listenThread.IsBackground = true;
                listenThread.Start();

                MessageBox.Show("Server đã khởi động thành công trên cổng 9000!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động server: " + ex.Message);
            }
        }

        private void StopServer()
        {
            try
            {
                serverRunning = false;
                server?.Close();

                lock (clients)
                {
                    foreach (var c in clients)
                    {
                        try { c.Shutdown(SocketShutdown.Both); c.Close(); } catch { }
                    }
                    clients.Clear();
                }
            }
            catch { }
        }

        private void ListenClient()
        {
            while (serverRunning)
            {
                try
                {
                    Socket client = server.Accept();
                    lock (clients) clients.Add(client);

                    Thread t = new Thread(() => ReceiveClient(client));
                    t.IsBackground = true;
                    t.Start();
                }
                catch { if (!serverRunning) break; }
            }
        }

        private void ReceiveClient(Socket client)
        {
            byte[] buffer = new byte[1024 * 64];
            try
            {
                while (serverRunning)
                {
                    int rec = client.Receive(buffer);
                    if (rec == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, rec);
                    ProcessClientMessage(client, msg);
                }
            }
            catch { }
            finally
            {
                lock (clients) clients.Remove(client);
                try { client.Close(); } catch { }
            }
        }

        private void SendToClient(Socket client, string msg)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    byte[] payload = Encoding.UTF8.GetBytes(msg);
                    byte[] lenPrefix = BitConverter.GetBytes(payload.Length); // little-endian
                                                                              // nếu muốn network order dùng IPAddress.HostToNetworkOrder
                    byte[] packet = new byte[4 + payload.Length];
                    Array.Copy(lenPrefix, 0, packet, 0, 4);
                    Array.Copy(payload, 0, packet, 4, payload.Length);
                    client.Send(packet);
                }
            }
            catch { }
        }

        private void SendToAllClients(string msg)
        {
            byte[] payload = Encoding.UTF8.GetBytes(msg);
            byte[] lenPrefix = BitConverter.GetBytes(payload.Length);
            byte[] packet = new byte[4 + payload.Length];
            Array.Copy(lenPrefix, 0, packet, 0, 4);
            Array.Copy(payload, 0, packet, 4, payload.Length);

            lock (clients)
            {
                foreach (var c in clients)
                {
                    try { c.Send(packet); } catch { }
                }
            }
        }



        #endregion

        #region Xử lý lệnh client
        private void ProcessClientMessage(Socket client, string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            string[] parts = msg.Split('|');
            string cmd = parts[0].ToUpperInvariant();

            try
            {
                switch (cmd)
                {
                    case "USER":
                        using (var conn = new SQLiteConnection(filedb))
                        {
                            conn.Open();
                            string sql = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES (@HoVaTen, @QuyenHan)";
                            using (var cmdSql = new SQLiteCommand(sql, conn))
                            {
                                cmdSql.Parameters.AddWithValue("@HoVaTen", parts[1]);
                                cmdSql.Parameters.AddWithValue("@QuyenHan", parts[2]);
                                cmdSql.ExecuteNonQuery();
                            }
                        }
                        SendToClient(client, "USER_OK|");
                        break;

                    case "FOOD":
                        using (var conn = new SQLiteConnection(filedb))
                        {
                            conn.Open();

                            string sqlID = "SELECT IDNCC FROM NguoiDung WHERE HoVaTen=@name LIMIT 1";
                            int id = -1;
                            using (var cmdSql = new SQLiteCommand(sqlID, conn))
                            {
                                cmdSql.Parameters.AddWithValue("@name", parts[1]);
                                var obj = cmdSql.ExecuteScalar();
                                if (obj != null) id = Convert.ToInt32(obj);
                            }
                            if (id == -1)
                            {
                                SendToClient(client, "ERROR|Người dùng không tồn tại");
                                return;
                            }

                            // ✅ Giữ nguyên form, chỉ thay logic ảnh
                            // Trước đây: kiểm tra File.Exists(parts[3]) → luôn sai
                            // Giờ: lưu trực tiếp base64 mà client gửi lên
                            string base64 = parts[3];

                            string sqlAdd = "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES (@Ten,@Img,@ID)";
                            using (var cmdSql = new SQLiteCommand(sqlAdd, conn))
                            {
                                cmdSql.Parameters.AddWithValue("@Ten", parts[2]);
                                cmdSql.Parameters.AddWithValue("@Img", base64);
                                cmdSql.Parameters.AddWithValue("@ID", id);
                                cmdSql.ExecuteNonQuery();
                            }
                        }

                        SendToClient(client, "FOOD_OK|");
                        break;



                    case "SHOW":
                        using (var conn = new SQLiteConnection(filedb))
                        {
                            conn.Open();
                            string sql = "SELECT TenMonAn, HinhAnh, (SELECT HoVaTen FROM NguoiDung WHERE IDNCC=MonAn.IDNCC) FROM MonAn";
                            using (var cmdSql = new SQLiteCommand(sql, conn))
                            using (var reader = cmdSql.ExecuteReader())
                            {
                                List<string> rows = new List<string>();
                                while (reader.Read())
                                {
                                    string ten = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                    string anh = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                    string nguoi = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                    rows.Add($"{ten},{anh},{nguoi}");
                                }
                                SendToClient(client, "DATA|" + string.Join(";", rows));
                            }
                        }
                        break;

                    case "RANDOM_PERSONAL":
                        using (var conn = new SQLiteConnection(filedb))
                        {
                            conn.Open();

                            // Tìm ID người dùng theo tên
                            string sqlID = "SELECT IDNCC FROM NguoiDung WHERE HoVaTen=@name LIMIT 1";
                            int id = -1;
                            using (var cmdSql = new SQLiteCommand(sqlID, conn))
                            {
                                cmdSql.Parameters.AddWithValue("@name", parts[1]);
                                var obj = cmdSql.ExecuteScalar();
                                if (obj != null) id = Convert.ToInt32(obj);
                            }
                            if (id == -1)
                            {
                                SendToClient(client, "ERROR|Người dùng không tồn tại");
                                return;
                            }

                            // Lấy danh sách món ăn của người đó
                            string sqlGet = "SELECT TenMonAn, HinhAnh FROM MonAn WHERE IDNCC=@id";
                            List<(string, string)> ds = new List<(string, string)>();
                            using (var cmdSql = new SQLiteCommand(sqlGet, conn))
                            {
                                cmdSql.Parameters.AddWithValue("@id", id);
                                using (var rd = cmdSql.ExecuteReader())
                                {
                                    while (rd.Read())
                                    {
                                        string tenMon = rd.GetString(0);
                                        string hinhAnh = rd.IsDBNull(1) ? "" : rd.GetString(1); // ✅ lấy base64 ảnh
                                        ds.Add((tenMon, hinhAnh));
                                    }
                                }
                            }

                            if (ds.Count == 0)
                            {
                                SendToClient(client, $"RANDOM_PERSONAL|{parts[1]}|Chưa có món nào|");
                                return;
                            }

                            // Random món trong danh sách
                            Random rnd = new Random();
                            var mon = ds[rnd.Next(ds.Count)];

                            // ✅ Gửi luôn ảnh base64 cho client
                            SendToClient(client, $"RANDOM_PERSONAL|{parts[1]}|{mon.Item1}|{mon.Item2}");
                        }
                        break;


                    case "DELETE_PERSONAL":
                        string nguoiXoa = parts[1];
                        using (var conn = new SQLiteConnection(filedb))
                        {
                            conn.Open();
                            string sql = @"DELETE FROM MonAn WHERE IDNCC=(SELECT IDNCC FROM NguoiDung WHERE HoVaTen=@name)";
                            using (var cmdSql = new SQLiteCommand(sql, conn))
                            {
                                cmdSql.Parameters.AddWithValue("@name", nguoiXoa);
                                cmdSql.ExecuteNonQuery();
                            }
                        }
                        SendToClient(client, "OK|Đã xóa món cá nhân");
                        break;

                    case "RANDOM_GLOBAL":
                        using (var conn = new SQLiteConnection(filedb))
                        {
                            conn.Open();

                            // Lấy toàn bộ món ăn trong database (có ảnh)
                            string sqlGet = "SELECT MonAn.TenMonAn, MonAn.HinhAnh, NguoiDung.HoVaTen " +
                                            "FROM MonAn JOIN NguoiDung ON MonAn.IDNCC = NguoiDung.IDNCC";
                            List<(string, string, string)> ds = new List<(string, string, string)>();
                            using (var cmdSql = new SQLiteCommand(sqlGet, conn))
                            {
                                using (var rd = cmdSql.ExecuteReader())
                                {
                                    while (rd.Read())
                                    {
                                        string tenMon = rd.GetString(0);
                                        string hinhAnh = rd.IsDBNull(1) ? "" : rd.GetString(1); // ✅ đọc base64 ảnh
                                        string tenNguoi = rd.GetString(2);
                                        ds.Add((tenMon, hinhAnh, tenNguoi));
                                    }
                                }
                            }

                            if (ds.Count == 0)
                            {
                                SendToClient(client, "RANDOM_GLOBAL|Không có món nào trong hệ thống|");
                                return;
                            }

                            // Random món từ danh sách toàn bộ
                            Random rnd = new Random();
                            var mon = ds[rnd.Next(ds.Count)];

                            // ✅ Gửi luôn ảnh base64 cho client
                            SendToClient(client, $"RANDOM_GLOBAL|{mon.Item3}|{mon.Item1}|{mon.Item2}");
                        }
                        break;


                    default:
                        SendToClient(client, "ERROR|Lệnh không hợp lệ");
                        break;
                }
            }
            catch (Exception ex)
            {
                SendToClient(client, "ERROR|" + ex.Message);
            }
        }


        private (string Name, string Image, string Provider)? GetRandomDish()
        {
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = @"
                        SELECT TenMonAn, HinhAnh, (SELECT HoVaTen FROM NguoiDung WHERE IDNCC = MonAn.IDNCC)
                        FROM MonAn ORDER BY RANDOM() LIMIT 1";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string ten = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            string anh = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            string nguoi = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            return (ten, anh, nguoi);
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        #endregion

        private void ngaunhien_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = @"
                SELECT MonAn.TenMonAn, MonAn.HinhAnh, NguoiDung.HoVaTen 
                FROM MonAn 
                JOIN NguoiDung ON MonAn.IDNCC = NguoiDung.IDNCC
                ORDER BY RANDOM() LIMIT 1";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string tenMon = reader.GetString(0);
                            string hinhAnhBase64 = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            string tenNguoi = reader.GetString(2);

                            tenmon.Text = tenMon;
                            tennguoi.Text = tenNguoi;

                            // Giải mã ảnh từ base64
                            if (!string.IsNullOrEmpty(hinhAnhBase64))
                            {
                                byte[] bytes = Convert.FromBase64String(hinhAnhBase64);
                                using (var ms = new MemoryStream(bytes))
                                {
                                    pictureBox2.Image = Image.FromStream(ms);
                                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                                }
                            }
                            else
                            {
                                pictureBox2.Image = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có món ăn nào trong cơ sở dữ liệu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị món ngẫu nhiên: " + ex.Message);
            }
        }

        private void resetmonan_Click(object sender, EventArgs e)
        {
            try
            {
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "monan.db");
                using (SQLiteConnection conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();

                    // 🧹 Xóa cả hai bảng (nếu tồn tại)
                    string dropTables = @"
                DROP TABLE IF EXISTS MonAn;
                DROP TABLE IF EXISTS NguoiDung;
            ";
                    using (SQLiteCommand cmd = new SQLiteCommand(dropTables, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 🧱 Tạo lại bảng trống
                    string createTables = @"
                CREATE TABLE NguoiDung (
                    IDNCC INTEGER PRIMARY KEY AUTOINCREMENT,
                    HoVaTen TEXT NOT NULL,
                    QuyenHan TEXT
                );

                CREATE TABLE MonAn (
                    IDMA INTEGER PRIMARY KEY AUTOINCREMENT,
                    TenMonAn TEXT NOT NULL,
                    HinhAnh TEXT,
                    IDNCC INTEGER,
                    FOREIGN KEY(IDNCC) REFERENCES NguoiDung(IDNCC)
                );
            ";
                    using (SQLiteCommand cmd = new SQLiteCommand(createTables, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                // 🧽 Làm sạch toàn bộ giao diện
                boxtenmon.Clear();
                boxanh.Clear();
                boxtennguoi.Clear();
                boxquyen.Clear();
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                dataGridView1.DataSource = null;

                // 🧺 Dọn combobox
                boxngcc.DataSource = null;
                boxngcc.Items.Clear();
                boxngcc.Text = "";
                boxngcc.SelectedIndex = -1;
                boxngcc.Refresh();

                MessageBox.Show("✅ Đã reset hoàn toàn cơ sở dữ liệu (MonAn + NguoiDung)!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi reset: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void boxngcc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
