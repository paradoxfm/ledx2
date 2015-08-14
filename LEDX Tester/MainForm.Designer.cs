namespace LEDX_Tester {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ColorButton1 = new System.Windows.Forms.Button();
			this.Color1 = new System.Windows.Forms.Panel();
			this.PingButton = new System.Windows.Forms.Button();
			this.сntrNum = new System.Windows.Forms.NumericUpDown();
			this.Color2 = new System.Windows.Forms.Panel();
			this.ColorButton2 = new System.Windows.Forms.Button();
			this.Color3 = new System.Windows.Forms.Panel();
			this.ColorButton3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.LogBox = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.Port = new System.IO.Ports.SerialPort(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PortSpeedBox = new System.Windows.Forms.ComboBox();
			this.PortNameBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Pr5 = new System.Windows.Forms.Button();
			this.Pr4 = new System.Windows.Forms.Button();
			this.Pr3 = new System.Windows.Forms.Button();
			this.Pr2 = new System.Windows.Forms.Button();
			this.Pr1 = new System.Windows.Forms.Button();
			this.PlPs = new System.Windows.Forms.Button();
			this.Sp1 = new System.Windows.Forms.Button();
			this.Sp2 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.BrUp = new System.Windows.Forms.Button();
			this.BrDn = new System.Windows.Forms.Button();
			this.Power = new System.Windows.Forms.Button();
			this.ClearLog = new System.Windows.Forms.Button();
			this.Sp0 = new System.Windows.Forms.Button();
			this.Sp3 = new System.Windows.Forms.Button();
			this.Sp4 = new System.Windows.Forms.Button();
			this.Br2 = new System.Windows.Forms.Button();
			this.Br3 = new System.Windows.Forms.Button();
			this.Br4 = new System.Windows.Forms.Button();
			this.Br5 = new System.Windows.Forms.Button();
			this.Programming = new System.Windows.Forms.Button();
			this.Frame1 = new System.Windows.Forms.Button();
			this.Frame2 = new System.Windows.Forms.Button();
			this.Frame3 = new System.Windows.Forms.Button();
			this.Finalize = new System.Windows.Forms.Button();
			this.ColorChange = new System.Windows.Forms.ColorDialog();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panelStatus = new System.Windows.Forms.StatusStrip();
			this.comPortStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.button1 = new System.Windows.Forms.Button();
			this.refreshCom = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.сntrNum)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panelStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// ColorButton1
			// 
			this.ColorButton1.Location = new System.Drawing.Point(18, 396);
			this.ColorButton1.Name = "ColorButton1";
			this.ColorButton1.Size = new System.Drawing.Size(64, 64);
			this.ColorButton1.TabIndex = 0;
			this.ColorButton1.Text = "Послать";
			this.ColorButton1.UseVisualStyleBackColor = true;
			this.ColorButton1.Click += new System.EventHandler(this.ColorButton1_Click);
			// 
			// Color1
			// 
			this.Color1.BackColor = System.Drawing.Color.Red;
			this.Color1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Color1.Location = new System.Drawing.Point(18, 326);
			this.Color1.Name = "Color1";
			this.Color1.Size = new System.Drawing.Size(64, 64);
			this.Color1.TabIndex = 1;
			this.Color1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Color1_MouseUp);
			// 
			// PingButton
			// 
			this.PingButton.Location = new System.Drawing.Point(209, 137);
			this.PingButton.Name = "PingButton";
			this.PingButton.Size = new System.Drawing.Size(47, 23);
			this.PingButton.TabIndex = 2;
			this.PingButton.Text = "Ping";
			this.PingButton.UseVisualStyleBackColor = true;
			this.PingButton.Click += new System.EventHandler(this.PingButton_Click);
			// 
			// сntrNum
			// 
			this.сntrNum.Location = new System.Drawing.Point(133, 137);
			this.сntrNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.сntrNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.сntrNum.Name = "сntrNum";
			this.сntrNum.Size = new System.Drawing.Size(63, 20);
			this.сntrNum.TabIndex = 3;
			this.сntrNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.сntrNum.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// Color2
			// 
			this.Color2.BackColor = System.Drawing.Color.Lime;
			this.Color2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Color2.Location = new System.Drawing.Point(88, 326);
			this.Color2.Name = "Color2";
			this.Color2.Size = new System.Drawing.Size(64, 64);
			this.Color2.TabIndex = 1;
			this.Color2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Color2_MouseUp);
			// 
			// ColorButton2
			// 
			this.ColorButton2.Location = new System.Drawing.Point(88, 396);
			this.ColorButton2.Name = "ColorButton2";
			this.ColorButton2.Size = new System.Drawing.Size(64, 64);
			this.ColorButton2.TabIndex = 0;
			this.ColorButton2.Text = "Послать";
			this.ColorButton2.UseVisualStyleBackColor = true;
			this.ColorButton2.Click += new System.EventHandler(this.ColorButton2_Click);
			// 
			// Color3
			// 
			this.Color3.BackColor = System.Drawing.Color.Blue;
			this.Color3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Color3.Location = new System.Drawing.Point(158, 326);
			this.Color3.Name = "Color3";
			this.Color3.Size = new System.Drawing.Size(64, 64);
			this.Color3.TabIndex = 1;
			this.Color3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Color3_MouseUp);
			// 
			// ColorButton3
			// 
			this.ColorButton3.Location = new System.Drawing.Point(158, 396);
			this.ColorButton3.Name = "ColorButton3";
			this.ColorButton3.Size = new System.Drawing.Size(64, 64);
			this.ColorButton3.TabIndex = 0;
			this.ColorButton3.Text = "Послать";
			this.ColorButton3.UseVisualStyleBackColor = true;
			this.ColorButton3.Click += new System.EventHandler(this.ColorButton3_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 139);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Номер контроллера:";
			// 
			// LogBox
			// 
			this.LogBox.Location = new System.Drawing.Point(311, 25);
			this.LogBox.Name = "LogBox";
			this.LogBox.Size = new System.Drawing.Size(357, 440);
			this.LogBox.TabIndex = 5;
			this.LogBox.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(306, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(162, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Лог общения с контроллером:";
			// 
			// Port
			// 
			this.Port.PortName = "COM3";
			this.Port.ReadTimeout = 500;
			this.Port.ReceivedBytesThreshold = 8;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.PortSpeedBox);
			this.groupBox1.Controls.Add(this.PortNameBox);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(238, 81);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Настройки COM порта";
			// 
			// PortSpeedBox
			// 
			this.PortSpeedBox.FormattingEnabled = true;
			this.PortSpeedBox.Location = new System.Drawing.Point(108, 50);
			this.PortSpeedBox.Name = "PortSpeedBox";
			this.PortSpeedBox.Size = new System.Drawing.Size(121, 21);
			this.PortSpeedBox.TabIndex = 1;
			this.PortSpeedBox.Text = "9600";
			this.PortSpeedBox.SelectedValueChanged += new System.EventHandler(this.PortSpeedBox_SelectedValueChanged);
			// 
			// PortNameBox
			// 
			this.PortNameBox.FormattingEnabled = true;
			this.PortNameBox.Location = new System.Drawing.Point(108, 23);
			this.PortNameBox.Name = "PortNameBox";
			this.PortNameBox.Size = new System.Drawing.Size(121, 21);
			this.PortNameBox.TabIndex = 1;
			this.PortNameBox.Text = "COM1";
			this.PortNameBox.SelectedValueChanged += new System.EventHandler(this.PortNameBox_SelectedValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Скорость обмена:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(38, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Имя порта:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Pr5);
			this.groupBox2.Controls.Add(this.Pr4);
			this.groupBox2.Controls.Add(this.Pr3);
			this.groupBox2.Controls.Add(this.Pr2);
			this.groupBox2.Controls.Add(this.Pr1);
			this.groupBox2.Location = new System.Drawing.Point(18, 179);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(152, 53);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Запуск програмы";
			// 
			// Pr5
			// 
			this.Pr5.Location = new System.Drawing.Point(121, 19);
			this.Pr5.Name = "Pr5";
			this.Pr5.Size = new System.Drawing.Size(23, 23);
			this.Pr5.TabIndex = 0;
			this.Pr5.Text = "5";
			this.Pr5.UseVisualStyleBackColor = true;
			this.Pr5.Click += new System.EventHandler(this.Pr5_Click);
			// 
			// Pr4
			// 
			this.Pr4.Location = new System.Drawing.Point(92, 19);
			this.Pr4.Name = "Pr4";
			this.Pr4.Size = new System.Drawing.Size(23, 23);
			this.Pr4.TabIndex = 0;
			this.Pr4.Text = "4";
			this.Pr4.UseVisualStyleBackColor = true;
			this.Pr4.Click += new System.EventHandler(this.Pr4_Click);
			// 
			// Pr3
			// 
			this.Pr3.Location = new System.Drawing.Point(63, 20);
			this.Pr3.Name = "Pr3";
			this.Pr3.Size = new System.Drawing.Size(23, 23);
			this.Pr3.TabIndex = 0;
			this.Pr3.Text = "3";
			this.Pr3.UseVisualStyleBackColor = true;
			this.Pr3.Click += new System.EventHandler(this.Pr3_Click);
			// 
			// Pr2
			// 
			this.Pr2.Location = new System.Drawing.Point(36, 20);
			this.Pr2.Name = "Pr2";
			this.Pr2.Size = new System.Drawing.Size(23, 23);
			this.Pr2.TabIndex = 0;
			this.Pr2.Text = "2";
			this.Pr2.UseVisualStyleBackColor = true;
			this.Pr2.Click += new System.EventHandler(this.Pr2_Click);
			// 
			// Pr1
			// 
			this.Pr1.Location = new System.Drawing.Point(7, 20);
			this.Pr1.Name = "Pr1";
			this.Pr1.Size = new System.Drawing.Size(23, 23);
			this.Pr1.TabIndex = 0;
			this.Pr1.Text = "1";
			this.Pr1.UseVisualStyleBackColor = true;
			this.Pr1.Click += new System.EventHandler(this.Pr1_Click);
			// 
			// PlPs
			// 
			this.PlPs.Image = ((System.Drawing.Image)(resources.GetObject("PlPs.Image")));
			this.PlPs.Location = new System.Drawing.Point(175, 188);
			this.PlPs.Name = "PlPs";
			this.PlPs.Size = new System.Drawing.Size(46, 43);
			this.PlPs.TabIndex = 8;
			this.PlPs.UseVisualStyleBackColor = true;
			this.PlPs.Click += new System.EventHandler(this.PlPs_Click);
			// 
			// Sp1
			// 
			this.Sp1.Location = new System.Drawing.Point(108, 238);
			this.Sp1.Name = "Sp1";
			this.Sp1.Size = new System.Drawing.Size(25, 24);
			this.Sp1.TabIndex = 0;
			this.Sp1.Text = "1";
			this.Sp1.UseVisualStyleBackColor = true;
			this.Sp1.Click += new System.EventHandler(this.Sp1_Click);
			// 
			// Sp2
			// 
			this.Sp2.Location = new System.Drawing.Point(137, 238);
			this.Sp2.Name = "Sp2";
			this.Sp2.Size = new System.Drawing.Size(27, 24);
			this.Sp2.TabIndex = 0;
			this.Sp2.Text = "2";
			this.Sp2.UseVisualStyleBackColor = true;
			this.Sp2.Click += new System.EventHandler(this.Sp2_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(18, 244);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Скорость:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(18, 286);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Яркость:";
			// 
			// BrUp
			// 
			this.BrUp.Location = new System.Drawing.Point(108, 281);
			this.BrUp.Name = "BrUp";
			this.BrUp.Size = new System.Drawing.Size(25, 23);
			this.BrUp.TabIndex = 0;
			this.BrUp.Text = "1";
			this.BrUp.UseVisualStyleBackColor = true;
			this.BrUp.Click += new System.EventHandler(this.Br1_Click);
			// 
			// BrDn
			// 
			this.BrDn.Location = new System.Drawing.Point(79, 280);
			this.BrDn.Name = "BrDn";
			this.BrDn.Size = new System.Drawing.Size(25, 24);
			this.BrDn.TabIndex = 0;
			this.BrDn.Text = "0";
			this.BrDn.UseVisualStyleBackColor = true;
			this.BrDn.Click += new System.EventHandler(this.Br0_Click);
			// 
			// Power
			// 
			this.Power.Image = ((System.Drawing.Image)(resources.GetObject("Power.Image")));
			this.Power.Location = new System.Drawing.Point(227, 188);
			this.Power.Name = "Power";
			this.Power.Size = new System.Drawing.Size(46, 43);
			this.Power.TabIndex = 8;
			this.Power.UseVisualStyleBackColor = true;
			this.Power.Click += new System.EventHandler(this.Power_Click);
			// 
			// ClearLog
			// 
			this.ClearLog.Location = new System.Drawing.Point(474, 1);
			this.ClearLog.Name = "ClearLog";
			this.ClearLog.Size = new System.Drawing.Size(80, 24);
			this.ClearLog.TabIndex = 0;
			this.ClearLog.Text = "Очистить";
			this.ClearLog.UseVisualStyleBackColor = true;
			this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
			// 
			// Sp0
			// 
			this.Sp0.Location = new System.Drawing.Point(79, 238);
			this.Sp0.Name = "Sp0";
			this.Sp0.Size = new System.Drawing.Size(25, 24);
			this.Sp0.TabIndex = 0;
			this.Sp0.Text = "0";
			this.Sp0.UseVisualStyleBackColor = true;
			this.Sp0.Click += new System.EventHandler(this.Sp0_Click);
			// 
			// Sp3
			// 
			this.Sp3.Location = new System.Drawing.Point(168, 238);
			this.Sp3.Name = "Sp3";
			this.Sp3.Size = new System.Drawing.Size(25, 24);
			this.Sp3.TabIndex = 0;
			this.Sp3.Text = "3";
			this.Sp3.UseVisualStyleBackColor = true;
			this.Sp3.Click += new System.EventHandler(this.Sp3_Click);
			// 
			// Sp4
			// 
			this.Sp4.Location = new System.Drawing.Point(197, 238);
			this.Sp4.Name = "Sp4";
			this.Sp4.Size = new System.Drawing.Size(25, 24);
			this.Sp4.TabIndex = 0;
			this.Sp4.Text = "4";
			this.Sp4.UseVisualStyleBackColor = true;
			this.Sp4.Click += new System.EventHandler(this.Sp4_Click);
			// 
			// Br2
			// 
			this.Br2.Location = new System.Drawing.Point(139, 280);
			this.Br2.Name = "Br2";
			this.Br2.Size = new System.Drawing.Size(25, 24);
			this.Br2.TabIndex = 0;
			this.Br2.Text = "2";
			this.Br2.UseVisualStyleBackColor = true;
			this.Br2.Click += new System.EventHandler(this.Br2_Click);
			// 
			// Br3
			// 
			this.Br3.Location = new System.Drawing.Point(168, 280);
			this.Br3.Name = "Br3";
			this.Br3.Size = new System.Drawing.Size(25, 24);
			this.Br3.TabIndex = 0;
			this.Br3.Text = "3";
			this.Br3.UseVisualStyleBackColor = true;
			this.Br3.Click += new System.EventHandler(this.Br3_Click);
			// 
			// Br4
			// 
			this.Br4.Location = new System.Drawing.Point(196, 280);
			this.Br4.Name = "Br4";
			this.Br4.Size = new System.Drawing.Size(25, 24);
			this.Br4.TabIndex = 0;
			this.Br4.Text = "4";
			this.Br4.UseVisualStyleBackColor = true;
			this.Br4.Click += new System.EventHandler(this.Br4_Click);
			// 
			// Br5
			// 
			this.Br5.Location = new System.Drawing.Point(224, 280);
			this.Br5.Name = "Br5";
			this.Br5.Size = new System.Drawing.Size(25, 24);
			this.Br5.TabIndex = 0;
			this.Br5.Text = "5";
			this.Br5.UseVisualStyleBackColor = true;
			this.Br5.Click += new System.EventHandler(this.Br5_Click);
			// 
			// Programming
			// 
			this.Programming.Location = new System.Drawing.Point(228, 326);
			this.Programming.Name = "Programming";
			this.Programming.Size = new System.Drawing.Size(76, 23);
			this.Programming.TabIndex = 9;
			this.Programming.Text = "Прошивка";
			this.Programming.UseVisualStyleBackColor = true;
			this.Programming.Click += new System.EventHandler(this.Programming_Click);
			// 
			// Frame1
			// 
			this.Frame1.Location = new System.Drawing.Point(227, 355);
			this.Frame1.Name = "Frame1";
			this.Frame1.Size = new System.Drawing.Size(76, 23);
			this.Frame1.TabIndex = 9;
			this.Frame1.Text = "Фрейм 1";
			this.Frame1.UseVisualStyleBackColor = true;
			this.Frame1.Click += new System.EventHandler(this.Frame1_Click);
			// 
			// Frame2
			// 
			this.Frame2.Location = new System.Drawing.Point(228, 384);
			this.Frame2.Name = "Frame2";
			this.Frame2.Size = new System.Drawing.Size(76, 23);
			this.Frame2.TabIndex = 9;
			this.Frame2.Text = "Фрейм 2";
			this.Frame2.UseVisualStyleBackColor = true;
			this.Frame2.Click += new System.EventHandler(this.Frame2_Click);
			// 
			// Frame3
			// 
			this.Frame3.Location = new System.Drawing.Point(228, 413);
			this.Frame3.Name = "Frame3";
			this.Frame3.Size = new System.Drawing.Size(76, 23);
			this.Frame3.TabIndex = 9;
			this.Frame3.Text = "Фрейм 3";
			this.Frame3.UseVisualStyleBackColor = true;
			this.Frame3.Click += new System.EventHandler(this.Frame3_Click);
			// 
			// Finalize
			// 
			this.Finalize.Location = new System.Drawing.Point(228, 442);
			this.Finalize.Name = "Finalize";
			this.Finalize.Size = new System.Drawing.Size(76, 23);
			this.Finalize.TabIndex = 9;
			this.Finalize.Text = "Завершить прошивку";
			this.Finalize.UseVisualStyleBackColor = true;
			this.Finalize.Click += new System.EventHandler(this.Finalize_Click);
			// 
			// ColorChange
			// 
			this.ColorChange.Color = System.Drawing.Color.Red;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Иконка.png");
			this.imageList1.Images.SetKeyName(1, "Elling_zvezdnoe_nebo-13.11.2009.jpg");
			this.imageList1.Images.SetKeyName(2, "Лампа T8.jpg");
			this.imageList1.Images.SetKeyName(3, "Chrysanthemum.jpg");
			this.imageList1.Images.SetKeyName(4, "Desert.jpg");
			this.imageList1.Images.SetKeyName(5, "Hydrangeas.jpg");
			this.imageList1.Images.SetKeyName(6, "Jellyfish.jpg");
			this.imageList1.Images.SetKeyName(7, "Koala.jpg");
			this.imageList1.Images.SetKeyName(8, "Lighthouse.jpg");
			this.imageList1.Images.SetKeyName(9, "Penguins.jpg");
			this.imageList1.Images.SetKeyName(10, "Tulips.jpg");
			// 
			// panelStatus
			// 
			this.panelStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comPortStatus});
			this.panelStatus.Location = new System.Drawing.Point(0, 470);
			this.panelStatus.Name = "panelStatus";
			this.panelStatus.Size = new System.Drawing.Size(674, 22);
			this.panelStatus.TabIndex = 11;
			this.panelStatus.Text = "statusStrip1";
			// 
			// comPortStatus
			// 
			this.comPortStatus.Name = "comPortStatus";
			this.comPortStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(18, 100);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(109, 23);
			this.button1.TabIndex = 12;
			this.button1.Text = "Закрыть порт";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// refreshCom
			// 
			this.refreshCom.Image = ((System.Drawing.Image)(resources.GetObject("refreshCom.Image")));
			this.refreshCom.Location = new System.Drawing.Point(256, 24);
			this.refreshCom.Name = "refreshCom";
			this.refreshCom.Size = new System.Drawing.Size(40, 40);
			this.refreshCom.TabIndex = 13;
			this.refreshCom.UseVisualStyleBackColor = true;
			this.refreshCom.Click += new System.EventHandler(this.refreshCom_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(674, 492);
			this.Controls.Add(this.refreshCom);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panelStatus);
			this.Controls.Add(this.Finalize);
			this.Controls.Add(this.Frame3);
			this.Controls.Add(this.Frame2);
			this.Controls.Add(this.Frame1);
			this.Controls.Add(this.Programming);
			this.Controls.Add(this.Power);
			this.Controls.Add(this.PlPs);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.LogBox);
			this.Controls.Add(this.ClearLog);
			this.Controls.Add(this.Br5);
			this.Controls.Add(this.Br4);
			this.Controls.Add(this.Br3);
			this.Controls.Add(this.Br2);
			this.Controls.Add(this.BrDn);
			this.Controls.Add(this.BrUp);
			this.Controls.Add(this.Sp2);
			this.Controls.Add(this.Sp4);
			this.Controls.Add(this.Sp3);
			this.Controls.Add(this.Sp0);
			this.Controls.Add(this.Sp1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.сntrNum);
			this.Controls.Add(this.PingButton);
			this.Controls.Add(this.Color3);
			this.Controls.Add(this.Color2);
			this.Controls.Add(this.Color1);
			this.Controls.Add(this.ColorButton3);
			this.Controls.Add(this.ColorButton2);
			this.Controls.Add(this.ColorButton1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(690, 530);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(690, 530);
			this.Name = "MainForm";
			this.Text = "Панель управления LEDX";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.сntrNum)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.panelStatus.ResumeLayout(false);
			this.panelStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ColorButton1;
		private System.Windows.Forms.Panel Color1;
		private System.Windows.Forms.Button PingButton;
		private System.Windows.Forms.NumericUpDown сntrNum;
		private System.Windows.Forms.Panel Color2;
		private System.Windows.Forms.Button ColorButton2;
		private System.Windows.Forms.Panel Color3;
		private System.Windows.Forms.Button ColorButton3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox LogBox;
		private System.Windows.Forms.Label label2;
		private System.IO.Ports.SerialPort Port;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox PortSpeedBox;
		private System.Windows.Forms.ComboBox PortNameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button Pr5;
		private System.Windows.Forms.Button Pr4;
		private System.Windows.Forms.Button Pr3;
		private System.Windows.Forms.Button Pr2;
		private System.Windows.Forms.Button Pr1;
		private System.Windows.Forms.Button PlPs;
		private System.Windows.Forms.Button Sp1;
		private System.Windows.Forms.Button Sp2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button BrUp;
		private System.Windows.Forms.Button BrDn;
		private System.Windows.Forms.Button Power;
		private System.Windows.Forms.Button ClearLog;
		private System.Windows.Forms.Button Sp0;
		private System.Windows.Forms.Button Sp3;
		private System.Windows.Forms.Button Sp4;
		private System.Windows.Forms.Button Br2;
		private System.Windows.Forms.Button Br3;
		private System.Windows.Forms.Button Br4;
		private System.Windows.Forms.Button Br5;
		private System.Windows.Forms.Button Programming;
		private System.Windows.Forms.Button Frame1;
		private System.Windows.Forms.Button Frame2;
		private System.Windows.Forms.Button Frame3;
		private System.Windows.Forms.Button Finalize;
		private System.Windows.Forms.ColorDialog ColorChange;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.StatusStrip panelStatus;
		private System.Windows.Forms.ToolStripStatusLabel comPortStatus;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button refreshCom;
	}
}

