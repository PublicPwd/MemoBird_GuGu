using System.Collections.Generic;

namespace MemoBird_GuGuJi.Classes
{
    class DeviceList
    {
        public static Dictionary<string, string> id = new Dictionary<string, string>();

        /// <summary>
        /// 设备列表中的内容是否发生改变
        /// </summary>
        public static bool deviceListChanged = true;
    }
}
