using Core.Entities;

namespace TestHelper.Mock
{
    public class ToleranceClassMock : ToleranceClass
    {
        public bool WasGetLowerLimitForValueCalled = false;
        public double GetLowerLimitForValueParameter;
        public double GetLowerLimitForValueReturnValue;

        public bool WasGetUpperLimitForValueCalled = false;
        public double GetUpperLimitForValueParameter;
        public double GetUpperLimitForValueReturnValue;

        public override double GetLowerLimitForValue(double value)
        {
            WasGetLowerLimitForValueCalled = true;
            GetLowerLimitForValueParameter = value;
            return GetLowerLimitForValueReturnValue;
        }

        public override double GetUpperLimitForValue(double value)
        {
            WasGetUpperLimitForValueCalled = true;
            GetUpperLimitForValueParameter = value;
            return GetUpperLimitForValueReturnValue;
        }
    }
}
