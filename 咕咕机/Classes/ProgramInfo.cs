using System;

namespace MemoBird.Classes
{
    class ProgramInfo
    {
        /// <summary>
        /// 程序配置文件目录
        /// </summary>
        public static string file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MemoBird_GuGuJi";

        /// <summary>
        /// 程序配置文件地址
        /// </summary>
        public static string deviceList = file + @"\DeviceList.xml";
    }
}
