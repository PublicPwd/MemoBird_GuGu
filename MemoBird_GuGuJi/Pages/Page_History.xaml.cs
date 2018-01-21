using MemoBird_GuGu.Windows;
using MemoBird_GuGu.Classes;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MemoBird_GuGu.Pages
{
    public partial class Page_History : Page
    {
        public Page_History()
        {
            InitializeComponent();
            DatePicker_Start.Text = DateTime.Now.ToString();
            DatePicker_End.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 读取该时间段内的 XML 历史记录文件，并将里面的信息显示到 DataGridView 中
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void FetchXmlList(string startDate, string endDate)
        {
            if (!Directory.Exists(ProgramInfo.History))
            {
                return;
            }
            DataGrid_List.Items.Clear();
            int start = int.Parse(startDate);
            int end = int.Parse(endDate);
            int n = 0;
            string[] memobirds = Directory.GetDirectories(ProgramInfo.History);
            foreach (string memobird in memobirds)
            {
                var xmlNames = from xmlName in Directory.GetFiles(memobird)
                               where int.TryParse(xmlName, out n) && int.Parse(xmlName) >= start && int.Parse(xmlName) <= end
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
                        DataGrid_List.Items.Add(new DataGridRow() { Item = new { col1 = memobirdId, col2 = date, col3 = value } });
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
            if (DataGrid_List.Items.Count == 0)
            {
                return;
            }
            if (DataGrid_List.SelectedItem == null)
            {
                return;
            }
            int index = DataGrid_List.SelectedIndex;
            string content = (DataGrid_List.Columns[2].GetCellContent(DataGrid_List.Items[index]) as TextBlock).Text;
            new Window_HistoryDetails(content).Show();
        }
    }
}
