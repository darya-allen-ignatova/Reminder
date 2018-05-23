namespace DI.Reminder.Common.Logger
{
    public interface ILogger
    {
        void  Error(string _message);
        void Debug(string _message);
        void Warn(string _message);
        void Fatal(string _message);
    }
}
