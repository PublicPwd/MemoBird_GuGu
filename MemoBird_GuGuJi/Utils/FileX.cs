using MemoBird_GuGu.Classes;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace MemoBird_GuGu.Utils
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

        /// <summary>
        /// 添加打印历史记录
        /// </summary>
        /// <param name="memobirdID">设备ID</param>
        /// <param name="content">打印内容</param>
        public static void SaveHistory(string memobirdID, string content)
        {
            string directory = ProgramInfo.History + "\\" + memobirdID;
            CreateDirectory(directory);
            DateTime dateTime = DateTime.Now;
            string filePath = directory + "\\" + dateTime.ToString("yyyyMMdd");
            XDocument xDocument = null;
            if (!File.Exists(filePath))
            {
                xDocument = new XDocument(new XElement("Histories"));
            }
            else
            {
                xDocument = XDocument.Load(filePath);
            }
            XElement xElementParent = xDocument.LastNode as XElement;
            XElement xElement = new XElement("History",
                new XAttribute("Date", dateTime.ToString()),
                new XAttribute("Value", content)
                );
            xElementParent.Add(xElement);
            xDocument.Save(filePath);
        }

        /// <summary>
        /// 将 Base64 编码字符串转换成图片
        /// </summary>
        /// <param name="content">Base64 编码</param>
        /// <returns>图片</returns>
        public static BitmapImage ImageFromBase64String(string content)
        {
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(content));
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public static string LoadConfig(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string text = sr.ReadLine();
                sr.Close();
                return text;
            }
        }
    }
}
