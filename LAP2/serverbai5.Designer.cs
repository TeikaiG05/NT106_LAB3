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
            this.themng = new System.Windows.Forms.Button();
            this.themmon = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.boxtennguoi = new System.Windows.Forms.TextBox();
            this.boxquyen = new System.Windows.Forms.TextBox();
            this.boxidmon = new System.Windows.Forms.TextBox();
            this.boxtenmon = new System.Windows.Forms.TextBox();
            this.boxanh = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xoamon = new System.Windows.Forms.Button();
            this.hienthimon = new System.Windows.Forms.Button();
            this.ngaunhien = new System.Windows.Forms.Button();
            this.tenmon = new System.Windows.Forms.Label();
            this.tennguoi = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.themanh = new System.Windows.Forms.Button();
            this.boxngcc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.resetmonan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // themng
            // 
            this.themng.Location = new System.Drawing.Point(111, 73);
            this.themng.Name = "themng";
            this.themng.Size = new System.Drawing.Size(64, 20);
            this.themng.TabIndex = 0;
            this.themng.Text = "Thêm";
            this.themng.UseVisualStyleBackColor = true;
            // 
            // themmon
            // 
            this.themmon.Location = new System.Drawing.Point(234, 250);
            this.themmon.Name = "themmon";
            this.themmon.Size = new System.Drawing.Size(64, 20);
            this.themmon.TabIndex = 1;
            this.themmon.Text = "Thêm";
            this.themmon.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(470, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(206, 284);
            this.dataGridView1.TabIndex = 2;
            // 
            // boxtennguoi
            // 
            this.boxtennguoi.Location = new System.Drawing.Point(81, 23);
            this.boxtennguoi.Name = "boxtennguoi";
            this.boxtennguoi.Size = new System.Drawing.Size(127, 20);
            this.boxtennguoi.TabIndex = 3;
            // 
            // boxquyen
            // 
            this.boxquyen.Location = new System.Drawing.Point(81, 48);
            this.boxquyen.Name = "boxquyen";
            this.boxquyen.Size = new System.Drawing.Size(127, 20);
            this.boxquyen.TabIndex = 4;
            // 
            // boxidmon
            // 
            this.boxidmon.Location = new System.Drawing.Point(111, 175);
            this.boxidmon.Name = "boxidmon";
            this.boxidmon.Size = new System.Drawing.Size(118, 20);
            this.boxidmon.TabIndex = 5;
            // 
            // boxtenmon
            // 
            this.boxtenmon.Location = new System.Drawing.Point(111, 200);
            this.boxtenmon.Name = "boxtenmon";
            this.boxtenmon.Size = new System.Drawing.Size(118, 20);
            this.boxtenmon.TabIndex = 6;
            // 
            // boxanh
            // 
            this.boxanh.Location = new System.Drawing.Point(111, 225);
            this.boxanh.Name = "boxanh";
            this.boxanh.Size = new System.Drawing.Size(118, 20);
            this.boxanh.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(273, 175);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // xoamon
            // 
            this.xoamon.Location = new System.Drawing.Point(470, 300);
            this.xoamon.Name = "xoamon";
            this.xoamon.Size = new System.Drawing.Size(64, 20);
            this.xoamon.TabIndex = 9;
            this.xoamon.Text = "Xóa";
            this.xoamon.UseVisualStyleBackColor = true;
            // 
            // hienthimon
            // 
            this.hienthimon.Location = new System.Drawing.Point(610, 300);
            this.hienthimon.Name = "hienthimon";
            this.hienthimon.Size = new System.Drawing.Size(64, 20);
            this.hienthimon.TabIndex = 10;
            this.hienthimon.Text = "Hiển thị";
            this.hienthimon.UseVisualStyleBackColor = true;
            // 
            // ngaunhien
            // 
            this.ngaunhien.Location = new System.Drawing.Point(369, 141);
            this.ngaunhien.Name = "ngaunhien";
            this.ngaunhien.Size = new System.Drawing.Size(86, 20);
            this.ngaunhien.TabIndex = 11;
            this.ngaunhien.Text = "Ngẫu nhiên";
            this.ngaunhien.UseVisualStyleBackColor = true;
            this.ngaunhien.Click += new System.EventHandler(this.ngaunhien_Click);
            // 
            // tenmon
            // 
            this.tenmon.AutoSize = true;
            this.tenmon.Location = new System.Drawing.Point(358, 25);
            this.tenmon.Name = "tenmon";
            this.tenmon.Size = new System.Drawing.Size(10, 13);
            this.tenmon.TabIndex = 12;
            this.tenmon.Text = "-";
            // 
            // tennguoi
            // 
            this.tennguoi.AutoSize = true;
            this.tennguoi.Location = new System.Drawing.Point(358, 50);
            this.tennguoi.Name = "tennguoi";
            this.tennguoi.Size = new System.Drawing.Size(10, 13);
            this.tennguoi.TabIndex = 13;
            this.tennguoi.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Tên";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Quyền hạn";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Tên món ăn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "ID món ăn";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Ảnh";
            // 
            // themanh
            // 
            this.themanh.Location = new System.Drawing.Point(234, 225);
            this.themanh.Name = "themanh";
            this.themanh.Size = new System.Drawing.Size(19, 20);
            this.themanh.TabIndex = 19;
            this.themanh.Text = "...";
            this.themanh.UseVisualStyleBackColor = true;
            // 
            // boxngcc
            // 
            this.boxngcc.FormattingEnabled = true;
            this.boxngcc.Location = new System.Drawing.Point(111, 250);
            this.boxngcc.Name = "boxngcc";
            this.boxngcc.Size = new System.Drawing.Size(118, 21);
            this.boxngcc.TabIndex = 20;
            this.boxngcc.SelectedIndexChanged += new System.EventHandler(this.boxngcc_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Người cung cấp";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(369, 66);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(86, 70);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // resetmonan
            // 
            this.resetmonan.Location = new System.Drawing.Point(540, 300);
            this.resetmonan.Name = "resetmonan";
            this.resetmonan.Size = new System.Drawing.Size(64, 20);
            this.resetmonan.TabIndex = 23;
            this.resetmonan.Text = "Reset";
            this.resetmonan.UseVisualStyleBackColor = true;
            this.resetmonan.Click += new System.EventHandler(this.resetmonan_Click);
            // 
            // serverbai5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.resetmonan);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.boxngcc);
            this.Controls.Add(this.themanh);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tennguoi);
            this.Controls.Add(this.tenmon);
            this.Controls.Add(this.ngaunhien);
            this.Controls.Add(this.hienthimon);
            this.Controls.Add(this.xoamon);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.boxanh);
            this.Controls.Add(this.boxtenmon);
            this.Controls.Add(this.boxidmon);
            this.Controls.Add(this.boxquyen);
            this.Controls.Add(this.boxtennguoi);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.themmon);
            this.Controls.Add(this.themng);
            this.Name = "serverbai5";
            this.Text = "bai6";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Button resetmonan;
    }
}