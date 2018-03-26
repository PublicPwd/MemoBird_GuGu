using MemoBird_GuGu.Classes;
using MemoBird_GuGu.OpenLibrary.ggApi;
using MemoBird_GuGu.Utils;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MemoBird_GuGu.Pages
{
    public partial class Page_Image : Page
    {
        public Page_Image()
        {
            InitializeComponent();
            ComboBox_DeviceList.ItemsSource = DeviceList.Details;
        }

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
            try
            {
                content = content + "P:" + Image_Content.Tag;
                memobirdID = ComboBox_DeviceList.SelectedValue.ToString();
                str = ggApiHelper.UserBind(memobirdID, "0");
                str = ggApiHelper.PrintPaper(memobirdID, Parsing.GetUserIDFromJsonString(str, "showapi_userid"), content);
                if (Parsing.GetUserIDFromJsonString(str, "showapi_res_code") == "1")
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
                Image_Content.Source = null;
                Image_Content.Tag = string.Empty;
                content = string.Empty;
                memobirdID = string.Empty;
                str = string.Empty;
                printcontentid = string.Empty;
                GC.Collect();
            }
        }

        #endregion

        #region Event Handlers

        private void Button_Image_Click(object sender, RoutedEventArgs e)
        {
            string[] fileNames = FileX.GetFileBrowserSelectedPath(false);
            if (fileNames.Length == 0)
            {
                return;
            }
            var image = System.Drawing.Image.FromFile(fileNames[0]);
            string base64 = ImageHelper.GetPoitImgBase64(image);
            Image_Content.Tag = base64;
            Image_Content.Source = FileX.ImageFromBase64String(base64);
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_DeviceList.Items.Count == 0)
            {
                MessageBox.Show(FindResource("pleaseadddevice").ToString());
                return;
            }
            if (Image_Content.Tag.ToString().Length == 0)
            {
                MessageBox.Show(FindResource("pleaseaddcontent").ToString());
                return;
            }
            PrintPaper();
        }

        #endregion
    }
}