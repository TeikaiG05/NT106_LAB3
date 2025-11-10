namespace Bai4
{
    partial class Client
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label1, label2, label3, label4, label5;
        private System.Windows.Forms.TextBox txtName, txtResult;
        private System.Windows.Forms.ComboBox cmbMovie, cmbRoom;
        private System.Windows.Forms.CheckedListBox clbSeats;
        private System.Windows.Forms.Button btnBook;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblServerIP = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbMovie = new System.Windows.Forms.ComboBox();
            this.cmbRoom = new System.Windows.Forms.ComboBox();
            this.clbSeats = new System.Windows.Forms.CheckedListBox();
            this.btnBook = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.ForeColor = System.Drawing.Color.White;
            this.lblServerIP.Location = new System.Drawing.Point(12, 10);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(54, 13);
            this.lblServerIP.TabIndex = 0;
            this.lblServerIP.Text = "Server IP:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(100, 7);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(120, 20);
            this.txtServerIP.TabIndex = 1;
            this.txtServerIP.Text = "127.0.0.1";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(100, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 2;
            // 
            // cmbMovie
            // 
            this.cmbMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMovie.Location = new System.Drawing.Point(100, 70);
            this.cmbMovie.Name = "cmbMovie";
            this.cmbMovie.Size = new System.Drawing.Size(200, 21);
            this.cmbMovie.TabIndex = 3;
            // 
            // cmbRoom
            // 
            this.cmbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoom.Location = new System.Drawing.Point(100, 100);
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.Size = new System.Drawing.Size(200, 21);
            this.cmbRoom.TabIndex = 4;
            // 
            // clbSeats
            // 
            this.clbSeats.CheckOnClick = true;
            this.clbSeats.Items.AddRange(new object[] {
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "B1",
            "B2",
            "B3",
            "B4",
            "B5",
            "C1",
            "C2",
            "C3",
            "C4",
            "C5"});
            this.clbSeats.Location = new System.Drawing.Point(100, 138);
            this.clbSeats.Name = "clbSeats";
            this.clbSeats.Size = new System.Drawing.Size(200, 94);
            this.clbSeats.TabIndex = 5;
            // 
            // btnBook
            // 
            this.btnBook.Location = new System.Drawing.Point(100, 248);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(75, 23);
            this.btnBook.TabIndex = 6;
            this.btnBook.Text = "Book";
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.txtResult.ForeColor = System.Drawing.Color.White;
            this.txtResult.Location = new System.Drawing.Point(352, 69);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(360, 150);
            this.txtResult.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Movie:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Room:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "Seats:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(349, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "Result:";
            // 
            // Client
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.Controls.Add(this.lblServerIP);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmbMovie);
            this.Controls.Add(this.cmbRoom);
            this.Controls.Add(this.clbSeats);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Name = "Client";
            this.Size = new System.Drawing.Size(730, 560);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
