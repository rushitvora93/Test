using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.Validator
{
    public interface ILocationToolAssignmentValidtor
    {
        bool Validate(LocationToolAssignmentModel locationToolAssignmentModel);
    }

    public class Validator<ErrorEnumType, Model> where ErrorEnumType : System.Enum 
                                                 where Model : class
    {
        public static bool Validate(Model model, Func<string, List<ErrorEnumType>> validate)
        {
            return Validate(validate, model?.GetType()?.GetProperties().Select(x => x.Name)?.ToList());
        }

        public static bool Validate(Func<string, List<ErrorEnumType>> validate, List<string> properties)
        {
            if (properties is null)
            {
                return true;
            }
            foreach (var property in properties)
            {
                var errors = validate.Invoke(property);
                if (errors != null && errors.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }


    public class LocationToolAssignmentValidator : ILocationToolAssignmentValidtor
    {

        private static List<string> clickWrenchFields = new List<string>
        {
            nameof(TestTechnique.EndCycleTime),
            nameof(TestTechnique.FilterFrequency),
            nameof(TestTechnique.CycleComplete),
            nameof(TestTechnique.MeasureDelayTime),
            nameof(TestTechnique.ResetTime),
            nameof(TestTechnique.CycleStart),
            nameof(TestTechnique.SlipTorque)
        };

        private static List<string> pulseDriverFields = new List<string>
        {
            nameof(TestTechnique.EndCycleTime),
            nameof(TestTechnique.FilterFrequency),
            nameof(TestTechnique.TorqueCoefficient),
            nameof(TestTechnique.MinimumPulse),
            nameof(TestTechnique.MaximumPulse),
            nameof(TestTechnique.Threshold),
        };

        private static List<string> powerToolFields = new List<string>
        {
            nameof(TestTechnique.EndCycleTime),
            nameof(TestTechnique.FilterFrequency),
            nameof(TestTechnique.CycleComplete),
            nameof(TestTechnique.MeasureDelayTime),
            nameof(TestTechnique.ResetTime),
            nameof(TestTechnique.MustTorqueAndAngleBeInLimits),
            nameof(TestTechnique.CycleStart),
            nameof(TestTechnique.StartFinalAngle)
        };

        private static List<string> peakValueFields = new List<string>
        {
            nameof(TestTechnique.EndCycleTime),
            nameof(TestTechnique.FilterFrequency),
            nameof(TestTechnique.MustTorqueAndAngleBeInLimits),
            nameof(TestTechnique.CycleStart),
            nameof(TestTechnique.StartFinalAngle)
        };


        public bool Validate(LocationToolAssignmentModel locationToolAssignmentModel)
        {
            if (locationToolAssignmentModel is null)
            {
                return true;
            }

            if (!Validator<TestParameterError, TestParametersModel>.Validate(locationToolAssignmentModel.TestParameters,
                (propertyName) => locationToolAssignmentModel.TestParameters.Entity.Validate(propertyName)))
            {
                return false;
            }

            if (locationToolAssignmentModel?.AssignedTool?.ToolModelModel?.ModelType is AbstractToolTypeModel toolTypeModel)
            {
                List<string> fields = new List<string>();
                switch (toolTypeModel)
                {
                    case ClickWrenchModel _:
                        fields = clickWrenchFields;
                        break;
                    case PulseDriverModel _:
                    case PulseDriverShutOffModel _:
                        fields = pulseDriverFields;
                        break;
                    case ECDriverModel _:
                    case GeneralModel _:
                        fields = powerToolFields;
                        break;
                    case ProductionWrenchModel _:
                    case MDWrenchModel _:
                        fields = peakValueFields;
                        break;
                }

                if(locationToolAssignmentModel?.TestTechnique?.Entity != null)
                {
                    if(!Validator<TestTechniqueError, TestTechniqueModel>.Validate(
                        (propetyName) => locationToolAssignmentModel.TestTechnique.Entity.Validate(propetyName),
                        fields))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
