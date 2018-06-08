using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MemoBird_GuGu.Windows
{
    public partial class Window_Tip : Window
    {
        private int seconds;

        public Window_Tip(string text, int seconds = 1000)
        {
            InitializeComponent();

            Title = text;
            Label_Text.Content = text;
            this.seconds = seconds;
            Loaded += Window_Tip_Loaded;
        }

        private void Window_Tip_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(seconds)
            };
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer dispatcherTimer = (DispatcherTimer)sender;
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= DispatcherTimer_Tick;
            Close();
        }
    }
}
