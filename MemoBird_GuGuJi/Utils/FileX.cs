using MemoBird_GuGuJi.Classes;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Xml;

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
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNode = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.AppendChild(xmlNode);
            xmlNode = xmlDocument.CreateElement("Devices");
            xmlDocument.AppendChild(xmlNode);
            XmlElement childElement = null;
            foreach (String key in DeviceList.id.Keys)
            {
                childElement = xmlDocument.CreateElement("Device");
                childElement.SetAttribute("Name", key);
                childElement.InnerText = DeviceList.id[key];
                xmlNode.AppendChild(childElement);
            }
            try
            {
                xmlDocument.Save(ProgramInfo.deviceList);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                xmlDocument = null;
                xmlNode = null;
                childElement = null;
            }
        }

        /// <summary>
        /// 读取系统“我的文档”中程序默认的配置文件,只在程序启动时运行一次
        /// </summary>
        public static void LoadDeviceList()
        {
            if (!File.Exists(ProgramInfo.deviceList))
            {
                CreateDirectory(ProgramInfo.file);
                return;
            }
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(ProgramInfo.deviceList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            XmlNode xmlNode = xmlDocument.LastChild;
            XmlElement xmlElement = null;
            for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
            {
                xmlElement = xmlNode.ChildNodes[i] as XmlElement;
                DeviceList.id.Add(xmlElement.GetAttribute("Name"), xmlElement.InnerText);
            }

            xmlDocument = null;
            xmlNode = null;
            xmlElement = null;
        }

        /// <summary>
        /// 弹出文件选择窗口
        /// </summary>
        /// <returns>选择的文件列表</returns>
        public static string[] GetFileBrowserSelectedPath(bool multiselect)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = multiselect;
            openFileDialog.Filter = "JPEG|*.jpg|PNG|*.png|BMP|*.bmp|ALL|*.*";
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
