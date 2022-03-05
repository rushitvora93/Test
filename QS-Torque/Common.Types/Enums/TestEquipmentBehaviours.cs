using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Types.Enums
{
    public enum TestEquipmentBehaviourTransferCurves
    {
        Never,
        OnlyNio,
        Always
    }

    public enum TestEquipmentBehaviourAskForIdent
    {
        Never,
        PerTest,
        PerVal,
        OnlyNio,
        PerRoute
    }

    public enum TestEquipmentBehaviourConfirmMeasurements
    {
        Never,
        OnlyNio,
        Always
    }
}
