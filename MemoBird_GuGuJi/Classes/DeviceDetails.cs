namespace MemoBird_GuGu.Classes
{
    public class DeviceDetails
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置设备信息
        /// </summary>
        /// <param name="name">设备名称</param>
        /// <param name="id">设备编号</param>
        public DeviceDetails(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
