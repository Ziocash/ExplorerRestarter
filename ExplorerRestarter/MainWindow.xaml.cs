using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace ExplorerRestarter
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private List<Process> processes;
        private Logger logger;
        public MainWindow()
        {
            InitializeComponent();
            logger = new Logger();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
            timer.Start();
            logger.Log(Severity.Log, "Initialization completed");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                processes[0].Kill();
                logger.Log(Severity.Log, "Restart completed");
            }
            catch(Exception ex)
            {
                logger.Log(Severity.Exception, ex.Message);
            }
        }

        private void UpdateInfo()
        {
            try
            {
                processes = new List<Process>(Process.GetProcessesByName("explorer").AsEnumerable());
                RunExplorerPID.Text = processes[0].Id.ToString();
                if (processes[0].Responding)
                {
                    RunExplorerResponse.Text = "Responding";
                    RunExplorerResponse.Foreground = Brushes.Green;
                }
                else
                {
                    RunExplorerResponse.Text = "Not responding";
                    RunExplorerResponse.Foreground = Brushes.Red;
                }
            }
            catch(Exception ex)
            {                   
                logger.Log(Severity.Exception, ex.Message);
            }
        }
    }
}
