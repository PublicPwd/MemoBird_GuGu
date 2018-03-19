using MemoBird_GuGu.Utils;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace MemoBird_GuGu.Classes
{
    class DeviceList
    {
        /// <summary>
        /// 设备列表
        /// </summary>
        public static ObservableCollection<DeviceDetails> Details = new ObservableCollection<DeviceDetails>();

        /// <summary>
        /// 读取系统“AppData”中程序默认的配置文件,只在程序启动时运行一次
        /// </summary>
        public static void Load()
        {
            if (!File.Exists(ProgramInfo.DeviceList))
            {
                FileX.CreateDirectory(ProgramInfo.File);
                return;
            }

            try
            {
                XDocument xDocument = XDocument.Load(ProgramInfo.DeviceList);
                xDocument.Descendants("Device").ToList().ForEach(device =>
                {
                    Details.Add(new DeviceDetails((string)device.Attribute("Name"), (string)device.Attribute("Value")));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 保存设备列表至系统“我的文档”中程序默认的配置文件
        /// </summary>
        public static void Save()
        {
            try
            {
                XElement xElementParent = new XElement("Devices");
                Details.ToList().ForEach(device =>
                {
                    XElement xElementChild = new XElement("Device",
                        new XAttribute("Name", device.Name),
                        new XAttribute("Value", device.Id)
                        );
                    xElementParent.Add(xElementChild);
                });
                xElementParent.Save(ProgramInfo.DeviceList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
