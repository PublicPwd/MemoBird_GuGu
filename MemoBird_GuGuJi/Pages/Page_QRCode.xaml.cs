using MemoBird_GuGuJi.Classes;
using MemoBird_GuGuJi.OpenLibrary.ggApi;
using MemoBird_GuGuJi.OpenLibrary.QRCoder;
using MemoBird_GuGuJi.Utils;
using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoBird_GuGuJi.Pages
{
    public partial class Page_QRCode : Page
    {
        private Bitmap qrCode = null;

        public Page_QRCode()
        {
            InitializeComponent();
        }

        #region Private Function

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

        /// <summary>
        /// 生成二维码
        /// </summary>
        private void GenerateQRCode()
        {
            this.qrCode = QRCoderHelper.Generate(this.textBox_Content.Tag + this.textBox_Content.Text);
            IntPtr intPtr = this.qrCode.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(intPtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.textBox_QRCode.Background = new ImageBrush(bitmapSource);
        }

        /// <summary>
        /// 打印二维码
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
                this.comboBox_Type.IsEnabled = false;
                try
                {
                    content = "P:" + OpenLibrary.ggApi.ImageHelper.GetPoitImgBase64(this.qrCode);
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
                    this.comboBox_Type.IsEnabled = true;
                    this.textBox_Content.Text = string.Empty;
                    this.textBox_QRCode.Background = null;

                    content = string.Empty;
                    memobirdID = string.Empty;
                    str = string.Empty;
                    printcontentid = string.Empty;
                }
            }));
        }

        #endregion

        #region Event Handlers

        private void textBox_Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.GenerateQRCode();
        }

        private void comboBox_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.textBox_Content != null)
            {
                this.textBox_Content.Text = string.Empty;
                this.textBox_QRCode.Background = null;
            }
        }

        private void button_Send_Click(object sender, RoutedEventArgs e)
        {
            if(this.comboBox_DeviceList.Items.Count==0)
            {
                MessageBox.Show(FindResource("pleaseadddevice").ToString());
                return;
            }
            if(this.textBox_Content.Text.Length==0)
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
