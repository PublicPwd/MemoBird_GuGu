using MemoBird_GuGuJi.Classes;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace MemoBird_GuGuJi.Utils
{
    class FileX
    {
        /// <summary>
        /// 创建文件夹目录
        /// </summary>
        public static bool CreateDirectory(string path)
        {
            bool bRe = false;
            if (Directory.Exists(path))
            {
                bRe = true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path);
                    bRe = true;
                }
                catch { }
            }
            return bRe;
        }

        /// <summary>
        /// 保存设备列表至系统“我的文档”中程序默认的配置文件，只在程序关闭时运行一次
        /// </summary>
        public static void SaveDeviceList()
        {
            try
            {
                XDocument xDocument = new XDocument();
                XElement xElementParent = new XElement("Devices");
                xDocument.Add(xElementParent);
                XElement xElementChild = null;
                foreach(string key in DeviceList.Id.Keys)
                {
                    xElementChild = new XElement("Device");
                    xElementChild.SetAttributeValue("Name", key);
                    xElementChild.Value = DeviceList.Id[key];
                    xElementParent.Add(xElementChild);
                }
                xDocument.Save(ProgramInfo.DeviceList);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 读取系统“我的文档”中程序默认的配置文件,只在程序启动时运行一次
        /// </summary>
        public static void LoadDeviceList()
        {
            if (!File.Exists(ProgramInfo.DeviceList))
            {
                CreateDirectory(ProgramInfo.File);
                return;
            }

            try
            {
                XDocument xDocument = XDocument.Load(ProgramInfo.DeviceList);
                var devices = xDocument.Descendants("Device");
                foreach(var device in devices)
                {
                    DeviceList.Id.Add((string)device.Attribute("Name"), device.Value);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 弹出文件选择窗口
        /// </summary>
        /// <param name="multiselect">是否允许多选</param>
        /// <returns>选择的文件列表</returns>
        public static string[] GetFileBrowserSelectedPath(bool multiselect)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = multiselect,
                Filter = "JPEG|*.jpg|PNG|*.png|BMP|*.bmp|ALL|*.*"
            };
            openFileDialog.ShowDialog();
            return openFileDialog.FileNames;
        }

        public static string LoadInfo()
        {
            StreamReader sr = new StreamReader("text.txt");
            string text = sr.ReadLine();
            sr.Close();
            return text;
        }
    }
}
