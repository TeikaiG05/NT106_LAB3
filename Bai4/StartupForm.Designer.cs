namespace Bai4
{
    partial class StartupForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel PanelTop;
        private System.Windows.Forms.Label LabelTitle;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnServer;
        private System.Windows.Forms.Button BtnClient;
        private System.Windows.Forms.Panel PanelMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.PanelTop = new System.Windows.Forms.Panel();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnServer = new System.Windows.Forms.Button();
            this.BtnClient = new System.Windows.Forms.Button();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.PanelTop.SuspendLayout();
            this.SuspendLayout();
            // PanelTop
            this.PanelTop.BackColor = System.Drawing.Color.FromArgb(44, 47, 51);
            this.PanelTop.Controls.Add(this.LabelTitle);
            this.PanelTop.Controls.Add(this.BtnClose);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(900, 40);
            this.PanelTop.TabIndex = 0;
            this.PanelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTop_MouseDown);
            // LabelTitle
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.LabelTitle.ForeColor = System.Drawing.Color.White;
            this.LabelTitle.Location = new System.Drawing.Point(15, 9);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(98, 20);
            this.LabelTitle.Text = "StartupForm";
            // BtnClose
            this.BtnClose.BackColor = System.Drawing.Color.Gray;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Location = new System.Drawing.Point(870, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(30, 30);
            this.BtnClose.Text = "X";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // BtnServer
            this.BtnServer.BackColor = System.Drawing.Color.FromArgb(47, 49, 54);
            this.BtnServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnServer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnServer.ForeColor = System.Drawing.Color.White;
            this.BtnServer.Location = new System.Drawing.Point(10, 60);
            this.BtnServer.Name = "BtnServer";
            this.BtnServer.Size = new System.Drawing.Size(150, 45);
            this.BtnServer.Text = "Server";
            this.BtnServer.UseVisualStyleBackColor = false;
            this.BtnServer.Click += new System.EventHandler(this.BtnServer_Click);
            // BtnClient
            this.BtnClient.BackColor = System.Drawing.Color.FromArgb(47, 49, 54);
            this.BtnClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClient.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnClient.ForeColor = System.Drawing.Color.White;
            this.BtnClient.Location = new System.Drawing.Point(10, 115);
            this.BtnClient.Name = "BtnClient";
            this.BtnClient.Size = new System.Drawing.Size(150, 45);
            this.BtnClient.Text = "Client";
            this.BtnClient.UseVisualStyleBackColor = false;
            this.BtnClient.Click += new System.EventHandler(this.BtnClient_Click);
            // PanelMain
            this.PanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelMain.BackColor = System.Drawing.Color.FromArgb(54, 57, 63);
            this.PanelMain.Location = new System.Drawing.Point(170, 40);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(730, 560);
            this.PanelMain.TabIndex = 3;
            // StartupForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(35, 39, 42);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.PanelTop);
            this.Controls.Add(this.BtnServer);
            this.Controls.Add(this.BtnClient);
            this.Controls.Add(this.PanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartupForm";
            this.PanelTop.ResumeLayout(false);
            this.PanelTop.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
