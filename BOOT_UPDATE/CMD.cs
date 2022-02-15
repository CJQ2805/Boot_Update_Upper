using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOOT_UPDATE
{
    class CMD
    {
        //进入升级指令
        public const byte UPDATE_CMD_APP_INTO_BOOT = 0x35;

        public const byte UPDATE_CMD_COMPEL_READY    = 0x34;
        public const byte UPDATE_CMD_COMPEL_READY_OK = 0x26;
        public const byte UPDATE_CMD_UPDATA_READY    = 0x31;
        public const byte UPDATE_CMD_UPDATA_READY_OK = 0x29;
        public const byte UPDATE_CMD_ERASE           = 0x32;
        public const byte UPDATE_CMD_ERASE_OK        = 0x21;
        public const byte UPDATE_CMD_CRC_OK          = 0x22;
        public const byte UPDATE_CMD_WRITE_ERROR     = 0x42;
        public const byte UPDATE_CMD_EXIT_UPDATE     = 0x37;
        public const byte UPDATE_CMD_EXIT_OK         = 0x41;
        public const byte UPDATE_CMD_FILE_END        = 0x45;
        public const byte UPDATE_CMD_LOAD_OK         = 0x25;

        //error code 
        public const byte UPDATE_CMD_ERROR_FRAME = 0x28;
        public const byte UPDATE_CMD_CRC_ERROR = 0x23;
        public const byte UPDATE_CMD_NON_APP = 0x24;
        public const byte UPDATE_CMD_OVER_TIME = 0x27;

    }
}
