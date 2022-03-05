using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FrameworksAndDrivers.DataGate
{
    public class DataGateFileSystemHumbleObject: IDataGateFileSystem
    {
        public Stream OpenFile(string path)
        {
            return File.Open(path, FileMode.Create);
        }

        public void WatchForFile(string path, Action<XElement> evaluateAction)
        {
            _stopSource = new TaskCompletionSource<bool>();
            _path = path;
            _evaluateAction = evaluateAction;
            Task.Run(() =>
            {
                using (var watcher = new FileSystemWatcher
                {
                    Path = Path.GetDirectoryName(path),
                    Filter = Path.GetFileName(path),
                    NotifyFilter =
                        NotifyFilters.LastWrite
                        | NotifyFilters.FileName
                        | NotifyFilters.CreationTime
                        | NotifyFilters.Size
                })
                {
                    watcher.Changed += OnChanged;
                    watcher.Created += OnChanged;
                    watcher.Renamed += OnRenamed;
                    watcher.Error += OnError;
                    watcher.EnableRaisingEvents = true;
                    _stopSource.Task.Wait();
                    watcher.EnableRaisingEvents = false;
                }
            });
        }

        private void OnChanged(object sender, FileSystemEventArgs args)
        {
            if (!File.Exists(args.FullPath))
            {
                return;
            }
            (sender as FileSystemWatcher).EnableRaisingEvents = false;
            _stopSource.SetResult(true);
            Thread.Sleep(100);
            _evaluateAction(ReadFile(args.FullPath));
        }

        private void OnRenamed(object sender, RenamedEventArgs args)
        {
            if (_path != args.FullPath || !File.Exists(args.FullPath))
            {
                return;
            }

            (sender as FileSystemWatcher).EnableRaisingEvents = false;
            _stopSource.SetResult(true);
            Thread.Sleep(100);
            _evaluateAction(ReadFile(args.FullPath));
        }

        private void OnError(object sender, ErrorEventArgs args)
        {
            _stopSource.SetResult(true);
            (sender as FileSystemWatcher).EnableRaisingEvents = false;
            // Logging?
        }

        public void StopWatchingForFile()
        {
            if (_stopSource != null && !_stopSource.Task.IsCompleted)
            {
                _stopSource.SetResult(true);
            }
        }

        public XElement ReadFile(string path)
        {
            return XElement.Load(path);
        }

        private Action<XElement> _evaluateAction;
        private string _path;
        private TaskCompletionSource<bool> _stopSource;
    }
}
