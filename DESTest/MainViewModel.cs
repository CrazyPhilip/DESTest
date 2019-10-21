using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DESTest
{
    public class MainViewModel : BaseViewModel
    {
        private string encodingFilePath;   //comment

        public string EncodingFilePath
        {
            get { return encodingFilePath; }
            set { SetProperty(ref encodingFilePath, value); }
        }

        private string decodingFilePath;   //comment

        public string DecodingFilePath
        {
            get { return decodingFilePath; }
            set { SetProperty(ref decodingFilePath, value); }
        }

        private int byteLength;   //comment

        public int ByteLength
        {
            get { return byteLength; }
            set { SetProperty(ref byteLength, value); }
        }

        private string key;   //comment

        public string Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }

        private string encodingText;   //comment

        public string EncodingText
        {
            get { return encodingText; }
            set { SetProperty(ref encodingText, value); }
        }

        private string decodingText;   //comment

        public string DecodingText
        {
            get { return decodingText; }
            set { SetProperty(ref decodingText, value); }
        }

        private string encodingFileInfo;   //comment

        public string EncodingFileInfo
        {
            get { return encodingFileInfo; }
            set { SetProperty(ref encodingFileInfo, value); }
        }

        private string decodingFileInfo;   //comment

        public string DecodingFileInfo
        {
            get { return decodingFileInfo; }
            set { SetProperty(ref decodingFileInfo, value); }
        }


        private bool isEncodeFileActive;   //comment

        public bool IsEncodeFileActive
        {
            get { return isEncodeFileActive; }
            set { SetProperty(ref isEncodeFileActive, value); }
        }

        private bool isDecodeFileActive;   //comment

        public bool IsDecodeFileActive
        {
            get { return isDecodeFileActive; }
            set { SetProperty(ref isDecodeFileActive, value); }
        }

        private bool isEncodeTextActive;   //comment

        public bool IsEncodeTextActive
        {
            get { return isEncodeTextActive; }
            set { SetProperty(ref isEncodeTextActive, value); }
        }

        private bool isDecodeTextActive;   //comment

        public bool IsDecodeTextActive
        {
            get { return isDecodeTextActive; }
            set { SetProperty(ref isDecodeTextActive, value); }
        }

        public MainViewModel()
        {
            ByteLength = 8;

        }

    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);

            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            if (changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
