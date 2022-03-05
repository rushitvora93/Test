using System;
using System.Collections.Generic;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class ConditionalAssistentPlan : ParentAssistentPlan
    {
        private readonly Func<bool> _condition;


        public override void AddNext(List<AssistentPlan> plans)
        {
            if (_condition())
            {
                base.AddNext(plans);
            }
        }
    
        public ConditionalAssistentPlan(List<ParentAssistentPlan> subPlans, Func<bool> condition) : base(subPlans)
        {
            _condition = condition;
        }
    }
}
