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
            this.comboBox_DeviceList.Items.Clear();

            if (DeviceList.id.Count == 0)
            {
                return;
            }

            foreach (string name in DeviceList.id.Keys)
            {
                this.comboBox_DeviceList.Items.Add(name);
            }
            this.comboBox_DeviceList.SelectedIndex = 0;
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 打印图片
        /// </summary>
        private void PrintPaper()
        {
            this.button_Send.Dispatcher.Invoke(new Action(delegate
            {
                string content = string.Empty;
                string memobirdID;
                string str;
                string printcontentid;
                System.Drawing.Image image = null;
                foreach (object obj in this.grid.Children)
                {
                    if (obj is Button)
                    {
                        (obj as Button).IsEnabled = false;
                    }
                }
                try
                {
                    for (int i = 0; i < this.listBox_ImageList.Items.Count; i++)
                    {
                        if (i != 0)
                        {
                            content = content + "|";
                        }
                        image = System.Drawing.Image.FromFile(this.listBox_ImageList.Items[i].ToString());
                        content = content + "P:" + OpenLibrary.ggApi.ImageHelper.GetPoitImgBase64(image);
                    }
                    memobirdID = DeviceList.id[this.comboBox_DeviceList.SelectedValue.ToString()];
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
                    foreach (object obj in this.grid.Children)
                    {
                        if (obj is Button)
                        {
                            (obj as Button).IsEnabled = true;
                        }
                    }
                    this.listBox_ImageList.Items.Clear();

                    content = string.Empty;
                    memobirdID = string.Empty;
                    str = string.Empty;
                    printcontentid = string.Empty;
                    image = null;
                }
            }));
        }

        #endregion

        #region Event Handlers

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            string[] fileNames = FileX.GetFileBrowserSelectedPath(true);
            foreach (string fileName in fileNames)
            {
                this.listBox_ImageList.Items.Add(fileName);
            }
        }

        private void button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox_ImageList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            Thread _thread = new Thread(new ThreadStart(PrintPaper));
            _thread.Start();
        }

        private void button_ShiftUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox_ImageList.SelectedIndex > 0)
            {
                object up = this.listBox_ImageList.SelectedItem;
                object down = this.listBox_ImageList.Items[this.listBox_ImageList.SelectedIndex - 1];
                this.listBox_ImageList.Items[this.listBox_ImageList.SelectedIndex - 1] = up;
                this.listBox_ImageList.Items[this.listBox_ImageList.SelectedIndex] = down;
                this.listBox_ImageList.SelectedItem = up;
            }
        }

        private void button_ShiftDown_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox_ImageList.SelectedIndex < this.listBox_ImageList.Items.Count - 1)
            {
                object down = this.listBox_ImageList.SelectedItem;
                object up = this.listBox_ImageList.Items[this.listBox_ImageList.SelectedIndex + 1];
                this.listBox_ImageList.Items[this.listBox_ImageList.SelectedIndex + 1] = down;
                this.listBox_ImageList.Items[this.listBox_ImageList.SelectedIndex] = up;
                this.listBox_ImageList.SelectedItem = down;
            }
        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            this.listBox_ImageList.Items.Remove(this.listBox_ImageList.SelectedItem);
        }

        #endregion
    }
}
