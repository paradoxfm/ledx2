using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LEDX_Tester {
	public partial class MainForm : Form {
		const byte ECHO = 0x00; // Проверка связи
		const byte SET_COLOR = 0x01; // Установить цвет
		const byte RUN_PROG = 0x02; // Запустить программу
		const byte PAUSE_PROG = 0x03; // Приостановить выполнение программы
		const byte RESUME_PROG = 0x04; // Возобновить выполнение программы
		const byte CHANGE_SP = 0x05; // Изменить скорость
		const byte CHANGE_BR = 0x06; // Изменить яркость
		const byte START_SYNC = 0x07; // Начать синхронную работу
		const byte STOP_SYNC = 0x08; // Остановить синхронную работу
		const byte ENTER_PROG_MODE = 0x09; // Войти в режим программирования
		const byte WRITE_FRAME = 0x0A; // Записать кадр
		const byte FINILIZE_PROG = 0x0B; // Завершить программирование
		const byte SYNC = 0x0C; // Синхро
		const byte LIVEDMX = 0x0D; // В режим DMX
		const byte ONOFFCOMM = 0x0E; // Включить/выключить визуальную часть контроллера
		const byte CHANGE_CTR_NUM = 0x0F;
		const byte PAUSE_TOGGLE = 0x10; // Поставить/снять с паузы

		byte[] buffer = new byte[15];
		byte[] RX_buf = new byte[15];
		string[] ERROR = {"TEST\n",
                             "SUCCESS: Ошибки нет\n",
                                "ERROR: Ошибка контрольной суммы\n",
                                "ERROR: Неизвестная команда\n",
                                "ERROR: Неожиданная команда\n",
                                "ERROR: Память заполнена\n"};

		//{0, 1, 2, 3, 4, 5, 6, 14};

		private void ClosePort() {
			if (Port.IsOpen == true) {
				Port.Close();
				LogBox.AppendText("Закрыт порт " + Port.PortName + '\n');
				comPortStatus.Text = "Закрыт";
			}
		}

		private void OpenPort() {
			if (Port.IsOpen != true) {
				Port.Open();
				LogBox.AppendText("Открыт порт " + Port.PortName + '\n');
				comPortStatus.Text = "Открыт";
				Port.DiscardInBuffer();
			}
		}

		private void ReceiveConfirm() {
			Port.DiscardInBuffer();
			try {
				for (int i = 0; i < 8; i++)
					Port.Read(RX_buf, i, 1);
				if (RX_buf[5] <= 5)
					LogBox.AppendText(ERROR[RX_buf[5]]);
				else
					LogBox.AppendText("CONTROLLER ERROR: Код ошибки " + RX_buf[5].ToString() + '\n');
			} catch {
				LogBox.AppendText("CONNECT ERROR: Таймаут ответа\n");
			}
			LogBox.ScrollToCaret();
		}

		private void Form1_Load(object sender, EventArgs e) {
			for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++) {
				PortNameBox.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
			}
			PortSpeedBox.Items.Add(9600);
			PortSpeedBox.Items.Add(38400);
			PortSpeedBox.Items.Add(115200);
			Port.PortName = PortNameBox.Text;

			comPortStatus.Text = (Port.IsOpen) ? "Открыт" : "Закрыт";
			buffer[0] = 0xAA;
			buffer[1] = Convert.ToByte('R');
			buffer[2] = Convert.ToByte(сntrNum.Value);
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (Port.IsOpen) {
				ClosePort();
			}
		}

		public MainForm() {
			InitializeComponent();
		}

		private void ColorButton1_Click(object sender, EventArgs e) {
			buffer[3] = SET_COLOR;
			buffer[4] = 3;
			buffer[5] = Color1.BackColor.R;
			buffer[6] = Color1.BackColor.G;
			buffer[7] = Color1.BackColor.B;
			buffer[8] = Convert.ToByte((buffer[1] + buffer[2] + buffer[3] + buffer[4] + buffer[5] + buffer[6] + buffer[7]) & 0xFF);
			buffer[9] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 10);
			LogBox.AppendText("Команда сменить цвет на {" + Color1.BackColor.R.ToString()
					+ ',' + Color1.BackColor.G.ToString() + ',' + Color1.BackColor.B.ToString() + "}\n");

			ReceiveConfirm();
		}

		private void ColorButton2_Click(object sender, EventArgs e) {
			buffer[3] = SET_COLOR;
			buffer[4] = 3;
			buffer[5] = Color2.BackColor.R;
			buffer[6] = Color2.BackColor.G;
			buffer[7] = Color2.BackColor.B;
			buffer[8] = Convert.ToByte((buffer[1] + buffer[2] + buffer[3] + buffer[4] + buffer[5] + buffer[6] + buffer[7]) & 0xFF);
			buffer[9] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 10);
			LogBox.AppendText("Команда сменить цвет на {" + Color2.BackColor.R.ToString()
					+ ',' + Color2.BackColor.G.ToString() + ',' + Color2.BackColor.B.ToString() + "}\n");

			ReceiveConfirm();
		}

		private void ColorButton3_Click(object sender, EventArgs e) {
			buffer[3] = SET_COLOR;
			buffer[4] = 3;
			buffer[5] = Color3.BackColor.R;
			buffer[6] = Color3.BackColor.G;
			buffer[7] = Color3.BackColor.B;
			buffer[8] = Convert.ToByte((buffer[1] + buffer[2] + buffer[3] + buffer[4] + buffer[5] + buffer[6] + buffer[7]) & 0xFF);
			buffer[9] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 10);
			LogBox.AppendText("Команда сменить цвет на {" + Color3.BackColor.R.ToString()
					+ ',' + Color3.BackColor.G.ToString() + ',' + Color3.BackColor.B.ToString() + "}\n");

			ReceiveConfirm();
		}

		private void Color1_MouseUp(object sender, MouseEventArgs e) {
			ColorChange.Color = Color1.BackColor;
			ColorChange.ShowDialog();
			Color1.BackColor = ColorChange.Color;
		}

		private void Color2_MouseUp(object sender, MouseEventArgs e) {
			ColorChange.Color = Color2.BackColor;
			ColorChange.ShowDialog();
			Color2.BackColor = ColorChange.Color;
		}

		private void Color3_MouseUp(object sender, MouseEventArgs e) {
			ColorChange.Color = Color3.BackColor;
			ColorChange.ShowDialog();
			Color3.BackColor = ColorChange.Color;
		}

		private void ClearLog_Click(object sender, EventArgs e) {
			LogBox.Clear();
		}

		private void PortSpeedBox_SelectedValueChanged(object sender, EventArgs e) {
			if (Port.BaudRate != (int)PortSpeedBox.SelectedItem) {
				ClosePort();
				Port.BaudRate = (int)PortSpeedBox.SelectedItem;
				LogBox.AppendText("Скорость порта изменена на " + Port.BaudRate.ToString() + " бод\n");
			}
			LogBox.ScrollToCaret();
		}

		private void PortNameBox_SelectedValueChanged(object sender, EventArgs e) {
			if (Port.PortName != PortNameBox.Text) {
				ClosePort();
				Port.PortName = PortNameBox.Text;
				LogBox.AppendText("Имя последовательного порта изменено на " + Port.PortName + '\n');
			}
			LogBox.ScrollToCaret();
		}

		private void Pr1_Click(object sender, EventArgs e) {
			buffer[3] = RUN_PROG;
			buffer[4] = 1;
			buffer[5] = 1;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + RUN_PROG + 1 + 1);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Запустить программу №1\n");

			ReceiveConfirm();
		}

		private void Pr2_Click(object sender, EventArgs e) {
			buffer[3] = RUN_PROG;
			buffer[4] = 1;
			buffer[5] = 2;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + RUN_PROG + 1 + 2);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Запустить программу №2\n");

			ReceiveConfirm();
		}

		private void Pr3_Click(object sender, EventArgs e) {
			buffer[3] = RUN_PROG;
			buffer[4] = 1;
			buffer[5] = 3;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + RUN_PROG + 1 + 3);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Запустить программу №3\n");

			ReceiveConfirm();
		}

		private void Pr4_Click(object sender, EventArgs e) {
			buffer[3] = RUN_PROG;
			buffer[4] = 1;
			buffer[5] = 4;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + RUN_PROG + 1 + 4);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Запустить программу №4\n");

			ReceiveConfirm();
		}

		private void Pr5_Click(object sender, EventArgs e) {
			buffer[3] = RUN_PROG;
			buffer[4] = 1;
			buffer[5] = 5;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + RUN_PROG + 1 + 5);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Запустить программу №5\n");

			ReceiveConfirm();
		}

		private void PingButton_Click(object sender, EventArgs e) {
			buffer[3] = ECHO;
			buffer[4] = 0;
			buffer[5] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + ECHO);
			buffer[6] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 7);
			LogBox.AppendText("Ping контроллера\n");

			ReceiveConfirm();
		}

		private void PlPs_Click(object sender, EventArgs e) {
			buffer[3] = PAUSE_TOGGLE;
			buffer[4] = 0;
			buffer[5] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + PAUSE_TOGGLE);
			buffer[6] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 7);
			LogBox.AppendText("Переключение паузы\n");
			ReceiveConfirm();
		}

		private void Power_Click(object sender, EventArgs e) {
			buffer[3] = ONOFFCOMM;
			buffer[4] = 0;
			buffer[5] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + ONOFFCOMM);
			buffer[6] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 7);
			LogBox.AppendText("Переключение питания\n");
			ReceiveConfirm();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
			buffer[2] = Convert.ToByte(сntrNum.Value);
		}

		private void Sp1_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_SP;
			buffer[4] = 1;
			buffer[5] = 1;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_SP + 1 + 1);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень скорости 1\n");

			ReceiveConfirm();
		}

		private void Sp2_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_SP;
			buffer[4] = 1;
			buffer[5] = 2;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_SP + 1 + 2);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень скорости 2\n");

			ReceiveConfirm();
		}

		private void Sp0_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_SP;
			buffer[4] = 1;
			buffer[5] = 0;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_SP + 1 + 0);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень скорости 0\n");

			ReceiveConfirm();
		}

		private void Sp3_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_SP;
			buffer[4] = 1;
			buffer[5] = 3;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_SP + 1 + 3);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень скорости 3\n");

			ReceiveConfirm();
		}

		private void Sp4_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_SP;
			buffer[4] = 1;
			buffer[5] = 4;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_SP + 1 + 4);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень скорости 4\n");

			ReceiveConfirm();
		}

		private void Br0_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_BR;
			buffer[4] = 1;
			buffer[5] = 0;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_BR + 1 + 0);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень яркости 0\n");

			ReceiveConfirm();
		}

		private void Br1_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_BR;
			buffer[4] = 1;
			buffer[5] = 1;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_BR + 1 + 1);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень яркости 1\n");

			ReceiveConfirm();
		}

		private void Br2_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_BR;
			buffer[4] = 1;
			buffer[5] = 2;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_BR + 1 + 2);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень яркости 2\n");

			ReceiveConfirm();
		}

		private void Br3_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_BR;
			buffer[4] = 1;
			buffer[5] = 3;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_BR + 1 + 3);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень яркости 3\n");

			ReceiveConfirm();
		}

		private void Br4_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_BR;
			buffer[4] = 1;
			buffer[5] = 4;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_BR + 1 + 4);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень яркости 4\n");

			ReceiveConfirm();
		}

		private void Br5_Click(object sender, EventArgs e) {
			buffer[3] = CHANGE_BR;
			buffer[4] = 1;
			buffer[5] = 5;
			buffer[6] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + CHANGE_BR + 1 + 5);
			buffer[7] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 8);
			LogBox.AppendText("Установить уровень яркости 5\n");

			ReceiveConfirm();
		}

		private void Programming_Click(object sender, EventArgs e) {
			buffer[3] = ENTER_PROG_MODE;
			buffer[4] = 0;
			buffer[5] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + ENTER_PROG_MODE);
			buffer[6] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 7);
			LogBox.AppendText("Войти в режим програмирования\n");

			ReceiveConfirm();
		}

		private void Frame1_Click(object sender, EventArgs e) {
			buffer[3] = WRITE_FRAME;
			buffer[4] = 8;
			buffer[5] = 100;
			buffer[6] = 0;
			buffer[7] = 0;
			buffer[8] = 0;
			buffer[9] = 0;
			buffer[10] = 255;
			buffer[11] = 255;
			buffer[12] = 0;
			buffer[13] = Convert.ToByte((Convert.ToByte('R') + buffer[2] + WRITE_FRAME + 8 + 100 + 255 + 255) & 0xFF);
			buffer[14] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 15);
			LogBox.AppendText("Фрейм №1\n");

			ReceiveConfirm();
		}

		private void Frame2_Click(object sender, EventArgs e) {
			buffer[3] = WRITE_FRAME;
			buffer[4] = 8;
			buffer[5] = 100;
			buffer[6] = 0;
			buffer[7] = 0;
			buffer[8] = 0;
			buffer[9] = 0;
			buffer[10] = 0;
			buffer[11] = 255;
			buffer[12] = 255;
			buffer[13] = Convert.ToByte((Convert.ToByte('R') + buffer[2] + WRITE_FRAME + 8 + 100 + 255 + 255) & 0xFF);
			buffer[14] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 15);
			LogBox.AppendText("Фрейм №2\n");

			ReceiveConfirm();


		}

		private void Frame3_Click(object sender, EventArgs e) {
			buffer[3] = WRITE_FRAME;
			buffer[4] = 8;
			buffer[5] = 100;
			buffer[6] = 0;
			buffer[7] = 0;
			buffer[8] = 0;
			buffer[9] = 0;
			buffer[10] = 255;
			buffer[11] = 0;
			buffer[12] = 255;
			buffer[13] = Convert.ToByte((Convert.ToByte('R') + buffer[2] + WRITE_FRAME + 8 + 100 + 255 + 255) & 0xFF);
			buffer[14] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 15);
			LogBox.AppendText("Фрейм №3\n");

			ReceiveConfirm();
		}

		private void Finalize_Click(object sender, EventArgs e) {
			buffer[3] = FINILIZE_PROG;
			buffer[4] = 0;
			buffer[5] = Convert.ToByte(Convert.ToByte('R') + buffer[2] + FINILIZE_PROG);
			buffer[6] = 0x55;

			OpenPort();
			Port.Write(buffer, 0, 7);
			LogBox.AppendText("Выйти из режима програмирования\n");

			ReceiveConfirm();
		}

		private void button1_Click(object sender, EventArgs e) {
			ClosePort();
		}

		private void refreshCom_Click(object sender, EventArgs e) {
			PortNameBox.Items.Clear();
			for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++) {
				PortNameBox.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
			}

		}

	}
}
