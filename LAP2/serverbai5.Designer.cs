using System.Drawing;
using System.Windows.Forms;

namespace LAP2
{
    partial class serverbai5
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
            themng = new Button();
            themmon = new Button();
            dataGridView1 = new DataGridView();
            boxtennguoi = new TextBox();
            boxquyen = new TextBox();
            boxidmon = new TextBox();
            boxtenmon = new TextBox();
            boxanh = new TextBox();
            pictureBox1 = new PictureBox();
            xoamon = new Button();
            hienthimon = new Button();
            ngaunhien = new Button();
            tenmon = new Label();
            tennguoi = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            themanh = new Button();
            boxngcc = new ComboBox();
            label8 = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // themng
            // 
            themng.Location = new Point(130, 84);
            themng.Name = "themng";
            themng.Size = new Size(75, 23);
            themng.TabIndex = 0;
            themng.Text = "Thêm";
            themng.UseVisualStyleBackColor = true;
            themng.Click += themng_Click;
            // 
            // themmon
            // 
            themmon.Location = new Point(273, 289);
            themmon.Name = "themmon";
            themmon.Size = new Size(75, 23);
            themmon.TabIndex = 1;
            themmon.Text = "Thêm";
            themmon.UseVisualStyleBackColor = true;
            themmon.Click += themmon_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(548, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(240, 328);
            dataGridView1.TabIndex = 2;
            // 
            // boxtennguoi
            // 
            boxtennguoi.Location = new Point(94, 26);
            boxtennguoi.Name = "boxtennguoi";
            boxtennguoi.Size = new Size(148, 23);
            boxtennguoi.TabIndex = 3;
            // 
            // boxquyen
            // 
            boxquyen.Location = new Point(94, 55);
            boxquyen.Name = "boxquyen";
            boxquyen.Size = new Size(148, 23);
            boxquyen.TabIndex = 4;
            // 
            // boxidmon
            // 
            boxidmon.Location = new Point(130, 202);
            boxidmon.Name = "boxidmon";
            boxidmon.Size = new Size(137, 23);
            boxidmon.TabIndex = 5;
            // 
            // boxtenmon
            // 
            boxtenmon.Location = new Point(130, 231);
            boxtenmon.Name = "boxtenmon";
            boxtenmon.Size = new Size(137, 23);
            boxtenmon.TabIndex = 6;
            // 
            // boxanh
            // 
            boxanh.Location = new Point(130, 260);
            boxanh.Name = "boxanh";
            boxanh.Size = new Size(137, 23);
            boxanh.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(319, 202);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // xoamon
            // 
            xoamon.Location = new Point(548, 346);
            xoamon.Name = "xoamon";
            xoamon.Size = new Size(75, 23);
            xoamon.TabIndex = 9;
            xoamon.Text = "Xóa";
            xoamon.UseVisualStyleBackColor = true;
            xoamon.Click += xoamon_Click;
            // 
            // hienthimon
            // 
            hienthimon.Location = new Point(713, 346);
            hienthimon.Name = "hienthimon";
            hienthimon.Size = new Size(75, 23);
            hienthimon.TabIndex = 10;
            hienthimon.Text = "Hiển thị";
            hienthimon.UseVisualStyleBackColor = true;
            hienthimon.Click += hienthimon_Click;
            // 
            // ngaunhien
            // 
            ngaunhien.Location = new Point(430, 163);
            ngaunhien.Name = "ngaunhien";
            ngaunhien.Size = new Size(100, 23);
            ngaunhien.TabIndex = 11;
            ngaunhien.Text = "Ngẫu nhiên";
            ngaunhien.UseVisualStyleBackColor = true;
            ngaunhien.Click += ngaunhien_Click;
            // 
            // tenmon
            // 
            tenmon.AutoSize = true;
            tenmon.Location = new Point(418, 29);
            tenmon.Name = "tenmon";
            tenmon.Size = new Size(12, 15);
            tenmon.TabIndex = 12;
            tenmon.Text = "-";
            // 
            // tennguoi
            // 
            tennguoi.AutoSize = true;
            tennguoi.Location = new Point(418, 58);
            tennguoi.Name = "tennguoi";
            tennguoi.Size = new Size(12, 15);
            tennguoi.TabIndex = 13;
            tennguoi.Text = "-";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 29);
            label3.Name = "label3";
            label3.Size = new Size(25, 15);
            label3.TabIndex = 14;
            label3.Text = "Tên";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 58);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 15;
            label4.Text = "Quyền hạn";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 239);
            label5.Name = "label5";
            label5.Size = new Size(69, 15);
            label5.TabIndex = 16;
            label5.Text = "Tên món ăn";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 210);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 17;
            label6.Text = "ID món ăn";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(28, 268);
            label7.Name = "label7";
            label7.Size = new Size(29, 15);
            label7.TabIndex = 18;
            label7.Text = "Ảnh";
            // 
            // themanh
            // 
            themanh.Location = new Point(273, 260);
            themanh.Name = "themanh";
            themanh.Size = new Size(22, 23);
            themanh.TabIndex = 19;
            themanh.Text = "...";
            themanh.UseVisualStyleBackColor = true;
            themanh.Click += themanh_Click;
            // 
            // boxngcc
            // 
            boxngcc.FormattingEnabled = true;
            boxngcc.Location = new Point(130, 289);
            boxngcc.Name = "boxngcc";
            boxngcc.Size = new Size(137, 23);
            boxngcc.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(28, 297);
            label8.Name = "label8";
            label8.Size = new Size(92, 15);
            label8.TabIndex = 21;
            label8.Text = "Người cung cấp";
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(430, 76);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 81);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 22;
            pictureBox2.TabStop = false;
            // 
            // bai6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox2);
            Controls.Add(label8);
            Controls.Add(boxngcc);
            Controls.Add(themanh);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(tennguoi);
            Controls.Add(tenmon);
            Controls.Add(ngaunhien);
            Controls.Add(hienthimon);
            Controls.Add(xoamon);
            Controls.Add(pictureBox1);
            Controls.Add(boxanh);
            Controls.Add(boxtenmon);
            Controls.Add(boxidmon);
            Controls.Add(boxquyen);
            Controls.Add(boxtennguoi);
            Controls.Add(dataGridView1);
            Controls.Add(themmon);
            Controls.Add(themng);
            Name = "bai6";
            Text = "bai6";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button themng;
        private Button themmon;
        private DataGridView dataGridView1;
        private TextBox boxtennguoi;
        private TextBox boxquyen;
        private TextBox boxidmon;
        private TextBox boxtenmon;
        private TextBox boxanh;
        private PictureBox pictureBox1;
        private Button xoamon;
        private Button hienthimon;
        private Button ngaunhien;
        private Label tenmon;
        private Label tennguoi;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button themanh;
        private ComboBox boxngcc;
        private Label label8;
        private PictureBox pictureBox2;
    }
}