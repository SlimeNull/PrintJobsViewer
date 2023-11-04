# PrintJobsViewer

Simple realtime print jobs viewer for Windows

## Principle

1. Receive print job change events through the ManagementEventWatcher
2. After obtaining the ID of the print job, query the specific information of the print job through WinAPI, 
3. For double-sided printing and the number of print copies, you can obtain DEVMODE and then obtain the information


## APIs used

1. ManagementEventWatcher
2. Printer APIs: OpenPrinter, ClosePrinter, GetJob