using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Input;

namespace EditRoom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            Keyboard.Focus(Editor);
        }

        #region commands

        #region file menu commands

        #region new

        private void NewCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (viewModel.SaveUnsavedChanges())
            {
                viewModel.New();
            }
        }

        private void NewCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion new

        #region open

        private void OpenCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (viewModel.SaveUnsavedChanges())
            {
                viewModel.OpenFile();
            }
        }

        private void OpenCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion open

        #region save

        private void SaveCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            viewModel.SaveFile();
        }

        private void SaveCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = viewModel.Dirty;
        }

        #endregion save

        #region close

        private void CloseCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (viewModel.SaveUnsavedChanges())
                this.Close();
        }

        private void CloseCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion close

        #endregion file menu commands

        #region edit menu commands

        #region cut, copy, paste

        private void CutCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Editor.Cut();
        }

        private void CutCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CopyCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Editor.Copy();
        }

        private void CopyCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PasteCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Editor.Paste();
        }

        private void PasteCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion cut, copy, paste

        #region undo redo

        private void UndoCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Editor.Undo();
        }

        private void UndoCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Editor.CanUndo;
        }

        private void RedoCommandExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Editor.Redo();
        }

        private void RedoCommandCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Editor.CanRedo;
        }

        #endregion undo redo

        #endregion edit menu commands

        #endregion commands
    }
}
