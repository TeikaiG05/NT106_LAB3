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
        // Database
        private string dbFilePath;
        private string filedb; // connection string

        // Socket server
        private Socket server;
        private List<Socket> clients = new List<Socket>();
        private Thread listenThread;
        private volatile bool serverRunning = false;

        public serverbai5()
        {
            InitializeComponent();

            // Prepare DB file path in application folder (safer than relative)
            dbFilePath = Path.Combine(Application.StartupPath, "monan.db");
            filedb = $"Data Source={dbFilePath};Version=3;";

            // Do NOT start server or open DB that may block UI here.
            // Initialize UI state if needed.
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
                MessageBox.Show("Lỗi khi khởi tạo cơ sở dữ liệu: " + ex.Message);
            }

            // Start server after form loaded so UI shows up even if server later fails
            StartServer();
        }

        private void Serverbai5_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        #region Database helpers

        private void EnsureDatabase()
        {
            // If db file not exist -> create and initialize schema
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();

                    string sqlNguoiDung = @"
                        CREATE TABLE IF NOT EXISTS NguoiDung
                        (
                            IDNCC INTEGER PRIMARY KEY AUTOINCREMENT,
                            HoVaTen TEXT NOT NULL,
                            QuyenHan TEXT
                        );";

                    string sqlMonAn = @"
                        CREATE TABLE IF NOT EXISTS MonAn
                        (
                            IDMA INTEGER PRIMARY KEY AUTOINCREMENT,
                            TenMonAn TEXT NOT NULL,
                            HinhAnh TEXT,
                            IDNCC INTEGER,
                            FOREIGN KEY(IDNCC) REFERENCES NguoiDung(IDNCC)
                        );";

                    using (var cmd = new SQLiteCommand(sqlNguoiDung, conn))
                        cmd.ExecuteNonQuery();
                    using (var cmd = new SQLiteCommand(sqlMonAn, conn))
                        cmd.ExecuteNonQuery();

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

                        // Ensure invoke if called from non-UI thread (we call from UI thread here)
                        boxngcc.DataSource = dt;
                        boxngcc.DisplayMember = "HoVaTen";
                        boxngcc.ValueMember = "IDNCC";
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load người dùng: " + ex.Message);
            }
        }

        #endregion

        #region UI button handlers (DB operations)

        private void themng_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxtennguoi.Text))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng.");
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
                    conn.Close();
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
                    boxanh.Text = ofd.FileName;
                    try
                    {
                        pictureBox1.Image?.Dispose();
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                    }
                    catch { pictureBox1.Image = null; }
                }
            }
        }

        private void themmon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxtenmon.Text) || boxngcc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập tên món ăn và chọn người cung cấp món ăn.");
                return;
            }

            string hinhanh = boxanh.Text;
            if (!string.IsNullOrEmpty(hinhanh) && !File.Exists(hinhanh))
            {
                MessageBox.Show("Vui lòng chọn hình ảnh hợp lệ.");
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
                        cmd.Parameters.AddWithValue("@HinhAnh", hinhanh);
                        cmd.Parameters.AddWithValue("@IDNCC", boxngcc.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                MessageBox.Show("Thêm món ăn thành công.");
                boxtenmon.Clear();
                boxanh.Clear();
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món ăn: " + ex.Message);
            }
        }

        private void hienthimon_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = @"
                        SELECT MonAn.IDMA, MonAn.TenMonAn, MonAn.HinhAnh, NguoiDung.HoVaTen 
                        FROM MonAn 
                        LEFT JOIN NguoiDung ON MonAn.IDNCC = NguoiDung.IDNCC";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // UI thread: safe
                        dataGridView1.DataSource = dt;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị món: " + ex.Message);
            }
        }

        private void xoamon_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn món ăn để xóa.");
                    return;
                }

                // Get IDMA column: may be int32 or string - convert safe
                var cell = dataGridView1.SelectedRows[0].Cells["IDMA"];
                if (cell == null || cell.Value == null)
                {
                    MessageBox.Show("Không tìm thấy ID món.");
                    return;
                }
                int idma = Convert.ToInt32(cell.Value);

                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "DELETE FROM MonAn WHERE IDMA = @IDMA";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDMA", idma);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                MessageBox.Show("Xóa món ăn thành công.");
                hienthimon_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa món: " + ex.Message);
            }
        }

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
                        LEFT JOIN NguoiDung ON MonAn.IDNCC = NguoiDung.IDNCC
                        ORDER BY RANDOM() LIMIT 1";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string ten = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            string anh = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            string nguoi = reader.IsDBNull(2) ? "" : reader.GetString(2);

                            tenmon.Text = ten;
                            tennguoi.Text = nguoi;

                            if (!string.IsNullOrEmpty(anh) && File.Exists(anh))
                            {
                                try
                                {
                                    pictureBox2.Image?.Dispose();
                                    pictureBox2.Image = Image.FromFile(anh);
                                }
                                catch
                                {
                                    pictureBox2.Image = null;
                                }
                            }
                            else
                            {
                                pictureBox2.Image?.Dispose();
                                pictureBox2.Image = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có món ăn trong cơ sở dữ liệu.");
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy ngẫu nhiên: " + ex.Message);
            }
        }

        #endregion

        #region Server socket

        private void StartServer()
        {
            try
            {
                // If already running, skip
                if (serverRunning) return;

                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 9000);
                server.Bind(ep);
                server.Listen(10);

                serverRunning = true;
                listenThread = new Thread(ListenClient);
                listenThread.IsBackground = true;
                listenThread.Start();

                // Use Invoke to interact with UI thread (MessageBox is fine)
                this.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("Server đã khởi động thành công trên cổng 9000!");
                }));
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("Lỗi khi khởi động server: " + ex.Message);
                }));
            }
        }

        private void StopServer()
        {
            try
            {
                serverRunning = false;

                try
                {
                    // Close listening socket
                    server?.Close();
                }
                catch { }

                // Close all client sockets
                lock (clients)
                {
                    foreach (var c in clients)
                    {
                        try { c.Shutdown(SocketShutdown.Both); c.Close(); }
                        catch { }
                    }
                    clients.Clear();
                }

                // Abort listen thread
                try
                {
                    if (listenThread != null && listenThread.IsAlive)
                    {
                        listenThread.Join(500);
                        if (listenThread.IsAlive) listenThread.Abort();
                    }
                }
                catch { }
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

                    Thread recv = new Thread(() => ReceiveClient(client));
                    recv.IsBackground = true;
                    recv.Start();
                }
                catch
                {
                    // if server closed, break
                    if (!serverRunning) break;
                }
            }
        }

        private void ReceiveClient(Socket client)
        {
            byte[] buffer = new byte[1024 * 8];
            try
            {
                while (serverRunning)
                {
                    int received = client.Receive(buffer);
                    if (received == 0) break;

                    string data = Encoding.UTF8.GetString(buffer, 0, received);
                    // Process message
                    ProcessClientMessage(client, data);
                }
            }
            catch
            {
                // ignore, client disconnected
            }
            finally
            {
                lock (clients) { clients.Remove(client); }
                try { client.Close(); } catch { }
            }
        }

        private void SendToClient(Socket client, string msg)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    var b = Encoding.UTF8.GetBytes(msg);
                    client.Send(b);
                }
            }
            catch { }
        }

        #endregion

        #region Process client messages (simple protocol)

        private void ProcessClientMessage(Socket client, string msg)
        {
            // Protocol: TYPE|...  (very simple)
            if (string.IsNullOrEmpty(msg)) return;

            string[] parts = msg.Split('|');
            string cmd = parts[0].ToUpperInvariant();

            try
            {
                switch (cmd)
                {
                    case "USER":
                        // USER|Name|Role
                        if (parts.Length >= 3)
                        {
                            string name = parts[1];
                            string role = parts[2];
                            InsertNguoiDung(name, role);
                            SendToClient(client, "USER_OK|");
                        }
                        break;

                    case "FOOD":
                        // FOOD|User|FoodName|ImagePath
                        if (parts.Length >= 4)
                        {
                            string user = parts[1];
                            string food = parts[2];
                            string img = parts[3];
                            InsertMonAn(user, food, img);
                            SendToClient(client, "FOOD_OK|");
                        }
                        break;

                    case "SHOW":
                        // return DATA|item1;item2;...
                        var data = BuildDataString();
                        SendToClient(client, "DATA|" + data);
                        break;

                    case "RANDOM_GLOBAL":
                        {
                            var rand = GetRandomDish();
                            if (rand != null)
                            {
                                string base64 = "";
                                if (File.Exists(rand.Value.Image))
                                {
                                    byte[] bytes = File.ReadAllBytes(rand.Value.Image);
                                    base64 = Convert.ToBase64String(bytes);
                                }

                                SendToClient(client, $"RANDOM|{rand.Value.Provider}|{rand.Value.Name}|{base64}");
                            }
                            else
                            {
                                SendToClient(client, "RANDOM|||");
                            }
                            break;
                        }

                }
            }
            catch
            {
                // ignore processing errors
            }
        }

        #endregion

        #region DB helper methods used by socket logic

        private void InsertNguoiDung(string name, string role)
        {
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES (@n, @r)";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@n", name);
                        cmd.Parameters.AddWithValue("@r", role);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                // update combobox on UI thread
                this.BeginInvoke(new Action(() => LoadNguoiDung()));
            }
            catch { }
        }

        private void InsertMonAn(string user, string food, string img)
        {
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC)
                        VALUES (@ten, @anh, (SELECT IDNCC FROM NguoiDung WHERE HoVaTen = @user LIMIT 1))";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ten", food);
                        cmd.Parameters.AddWithValue("@anh", img);
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch { }
        }

        private string BuildDataString()
        {
            var sb = new StringBuilder();
            try
            {
                using (var conn = new SQLiteConnection(filedb))
                {
                    conn.Open();
                    string sql = "SELECT TenMonAn, HinhAnh, (SELECT HoVaTen FROM NguoiDung WHERE IDNCC = MonAn.IDNCC) FROM MonAn";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ten = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            string anh = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            string nguoi = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            sb.Append($"{ten},{anh},{nguoi};");
                        }
                    }
                    conn.Close();
                }
            }
            catch { }
            return sb.ToString();
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
                    conn.Close();
                }
            }
            catch { }
            return null;
        }

        #endregion
    }
}
