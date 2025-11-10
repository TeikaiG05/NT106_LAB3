namespace Bai6
{
    partial class Client
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
            this.btDisconnect = new System.Windows.Forms.Button();
            this.btSend = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvChat = new System.Windows.Forms.ListView();
            this.btBrowse = new System.Windows.Forms.Button();
            this.cbSendto = new System.Windows.Forms.ComboBox();
            this.lvParticipants = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(422, 394);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(81, 39);
            this.btDisconnect.TabIndex = 7;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(572, 442);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(81, 39);
            this.btSend.TabIndex = 6;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(320, 394);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(81, 39);
            this.btConnect.TabIndex = 5;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(133, 394);
            this.tbUser.Multiline = true;
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(153, 39);
            this.tbUser.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Your name:";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(133, 442);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(391, 39);
            this.tbMessage.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 455);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Message:";
            // 
            // lvChat
            // 
            this.lvChat.HideSelection = false;
            this.lvChat.Location = new System.Drawing.Point(69, 27);
            this.lvChat.Name = "lvChat";
            this.lvChat.Size = new System.Drawing.Size(584, 316);
            this.lvChat.TabIndex = 12;
            this.lvChat.UseCompatibleStateImageBehavior = false;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(530, 442);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(36, 39);
            this.btBrowse.TabIndex = 13;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // cbSendto
            // 
            this.cbSendto.FormattingEnabled = true;
            this.cbSendto.Location = new System.Drawing.Point(532, 349);
            this.cbSendto.Name = "cbSendto";
            this.cbSendto.Size = new System.Drawing.Size(121, 21);
            this.cbSendto.TabIndex = 14;
            // 
            // lvParticipants
            // 
            this.lvParticipants.HideSelection = false;
            this.lvParticipants.Location = new System.Drawing.Point(697, 54);
            this.lvParticipants.Name = "lvParticipants";
            this.lvParticipants.Size = new System.Drawing.Size(120, 289);
            this.lvParticipants.TabIndex = 15;
            this.lvParticipants.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(726, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Participants";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 504);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvParticipants);
            this.Controls.Add(this.cbSendto);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.lvChat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.tbUser);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvChat;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.ComboBox cbSendto;
        private System.Windows.Forms.ListView lvParticipants;
        private System.Windows.Forms.Label label3;
    }
}