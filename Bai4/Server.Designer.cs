namespace Bai4
{
    partial class Server
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(35, 39, 42);
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Location = new System.Drawing.Point(12, 12);
            this.txtLog.Size = new System.Drawing.Size(700, 540);
            // 
            // Server
            // 
            this.BackColor = System.Drawing.Color.FromArgb(54, 57, 63);
            this.Controls.Add(this.txtLog);
            this.Size = new System.Drawing.Size(730, 560);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
