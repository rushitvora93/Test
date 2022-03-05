using Core;
using Core.Diffs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.ChangesGenerators
{
    public interface ILocationChangesGenerator
    {
        IEnumerable<SingleValueChange> GetLocationChanges(LocationDiff diff);
    }

    public class LocationChangesGenerator : ILocationChangesGenerator
    {
        private ICatalogProxy _localization;
        private ILocationDisplayFormatter _locationDisplayFormatter;

        public LocationChangesGenerator(ICatalogProxy localization, ILocationDisplayFormatter locationDisplayFormatter)
        {
            _localization = localization;
            _locationDisplayFormatter = locationDisplayFormatter;
        }

        public IEnumerable<SingleValueChange> GetLocationChanges(LocationDiff diff)
        {
            string entity = _locationDisplayFormatter.Format(diff.NewLocation);

            if (diff.OldLocation.Number?.ToDefaultString() != diff.NewLocation.Number?.ToDefaultString())
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Number"),
                    OldValue = diff.OldLocation.Number?.ToDefaultString(),
                    NewValue = diff.NewLocation.Number?.ToDefaultString()
                };
            }
            if (diff.OldLocation.Description?.ToDefaultString() != diff.NewLocation.Description?.ToDefaultString())
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Description"),
                    OldValue = diff.OldLocation.Description?.ToDefaultString(),
                    NewValue = diff.NewLocation.Description?.ToDefaultString()
                };
            }
            if (!diff.OldLocation.ControlledBy.Equals(diff.NewLocation.ControlledBy))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Controlled by"),
                    OldValue = _localization.GetParticularString("LocationAttribute", diff.OldLocation.ControlledBy.ToString()),
                    NewValue = _localization.GetParticularString("LocationAttribute", diff.NewLocation.ControlledBy.ToString())
                };
            }
            if (!diff.OldLocation.SetPointTorque.Equals(diff.NewLocation.SetPointTorque))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Setpoint torque"),
                    OldValue = diff.OldLocation.SetPointTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.SetPointTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if ((diff?.OldLocation?.ToleranceClassTorque == null && diff.OldLocation?.ToleranceClassTorque != null)
                || (!diff.OldLocation?.ToleranceClassTorque?.EqualsById(diff.NewLocation?.ToleranceClassTorque) ?? false))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Tolerance class torque"),
                    OldValue = diff.OldLocation.ToleranceClassTorque.Name,
                    NewValue = diff.NewLocation?.ToleranceClassTorque?.Name
                };
            }
            if ((diff?.OldLocation?.ToleranceClassAngle == null && diff.OldLocation?.ToleranceClassAngle != null)
                || (!diff.OldLocation?.ToleranceClassAngle?.EqualsById(diff.NewLocation?.ToleranceClassAngle) ?? false))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Tolerance class angle"),
                    OldValue = diff.OldLocation.ToleranceClassAngle.Name,
                    NewValue = diff.NewLocation?.ToleranceClassAngle?.Name
                };
            }
            if (!diff.OldLocation.MinimumTorque.Equals(diff.NewLocation.MinimumTorque))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Minimum torque"),
                    OldValue = diff.OldLocation.MinimumTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MinimumTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.MaximumTorque.Equals(diff.NewLocation.MaximumTorque))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Maximum torque"),
                    OldValue = diff.OldLocation.MaximumTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MaximumTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.ThresholdTorque.Equals(diff.NewLocation.ThresholdTorque))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Threshold torque"),
                    OldValue = diff.OldLocation.ThresholdTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.ThresholdTorque.Nm.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.SetPointAngle.Equals(diff.NewLocation.SetPointAngle))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Setpoint angle"),
                    OldValue = diff.OldLocation.SetPointAngle.Degree.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.SetPointAngle.Degree.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.MinimumAngle.Equals(diff.NewLocation.MinimumAngle))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Minimum angle"),
                    OldValue = diff.OldLocation.MinimumAngle.Degree.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MinimumAngle.Degree.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (!diff.OldLocation.MaximumAngle.Equals(diff.NewLocation.MaximumAngle))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Maximum angle"),
                    OldValue = diff.OldLocation.MaximumAngle.Degree.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocation.MaximumAngle.Degree.ToString(CultureInfo.CurrentCulture)
                };
            }
            if (diff.OldLocation.ConfigurableField1?.ToDefaultString() != diff.NewLocation.ConfigurableField1?.ToDefaultString())
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Cost center"),
                    OldValue = diff.OldLocation.ConfigurableField1?.ToDefaultString(),
                    NewValue = diff.NewLocation.ConfigurableField1?.ToDefaultString()
                };
            }
            if (diff.OldLocation.ConfigurableField2?.ToDefaultString() != diff.NewLocation.ConfigurableField2?.ToDefaultString())
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Category"),
                    OldValue = diff.OldLocation.ConfigurableField2?.ToDefaultString(),
                    NewValue = diff.NewLocation.ConfigurableField2?.ToDefaultString()
                };
            }
            if (!diff.OldLocation.ConfigurableField3.Equals(diff.NewLocation.ConfigurableField3))
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Documentation"),
                    OldValue = _localization.GetString(diff.OldLocation.ConfigurableField3 ? "Yes" : "No"),
                    NewValue = _localization.GetString(diff.NewLocation.ConfigurableField3 ? "Yes" : "No")
                };
            }
            if (diff.OldLocation.Comment != diff.NewLocation.Comment)
            {
                yield return new SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.GetParticularString("LocationAttribute", "Comment"),
                    OldValue = diff.OldLocation.Comment,
                    NewValue = diff.NewLocation.Comment
                };
            }
        }
    }
}
