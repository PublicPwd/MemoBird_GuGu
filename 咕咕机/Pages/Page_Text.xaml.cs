﻿using MemoBird.APIs;
using MemoBird.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoBird.Pages
{
    /// <summary>
    /// Page_Text.xaml 的交互逻辑
    /// </summary>
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

        private string getUserIDFromJsonString(string str, string field)
        {
            int index = str.IndexOf(field);
            string value = str.Substring(index, str.Length - index).Replace(field + "\":", string.Empty);
            if (value[0].Equals('\"'))
            {
                value = value.Substring(1, value.Length - 1);
            }
            char[] parms = { ',', '\"', '}' };
            value = value.Substring(0, value.IndexOfAny(parms));
            return value;
        }

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
                    str = ggApiHelper.PrintPaper(memobirdID, this.getUserIDFromJsonString(str, "showapi_userid"), content);
                    printcontentid = this.getUserIDFromJsonString(str, "printcontentid");
                    while (true)
                    {
                        str = ggApiHelper.GetPrintStatus(printcontentid);
                        if (this.getUserIDFromJsonString(str, "showapi_res_code").Equals("1"))
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
                MessageBox.Show(FindResource("pleaseinputcontent").ToString());
                return;
            }
            Thread _thread = new Thread(new ThreadStart(PrintPaper));
            _thread.Start();
        }

        #endregion
    }
}
