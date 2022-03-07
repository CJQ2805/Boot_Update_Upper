using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace BOOT_UPDATE
{
    public partial class CJQ2805 : Form
    {
        bool IsUpdata = false;
        byte IsUpdata_state = 0;

        CJQ_2805.SerialManager loaderserial;
        CJQ_2805.HexParse      loaderhex;
        CJQ_2805.CommPack      loaderframe;
        About About = new About();
        //文件创建标志
        bool readFileFlag = false; 


        public CJQ2805()
        {
            InitializeComponent();
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void CJQ2805_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
                    this.cmPort.Items.Add(com);
                   
            }
            catch
            {
                CommStatus.Text = "找不到通信端口! ";
            }
            loaderserial = new CJQ_2805.SerialManager();
            loaderhex = new CJQ_2805.HexParse();
            loaderframe = new CJQ_2805.CommPack();
        }




        private void OpenBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (loaderserial.IsOpen() != true)
                {
                    String open_com = "打开串口: ";
                    if (cmPort.Text == "选择串口")
                    {
                        MessageBox.Show("串口不能为空！");
                        return;
                    }
                    loaderserial.Open(cmPort.Text, (int)19200); //安全打开串口
                    cmPort.Enabled = false;
                    open_com += sComm.PortName.Substring(3, 1) + ",";
                    open_com += "波特率: " + sComm.BaudRate.ToString() + "，";
                    open_com += "数据位: " + sComm.DataBits.ToString() + "，";
                    open_com += "停止位: " + "1" + ", ";
                    open_com += "校验位: " + "无";

                    CommStatus.Text = open_com;

                    IsUpdata = false;
                    IsUpdata_state = 0;
                    OpenBtn.Text = "关闭串口";
                    ReadFileBtn.Enabled = true;
                }
                else {
                    ReadFileBtn.Enabled = false;
                    UpDate_Btn.Enabled = false;
                    OpenBtn.Text = "打开串口";
                    cmPort.Enabled = true;
                    loaderserial.Close();

                    this.TopMost = false;
                
                }

            }
            catch (Exception Err)
            {
                ReadFileBtn.Enabled = false;
                UpDate_Btn.Enabled = false;
                OpenBtn.Text = "打开串口";
                loaderserial.Close();
                Debug.WriteLine("Err: "+Err.Message.ToString());
                CommStatus.Text = "串口出错！";
            }
        }
       
        private void ReadFileBtn_Click(object sender, EventArgs e)
        {   
            readFileFlag = true;
            loaderhex.set_list_view(listbox_hex, readFileFlag);
            UpDate_Btn.Enabled = true;
        }






        /// <summary>
        /// 延时函数 ms级
        /// </summary>
        /// <param name="DelayTime"></param>
        public void Delay_Ms(int DelayTime)
        {
            int itime = Environment.TickCount;

            while (true)
            {
                if (Environment.TickCount - itime >= DelayTime)
                {
                    break;
                }

                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        
        }

        public void delay_time(byte time)
        {
            byte btout_time_cnt = 0;

            while (btout_time_cnt < time)
            {
                btout_time_cnt++;
                System.Threading.Thread.Sleep(25);
                if (loaderserial.get_rc_cnt() > 7)
                {
                    Debug.WriteLine("up_stat wait" + btout_time_cnt.ToString());
                    break;
                }

            }

            if (btout_time_cnt >= 10)
            {
                Debug.WriteLine("up_stat wait " + "超时");    
            }
        
        }

        private void UpDate_Btn_Click(object sender, EventArgs e)
        {
            UpDate_Btn.Enabled = false;
            Thread R1 = new Thread(Updata_process);
            R1.IsBackground = true;
            R1.Start();
        }

        public void Updata_process()
        {
            byte i = 0;
            //System.Threading.Thread.Sleep(150);
            //for (byte i = 0; i < 3; i++) 
            //{
            //    loaderserial.clear_rev();
            //    loaderserial.Write(loaderframe);
            //}
 

            for (i = 0; i <= 5; i++)
            {
                loaderserial.clear_rev();
                loaderframe.TxCmd(BOOT_UPDATE.CMD.UPDATE_CMD_APP_INTO_BOOT);
                loaderserial.Write(loaderframe);//上一句只是组包，没有发送
                Display_label("update start!",Color.DarkGray, Color.Green);
                delay_time(20);
                if (loaderserial.get_rc_cnt() > 7)
                {
                    if (loaderframe.IsRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_APP_INTO_BOOT) == 1)
                    {
                        Display_label("app into boot ok", Color.DarkGray, Color.Green);

                    }
                }

            }
            //进入IAP程序成功
            for (i = 0; i < 3; i++)
            {
                loaderserial.clear_rev();
                Display_label("准备流程", Color.DarkGray, Color.Green);
                loaderserial.Writebyte(BOOT_UPDATE.CMD.UPDATE_CMD_COMPEL_READY);
                delay_time(25);
                if (loaderserial.get_rc_cnt() > 7)
                {
                    if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_COMPEL_READY_OK) == 1)
                    {
                        Display_label("准备就绪", Color.DarkGray, Color.Green);
                    }
                    else 
                    {
                        Display_label("准备流程重连", Color.DarkGray, Color.Yellow);

                    }
                }
            }

            //准备进行IAP处理
            for (i = 0; i < 3; i++)
            {

                loaderserial.clear_rev();
                Display_label("updata ready", Color.DarkGray, Color.Green);
                loaderserial.Writebyte(BOOT_UPDATE.CMD.UPDATE_CMD_UPDATA_READY);
                delay_time(25);
                if (loaderserial.get_rc_cnt() > 7)
                {
                    if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_UPDATA_READY_OK) == 1)
                    {
                        Display_label("updata ready ok",Color.DarkGray,Color.Green);
                        break;

                    }
                    else {
                        Display_label("数据升级重连",Color.DarkGray,Color.Green);
                        
                    }
                }
            }

            //擦除下位机flash
            for (i = 0; i < 3; i++)
            {
                loaderserial.clear_rev();
                Display_label("擦除内存流程",Color.DarkGray,Color.Green);
                loaderframe.TxCmd(BOOT_UPDATE.CMD.UPDATE_CMD_ERASE);
                loaderserial.Write(loaderframe);        //上一句只是组包，没有发送
                delay_time(10);

                if (loaderserial.get_rc_cnt() > 7)
                {
                    if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_ERASE_OK) == 1)
                    {
                        Display_label("擦除成功", Color.DarkGray, Color.Green);
                        break;
                    }
                    else 
                    {
                        Display_label("擦除重连",Color.DarkGray, Color.Green);
                    }
                }

            }


            //这里开始是第二次

            for (i = 0; i < 3; i++)
            {
                loaderserial.clear_rev();
                Display_label("准备流程", Color.DarkGray, Color.Green);
                loaderserial.Writebyte(BOOT_UPDATE.CMD.UPDATE_CMD_COMPEL_READY);
                delay_time(25);
                if (loaderserial.get_rc_cnt() > 7)
                {
                    if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_COMPEL_READY_OK) == 1)
                    {
                        Display_label("准备就绪 OK", Color.DarkGray, Color.Green);
                        break;
                    }
                    else
                    {
                        Display_label("准备 Try", Color.DarkGray, Color.Green);
                    }
                }
            }


            for (i = 0; i < 3; i++)
            {
                loaderserial.clear_rev();
                Display_label("升级就绪",Color.DarkGray,Color.Green);
                loaderserial.Writebyte(BOOT_UPDATE.CMD.UPDATE_CMD_UPDATA_READY);
                delay_time(25);
                if (loaderserial.get_rc_cnt() > 7)
                {
                    if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_UPDATA_READY_OK) == 1)
                    {
                        Display_label("升级就绪 OK", Color.DarkGray, Color.Green);
                        break;
                    }
                    else {

                        Display_label("升级重连"+i.ToString(),Color.DarkGray,Color.Green);
                    }

                }
            }

            for (i = 0; i < 3; i++)
            {
                loaderserial.clear_rev();
                Display_label("擦除流程", Color.DarkGray, Color.Green);
                loaderserial.Writebyte(BOOT_UPDATE.CMD.UPDATE_CMD_COMPEL_READY);
                delay_time(25);
                if (loaderserial.get_rc_cnt() > 7)
                {
                    if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_COMPEL_READY_OK) == 1)
                    {
                        Display_label("擦除成功", Color.DarkGray, Color.Green);
                        break;
                    }
                    else
                    {
                        Display_label("擦除重连", Color.DarkGray, Color.Yellow);
                    }
                }
            }
            Display_label("升级中", Color.DarkGray, Color.Green);
            Thread R2 = new Thread(Iap_Download);
            R2.IsBackground = true;
            R2.Start();
            
        }

        volatile static int Data_Len = 0;
        public UInt16 Seq = 0;


        private void Iap_Download()
        {
            UInt16 i, j;
            int len, TxtSeq = 0;
            string tmp_str = string.Empty;

            byte[] flash_data_arr = new byte[255];
            UInt16 flash_addr = 0;

            Data_Len = 0;
            Seq = 0;

            UInt16 last_flash_addr = 0xFFFF;
            bool new_block_flag = false;

            for (i = 0; i < 60000; i++)
            {
                if (TxtSeq < listbox_hex.Items.Count)
                {
                    TxtSeq = i;
                    listbox_hex.Invoke(new EventHandler(delegate
                    {
                        listbox_hex.SelectedIndex = TxtSeq;
                        tmp_str = listbox_hex.Text;
                        display_progressbar((TxtSeq * 100) / listbox_hex.Items.Count);
                    }));

                    Debug.WriteLine("TxtSeq:" + Convert.ToString(TxtSeq));
                    Debug.WriteLine("DevDataProg" + Convert.ToString(Seq));

                    if (tmp_str.Length == 43)
                    {
                        if (tmp_str.Substring(7, 2) == "00")
                        {
                            //有效数据长度 为 43
                            flash_addr = Convert.ToUInt16(tmp_str.Substring(3, 4), 16);
                            if (flash_addr == 0x00c0)
                            {
                                continue;
                            }
                            // 新的FLASH块
                            if ((flash_addr / 128) != (last_flash_addr / 128))
                            {
                                new_block_flag = true;
                                //上个Flash块中有数据，整理，发送
                                if (Data_Len != 0)
                                {
                                    if (Data_Len < 128)
                                    {
                                        //插入数据 0xff
                                        for (j = (ushort)Data_Len; j < 128; j++)
                                        {
                                            flash_data_arr[j] = 0xFF;
                                            Data_Len++;
                                        }
                                    }
                                    if (FlashWrite((UInt16)((last_flash_addr / 128) * 128), flash_data_arr, (byte)(Data_Len + 2)) == false) return;

                                    Data_Len = 0;    // 复位
                                }

                                //当前新的块，看看是否需要整理数据
                                if (flash_addr % 128 != 0)
                                {
                                    Data_Len = 0;
                                    for (j = 0; j < flash_addr % 128; j++)
                                    {
                                        flash_data_arr[j] = 0xFF;
                                        Data_Len++;
                                    }

                                }
                            }
                        }


                        if (new_block_flag == true)
                        {
                            new_block_flag = false;
                        }
                        //因为新的块开头地址数据已经被补齐
                        //查看当前块中间的地址是否有跳跃，需要补数据
                        else
                        {
                            if (flash_addr != last_flash_addr + 16)
                            {
                                for (j = (ushort)Data_Len; j < flash_addr % 128; j++)
                                {
                                    flash_data_arr[Data_Len] = 0xFF;
                                    Data_Len++;
                                }
                            }
                        }

                        //提取FLASH 数据
                        for (j = 0; j < 16; j++)
                        {
                            flash_data_arr[Data_Len] = Convert.ToByte(tmp_str.Substring(9 + j * 2, 2), 16);
                            Data_Len++;
                        }

                        last_flash_addr = flash_addr;

                    }
                    else if (tmp_str.Substring(7, 2) == "05")
                    {
                        if (Data_Len > 0)
                        {
                            for (j = (ushort)Data_Len; j < 128; j++)
                            {
                                flash_data_arr[j] = 0xFF;
                                Data_Len++;
                            }

                       

                        if (FlashWrite((UInt16)((last_flash_addr / 128) * 128), flash_data_arr, (byte)(Data_Len + 2)) == false) return;
                        Data_Len = 0;
                        }
                  

                        loaderserial.clear_rev();
                        loaderframe.TxCmd(BOOT_UPDATE.CMD.UPDATE_CMD_FILE_END);
                        loaderserial.Write(loaderframe);
                        delay_time(10);

                        if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_LOAD_OK) == 1)
                        {
                            Debug.WriteLine("Load_OK" + "退出成功");
                        }
                        else
                        {
                            Debug.WriteLine("File_End" + "退出出错");
                        }

                        Seq = 0;
                        display_progressbar(100);

                        IsUpdata = true;

                        Display_label("升级完成", Color.Green, Color.Black);
                        return;
                    }
                }
               
            }
            return;

        }

     
        private bool FlashWrite(UInt16 addr, byte[] data_arr, byte DataL)
        {
            if (IsFlashWriteSuccess(addr, Seq, data_arr, (byte)DataL) != true)
            {
                byte k;
                for (k = 0; k < 255; k++)
                {
                    loaderserial.Writebyte(0xff);
                }
                System.Threading.Thread.Sleep(20);
                loaderserial.clear_rev();
                loaderframe.TxCmd(BOOT_UPDATE.CMD.UPDATE_CMD_EXIT_UPDATE);
                loaderserial.Write(loaderframe);
                Delay_Ms(600);
                if (loaderframe.IsCommandRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_EXIT_OK) == 1)
                {
                    Debug.WriteLine("FlashWrite" + "退出成功");
                }
                else
                {
                    Debug.WriteLine("FlashWrite" + "退出出错");
                }
                Seq = 0;
                IsUpdata = true;
                Display_label("升级失败",Color.Red,Color.Black);
                return false;
            }
            return true;
        }

        public bool IsFlashWriteSuccess(UInt16 addr, UInt16 l_seq, byte[] data_arr, byte DataL)
        {
            byte tx_retry_cnt = 0;
            while (true)
            {
                if (tx_retry_cnt < 3)
                {
                
                    Debug.Write("IsFlashWriteSuccess" + Convert.ToString(tx_retry_cnt));
                    loaderserial.clear_rev();
                    loaderframe.TxFlashDataFrame(addr, l_seq, data_arr, DataL);
                    loaderserial.Write(loaderframe);
                    System.Threading.Thread.Sleep(50);
                    delay_time(20);

                    if (loaderframe.IsDataRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_CRC_OK, l_seq) == true)
                    {
                        Seq++;
                        return true;      // 数据正常
                    }
                    else if (loaderframe.IsDataRespondOk(loaderserial, BOOT_UPDATE.CMD.UPDATE_CMD_WRITE_ERROR, l_seq) == true)
                    {
                        Debug.Write("Flash Write Error!!!");
                        return false;
                    }
                    else
                    {
                        tx_retry_cnt++;   //重发，错误的信息帧（校验值出错）或者错误的返回帧
                        System.Threading.Thread.Sleep(300);
                    }
                }
                else
                {
                    return false;
                }

            }
        }

        private void lab_disp_up_state_Click(object sender, EventArgs e)
        {

        }

        private void display_progressbar(int value)
        { 
            progressBar1.Invoke(new EventHandler(delegate
            {
                progressBar1.Value = value;
                
            }));
        
        }

        private void Display_label(string test_str, Color back_color,Color fore_color)
        {
            lab_disp_up_state.Invoke(new EventHandler(delegate{

                lab_disp_up_state.Text = test_str;
                lab_disp_up_state.BackColor = back_color;
                lab_disp_up_state.ForeColor = fore_color;
            }));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (cmPort.Enabled)
            {
                loaderserial.SearchAndAddSerialToComboBox(cmPort);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AboutBtn_Click(object sender, EventArgs e)
        {
            About.ShowDialog();
        }
    }
}


