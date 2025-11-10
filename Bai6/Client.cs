using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai6
{
    public partial class Client : Form
    {
        private TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;
        private CancellationTokenSource cts;
        private bool isReading = false;
        private bool isConnecting = false;
        private bool isAttaching = false;

        public Client()
        {
            InitializeComponent();
            this.Load += Client_Load;
            btConnect.Click += btConnect_Click;
            btSend.Click += btSend_Click;
            btDisconnect.Click += btDisconnect_Click;
            btBrowse.Click += btBrowse_Click;
        }

        private void Client_Load(object sender, EventArgs e)
        {
            btSend.Enabled = false;
            btDisconnect.Enabled = false;

            lvChat.View = View.List;
            lvChat.Columns.Clear();
            lvChat.HeaderStyle = ColumnHeaderStyle.None;

            lvParticipants.View = View.List;

            cbSendto.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSendto.Items.Clear();
            cbSendto.Items.Add("ALL");
            cbSendto.SelectedIndex = 0;

            tbMessage.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    btSend.PerformClick();
                }
            };
        }

        private void AddLine(string text)
        {
            if (IsDisposed) return;
            this.Invoke((Action)(() =>
            {
                lvChat.Items.Add(text);
                lvChat.Items[lvChat.Items.Count - 1].EnsureVisible();
            }));
        }

        private void SetParticipants(string[] users)
        {
            if (users == null) users = new string[0];
            var me = tbUser.Text?.Trim() ?? "";

            this.Invoke((Action)(() =>
            {
                string current = cbSendto.SelectedItem as string;

                lvParticipants.Items.Clear();
                cbSendto.Items.Clear();
                cbSendto.Items.Add("ALL");

                foreach (var u in users)
                {
                    if (string.IsNullOrWhiteSpace(u)) continue;
                    lvParticipants.Items.Add(u);
                    if (!u.Equals(me, StringComparison.OrdinalIgnoreCase))
                        cbSendto.Items.Add(u);
                }

                int idx = (current != null) ? cbSendto.Items.IndexOf(current) : -1;
                cbSendto.SelectedIndex = (idx >= 0) ? idx : 0;
            }));
        }

        private string NormalizeForDisplay(string line)
        {
            if (line.StartsWith("USERS|")) return null;
            if (line.StartsWith("SYSTEM|", StringComparison.OrdinalIgnoreCase)) return null;
            if (line.StartsWith("ERROR|" , StringComparison.OrdinalIgnoreCase)) return null;
            if (line.StartsWith("NAME|"  , StringComparison.OrdinalIgnoreCase)) return null;

            string me = tbUser.Text.Trim();
            string s = line;

            int pmIdx = s.IndexOf(" (pm):", StringComparison.OrdinalIgnoreCase);
            if (pmIdx >= 0)
            {
                string sender = s.Substring(0, pmIdx);
                string body = s.Substring(pmIdx + " (pm):".Length).Trim();
                return sender + " (Tin nhan rieng): " + body;
            }

            int sep = s.IndexOf(": ");
            if (sep > 0)
            {
                string sender = s.Substring(0, sep);
                if (sender.Equals(me, StringComparison.OrdinalIgnoreCase))
                    return "Me: " + s.Substring(sep + 2);
            }

            return s;
        }

        private async Task ReadLoopAsync(CancellationToken token)
        {
            try
            {
                string line;
                while (!token.IsCancellationRequested &&
                       reader != null &&
                       (line = await reader.ReadLineAsync()) != null)
                {
                    if (line.StartsWith("USERS|"))
                    {
                        var list = line.Substring(6)
                                       .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        SetParticipants(list);
                        continue;
                    }
                    if (line.StartsWith("FILE|") || line.StartsWith("FILEPM|"))
                    {
                        HandleIncomingFile(line);
                        continue;
                    }
                    var display = NormalizeForDisplay(line);
                    if (display != null) AddLine(display);
                }
            }
            catch (IOException) { AddLine("Receive error: connection closed."); }
            catch (ObjectDisposedException) { }
            catch (Exception ex) { AddLine("Receive error: " + ex.Message); }
            finally
            {
                isReading = false;
                this.Invoke((Action)(() =>
                {
                    btConnect.Enabled = true;
                    btSend.Enabled = false;
                    btDisconnect.Enabled = false;
                }));
            }
        }

        private async void btConnect_Click(object sender, EventArgs e)
        {
            if (isConnecting) return;
            if (tcpClient != null && tcpClient.Connected) return;

            var name = tbUser.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Please enter your name."); return; }

            isConnecting = true;
            btConnect.Enabled = false;
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(IPAddress.Parse("25.5.134.104"), 8080);

                var ns = tcpClient.GetStream();
                reader = new StreamReader(ns, Encoding.ASCII);
                writer = new StreamWriter(ns, Encoding.ASCII) { AutoFlush = true };
                await writer.WriteLineAsync("NAME|" + name);

                btSend.Enabled = true;
                btDisconnect.Enabled = true;

                if (!isReading)
                {
                    isReading = true;
                    cts = new CancellationTokenSource();
                    _ = ReadLoopAsync(cts.Token);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect error: " + ex.Message);
                btConnect.Enabled = true;
            }
            finally
            {
                isConnecting = false;
            }
        }

        private async void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                {
                    MessageBox.Show("Not connected to server.");
                    return;
                }

                var text = tbMessage.Text.Trim();
                if (string.IsNullOrWhiteSpace(text)) return;

                var to = (cbSendto.SelectedItem as string) ?? "ALL";

                if (!to.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    await writer.WriteLineAsync($"/pm {to} {text}");
                    AddLine($"Me → {to}: {text}");
                }
                else
                {
                    await writer.WriteLineAsync(text);
                }

                tbMessage.Clear();
                tbMessage.Focus();
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
                cts?.Cancel();
                isReading = false;

                try { writer?.WriteLine("quit"); } catch { }
                try { reader?.Dispose(); } catch { }
                try { writer?.Dispose(); } catch { }
                try { tcpClient?.Close(); } catch { }

                reader = null; writer = null; tcpClient = null;
            }
            finally
            {
                btConnect.Enabled = true;
                btSend.Enabled = false;
                btDisconnect.Enabled = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                cts?.Cancel();
                isReading = false;
                reader?.Dispose();
                writer?.Dispose();
                tcpClient?.Close();
            }
            catch { }
            base.OnFormClosing(e);
        }

        private async void btBrowse_Click(object sender, EventArgs e)
        {
            if (isAttaching) return;       
            isAttaching = true;
            btBrowse.Enabled = false;
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                { MessageBox.Show("Not connected to server."); return; }

                using (var ofd = new OpenFileDialog())
                {
                    ofd.Title = "Choose image (.jpg/.png) or text (.txt)";
                    ofd.Filter = "Images/Text|*.jpg;*.jpeg;*.png;*.txt|JPEG|*.jpg;*.jpeg|PNG|*.png|Text|*.txt";
                    ofd.Multiselect = false;

                    if (ofd.ShowDialog(this) != DialogResult.OK) return;

                    string filePath = ofd.FileName;
                    string fileName = System.IO.Path.GetFileName(filePath);
                    string mime = GetMimeFromExt(System.IO.Path.GetExtension(filePath));

                    byte[] bytes = File.ReadAllBytes(filePath);
                    string b64 = Convert.ToBase64String(bytes);

                    string to = (cbSendto.SelectedItem as string) ?? "ALL";

                    if (!to.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                    {
                        await writer.WriteLineAsync($"FILE|{to}|{fileName}|{mime}|{b64}");
                        AddLine($"Me → {to}: (file) {fileName}");
                    }
                    else
                    {
                        await writer.WriteLineAsync($"FILE|ALL|{fileName}|{mime}|{b64}");
                        AddLine($"Me: (file) {fileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Browse error: " + ex.Message);
            }
            finally
            {
                btBrowse.Enabled = true;    
                isAttaching = false;
            }
        }

        private string GetMimeFromExt(string ext)
        {
            if (string.IsNullOrEmpty(ext)) return "application/octet-stream";
            ext = ext.ToLowerInvariant();
            if (ext == ".jpg" || ext == ".jpeg") return "image/jpeg";
            if (ext == ".png") return "image/png";
            if (ext == ".txt") return "text/plain";
            return "application/octet-stream";
        }

        private void HandleIncomingFile(string line)
        {
            bool isPm = line.StartsWith("FILEPM|", StringComparison.OrdinalIgnoreCase);
            var parts = line.Split(new[] { '|' }, 5);
            if (parts.Length < 5) return;

            string sender = parts[1];
            string filename = parts[2];
            string mime = parts[3];
            string b64 = parts[4];

            // Thư mục lưu
            string root = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ChatDownloads");
            Directory.CreateDirectory(root);

            // Tránh trùng tên
            string safeName = $"{System.IO.Path.GetFileNameWithoutExtension(filename)}_{DateTime.Now:yyyyMMdd_HHmmssfff}{System.IO.Path.GetExtension(filename)}";
            string savePath = System.IO.Path.Combine(root, safeName);

            try
            {
                byte[] bytes = Convert.FromBase64String(b64);
                File.WriteAllBytes(savePath, bytes);

                string note = isPm
                    ? $"{sender} (Tin nhan rieng): (file) {filename} -> saved: {savePath}"
                    : $"{sender}: (file) {filename} -> saved: {savePath}";
                AddLine(note);
            }
            catch (Exception ex)
            {
                AddLine("Receive file error: " + ex.Message);
            }
        }

    }
}
