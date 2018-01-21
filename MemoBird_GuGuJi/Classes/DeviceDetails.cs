namespace MemoBird_GuGu.Classes
{
    class DeviceDetails
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public static string Name { get; private set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public static string Id { get; private set; }

        /// <summary>
        /// 设置设备信息
        /// </summary>
        /// <param name="name">设备名称</param>
        /// <param name="id">设备编号</param>
        public static void SetDeviceDetails(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
