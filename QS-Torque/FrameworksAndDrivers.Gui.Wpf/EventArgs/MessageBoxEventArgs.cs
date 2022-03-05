using System;
using System.Threading.Tasks;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.EventArgs
{
    public enum ExecutionMode
    {
        Sync,
        Async
    }

    public class MessageBoxEventArgs
    {
        public string Text { get; private set; }
        public string Caption { get; private set; }
        public MessageBoxButton MessageBoxButton { get; private set; }
        public MessageBoxImage MessageBoxImage { get; private set; }
        public MessageBoxResult MessageBoxResult { get; private set; }
        public MessageBoxOptions MessageBoxOptions { get; private set; }
        public ExecutionMode ExecutionMode { get; private set; }

        public Action<MessageBoxResult> ResultAction { get; private set; }

        


        #region Methods
        public void Show()
        {
            MessageBoxResult result = MessageBoxResult.None;
            
            if(ExecutionMode == ExecutionMode.Sync)
            {
                result = MessageBox.Show(Text, Caption, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions);
            }
            else if (ExecutionMode == ExecutionMode.Async)
            {
                result = Task.Run(() => MessageBox.Show(Text, Caption, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions)).Result;
            }

            ResultAction(result);
        }

        public void Show(Window owner)
        {
            MessageBoxResult result = MessageBoxResult.None;

            if (ExecutionMode == ExecutionMode.Sync)
            {
                result = MessageBox.Show(owner, Text, Caption, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions);
            }
            else if (ExecutionMode == ExecutionMode.Async)
            {
                result = Task.Run(() => MessageBox.Show(owner, Text, Caption, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions)).Result;
            }

            ResultAction(result);
        }
        #endregion


        // Cstor
        public MessageBoxEventArgs(Action<MessageBoxResult> resultAction,
                                   string text,
                                   string caption = "",
                                   MessageBoxButton messageBoxButton = MessageBoxButton.OK,
                                   MessageBoxImage messageBoxImage = MessageBoxImage.None,
                                   MessageBoxResult messageBoxResult = MessageBoxResult.None,
                                   MessageBoxOptions messageBoxOptions = MessageBoxOptions.None,
                                   ExecutionMode executionMode = ExecutionMode.Sync)
        {
            // Save parameters
            ResultAction = resultAction;
            Text = text;
            Caption = caption;
            MessageBoxButton = messageBoxButton;
            MessageBoxImage = messageBoxImage;
            MessageBoxResult = messageBoxResult;
            MessageBoxOptions = messageBoxOptions;
            ExecutionMode = executionMode;
        }
    }
}
