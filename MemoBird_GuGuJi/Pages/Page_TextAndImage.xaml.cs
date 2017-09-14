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
        /// 打印文本与图片
        /// </summary>
        private void PrintPaper()
        {
            this.button_Send.Dispatcher.Invoke(new Action(delegate
            {
                string content = string.Empty;
                string memobirdID;
                string str;
                string printcontentid;
                string itemString;
                char c;
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
                    for (int i = 0; i < this.listBox_List.Items.Count; i++)
                    {
                        itemString = this.listBox_List.Items[i].ToString();
                        c = itemString[0];
                        itemString = itemString.Substring(2, itemString.Length - 2);
                        if (i != 0)
                        {
                            content = content + "|";
                        }
                        if (c == 'P')
                        {
                            image = System.Drawing.Image.FromFile(itemString);
                            content = content + "P:" + OpenLibrary.ggApi.ImageHelper.GetPoitImgBase64(image);
                        }
                        else
                        {
                            content = content + "T:" + Convert.ToBase64String(Encoding.Default.GetBytes(itemString));
                        }
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
                    this.listBox_List.Items.Clear();

                    content = string.Empty;
                    memobirdID = string.Empty;
                    str = string.Empty;
                    printcontentid = string.Empty;
                    itemString = string.Empty;
                    image = null;
                }
            }));
        }

        #endregion

        #region Event Handlers

        private void button_Send_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.listBox_List.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            Thread _thread = new Thread(new ThreadStart(PrintPaper));
            _thread.Start();
        }

        private void button_AddText_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new Window_AddText().ShowDialog();

            if (AddedContent.text.Length == 0)
            {
                return;
            }

            this.listBox_List.Items.Add("T:" + AddedContent.text);

            AddedContent.text = string.Empty;
        }

        private void button_AddImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] fileNames = FileX.GetFileBrowserSelectedPath(true);
            foreach (string fileName in fileNames)
            {
                this.listBox_List.Items.Add("P:" + fileName);
            }
        }

        private void button_ShiftUp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.listBox_List.SelectedIndex > 0)
            {
                object up = this.listBox_List.SelectedItem;
                object down = this.listBox_List.Items[this.listBox_List.SelectedIndex - 1];
                this.listBox_List.Items[this.listBox_List.SelectedIndex - 1] = up;
                this.listBox_List.Items[this.listBox_List.SelectedIndex] = down;
                this.listBox_List.SelectedItem = up;
            }
        }

        private void button_ShiftDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.listBox_List.SelectedIndex < this.listBox_List.Items.Count - 1)
            {
                object down = this.listBox_List.SelectedItem;
                object up = this.listBox_List.Items[this.listBox_List.SelectedIndex + 1];
                this.listBox_List.Items[this.listBox_List.SelectedIndex + 1] = down;
                this.listBox_List.Items[this.listBox_List.SelectedIndex] = up;
                this.listBox_List.SelectedItem = down;
            }
        }

        private void button_Remove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.listBox_List.Items.Remove(this.listBox_List.SelectedItem);
        }

        #endregion
    }
}
