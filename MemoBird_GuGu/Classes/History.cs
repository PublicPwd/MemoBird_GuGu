namespace MemoBird_GuGu.Classes
{
    class History
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 打印历史
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <param name="date">打印日期</param>
        /// <param name="content">打印内容</param>
        public History(string id, string date, string content)
        {
            Id = id;
            Date = date;
            Content = content;
        }
    }
}
