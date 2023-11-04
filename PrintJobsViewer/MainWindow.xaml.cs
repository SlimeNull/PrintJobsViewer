using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Management;
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
using PrintJobsViewer.ViewModels;
using System.Printing;
using PrintJobsViewer.Models;
using System.Reflection;

namespace PrintJobsViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string QueryClassName = "__InstanceOperationEvent";
        const string QueryCondition = "TargetInstance ISA 'Win32_PrintJob'";

        readonly ManagementEventWatcher PrintJobsWatcher = new ManagementEventWatcher(
            new WqlEventQuery(QueryClassName, TimeSpan.FromSeconds(1), QueryCondition));

        public MainWindow()
        {

            DataContext = this;
            InitializeComponent();
        }


        public MainViewModel ViewModel { get; } = new();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrintJobsWatcher.EventArrived += PrintJobsWatcher_EventArrived;
            PrintJobsWatcher.Start();

            UpdatePrintJobs();
        }

        private void PrintJobsWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            UpdatePrintJobs();
        }

        public void UpdatePrintJobs()
        {
            PrintServer printServer = new PrintServer();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                PrintQueue printQueue = new PrintQueue(printServer, printer);
                foreach (PrintSystemJobInfo jobInfo in printQueue.GetPrintJobInfoCollection())
                {
                    bool createNew = false;
                    if (ViewModel.PrinterTasks.FirstOrDefault(job => job.Id == jobInfo.JobIdentifier) is not PrintJob job)
                    {
                        job = new PrintJob();
                        createNew = true;
                    }

                    PrintJob.Populate(job, printer, (uint)jobInfo.JobIdentifier);

                    if (createNew)
                        Dispatcher.Invoke(() => ViewModel.PrinterTasks.Add(job));
                }
            }
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            var version = Assembly
                .GetEntryAssembly()
                .GetName()
                .Version;

            MessageBox.Show(this,
                $"""
                Print jobs viewer (v{version})
                """,
                "About");
        }
    }
}
