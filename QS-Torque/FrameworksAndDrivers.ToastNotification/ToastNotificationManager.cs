using System;
using System.IO;
using System.Reflection;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Core;
using FrameworksAndDrivers.Localization;
using log4net;

namespace FrameworksAndDrivers.ToastNotification
{
    public class ToastNotificationManager : INotificationManager
    {
        #region properties
        private readonly LocalizationWrapper _localization;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ToastNotificationManager));
        private const string AppId = "QS-Torque";
        #endregion

        public ToastNotificationManager(LocalizationWrapper localization)
        {
            _localization = localization;
        }

        public void SendSuccessNotification(int? changes = null)
        {
            var messageText = changes == null ?
                _localization.Strings.GetParticularString("ToastNotificationManager", "Action was successful") :
                changes.ToString() + " " + _localization.Strings.GetParticularString("ToastNotificationManager", "changes were saved successfully");
            SendToastMessage(messageText, "Done.png");
        }

        public void SendSuccessfulToolTestDateCalculationNotification()
        {
            var messageText = _localization.Strings.GetParticularString("ToastNotificationManager", "Tool test dates were calculated successfully");
            SendToastMessage(messageText, "Done.png");
        }

        public void SendSuccessfulProcessControlDateCalculationNotification()
        {
            var messageText = _localization.Strings.GetParticularString("ToastNotificationManager", "Process control dates were calculated successfully");
            SendToastMessage(messageText, "Done.png");
        }

        public void SendFailedTestDateCalculationNotification()
        {
            var messageText = _localization.Strings.GetParticularString("ToastNotificationManager", "The test date calculation failed");
            SendToastMessage(messageText, "Error.png");
        }

        private void SendToastMessage(string messageText, string iconName)
        {
            try
            {
                var toastXml = Windows.UI.Notifications.ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);

                var toastText = toastXml.GetElementsByTagName("text");
                toastText[0].AppendChild(toastXml.CreateTextNode(messageText));

                var imagePath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)}/ToastIcons/{iconName}";
                var toastImage = toastXml.GetElementsByTagName("image");
                toastImage[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

                var toastNotification = new Windows.UI.Notifications.ToastNotification(toastXml);
                toastNotification.Dismissed += Toast_Dismissed;

                var toastNode = toastXml.SelectSingleNode("/toast");
                (toastNode as XmlElement)?.SetAttribute("duration", "short");

                Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier(AppId).Show(toastNotification);
                LogToastSetting();
            }
            catch (Exception e)
            {
                Log.Error($"Error while showing toast notification", e);
            }
        }

        private static void LogToastSetting()
        {

            var notifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier(AppId);
            if (notifier.Setting == NotificationSetting.Enabled)
            {
                return;
            }

            Log.Warn($"ToastNotifications for QST not enabled! (" + notifier.Setting + ")");
        }

        private void Toast_Dismissed(Windows.UI.Notifications.ToastNotification sender, ToastDismissedEventArgs args)
        {
            if (args.Reason == ToastDismissalReason.UserCanceled || args.Reason == ToastDismissalReason.TimedOut)
            {
                Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier(AppId).Hide(sender);
            }
        }
    }
}
