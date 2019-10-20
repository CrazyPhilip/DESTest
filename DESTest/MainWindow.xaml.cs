using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        byte[] byteArray;
        byte[] decodeByteArray;
        int remainder;
        DESEncrypt dESEncrypt = new DESEncrypt();

        [DllImport("DES_dll", EntryPoint = "add")]
        public static extern int add(int a, int b);

        [DllImport("DES_dll", EntryPoint = "endes")]
        public static extern void endes(byte[] m, byte[] k, byte[] e);

        [DllImport("DES_dll", EntryPoint = "undes")]
        public static extern void undes(byte[] m, byte[] k, byte[] e);

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mainViewModel;
        }

        /// <summary>
        /// 读取文件到byte数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private byte[] FileToByte(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] temp = new byte[fs.Length];
                    fs.Read(temp, 0, temp.Length);
                    return temp;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 存入文件，不编码
        /// </summary>
        /// <param name="array"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool ByteToFileNotEncoding(byte[] array, string fileName)
        {
            bool result = false;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    result = true;
                }
            }
            catch
            {

                result = false;
            }
            return result;
        }

        /// <summary>
        /// 加密文本
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string EncodeText(string encodingText, string key)
        {
            string result = "";
            try
            {
                dESEncrypt.Key = key;
                string encodedStr = dESEncrypt.DefaultEncrypt(encodingText);
                result = encodedStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 解密文本
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string DecodeText(string decodingText, string key)
        {
            string result = "";
            try
            {
                dESEncrypt.Key = key;
                string encodedStr = dESEncrypt.DefaultDecrypt(decodingText);
                result = encodedStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 存入文件时加密
        /// </summary>
        /// <param name="array"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool EncodeFile(byte[] array, string fileName, string key)
        {
            bool result = false;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] m = new byte[8];
                    byte[] e = new byte[8];
                    byte[] k = Encoding.UTF8.GetBytes(key);

                    //int index = 0;
                    int step = mainViewModel.ByteLength;

                    remainder = array.Length % step;
                    int gap = step - remainder;

                    int times = 0;
                    int start_index = step * times;
                    while (start_index < array.Length)
                    {
                        byte[] temp = new byte[step];

                        for (int i = 0; i < step; i++)
                        {
                            temp[i] = start_index + i < array.Length ? array[start_index + i] : (byte)gap;
                        }

                        //string str = Encoding.Default.GetString(temp);
                        //string encodedStr = dESEncrypt.DefaultEncrypt(str);
                        //byte[] writeBytes = Encoding.Default.GetBytes(encodedStr);
                        m = temp;
                        endes(m, k, e);

                        fs.Write(e, 0, e.Length);

                        times++;
                        start_index = step * times;
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool DecodeFile(byte[] array, string fileName, string key)
        {
            bool result = false;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] m = new byte[8];
                    byte[] e = new byte[8];
                    byte[] k = Encoding.UTF8.GetBytes(key);

                    //int index = 0;
                    int step = mainViewModel.ByteLength;

                    int times = 0;
                    int total_times = array.Length / step;
                    int start_index = step * times;
                    while (start_index < array.Length)
                    {
                        byte[] temp = new byte[step];

                        for (int i = 0; i < step; i++)
                        {
                            temp[i] = array[start_index + i];
                        }

                        m = temp;
                        undes(m, k, e);

                        if (times != total_times)
                        {
                            fs.Write(e, 0, e.Length);
                        }
                        else
                        {
                            fs.Write(e, 0, remainder);
                        }

                        times++;
                        start_index = step * times;
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }
            return result;
        }

        #region 加解密文件
        /// <summary>
        /// 选择明文文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectEncodingFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Title = "请选择文件";
            dialog.Filter = "任何文件(*.*)|*.*";

            var result = dialog.ShowDialog();
            if (result == true)
            {
                mainViewModel.EncodingFilePath = dialog.FileName;
                mainViewModel.DecodingFilePath = dialog.FileName.Split('.')[0] + "_encoded.enc";
            }
        }

        /// <summary>
        /// 选择密文文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectDecodingFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Title = "请选择文件";
            dialog.Filter = "任何文件(*.*)|*.*";

            var result = dialog.ShowDialog();
            if (result == true)
            {
                mainViewModel.DecodingFilePath = dialog.FileName;
                mainViewModel.EncodingFilePath = dialog.FileName.Split('.')[0] + "_decoded.dec";
            }
        }

        /// <summary>
        /// 加密文件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EncodeFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mainViewModel.EncodingFilePath))
            {
                byteArray = FileToByte(mainViewModel.EncodingFilePath);
            }

            if (byteArray != null)
            {
                //ByteToFileNotEncoding(byteArray, fileName);
                bool flag = EncodeFile(byteArray, mainViewModel.DecodingFilePath, mainViewModel.Key);
                if (flag)
                {
                    MessageBox.Show("加密成功");
                }
                else
                {
                    MessageBox.Show("加密失败");
                }
            }
        }

        /// <summary>
        /// 解密文件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecodeFileButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(mainViewModel.ByteLength.ToString());
            if (!string.IsNullOrWhiteSpace(mainViewModel.DecodingFilePath))
            {
                decodeByteArray = FileToByte(mainViewModel.DecodingFilePath);
            }

            if (decodeByteArray != null)
            {
                //string str = Encoding.Default.GetString(decodeByteArray);
                bool flag = DecodeFile(decodeByteArray, mainViewModel.EncodingFilePath, mainViewModel.Key);
                if (flag)
                {
                    MessageBox.Show("解密成功");
                }
                else
                {
                    MessageBox.Show("解密失败");
                }
            }
        }
        #endregion

        #region 加解密文本
        /// <summary>
        /// 清空明文文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearEncodingTextButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.EncodingText = "";
        }

        /// <summary>
        /// 清空密文文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearDecodingTextButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.DecodingText = "";
        }

        /// <summary>
        /// 加密文本按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EncodeTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mainViewModel.EncodingText))
            {
                mainViewModel.DecodingText = EncodeText(mainViewModel.EncodingText, mainViewModel.Key);
            }
        }

        /// <summary>
        /// 解密文本按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecodeTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mainViewModel.DecodingText))
            {
                mainViewModel.EncodingText = DecodeText(mainViewModel.DecodingText, mainViewModel.Key);
            }
        }
        #endregion

    }
}
