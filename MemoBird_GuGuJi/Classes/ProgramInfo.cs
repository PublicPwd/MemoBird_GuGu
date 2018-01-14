using System;

namespace MemoBird_GuGuJi.Classes
{
    class ProgramInfo
    {
        /// <summary>
        /// 程序配置文件目录
        /// </summary>
        public static readonly string File = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MemoBird_GuGuJi";

        /// <summary>
        /// 程序配置文件地址
        /// </summary>
        public static readonly string DeviceList = File + @"\DeviceList.xml";

        /// <summary>
        /// 打印记录
        /// </summary>
        public static readonly string History = File + @"\History";
    }
}
