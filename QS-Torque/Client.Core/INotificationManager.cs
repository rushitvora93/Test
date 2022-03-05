namespace Core
{
    public interface INotificationManager
    {
        void SendSuccessNotification(int? changes = null);
        void SendSuccessfulToolTestDateCalculationNotification();
        void SendSuccessfulProcessControlDateCalculationNotification();
        void SendFailedTestDateCalculationNotification();
    }
}
