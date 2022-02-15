using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CJQ_2805
{
	 class SerialManager
	{
		#region Members

		private const int WaitCounts = 10;
		private const int Time_Out = 2000; //ms

		private SerialPort m_SerialPort;
		private TimeSpan m_timeout = new TimeSpan(0,0,0,0,Time_Out);


        byte[] my_CommRevArr = new byte[255];
		byte my_Comm_rev_cnt = 0;

		public byte[] recFrame
        {
			get 
			{
				return my_CommRevArr;
			}

			set 
			{
				my_CommRevArr = value;	
			}
		}
	
		#endregion Members

		#region Constructors

		public SerialManager()
		{
			m_SerialPort = new SerialPort();

		}

		#endregion Constructors

		#region	Methods

		public void clear_rev()
		{
			my_Comm_rev_cnt = 0;
		}

		public byte get_rc_cnt()
		{
			return my_Comm_rev_cnt;
		}


		private void my_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
			try
			{
				byte n = (byte)m_SerialPort.BytesToRead;
				byte[] buf = new byte[n];		//声明一个临时数组存储当前来的串口数据
				m_SerialPort.Read(buf, 0, n);   //读缓冲数据

				for (int i = 0; i < buf.Length; i++)
				{
					my_CommRevArr[my_Comm_rev_cnt + i] = buf[i];	//将接收的数据连接
				}
				my_Comm_rev_cnt += n;  //增加接收计数

			}
			catch(Exception err)
			{
				Debug.WriteLine("Error:" + err.Message);			
			}	
		
		}

		public void setBaudRate(int baudRate)
		{
			try
			{
				m_SerialPort.Close();
				m_SerialPort.BaudRate = baudRate;
				m_SerialPort.Open();
			}
			catch (Exception err)
			{
				Debug.WriteLine("Error: "+err.Message);
			}
		
		}

		public void Open(string port, int baudRate)
        {
			try
			{
				Close();
				m_SerialPort.PortName = port;
				m_SerialPort.BaudRate = baudRate;
				m_SerialPort.Parity = Parity.None;
				m_SerialPort.StopBits = StopBits.One;
				m_SerialPort.WriteTimeout = Time_Out;

				m_SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.my_DataReceived);   //注册事件。
				m_SerialPort.Open();
			}
			catch (Exception err)
			{
				Debug.WriteLine("error: "+err.Message);
			}
        }

		public void Close()
        {
			if (m_SerialPort.IsOpen)
				m_SerialPort.Close();

        }

		public bool IsOpen()
		{
			return m_SerialPort.IsOpen;
		}

		public void Write(CommPack serial)
		{
			try
			{
				m_SerialPort.DiscardInBuffer();
				this.m_SerialPort.Write(serial.Frame, 0, serial.length());
				Debug.WriteLine("send: " + serial.Frame[0].ToString() + "len:" + serial.length().ToString());

			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error: "+ex.Message);
			
			}
		}

		public void Writebyte(byte data)
		{
			try
			{
				m_SerialPort.DiscardInBuffer();
				byte[] l_tx_arr = new byte[1];
				l_tx_arr[0] = data;
				this.m_SerialPort.Write(l_tx_arr, 0, 1);
				Debug.WriteLine("Writebyte: " + data);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error: "+ex.Message);
			
			}
		
		}




		public void SearchAndAddSerialToComboBox(ToolStripComboBox Mybox)
		{
			string Buffer;
			Mybox.Items.Clear();

			for (byte i = 1; i < 21; i++) // 这里只定义了串口号到20
			{
				try
				{
					Buffer = "COM" + i.ToString();
					m_SerialPort.PortName = Buffer;
					m_SerialPort.Open();
					Mybox.Items.Add(Buffer);
					m_SerialPort.Close();
				}
				catch
				{


				}
			}
		}
        #endregion Methods
    }
}
