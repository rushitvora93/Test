using FrameworksAndDrivers.Gui.Wpf.Model;
using System.Collections.Generic;
using System.Linq;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class ParentAssistentPlan
    {
        protected List<ParentAssistentPlan> _subAssistentPlans;

        public virtual void AddNext(List<AssistentPlan> plans)
        {
            if (_subAssistentPlans == null)
            {
                return;
            }
            
            foreach (var sub in _subAssistentPlans)
            {
                sub.AddNext(plans);
            }
        }

        /// <summary>
        /// In the spezific AssistentPlans can be loaded the data in this method (is invoked at any start of the Assistent, the AssistentPlan is reffered to)
        /// </summary>
        public virtual void Initialize()
        {
            if (_subAssistentPlans == null)
            {
                return;
            }

            foreach (var sub in _subAssistentPlans)
            {
                sub.Initialize();
            }
        }
        
        public ParentAssistentPlan(List<ParentAssistentPlan> subPlans)
        {
            _subAssistentPlans = subPlans;
        }
    }

    public class AssistentPlan : ParentAssistentPlan
    {
        public AssistentItemModel AssistentItem { get; protected set; }


        public override void AddNext(List<AssistentPlan> plans)
        {
            if (AssistentItem != null)
            {
                plans.Add(this);
            }

            base.AddNext(plans);
        }


        public AssistentPlan(AssistentItemModel item) : base(null)
        {
            AssistentItem = item;
        }
        public AssistentPlan(AssistentItemModel item, List<AssistentPlan> subPlans) : base(subPlans.OfType<ParentAssistentPlan>().ToList())
        {
            AssistentItem = item;
        }
    }
    
    public class AssistentPlan<T> : AssistentPlan
    {
        public new AssistentItemModel<T> AssistentItem
        {
            get { return base.AssistentItem as AssistentItemModel<T>; }
        }

        public AssistentPlan(AssistentItemModel<T> item) : base(item) { }
        public AssistentPlan(List<AssistentPlan> subPlans, AssistentItemModel<T> item) : base(item, subPlans) { }
    }

    public class ListAssistentPlan<T> : AssistentPlan<T>
    {
        public new ListAssistentItemModel<T> AssistentItem
        {
            get { return base.AssistentItem as ListAssistentItemModel<T>; }
        }

        public ListAssistentPlan(ListAssistentItemModel<T> item) : base(item) { }

        public ListAssistentPlan(List<AssistentPlan> subPlans, ListAssistentItemModel<T> item) : base(subPlans, item) { }
    }
}
