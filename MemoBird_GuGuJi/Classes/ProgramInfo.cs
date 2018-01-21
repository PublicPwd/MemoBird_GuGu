using System;

namespace MemoBird_GuGu.Classes
{
    class ProgramInfo
    {
        /// <summary>
        /// 程序配置文件目录
        /// </summary>
        public static readonly string File = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MemoBird_GuGu";

        /// <summary>
        /// 程序配置文件地址
        /// </summary>
        public static readonly string DeviceList = File + @"\DeviceList";

        /// <summary>
        /// 打印记录
        /// </summary>
        public static readonly string History = File + @"\History";
    }
}
