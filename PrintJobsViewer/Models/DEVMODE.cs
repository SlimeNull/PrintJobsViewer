using System.Runtime.InteropServices;

namespace PrintJobsViewer.Models
{
    // DEVMODE结构体定义
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DEVMODE
    {
        const int CCHDEVICENAME = 64;
        const int CCHFORMNAME = 64;

        public fixed byte dmDeviceName[CCHDEVICENAME];
        public ushort dmSpecVersion;
        public ushort dmDriverVersion;
        public ushort dmSize;
        public ushort dmDriverExtra;
        public uint dmFields;
        public ushort dmOrientation;
        public ushort dmPaperSize;
        public ushort dmPaperLength;
        public ushort dmPaperWidth;
        public ushort dmScale;
        public ushort dmCopies;
        public ushort dmDefaultSource;
        public ushort dmPrintQuality;
        public ushort dmColor;
        public ushort dmDuplex;
        public ushort dmYResolution;
        public ushort dmTTOption;
        public ushort dmCollate;
        public fixed byte dmFormName[CCHFORMNAME];
        public ushort dmUnusedPadding;
        public ushort dmBitsPerPel;
        public uint dmPelsWidth;
        public uint dmPelsHeight;
        public uint dmDisplayFlags;
        public uint dmDisplayFrequency;
        public uint dmICMMethod;
        public uint dmICMIntent;
        public uint dmMediaType;
        public uint dmDitherType;
        public uint dmReserved1;
        public uint dmReserved2;
        public uint dmPlanningWidth;
        public uint dmPlanningHeight;
    }
}
