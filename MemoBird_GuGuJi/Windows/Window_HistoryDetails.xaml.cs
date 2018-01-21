using MemoBird_GuGu.Utils;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGu.Windows
{
    public partial class Window_HistoryDetails : Window
    {
        public Window_HistoryDetails(string content)
        {
            InitializeComponent();
            ShowContent(content);
        }

        /// <summary>
        /// 显示打印的内容
        /// </summary>
        /// <param name="content">打印的内容</param>
        private void ShowContent(string content)
        {
            string[] contents = content.Split('|');
            ListBoxItem listBoxItem = null;
            foreach(string c in contents)
            {
                string type = c.Substring(0, 2);
                if (type == "T:")
                {
                    byte[] buffer = Convert.FromBase64String(c.Substring(2, c.Length - 2));
                    listBoxItem = new ListBoxItem
                    {
                        Content = Encoding.Default.GetString(buffer)
                    };
                    ListBox_Content.Items.Add(listBoxItem);
                }
                else
                {
                    Image img = new Image
                    {
                        Source = FileX.ImageFromBase64String(c.Substring(2, c.Length - 2))
                    };
                    ListBox_Content.Items.Add(img);
                }
            }
            contents = null;
            GC.Collect();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Close();
        }
    }
}
