using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TestHelper.Factories
{
    public class CreateTestParameters
    {
        public static TestParameters Anonymous()
        {
            return new TestParameters();
        }

        public enum Parameter
        {
            SetPointTorque,
            MinimumTorque,
            MaximumTorque,
            ControlledBy
        }

        public static TestParameters WithDynamicParameters(
            List<(Parameter param, string value)> parameters)
        {
            var testParameters = Anonymous();
            foreach(var parameterPair in parameters)
            {
                switch (parameterPair.param)
                {
                    case Parameter.SetPointTorque:
                        testParameters.SetPointTorque = Torque.FromNm(double.Parse(parameterPair.value));
                        break;

                    case Parameter.MinimumTorque:
                        testParameters.MinimumTorque = Torque.FromNm(double.Parse(parameterPair.value));
                        break;

                    case Parameter.MaximumTorque:
                        testParameters.MaximumTorque = Torque.FromNm(double.Parse(parameterPair.value));
                        break;

                    case Parameter.ControlledBy:
                        testParameters.ControlledBy = (LocationControlledBy)int.Parse(parameterPair.value);
                        break;
                }
            }
            return testParameters;
        }
    }
}
