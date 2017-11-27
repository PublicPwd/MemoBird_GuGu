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

        /// <summary>
        /// 生成二维码
        /// </summary>
        private void GenerateQRCode()
        {
            qrCode = QRCoderHelper.Generate(TextBox_Content.Tag + TextBox_Content.Text);
            IntPtr intPtr = qrCode.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(intPtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            TextBox_QRCode.Background = new ImageBrush(bitmapSource);
        }

        /// <summary>
        /// 打印二维码
        /// </summary>
        private void PrintPaper()
        {
            string content;
            string memobirdID;
            string str;
            string printcontentid;
            try
            {
                content = "P:" + ImageHelper.GetPoitImgBase64(qrCode);
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
                TextBox_Content.Text = string.Empty;
                TextBox_QRCode.Background = null;
                content = string.Empty;
                memobirdID = string.Empty;
                str = string.Empty;
                printcontentid = string.Empty;
                GC.Collect();
            }
        }

        #endregion

        #region Event Handlers

        private void TextBox_Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.GenerateQRCode();
        }

        private void ComboBox_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextBox_Content != null)
            {
                TextBox_Content.Text = string.Empty;
                TextBox_QRCode.Background = null;
            }
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_DeviceList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseadddevice").ToString());
                return;
            }
            if (TextBox_Content.Text.Length == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            PrintPaper();
        }

        #endregion
    }
}