namespace Bai3
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btServer = new System.Windows.Forms.Button();
            this.btClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btServer
            // 
            this.btServer.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btServer.Location = new System.Drawing.Point(44, 39);
            this.btServer.Name = "btServer";
            this.btServer.Size = new System.Drawing.Size(287, 56);
            this.btServer.TabIndex = 0;
            this.btServer.Text = "Open TCP Server";
            this.btServer.UseVisualStyleBackColor = true;
            this.btServer.Click += new System.EventHandler(this.btServer_Click);
            // 
            // btClient
            // 
            this.btClient.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClient.Location = new System.Drawing.Point(44, 127);
            this.btClient.Name = "btClient";
            this.btClient.Size = new System.Drawing.Size(287, 56);
            this.btClient.TabIndex = 1;
            this.btClient.Text = "Open new TCP Client";
            this.btClient.UseVisualStyleBackColor = true;
            this.btClient.Click += new System.EventHandler(this.btClient_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 219);
            this.Controls.Add(this.btClient);
            this.Controls.Add(this.btServer);
            this.Name = "Menu";
            this.Text = "LAB3_BAI03";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btServer;
        private System.Windows.Forms.Button btClient;
    }
}

