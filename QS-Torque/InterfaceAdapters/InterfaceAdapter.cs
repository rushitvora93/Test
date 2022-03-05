using System;
using System.Collections.Generic;
using System.Linq;

namespace InterfaceAdapters
{
    public interface IInterfaceAdapter<T>
    {
        void RemoveGuiInterface(T guiInterface);
    }

    public class InterfaceAdapter<T>: BindableBase, IInterfaceAdapter<T>
    {
        private List<WeakReference> _guiInterfaces = new List<WeakReference>();

        public void RegisterGuiInterface(T guiInterface)
        {
            _guiInterfaces.Add(new WeakReference(guiInterface));
        }

        public void RemoveGuiInterface(T guiInterface)
        {
            var found = _guiInterfaces.FirstOrDefault(x => x.Target?.Equals(guiInterface) ?? false);
            if (found != null)
            {
                _guiInterfaces.Remove(found);
            }
        }

        public void InvokeActionOnGuiInterfaces(Action<T> action)
        {
            RemoveDeadWeakReferences();
            var weakReferences = new WeakReference[_guiInterfaces.Count];
            _guiInterfaces.CopyTo(weakReferences);
            foreach (var weakReference in weakReferences)
            {
                if (weakReference.Target is T guiInterface)
                {
                    action(guiInterface);
                }
            }
            RemoveDeadWeakReferences();
        }

        private void RemoveDeadWeakReferences()
        {
            _guiInterfaces.RemoveAll(x => !x.IsAlive);
        }
    }
}
