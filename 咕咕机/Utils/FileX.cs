using MemoBird.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace MemoBird.Utils
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
            if (DeviceList.id.Count == 0)
            {
                return;
            }
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
    }
}
