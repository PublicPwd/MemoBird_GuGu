using MemoBird_GuGuJi.Classes;
using MemoBird_GuGuJi.OpenLibrary.ggApi;
using MemoBird_GuGuJi.OpenLibrary.QRCoder;
using MemoBird_GuGuJi.Utils;
using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoBird_GuGuJi.Pages
{
    public partial class Page_Text : Page
    {
        private string contentText = string.Empty;
        private Bitmap contnetBitmap = null;

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
        /// 打印文字或二维码
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
                    if (this.textBox_Content.IsEnabled)
                    {
                        content = "T:" + Convert.ToBase64String(Encoding.Default.GetBytes(this.textBox_Content.Text));
                    }
                    else
                    {
                        content = "P:" + ImageHelper.GetPoitImgBase64(this.contnetBitmap);
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
                    this.button_Send.IsEnabled = true;
                    this.textBox_Content.Text = string.Empty;

                    content = string.Empty;
                    memobirdID = string.Empty;
                    str = string.Empty;
                    printcontentid = string.Empty;
                }
            }));
        }

        /// <summary>
        /// 切换文字与二维码模式
        /// </summary>
        private void ChangeControl()
        {
            if (this.textBox_Content.IsEnabled)
            {
                this.button_QRCode.Content = FindResource("text");
                this.contentText = this.textBox_Content.Text;
                this.textBox_Content.Text = string.Empty;
                this.contnetBitmap = QRCoderHelper.Generate(this.contentText);
                IntPtr intPtr = contnetBitmap.GetHbitmap();
                BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(intPtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                this.textBox_Content.Background = new ImageBrush(bitmapSource);
            }
            else
            {
                this.button_QRCode.Content = FindResource("qrcode");
                this.textBox_Content.Text = this.contentText;
                this.textBox_Content.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            }
            this.textBox_Content.IsEnabled = !this.textBox_Content.IsEnabled;
        }

        #endregion

        #region Event Handlers

        private void button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (this.comboBox_DeviceList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseadddevice").ToString());
                return;
            }
            if (this.textBox_Content.Text.Length + this.contentText.Length == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            Thread _thread = new Thread(new ThreadStart(PrintPaper));
            _thread.Start();
        }

        private void button_QRCode_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeControl();
        }
        #endregion
    }
}