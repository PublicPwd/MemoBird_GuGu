using MemoBird_GuGuJi.Classes;
using MemoBird_GuGuJi.OpenLibrary.ggApi;
using MemoBird_GuGuJi.Utils;
using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGuJi.Windows
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
            char c;
            System.Drawing.Image image = null;
            try
            {
                for (int i = 0; i < ListBox_List.Items.Count; i++)
                {
                    itemString = ListBox_List.Items[i].ToString();
                    c = itemString[0];
                    itemString = itemString.Substring(2, itemString.Length - 2);
                    if (i != 0)
                    {
                        content = content + "|";
                    }
                    if (c == 'P')
                    {
                        image = System.Drawing.Image.FromFile(itemString);
                        content = content + "P:" + ImageHelper.GetPoitImgBase64(image);
                    }
                    else
                    {
                        content = content + "T:" + Convert.ToBase64String(Encoding.Default.GetBytes(itemString + "\n"));
                    }
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
                ListBox_List.Items.Clear();
                content = string.Empty;
                memobirdID = string.Empty;
                str = string.Empty;
                printcontentid = string.Empty;
                itemString = string.Empty;
                image = null;
                GC.Collect();
            }
        }

        #endregion

        #region Event Handlers

        private void Button_Send_Click(object sender, System.Windows.RoutedEventArgs e)
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

        private void Button_AddText_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new Window_AddText().ShowDialog();

            if (AddedContent.Text.Length == 0)
            {
                return;
            }

            ListBox_List.Items.Add("T:" + AddedContent.Text);

            AddedContent.Text = string.Empty;
        }

        private void Button_AddImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] fileNames = FileX.GetFileBrowserSelectedPath(true);
            foreach (string fileName in fileNames)
            {
                ListBox_List.Items.Add("P:" + fileName);
            }
        }

        private void Button_ShiftUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ListBox_List.SelectedIndex > 0)
            {
                object up = ListBox_List.SelectedItem;
                object down = ListBox_List.Items[ListBox_List.SelectedIndex - 1];
                ListBox_List.Items[ListBox_List.SelectedIndex - 1] = up;
                ListBox_List.Items[ListBox_List.SelectedIndex] = down;
                ListBox_List.SelectedItem = up;
            }
        }

        private void Button_ShiftDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ListBox_List.SelectedIndex < ListBox_List.Items.Count - 1)
            {
                object down = ListBox_List.SelectedItem;
                object up = ListBox_List.Items[ListBox_List.SelectedIndex + 1];
                ListBox_List.Items[ListBox_List.SelectedIndex + 1] = down;
                ListBox_List.Items[ListBox_List.SelectedIndex] = up;
                ListBox_List.SelectedItem = down;
            }
        }

        private void Button_Remove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBox_List.Items.Remove(ListBox_List.SelectedItem);
        }

        #endregion
    }
}