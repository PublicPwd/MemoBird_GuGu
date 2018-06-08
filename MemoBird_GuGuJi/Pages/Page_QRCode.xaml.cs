using MemoBird_GuGu.Classes;
using MemoBird_GuGu.OpenLibrary.ggApi;
using MemoBird_GuGu.OpenLibrary.QRCoder;
using MemoBird_GuGu.Utils;
using MemoBird_GuGu.Utils.WebApi;
using MemoBird_GuGu.Windows;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoBird_GuGu.Pages
{
    public partial class Page_QRCode : Page
    {
        private Bitmap qrCode = null;

        public Page_QRCode()
        {
            InitializeComponent();
            TextBox_Content.Focus();
            ComboBox_DeviceList.ItemsSource = DeviceList.Details;
            TextBox_QRCode.Background = null;
        }

        #region Private Function

        /// <summary>
        /// 打印二维码
        /// </summary>
        private void PrintPaper()
        {
            try
            {
                string content = "P:" + ImageHelper.GetPoitImgBase64(qrCode);
                string memobirdID = ComboBox_DeviceList.SelectedValue.ToString();
                string str = WebApiHelper.PrintPaper(content, memobirdID);
                if (Parsing.GetValueFromJsonString(str, "showapi_res_code") == "1")
                {
                    FileX.SaveHistory(memobirdID, content);
                }
                else
                {
                    MessageBox.Show(FindResource("printfail").ToString());
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
                GC.Collect();
            }
        }

        private void ShowQRCode(string content)
        {
            qrCode = QRCoderHelper.Generate(content);
            IntPtr intPtr = qrCode.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(intPtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageBrush imageBrush = new ImageBrush(bitmapSource);
            TextBox_QRCode.Background = imageBrush;
        }

        #endregion

        #region Event Handlers

        private void TextBox_Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_Content.Text.Length == 0)
            {
                TextBox_QRCode.Background = null;
                return;
            }
            ShowQRCode((ComboBox_Type.SelectedItem as ComboBoxItem).Tag + TextBox_Content.Text);
        }

        private void ComboBox_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextBox_Content == null)
            {
                return;
            }
            TextBox_Content.Text = string.Empty;
            TextBox_QRCode.Background = null;

            if (ComboBox_Type.SelectedIndex == 4)
            {
                Grid_Netword.Visibility = Visibility.Visible;
                TextBox_Content.Visibility = Visibility.Hidden;
                TextBox_QRCode.Margin = new Thickness(25, 175, 175, 55);
            }
            else
            {
                Grid_Netword.Visibility = Visibility.Hidden;
                TextBox_Content.Visibility = Visibility.Visible;
                TextBox_QRCode.Margin = new Thickness(25, 55, 175, 55);
            }
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_DeviceList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseadddevice").ToString());
                return;
            }
            if (TextBox_QRCode.Background == null)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            new Window_Tip(FindResource("printing").ToString()).Show();
            PrintPaper();
        }

        private void NetWorkInfoChanged()
        {
            if (CheckBox_IsHiddenNetwork == null)
            {
                return;
            }
            string isHiddenNetwork = (bool)CheckBox_IsHiddenNetwork.IsChecked ? "1" : string.Empty;
            string text = $"WIFI:S:{TextBox_SSIDName.Text};P:{TextBox_Password.Text};T:{ComboBox_PasswordType.Text};H:{isHiddenNetwork};";
            ShowQRCode(text);
        }

        private void SSIDNameAndPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            NetWorkInfoChanged();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NetWorkInfoChanged();
        }

        private void CheckBox_IsHiddenNetwork_Checked(object sender, RoutedEventArgs e)
        {
            NetWorkInfoChanged();
        }

        #endregion
    }
}