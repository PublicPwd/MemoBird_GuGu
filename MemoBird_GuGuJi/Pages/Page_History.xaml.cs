using MemoBird_GuGu.Classes;
using MemoBird_GuGu.Windows;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MemoBird_GuGu.Pages
{
    public partial class Page_History : Page
    {
        ObservableCollection<History> histories = new ObservableCollection<History>();

        public Page_History()
        {
            InitializeComponent();
            DatePicker_Start.Text = DateTime.Now.ToString();
            DatePicker_End.Text = DateTime.Now.ToString();
            DataGrid_List.ItemsSource = histories;
        }

        /// <summary>
        /// 读取该时间段内的 XML 历史记录文件，并将里面的信息显示到 DataGrid 中
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        private void FetchXmlList(string startDate, string endDate)
        {
            if (!Directory.Exists(ProgramInfo.History))
            {
                return;
            }
            histories.Clear();
            int start = int.Parse(startDate);
            int end = int.Parse(endDate);
            int tryParseOut = 0;
            foreach (string memobird in Directory.GetDirectories(ProgramInfo.History))
            {
                string[] files = Directory.GetFiles(memobird);
                var xmlNames = from xmlName in Directory.GetFiles(memobird)
                               where int.TryParse(Path.GetFileName(xmlName), out tryParseOut) && int.Parse(Path.GetFileName(xmlName)) >= start && int.Parse(Path.GetFileName(xmlName)) <= end
                               select Path.GetFileName(xmlName);
                foreach (var xmlName in xmlNames)
                {
                    string memobirdId = Path.GetFileName(memobird);
                    XDocument xDocument = XDocument.Load(memobird + "\\" + xmlName);
                    var xElements = xDocument.Descendants("History");
                    foreach (XElement xElement in xElements)
                    {
                        string date = (string)xElement.Attribute("Date");
                        string value = (string)xElement.Attribute("Value");
                        histories.Add(new History(memobirdId, date, value));
                    }
                }
            }
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            string startDate = DatePicker_Start.Text;
            string endDate = DatePicker_End.Text;
            if (startDate.Length * endDate.Length == 0)
            {
                return;
            }
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            FetchXmlList(start.ToString("yyyyMMdd"), end.ToString("yyyyMMdd"));
        }

        private void DataGrid_List_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = DataGrid_List.SelectedItem as History;
            if (item == null)
            {
                return;
            }
            new Window_HistoryDetails(item.Content).Show();
        }

        private void Button_Details_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_List_MouseDoubleClick(null, null);
        }
    }
}
