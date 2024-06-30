namespace Core;

public class DisplayAlertEventArgs : EventArgs
{
    public string Title { get; }
    public string Message { get; }
    public string Cancel { get; }

    public DisplayAlertEventArgs(string title, string message, string cancel)
    {
        Title = title;
        Message = message;
        Cancel = cancel;
    }
}