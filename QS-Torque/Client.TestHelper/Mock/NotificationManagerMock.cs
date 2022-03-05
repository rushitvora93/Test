using System.Threading.Tasks;
using Core;

namespace TestHelper.Mock
{
    public class NotificationManagerMock : INotificationManager
    {
        public bool SendSuccessNotificationCalled { get; set; } = false;
        public readonly TaskCompletionSource<bool> SendSuccessNotificationTaskCalled = new TaskCompletionSource<bool>();
        public int? SendSuccessNotificationParameter { get; set; }
        public bool SendSuccessfulTestDateCalculationNotificationCalled { get; set; }
        public bool SendFailedTestDateCalculationNotificationCalled { get; set; }
        public bool SendSuccessfulProcessControlDateCalculationNotificationCalled { get; set; }

        public void SendSuccessNotification(int? changes)
        {
            SendSuccessNotificationParameter = changes;
            SendSuccessNotificationCalled = true;
            SendSuccessNotificationTaskCalled.SetResult(true);
        }

        public void SendSuccessfulToolTestDateCalculationNotification()
        {
            SendSuccessfulTestDateCalculationNotificationCalled = true;
        }

        public void SendFailedTestDateCalculationNotification()
        {
            SendFailedTestDateCalculationNotificationCalled = true;
        }

        public void SendSuccessfulProcessControlDateCalculationNotification()
        {
            SendSuccessfulProcessControlDateCalculationNotificationCalled = true;
        }
    }
}
