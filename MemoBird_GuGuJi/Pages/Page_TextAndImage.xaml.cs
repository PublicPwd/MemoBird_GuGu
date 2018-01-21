using MemoBird_GuGu.Classes;
using MemoBird_GuGu.OpenLibrary.ggApi;
using MemoBird_GuGu.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGu.Windows
{
    public partial class Page_TextAndImage : Page
    {
        public Page_TextAndImage()
        {
            InitializeComponent();
        }

        #region Public Function

        /// <summary>
        /// 往 DataGrid 中填充设备信息
        /// </summary>
        public void FillContent()
        {
            ComboBox_DeviceList.Items.Clear();

            if (DeviceList.Id.Count == 0)
            {
                return;
            }

            foreach (string name in DeviceList.Id.Keys)
            {
                ComboBox_DeviceList.Items.Add(name);
            }
            ComboBox_DeviceList.SelectedIndex = 0;
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 打印文本与图片
        /// </summary>
        private void PrintPaper()
        {
            string content = string.Empty;
            string memobirdID;
            string str;
            string printcontentid;
            string itemString;
            try
            {
                for (int i = 0; i < ListBox_List.Items.Count; i++)
                {
                    if (i != 0)
                    {
                        content = content + "|";
                    }
                    content = content + (ListBox_List.Items[i] as ListBoxItem).Tag;
                }
                memobirdID = DeviceList.Id[ComboBox_DeviceList.SelectedValue.ToString()];
                str = ggApiHelper.UserBind(memobirdID, "0");
                str = ggApiHelper.PrintPaper(memobirdID, Parsing.GetUserIDFromJsonString(str, "showapi_userid"), content);
                printcontentid = Parsing.GetUserIDFromJsonString(str, "printcontentid");
                while (true)
                {
                    str = ggApiHelper.GetPrintStatus(printcontentid);
                    if (Parsing.GetUserIDFromJsonString(str, "showapi_res_code").Equals("1"))
                    {
                        FileX.SaveHistory(memobirdID, content);
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ListBox_List.Items.Clear();
                content = string.Empty;
                memobirdID = string.Empty;
                str = string.Empty;
                printcontentid = string.Empty;
                itemString = string.Empty;
                GC.Collect();
            }
        }

        #endregion

        #region Event Handlers

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_DeviceList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseadddevice").ToString());
                return;
            }
            if (ListBox_List.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            PrintPaper();
        }

        private void Button_AddText_Click(object sender, RoutedEventArgs e)
        {
            Window_AddText window_AddText = new Window_AddText();
            window_AddText.ShowDialog();

            if (window_AddText.Text.Length == 0)
            {
                return;
            }

            ListBoxItem listBoxItem = new ListBoxItem()
            {
                Content = window_AddText.Text,
                Tag = "T:" + Convert.ToBase64String(Encoding.Default.GetBytes(window_AddText.Text))
            };
            ListBox_List.Items.Add(listBoxItem);
        }

        private void Button_AddImage_Click(object sender, RoutedEventArgs e)
        {
            string[] fileNames = FileX.GetFileBrowserSelectedPath(false);
            if (fileNames.Length == 0)
            {
                return;
            }
            var image = System.Drawing.Image.FromFile(fileNames[0]);
            string base64 = ImageHelper.GetPoitImgBase64(image);
            Image img = new Image()
            {
                Source = FileX.ImageFromBase64String(base64)
            };
            ListBoxItem listBoxItem = new ListBoxItem()
            {
                Content = img,
                Tag = "P:" + base64
            };
            ListBox_List.Items.Add(listBoxItem);
        }

        private void Button_ShiftUp_Click(object sender, RoutedEventArgs e)
        {
            int index = ListBox_List.SelectedIndex;
            if (index > 0)
            {
                List<object> list = new List<object>();
                for (int i = 0; i < ListBox_List.Items.Count; i++)
                {
                    if (i == index - 1)
                    {
                        list.Add(ListBox_List.Items[index]);
                        list.Add(ListBox_List.Items[index - 1]);
                        i++;
                        continue;
                    }
                    list.Add(ListBox_List.Items[i]);
                }
                ListBox_List.Items.Clear();
                foreach (object obj in list)
                {
                    ListBox_List.Items.Add(obj);
                }
            }
        }

        private void Button_ShiftDown_Click(object sender, RoutedEventArgs e)
        {
            int index = ListBox_List.SelectedIndex;
            if (index < ListBox_List.Items.Count - 1)
            {
                List<object> list = new List<object>();
                for(int i=0;i<ListBox_List.Items.Count;i++)
                {
                    if(i==index)
                    {
                        list.Add(ListBox_List.Items[index + 1]);
                        list.Add(ListBox_List.Items[index]);
                        i++;
                        continue;
                    }
                    list.Add(ListBox_List.Items[i]);
                }
                ListBox_List.Items.Clear();
                foreach(object obj in list)
                {
                    ListBox_List.Items.Add(obj);
                }
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            ListBox_List.Items.Remove(ListBox_List.SelectedItem);
        }

        #endregion
    }
}