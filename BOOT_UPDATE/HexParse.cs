using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CJQ_2805
{
    class HexParse
    {
        #region Members
        ListBox my_listbox  = new ListBox();
        uint flashExAddress = 0x1000;
        int file_len;
        byte[] hexchar = new byte[] { };
        UInt16 DataCRC16 = 0;
        UInt16 FileSize = 0;
        string version_str = string.Empty;
        long  f_length;
        #endregion Members

        public byte[] HEX
        {
            get 
            {
                return hexchar;
            }

            set 
            {
                hexchar = value;
            }
        }

        UInt32 Total_Length = 0;

        public void set_list_view(ListBox listBox1, bool readFileFlag)
        {
            if (readFileFlag == true)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "选择文件";
                fileDialog.Filter = "hex files (*.hex)|*.hex";
                fileDialog.FilterIndex = 1;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    String fileName = fileDialog.FileName;

                    StreamReader objReader = new StreamReader(fileName);
                    string sLine = string.Empty;

                    string newLine = string.Empty;
                    string tmp_str = string.Empty;
                    string file_name = string.Empty;

                    UInt16 code_addr, rebuilt_addr_min, rebuilt_addr_max, i, code_len;
                    UInt16 richbox_line = 0;
   
                    bool hex_err_flag = false;
                    
                    file_name = fileDialog.SafeFileName;
                    listBox1.Items.Clear();

                    int name_len = file_name.Length;

                    if (fileName.Substring(name_len - 3, 3) == "hex")
                    { 
                        
                    }

                    while (sLine != null)
                    {
                        sLine = objReader.ReadLine();
                        if (sLine != null)
                        {
                            if (sLine.Substring(7, 2) == "00")
                            {
                                //这里做的是将长度不足16的补齐为16字节发送，填充的是0xFF
                                if (sLine.Substring(1, 2) != "10")
                                {
                                    code_addr = Convert.ToUInt16(sLine.Substring(3, 4), 16);
                                    code_len = Convert.ToUInt16(sLine.Substring(1,2),16);

                                    rebuilt_addr_min = (UInt16)(code_addr / 16 * 16);
                                    rebuilt_addr_max = (UInt16)((code_addr / 16 + 1) * 16 - 1);

                                    newLine = ":10";

                                    //补足地址的长度
                                    tmp_str = Convert.ToString(rebuilt_addr_min, 16);
                                    if (tmp_str.Length == 3)
                                    {
                                        tmp_str = '0' + tmp_str;
                                    }
                                    else if (tmp_str.Length == 2)
                                    {
                                        tmp_str = "00" + tmp_str;
                                    }
                                    else if (tmp_str.Length == 1)
                                    {
                                        tmp_str = "000" + tmp_str;
                                    }
                                    newLine += tmp_str;
                                    newLine += "00";

                                    for (i = rebuilt_addr_min; i < rebuilt_addr_max + 1; i++)
                                    {
                                        if (i < code_addr)
                                        {
                                            newLine += "FF";
                                        }
                                        else if (i >= (code_addr + code_len))
                                        {
                                            newLine += "FF";
                                        }
                                        else {
                                            newLine += sLine.Substring(9 + (i - code_addr) * 2, 2);
                                        }
                                    }
                                    sLine = newLine;
                                }
                            }

                            sLine = sLine.ToUpper();
                            listBox1.Items.Add(sLine);
                            richbox_line++;
                        }
                    }

                    objReader.Close();
                    string real_info_str = string.Empty;

                    if (hex_err_flag == false)
                    {
                        Total_Length = 0;
                        list_data_handle(listBox1);
                        TotalLenProg(listBox1);
                    

                    }
                    else
                    {
                        listBox1.Items.Clear();
                        hex_err_flag = false;
                        return;
                    }
                }
            }
        }


        private void list_data_handle(ListBox listBox1)
        {
            UInt16 i, j;

            string last_val_str = string.Empty;
            string tmp_str = string.Empty;

            for (i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                tmp_str = listBox1.Text;
                if (tmp_str.Substring(7, 2) == "00")
                {
                    if (tmp_str.Length == 41)
                    {
                        if (last_val_str != string.Empty)
                        {
                            if (tmp_str.Substring(3, 4) == last_val_str.Substring(3, 4))
                            {
                                char[] cc = tmp_str.ToCharArray();
                                char[] dd = last_val_str.ToCharArray();

                                for (j = 0; j < 16; j++)
                                {
                                    if (tmp_str.Substring(9 + j * 2, 2) == "FF")
                                    {
                                        if (last_val_str.Substring(9 + j * 2, 2) != "FF")
                                        {
                                            cc[9 + j * 2] = dd[9 + j * 2];
                                            cc[9 + j * 2 + 1] = dd[9 + j * 2 + 1];
                                        }
                                    }
                                }

                                tmp_str = new string(cc);
                                listBox1.Items[i] = tmp_str;
                                listBox1.Items[i - 1] = "";

                            }
                        }
                        last_val_str = tmp_str;
                    }
                }
            }

            //删除空格
            for (i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                tmp_str = listBox1.Text;

                if (tmp_str == "")
                {
                    listBox1.Items.RemoveAt(i);
                    i = (UInt16)(i - 1);
                }
            }

            //加入校验和
            for (i = 0; i < listBox1.Items.Count; i++)
            {
                byte tmp_data = 0;
                byte check_sum = 0;
                string tmp_str2;

                listBox1.SelectedIndex = i;
                tmp_str = listBox1.Text;
                f_length += 64;
                if (tmp_str.Length == 41)
                {
                    check_sum = 0;

                    for (j = 0; j < 20; j++)
                    {
                        tmp_data = Convert.ToByte(tmp_str.Substring(j * 2 + 1, 2), 16);
                        check_sum += tmp_data;
                    }

                    check_sum = (byte)(~check_sum);
                    check_sum += 1;
                    tmp_str2 = Convert.ToString(check_sum, 16);

                    if (tmp_str2.Length == 1)
                    {
                        tmp_str2 = '0' + tmp_str2;
                    }

                    tmp_str = tmp_str + tmp_str2;
                    tmp_str = tmp_str.ToUpper();
                    listBox1.Items[i] = tmp_str;
                }
            }
        }

        private void TotalLenProg(ListBox listBox1)
        {
            Int16 i, j;
            string tmp_str = string.Empty;
            byte[] flash_data_arr = new byte[64];
            UInt16 flash_addr = 0;
            UInt16 last_flash_addr = 0xFFFF;
            byte data_len = 0;
            bool new_block_flag = false;

            for (i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                tmp_str = listBox1.Text;
                if (tmp_str.Length == 43)
                {
                    if (tmp_str.Substring(7, 2) == "00")
                    {
                        flash_addr = Convert.ToUInt16(tmp_str.Substring(3, 4), 16);

                        if ((flash_addr / 64) != (last_flash_addr / 64))
                        {
                            new_block_flag = true;

                            if (data_len != 0)
                            {
                                if (data_len < 64)
                                {
                                    for (j = data_len; j < 64; j++)
                                    {
                                        flash_data_arr[j] = 0xFF;
                                        data_len++;
                                    }
                                }

                                for (j = 0; j < 64; j++)
                                {
                                    DataCRC16 += flash_data_arr[j];
                                }
                                Total_Length++;
                                data_len = 0;
                            }

                            if (flash_addr % 64 != 0)
                            {
                                data_len = 0;
                                for (j = 0; j < flash_addr % 64; j++)
                                {
                                    flash_data_arr[j] = 0xFF;
                                    data_len++;
                                }
                            }
                        }


                        if (new_block_flag == true)
                        {
                            new_block_flag = false;
                        }

                        else
                        {

                            if (flash_addr != last_flash_addr + 16)
                            {
                                flash_data_arr[data_len] = 0xFF;
                                data_len++;
                            }
                        }

                        //提取flash数据
                        for (j = 0; j < 16; j++)
                        {
                            flash_data_arr[data_len] = Convert.ToByte(tmp_str.Substring(9 + j * 2, 2), 16);
                            data_len++;
                        }

                        last_flash_addr = flash_addr;
                    }

                }
                else if (tmp_str.Substring(7, 2) == "04" && tmp_str.Length == 15)
                {
                    //上一个FLASH块中有数据，整理，发送
                    if (data_len != 0)
                    {
                        if (data_len < 64)
                        {
                            for (j = data_len; j < 64; j++)
                            {
                                flash_data_arr[j] = 0xFF;
                                data_len++;
                            }
                        }

                        for (j = 0; j < 64; j++)
                        {
                            DataCRC16 += flash_data_arr[j];
                        }

                        Total_Length++;
                    }
                    break;
                }
            
            }
        }

    }
}
