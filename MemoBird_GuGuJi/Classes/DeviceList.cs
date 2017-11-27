using System.Collections.Generic;

namespace MemoBird_GuGuJi.Classes
{
    class DeviceList
    {
        private static Dictionary<string, string> id = new Dictionary<string, string>();
        private static bool deviceListChanged = true;

        /// <summary>
        /// 设备 ID 及对应的名称
        /// </summary>
        public static Dictionary<string, string> Id { get => id; set => id = value; }

        /// <summary>
        /// 标记设备列表中的内容是否发生改变
        /// </summary>
        public static bool DeviceListChanged { get => deviceListChanged; set => deviceListChanged = value; }
    }
}
