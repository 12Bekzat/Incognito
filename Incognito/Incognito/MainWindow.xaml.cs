using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.Windows.Threading;

namespace Incognito
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveScreenshot(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrWhiteSpace(period.Text))
            {
                MessageBox.Show("Введите период!");
            }
            else
            {
                if(!int.TryParse(period.Text, out var timePeriod))
                {
                    MessageBox.Show("Введите минуту в числах!");
                }
                else
                {
                    DispatcherTimer dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Tick += new EventHandler(ScreenshotWindow);
                    dispatcherTimer.Interval = new TimeSpan(0, timePeriod, 0);
                    dispatcherTimer.Start();
                }
            }
        }

        private void ScreenshotWindow(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\толеутайб\Pictures\Incognito");

            if(!Directory.Exists(@"C:\Users\толеутайб\Pictures\Incognito"))
            {
                Directory.CreateDirectory(@"C:\Users\толеутайб\Pictures\Incognito");
            }

            Bitmap bitmap = new Bitmap((int)System.Windows.SystemParameters.PrimaryScreenWidth,
                                    (int)System.Windows.SystemParameters.PrimaryScreenHeight);
            Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            bitmap.Save(
                String.Format(@"C:\Users\толеутайб\Pictures\Incognito" + "\\prtScreen{0}{1}{2}{3}.jpg",
                DateTime.Now.ToShortDateString(), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                ImageFormat.Jpeg
                );
        }

        private void ClearTextbox(object sender, RoutedEventArgs e)
        {
            period.Text = String.Empty;
        }
    }
}
