namespace PrintJobsViewer.Models
{
    public enum PrintJobStatus
    {
        None                  = 0,
        Paused                = 1 << 0,
        Error                 = 1 << 1,
        Deleting              = 1 << 2,
        Spooling              = 1 << 3,
        Printing              = 1 << 4,
        Offline               = 1 << 5,
        PaperOut              = 1 << 6,
        Printed               = 1 << 7,
        Deleted               = 1 << 8,
        BlockedDevq           = 1 << 9, 
        UserIntervention      = 1 << 10,
        Restart               = 1 << 11,
        Complete              = 1 << 12,
        Retained              = 1 << 13,
        RenderingLocally      = 1 << 14,
    }
}
