using System;
using Server.Core.Entities;

namespace Server.TestHelper.Mocks
{
    public class ProcessControlTechMock : ProcessControlTech
    {
        public override bool EqualsByContent(ProcessControlTech otherProcessControlTech)
        {
            throw new NotImplementedException();
        }

        public override ProcessControlTech CopyDeep()
        {
            throw new NotImplementedException();
        }

        public override bool EqualsById(ProcessControlTech other)
        {
            throw new NotImplementedException();
        }

        public override void UpdateWith(ProcessControlTech other)
        {
            throw new NotImplementedException();
        }
    }
}
