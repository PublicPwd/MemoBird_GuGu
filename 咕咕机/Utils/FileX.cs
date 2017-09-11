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
            xmlNode = xmlDocument.CreateElement("Device");
            xmlDocument.AppendChild(xmlNode);
            XmlNode childNode = null;
            foreach (String key in DeviceList.id.Keys)
            {
                childNode = xmlDocument.CreateElement(key);
                childNode.InnerText = DeviceList.id[key];
                xmlNode.AppendChild(childNode);
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
                childNode = null;
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
            for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
            {
                DeviceList.id.Add(xmlNode.ChildNodes[i].Name, xmlNode.ChildNodes[i].InnerText);
            }

            xmlDocument = null;
            xmlNode = null;
        }
    }
}
