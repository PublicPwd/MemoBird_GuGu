using MemoBird_GuGuJi.Classes;
using MemoBird_GuGuJi.OpenLibrary.ggApi;
using MemoBird_GuGuJi.Utils;
using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGuJi.Pages
{
    public partial class Page_Text : Page
    {
        public Page_Text()
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
        /// 打印文字
        /// </summary>
        private void PrintPaper()
        {
            this.button_Send.Dispatcher.Invoke(new Action(delegate
            {
                string content;
                string memobirdID;
                string str;
                string printcontentid;
                this.button_Send.IsEnabled = false;
                try
                {
                    content = "T:" + Convert.ToBase64String(Encoding.Default.GetBytes(this.textBox_Content.Text));
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
                    this.button_Send.IsEnabled = true;
                    this.textBox_Content.Text = string.Empty;

                    content = string.Empty;
                    memobirdID = string.Empty;
                    str = string.Empty;
                    printcontentid = string.Empty;
                }
            }));
        }

        #endregion

        #region Event Handlers

        private void button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBox_Content.Text.Length == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            Thread _thread = new Thread(new ThreadStart(PrintPaper));
            _thread.Start();
        }

        #endregion
    }
}
