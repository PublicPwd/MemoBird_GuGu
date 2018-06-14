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
        
        public const string UpdateUrl = "https://api.github.com/repos/PublicPwd/MemoBird_GuGuJi/releases/latest";

        public const string UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36";
    }
}
