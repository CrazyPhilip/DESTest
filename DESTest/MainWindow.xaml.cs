using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Shell32;

namespace DESTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mainViewModel;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(mainViewModel.ByteLength.ToString());
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Title = "请选择文件";
            dialog.Filter = "任何文件(*.*)|*.*";

            var result = dialog.ShowDialog();
            if (result == true)
            {
                //AddMusic(dialog.FileNames);
                mainViewModel.FilePath = dialog.FileName;
            }

            //FileStream fileStream4 = new FileStream("", FileMode.Open, FileAccess.Read, FileShare.Read, 10, FileOptions.None);
        }
    }
}
