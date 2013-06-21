using System;
using System.Collections.Generic;
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
using System.IO;
using SeleniumExecuter;
using System.Threading;
using System.Windows.Threading;

namespace TestRunnerUI
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

        #region UI Async Update
        /// <summary>
        /// Adds a new message from a different thread.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int InsertNewMessageAsync(string line, string command, string target, string value, string error)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action<string, string, string, string, string>(InsertNewMessage),
                    line, command, target, value, error);
            return 0;
        }

        /// <summary>
        /// Gets the file name from a different thread.
        /// </summary>
        /// <returns></returns>
        private string GetFileNameAsync()
        {
            UiMessageContainer container = new UiMessageContainer();
            Dispatcher.Invoke(DispatcherPriority.Normal,
                   new Action<UiMessageContainer>(GetFileName), container);
            return container.FileName;
        }

        /// <summary>
        /// Gets the type of the browser to run from a different thread.
        /// </summary>
        /// <returns></returns>
        private BrowserType GetBrowserTypeAsync()
        {
            UiMessageContainer container = new UiMessageContainer();
            Dispatcher.Invoke(DispatcherPriority.Normal,
                   new Action<UiMessageContainer>(GetBrowserType), container);
            return container.BrowserType;
        }

        /// <summary>
        /// Gets the speed selected for the test from a different thread.
        /// </summary>
        /// <returns></returns>
        private double GetSpeedAsync()
        {
            UiMessageContainer container = new UiMessageContainer();
            Dispatcher.Invoke(DispatcherPriority.Normal,
                   new Action<UiMessageContainer>(GetSpeed), container);
            return container.Speed;
        }
        #endregion

        #region UI Update
        /// <summary>
        /// Add a line to the data view.
        /// </summary>
        /// <param name="line">The number of line that is being executed.</param>
        /// <param name="command">Command that is executed.</param>
        /// <param name="target">Target element the command is executed on.</param>
        /// <param name="value">The value used from the command.</param>
        /// <param name="error">Error message if an error has occurred.</param>
        private void InsertNewMessage(string line, string command, string target, string value, string error)
        {
            gvLog.Items.Add(new LogMessage()
                {
                    Line = line,
                    Command = command,
                    Target = target,
                    Value = value,
                    Error = error
                });
        }

        /// <summary>
        /// Gets the file name selected by the user.
        /// </summary>
        /// <param name="container">Container class for result.</param>
        private void GetFileName(UiMessageContainer container)
        {
            container.FileName = txtFileName.Text;
        }

        /// <summary>
        /// Gets the browser type selected by the user.
        /// </summary>
        /// <param name="container">Container class for result.</param>
        private void GetBrowserType(UiMessageContainer container)
        {
            container.BrowserType = BrowserType.Firefox;
            if (Chrome.IsChecked.HasValue && Chrome.IsChecked.Value) container.BrowserType = BrowserType.Chrome;
            if (IE.IsChecked.HasValue && IE.IsChecked.Value) container.BrowserType = BrowserType.InternetExplorer;
        }

        /// <summary>
        /// Gets the speed selected by the user.
        /// </summary>
        /// <param name="container">Container class for result</param>
        private void GetSpeed(UiMessageContainer container)
        {
            container.Speed = sldSpeed.Value;
        }

        #endregion

        #region UI Events
        /// <summary>
        /// Handle the "Browse" button which selected a file for the test.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                txtFileName.Text = dialog.FileName;
            }

        }

        /// <summary>
        /// Handles the "Go" button which starts the test.
        /// </summary>
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            gvLog.Items.Clear();
            ThreadStart t = delegate() { RunScript(); };
            new Thread(t).Start();
        }

        /// <summary>
        /// Change the width of columns on the ListView when user
        /// changes the size of the window.
        /// </summary>
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35; // take into account vertical scrollbar
            var col2 = 0.20;
            var col3 = 0.20;
            var col4 = 0.30;
            var col5 = 0.30;

            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
            gView.Columns[4].Width = workingWidth * col5;

        }

        #endregion

        /// <summary>
        /// Runs the Selenium engine with the test.
        /// </summary>
        public void RunScript()
        {
            try
            {
                CommandEngineSettings.Speed = GetSpeedAsync();
                string html = File.ReadAllText(GetFileNameAsync());
                Executer.RunIDETestFile(html, GetBrowserTypeAsync(), InsertNewMessageAsync);
            }
            catch (Exception ex)
            {
                InsertNewMessageAsync(string.Empty, string.Empty, string.Empty, string.Empty, ex.Message);
            }
        }

    }
}
