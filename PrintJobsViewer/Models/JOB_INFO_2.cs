using System.Runtime.InteropServices;

namespace PrintJobsViewer.Models
{
    [StructLayout(LayoutKind.Sequential)]
    struct JOB_INFO_2
    {
        public uint JobId;
        public nint pPrinterName;
        public nint pMachineName;
        public nint pUserName;
        public nint pDocument;
        public nint pNotifyName;
        public nint pDatatype;
        public nint pPrintProcessor;
        public nint pParameters;
        public nint pDriveName;
        public nint pDevMode;
        public nint pStatus;
        public nint pSecurityDescriptor;
        public uint Status;
        public uint Priority;
        public uint Position;
        public uint StartTime;
        public uint UntilTime;
        public uint TotalPages;
        public uint Size;
        public SYSTEMTIME Submitted;
        public uint Time;
        public uint PagePrinted;
    }
}
