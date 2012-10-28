using System;
using System.Text;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Windows;

namespace EditRoom
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region fields

        // todo: set a sensible options path
        private static string defaultText = "Edit Room is a very simple text editor for Windows." +
            Environment.NewLine + Environment.NewLine +
            Environment.NewLine + Environment.NewLine +
            "Keyboard shortcuts:" +
            Environment.NewLine + Environment.NewLine +
            Environment.NewLine + Environment.NewLine +
            "* File Menu:" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+N - New file" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+O - Open file" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+S - Save file" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+W - Exit Edit Room" +
            Environment.NewLine + Environment.NewLine +
            Environment.NewLine + Environment.NewLine +
            "* Edit Menu:" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+X - Cut" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+C - Copy" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+V - Paste" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+Z - Undo" +
            Environment.NewLine + Environment.NewLine +
            "   - Ctrl+Y - Redo";

        private Options Settings = new Options(@"C:\test.cfg");
        private string currentFilePath = string.Empty;
        private bool dirty = false;
        
        private string text = defaultText;
        private SolidColorBrush backgroundColor;
        private SolidColorBrush foregroundColor;
        private FontFamily fontFamily;
        private uint fontSize;

        #endregion fields

        #region properties

        public string CurrentFilePath
        {
            get { return currentFilePath; }
            set
            {
                if (currentFilePath != value)
                {
                    currentFilePath = value;
                    OnPropertyChanged("CurrentFilePath");
                }
            }
        }

        public bool Dirty
        {
            get { return dirty; }
            set
            {
                if (dirty != value)
                {
                    dirty = value;
                    OnPropertyChanged("Dirty");
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                Dirty = true;
                OnPropertyChanged("Text");
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        public SolidColorBrush ForegroundColor
        {
            get { return foregroundColor; }
            set
            {
                foregroundColor = value;
                OnPropertyChanged("ForegroundColor");
            }
        }

        public FontFamily FontFamily
        {
            get { return fontFamily; }
            set
            {
                fontFamily = value;
                OnPropertyChanged("FontFamily");
            }
        }

        public uint FontSize
        {
            get { return fontSize; }
            set
            {
                if (fontSize != value)
                {
                    fontSize = value;
                    OnPropertyChanged("FontSize");
                }
            }
        }

        #endregion properties

        #region public methods

        public MainWindowViewModel()
        {
            BackgroundColor = new SolidColorBrush(Settings.Background);
            ForegroundColor = new SolidColorBrush(Settings.Foreground);
            FontFamily = new FontFamily(Settings.FontFamily);
            FontSize = Settings.FontSize;
        }

        public void New()
        {
            Text = string.Empty;
            Dirty = false;
        }

        public void OpenFile()
        {
            var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.AddExtension = true;
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                using (var reader = new StreamReader(ofd.OpenFile()))
                    Text = reader.ReadToEnd();

                Dirty = false;
            }
        }

        public bool SaveUnsavedChanges()
        {
            if (Dirty)
            {
                var text = "Do you wish to save your unsaved changes?";
                var caption = "Unsaved changes";
                var result = MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.Cancel)
                    return false;

                if (result == MessageBoxResult.Yes)
                    SaveFile();
            }
            return true;
        }

        /// <summary>
        /// Saves the file to disk
        /// </summary>
        public void SaveFile()
        {
            if (CurrentFilePath == string.Empty)
            {
                var sfd = new SaveFileDialog();
                sfd.OverwritePrompt = true;
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (sfd.ShowDialog() == true)
                    CurrentFilePath = sfd.FileName;
                else
                    return;
            }

            using (var writer = File.CreateText(CurrentFilePath))
                writer.Write(Text);

            Dirty = false;
        }



        #endregion public methods

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires when a public property is changed and is handled when that property is being observed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion INotifyPropertyChanged
    }
}
