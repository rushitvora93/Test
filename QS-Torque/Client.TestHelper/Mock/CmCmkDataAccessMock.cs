using System;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class CmCmkDataAccessMock : ICmCmkDataAccess
    {
        public (double cm, double cmk) LoadCmCmk()
        {
            if (LoadCmCmkThrowsException)
            {
                throw new Exception();
            }

            LoadCmCmkCallCount++;
            return CmCmkToLoad;
        }

        public bool LoadCmCmkThrowsException { get; set; }
        public (double cm, double cmk) CmCmkToLoad { get; set; }
        public int LoadCmCmkCallCount { get; set; }
    }
}
