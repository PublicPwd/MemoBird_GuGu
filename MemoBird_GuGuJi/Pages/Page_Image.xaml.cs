using MemoBird_GuGuJi.Classes;
using MemoBird_GuGuJi.OpenLibrary.ggApi;
using MemoBird_GuGuJi.Utils;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGuJi.Pages
{
    public partial class Page_Image : Page
    {
        public Page_Image()
        {
            InitializeComponent();
        }

        #region Public Function

        /// <summary>
        /// 往 ComboBox 中填充设备列表
        /// </summary>
        public void FillContnet()
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
        /// 打印图片
        /// </summary>
        private void PrintPaper()
        {
            string content = string.Empty;
            string memobirdID;
            string str;
            string printcontentid;
            System.Drawing.Image image = null;
            try
            {
                for (int i = 0; i < ListBox_ImageList.Items.Count; i++)
                {
                    if (i != 0)
                    {
                        content = content + "|";
                    }
                    image = System.Drawing.Image.FromFile(ListBox_ImageList.Items[i].ToString());
                    content = content + "P:" + OpenLibrary.ggApi.ImageHelper.GetPoitImgBase64(image);
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
                ListBox_ImageList.Items.Clear();

                content = string.Empty;
                memobirdID = string.Empty;
                str = string.Empty;
                printcontentid = string.Empty;
                image = null;
                GC.Collect();
            }
        }

        #endregion

        #region Event Handlers

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            string[] fileNames = FileX.GetFileBrowserSelectedPath(true);
            foreach (string fileName in fileNames)
            {
                ListBox_ImageList.Items.Add(fileName);
            }
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_DeviceList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseadddevice").ToString());
                return;
            }
            if (ListBox_ImageList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            PrintPaper();
        }

        private void Button_ShiftUp_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_ImageList.SelectedIndex > 0)
            {
                object up = ListBox_ImageList.SelectedItem;
                object down = ListBox_ImageList.Items[ListBox_ImageList.SelectedIndex - 1];
                ListBox_ImageList.Items[ListBox_ImageList.SelectedIndex - 1] = up;
                ListBox_ImageList.Items[ListBox_ImageList.SelectedIndex] = down;
                ListBox_ImageList.SelectedItem = up;
            }
        }

        private void Button_ShiftDown_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_ImageList.SelectedIndex < ListBox_ImageList.Items.Count - 1)
            {
                object down = ListBox_ImageList.SelectedItem;
                object up = ListBox_ImageList.Items[ListBox_ImageList.SelectedIndex + 1];
                ListBox_ImageList.Items[ListBox_ImageList.SelectedIndex + 1] = down;
                ListBox_ImageList.Items[ListBox_ImageList.SelectedIndex] = up;
                ListBox_ImageList.SelectedItem = down;
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            ListBox_ImageList.Items.Remove(ListBox_ImageList.SelectedItem);
        }

        #endregion
    }
}
