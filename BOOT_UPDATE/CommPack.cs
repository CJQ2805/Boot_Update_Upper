using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJQ_2805
{
    class CommPack
    {
        Byte[] send_data = new Byte[300];
        byte lenAdd = 0;

        UInt16 crc16;

        public byte[] Frame
        {
            get
            {
                return send_data;
            }
            set
            {
                send_data = value;
            }
        }

        void setDefaultLen()
        {
            lenAdd = 2;
        }

        private void setCrc()
        {
            send_data[lenAdd++] = (byte)crc16;
            send_data[lenAdd++] = (byte)(crc16 >> 8);
        }

        private void setUpCrc()
        {
            send_data[lenAdd++] = (byte)(crc16 >> 8);
            send_data[lenAdd++] = (byte)crc16;
        }

        public bool IsDataRespondOk(CJQ_2805.SerialManager my_comm, byte respond, UInt16 seq)
        {
            if (my_comm.recFrame[0] == 0xFE
                && my_comm.recFrame[1] == 0xFE
                && my_comm.recFrame[7] == 0xBB)
            {
                UInt16 crc_check = 0;
                crc_check = CRC_16(my_comm.recFrame, 5);
                if (my_comm.recFrame[5] == (byte)(crc_check >> 8) && my_comm.recFrame[6] == (byte)crc_check)
                {
                    if (my_comm.recFrame[2] == respond)
                    {
                        if (my_comm.recFrame[3] == (byte)(seq >> 8) && my_comm.recFrame[4] == (byte)seq)
                        {
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public int IsRespondOk(CJQ_2805.SerialManager my_comm, byte cmd)
        {
            if (my_comm.recFrame[0] == 0xFE
               && my_comm.recFrame[1] == 0xFE
               && my_comm.recFrame[2] == cmd)
            {
                if((recFrameCrcCheck(my_comm.recFrame,my_comm.get_rc_cnt()) ==true))
                {
                    return 1;
                }
            }
            return 0;
        }

        public int rec_FrameCheck(CJQ_2805.SerialManager my_comm)
        { 
            if((my_comm.recFrame[0] == 0xFE)
                &&(my_comm.recFrame[my_comm.get_rc_cnt() -1] == 0xBB))
            {
                if (recFrameCrcCheck(my_comm.recFrame, my_comm.get_rc_cnt()) == true)
                {
                    return 1;
                }

            }
            return 0;
        }

        public byte length()
        {
            return lenAdd;
        }

        public void txUpCmdFrame(byte cmd)
        {
            lenAdd = 0;
            send_data[lenAdd++] = cmd;
        }



        public int IsBoot(CJQ_2805.SerialManager my_comm)
        {
            if (my_comm.recFrame[0] == 0xFE
               && my_comm.recFrame[1] == 0xFE)
            {
                UInt16 crc_check = 0;
                crc_check = CRC_16(my_comm.recFrame, 5);
                if (my_comm.recFrame[5] == (byte)(crc_check >> 8) && my_comm.recFrame[6] == (byte)crc_check)
                {
                    if (my_comm.recFrame[2] == 0x28
                      || my_comm.recFrame[2] == 0x24
                      || my_comm.recFrame[2] == 0x23)
                    {
                        if (my_comm.recFrame[7] == 0xBB)
                        {
                            return 1;
                        }
                        else return 0;
                    }
                    else if (my_comm.recFrame[2] == 0x27)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else return 0;

            }
            else return 0;
        
        }

        public int IsCommandRespondOk(CJQ_2805.SerialManager my_comm, byte respond)
        {
            if (my_comm.recFrame[0] == 0xFE
               && my_comm.recFrame[1] == 0xFE
               && my_comm.recFrame[7] == 0xBB)
            {
                UInt16 crc_check = 0;
                crc_check = CRC_16(my_comm.recFrame, 5);
                if (my_comm.recFrame[5] == (byte)(crc_check >> 8) && my_comm.recFrame[6] == (byte)crc_check)
                {
                    if (my_comm.recFrame[2] == respond)
                    {
                        return 1;
                    }
                    else return 0;
                }
                else return 0;
            }
            else return 0;
        }

        public void TxCmd(byte cmd)
        {
            setDefaultLen();
            send_data[0] = 0xFE;
            send_data[1] = 0xFE;
            send_data[lenAdd++] = cmd;
            send_data[lenAdd++] = 0;
            crc16 = CRC_16(send_data, 4);
            setUpCrc();
            send_data[lenAdd++] = 0xBB;
        }
        public void TxCmd_1(byte cmd)
        {
            setDefaultLen();
            send_data[0] = 0xFE;
            send_data[1] = 0xFE;
            send_data[lenAdd++] = cmd;
            send_data[lenAdd++] = 0x01;
            send_data[lenAdd++] = 0x01;
            crc16 = CRC_16(send_data,5);
            setUpCrc();
            send_data[lenAdd++] = 0xBB;
        }


        UInt16 crc16_check(byte[] data_arr, byte len)
        {
            byte tmp = 0;
            UInt16 crc_check = 0;
            for (tmp = 2; tmp < len; tmp++)
            {
                crc_check = (UInt16)((crc_check >> 8) | (crc_check << 8));
                crc_check ^= data_arr[tmp];
                crc_check ^= (UInt16)((crc_check & 0xff) >> 4);
                crc_check ^= (UInt16)((crc_check << 8) << 4);
                crc_check ^= (UInt16)(((crc_check & 0xff) << 4) << 1);
            }
            return crc_check;
        }
        public bool recFrameCrcCheck(byte[] tx_arr, byte len)
        {
            byte tmp = 0;
            UInt16 crc_check = 0;

            for (tmp = 2; tmp < len - 3; tmp++)
            {
                crc_check = (UInt16)((crc_check >> 8) | (crc_check << 8));
                crc_check ^= tx_arr[tmp];
                crc_check ^= (UInt16)((crc_check & 0xff) >> 4);
                crc_check ^= (UInt16)((crc_check << 8) << 4);
                crc_check ^= (UInt16)(((crc_check & 0xff) << 4) << 1);
            }

            if (tx_arr[len - 3] == (byte)crc_check
                && tx_arr[len - 2] == (crc_check >> 8))
            {
                return true;

            }
            else
            {
                return false;
            }
        }


        public void TxFlashDataFrame(UInt16 addr, UInt16 seq, byte[] data_arr, byte DataLen)
        {
            byte i;
            UInt16 crc_check = 0;
            send_data[0] = 0xFE;
            send_data[1] = 0xFE;
            send_data[2] = 0x33;
            send_data[3] = DataLen;
            send_data[4] = (byte)(addr >> 8);
            send_data[5] = (byte)addr;

            for (i = 0; i < DataLen - 2; i++)
            {
                send_data[i + 6] = data_arr[i];
            }
            send_data[DataLen + 4] = (byte)(seq >> 8);
            send_data[DataLen + 5] = (byte)seq;
            crc_check = CRC_16(send_data, (byte)(DataLen + 6));
            send_data[DataLen + 6] = (byte)(crc_check >> 8);
            send_data[DataLen + 7] = (byte)crc_check;

            send_data[DataLen + 8] = 0xBB;
            lenAdd = (byte)(DataLen + 9);
        }

        private UInt16 CRC_16(byte[] data_arr, byte len)      //ptr为数据指针，len为数据长度（传输字节数）
        {
            byte i;
            byte j = 0;
            UInt16 CRC_Value = 0;                   //每次校验前都需要清零
            while ((len) != 0)                         //(len--)!=0会先赋值即进行减1，然后再进行比较
            {
                for (i = 0x80; i != 0; i >>= 1)              //for循环中实现的功能为：遇到第一个为1的数据位时，假设后面的数据为0，后面的数据与生成码简式进行异或运算，
                {                                   //将得到的值放入CRC中，当成数据帧第一次与生成码的异或值，如果接下来的数据位也为一，则将CRC左移一位再与0x1021做异或运算
                                                    //如果接下来的数据位为0时，若CRC对应的位为0则只将CRC左移一位，若为1则要与0x1021做异或。
                    if ((CRC_Value & 0x8000) != 0)             //如果CRC里面的某位与对应的数据位同时为1，则要将CRC进行两次异或运算，相当于没有改变CRC的值，只是将它左移一位，
                    {                               //因为如果他们都为1时，则意味数据帧数据位与对应的生成码对应数据位都为1，他们异或的结果是0，那么实际下一次是不用该位再做异或运算的。
                        CRC_Value <<= 1;                    //基本思想就是一位一位的异或。
                        CRC_Value ^= 0x1021;
                    }

                    else CRC_Value <<= 1;

                    if ((data_arr[j] & i) != 0) CRC_Value ^= 0x1021;
                }
                j++;
                len--;
            }
            return (CRC_Value);
        }

    }
}
