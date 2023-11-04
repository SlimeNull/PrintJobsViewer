using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PrintJobsViewer.Utilities;

namespace PrintJobsViewer.Models
{

    public partial class PrintJob : ObservableObject
    {
        [ObservableProperty]
        private uint _id;

        [ObservableProperty]
        private string _document = string.Empty;

        [ObservableProperty]
        private string _printerName = string.Empty;

        [ObservableProperty]
        private string _machineName = string.Empty;

        [ObservableProperty]
        private string _userName = string.Empty;

        [ObservableProperty]
        private string _notifyName = string.Empty;

        [ObservableProperty]
        private string _dataType = string.Empty;

        [ObservableProperty]
        private string _printProcessor = string.Empty;

        [ObservableProperty]
        private string _parameters = string.Empty;

        [ObservableProperty]
        private string _driverName = string.Empty;

        [ObservableProperty]
        private PrintJobStatus _status = PrintJobStatus.None;

        [ObservableProperty]
        private PrintJobPriority _priority = PrintJobPriority.Default;

        [ObservableProperty]
        private uint _position;

        [ObservableProperty]
        private uint _startTime;

        [ObservableProperty]
        private uint _untilTime;

        [ObservableProperty]
        private uint _totalPages;

        [ObservableProperty]
        private uint _copyCount;

        [ObservableProperty]
        private uint _size;

        [ObservableProperty]
        private PrintJobDuplex _duplex;

        [ObservableProperty]
        private DateTime _submitTime;

        [ObservableProperty]
        private uint _pagesPrinted;




        public static unsafe bool Populate(PrintJob printJob, string printerName, uint jobId)
        {
            IntPtr hPrinter;
            if (!NativeMethods.OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                return false;

            try
            {
                NativeMethods.GetJob(hPrinter, jobId, 2, IntPtr.Zero, 0, out uint pcbNeeded);
                if (pcbNeeded == 0)
                    return false;

                IntPtr pJob = Marshal.AllocCoTaskMem((int)pcbNeeded);


                try
                {
                    if (!NativeMethods.GetJob(hPrinter, jobId, 2, pJob, pcbNeeded, out _))
                        return false;

                    JOB_INFO_2 job = Marshal.PtrToStructure<JOB_INFO_2>(pJob);

                    printJob.Id = job.JobId;
                    printJob.Document = Marshal.PtrToStringAuto(job.pDocument);
                    printJob.PrinterName = Marshal.PtrToStringAuto(job.pPrinterName);
                    printJob.MachineName = Marshal.PtrToStringAuto(job.pMachineName);
                    printJob.UserName = Marshal.PtrToStringAuto(job.pUserName);
                    printJob.NotifyName = Marshal.PtrToStringAuto(job.pNotifyName);
                    printJob.DataType = Marshal.PtrToStringAuto(job.pDatatype);
                    printJob.PrintProcessor = Marshal.PtrToStringAuto(job.pPrintProcessor);
                    printJob.Parameters = Marshal.PtrToStringAuto(job.pDriveName);
                    printJob.DriverName = Marshal.PtrToStringAuto(job.pDriveName);
                    printJob.Status = (PrintJobStatus)job.Status;
                    printJob.Priority = (PrintJobPriority)job.Priority;
                    printJob.Position = job.Position;
                    printJob.StartTime = job.StartTime;
                    printJob.UntilTime = job.UntilTime;
                    printJob.TotalPages = job.TotalPages;
                    printJob.Size = job.Size;

                    var jobTime = job.Submitted;
                    printJob.SubmitTime = new DateTime(jobTime.wYear, jobTime.wMonth, jobTime.wDay, jobTime.wHour, jobTime.wMinute, jobTime.wSecond, jobTime.wMilliseconds);

                    printJob.PagesPrinted = job.PagePrinted;

                    if (job.pDevMode != IntPtr.Zero)
                    {
                        DEVMODE devMode = Marshal.PtrToStructure<DEVMODE>(job.pDevMode);

                        printJob.CopyCount = devMode.dmCopies;
                        printJob.Duplex = (PrintJobDuplex)devMode.dmDuplex;
                    }


                    return true;
                }
                finally
                {
                    Marshal.FreeCoTaskMem(pJob);
                }
            }
            finally
            {
                NativeMethods.ClosePrinter(hPrinter);
            }
        }
    }
}
