using FrameworksAndDrivers.Gui.Wpf.Model;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.TreeStructure
{
    public class StructureTreeViewItemAdv : TreeViewItemAdv
    {
        public StructureTreeViewItemAdv TreeParent { get; set; }
        public ITreeLevel Level { get; set; }
        private ImageSource _leftItemImageOnParentExpanded;
        
        public virtual void Remove(bool removeCascade)
        {
            TreeParent?.Items.Remove(this);
            Level?.Remove(this);

            if (removeCascade)
            {
                if(TreeParent is IHasItemsExceptDummy hasItemsExceptDummy)
                {
                    if(!hasItemsExceptDummy.HasItemsExceptDummy())
                    {
                        TreeParent.Remove(removeCascade);
                    }
                }
                else if (TreeParent?.Items.Count == 0)
                {
                    TreeParent.Remove(removeCascade);
                } 
            }
        }

        private void This_Expanded(object sender, RoutedEventArgs e)
        {
            foreach (TreeViewItemAdv tvi in this.Items)
            {
                if (tvi is StructureTreeViewItemAdv stvi && stvi.LeftImageSource == null)
                {
                    stvi.LeftImageSource = stvi._leftItemImageOnParentExpanded;
                }
            }
        }
        
        public StructureTreeViewItemAdv(ImageSource leftItemImageOnParentExpanded = null)
        {
            this.IsEditable = false;
            this.ImageHeight = 15;
            this.FontSize = 12;
            _leftItemImageOnParentExpanded = leftItemImageOnParentExpanded;
            this.Expanded += This_Expanded;
            (this.Items.SourceCollection as INotifyCollectionChanged).CollectionChanged += StructureTreeViewItemAdv_CollectionChanged;
        }

        private void StructureTreeViewItemAdv_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && this.IsExpanded)
            {
                foreach (TreeViewItemAdv tvi in e.NewItems)
                {
                    if (tvi is StructureTreeViewItemAdv stvi && stvi.LeftImageSource == null)
                    {
                        stvi.LeftImageSource = stvi._leftItemImageOnParentExpanded;
                    } 
                }
            }
        }
    }

    public class StructureTreeViewItemAdv<T> : StructureTreeViewItemAdv
    {
        public DisplayMemberModel<T> DisplayMember { get; private set; }

        private void DisplayMember_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DisplayMemberModel<T>.DisplayMember))
            {
                this.Header = DisplayMember.DisplayMember;
            }
        }

        public StructureTreeViewItemAdv(DisplayMemberModel<T> displayMember, ImageSource leftItemImageOnParentExpanded = null) : base(leftItemImageOnParentExpanded)
        {
            DisplayMember = displayMember;
            Header = DisplayMember.DisplayMember;
            DisplayMember.PropertyChanged += DisplayMember_PropertyChanged;
        }
    }

    interface IHasItemsExceptDummy
    {
        bool HasItemsExceptDummy();
    }

    public class AlwaysExpandableTreeViewItemAdv<T> : StructureTreeViewItemAdv<T>, IHasItemsExceptDummy
    {
        private TreeViewItemAdv _dummy;

        public bool HasAlreadyExpanded;

        private bool _removeItemCascadeIfItGetsEmpty;

        public bool HasItemsExceptDummy()
        {
            return !(this.Items.Count == 1 && this.Items[0] == _dummy);
        }

        public override void Remove(bool removeCascade)
        {
            if (_removeItemCascadeIfItGetsEmpty)
            {
                base.Remove(removeCascade);
            }
            else
            {
                // Do nothing cannot be removed
                this.IsExpanded = false;
            }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if(e.NewItems.Count > 0 && !e.NewItems.Contains(_dummy))
                    {
                        this.Items.Remove(_dummy);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (this.Items.Count == 0)
                    {
                        this.Items.Add(_dummy);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    if (this.Items.Count == 0)
                    {
                        this.Items.Add(_dummy); 
                    }
                    break;
            }
        }

        public AlwaysExpandableTreeViewItemAdv(DisplayMemberModel<T> displayMember, bool removeItemCascadeIfItGetsEmpty = false) : base(displayMember)
        {
            _dummy = new TreeViewItemAdv();
            _removeItemCascadeIfItGetsEmpty = removeItemCascadeIfItGetsEmpty;
            this.Items.Add(_dummy);
        }
    }
}
