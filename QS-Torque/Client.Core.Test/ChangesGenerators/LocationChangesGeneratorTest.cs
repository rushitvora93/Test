using Client.Core.ChangesGenerators;
using Client.TestHelper.Mock;
using Core.Diffs;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelper.Mock;

namespace Client.Core.Test.ChangesGenerators
{
    public class LocationChangesGeneratorTest
    {
        [TestCase("qw", "as", true)]
        [TestCase(null, "as", true)]
        [TestCase("qw", null, true)]
        [TestCase("qw", "qw", false)]
        [TestCase(null, null, false)]
        public void NumberTest(string oldVal, string newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { Number = oldVal == null ? null : new LocationNumber(oldVal) },
                NewLocation = new Location() { Number = newVal == null ? null : new LocationNumber(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if(successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Number", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal, resultChanges[0].OldValue);
                Assert.AreEqual(newVal, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase("qw", "as", true)]
        [TestCase(null, "as", true)]
        [TestCase("qw", null, true)]
        [TestCase("qw", "qw", false)]
        [TestCase(null, null, false)]
        public void DescriptionTest(string oldVal, string newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { Description = oldVal == null ? null : new LocationDescription(oldVal) },
                NewLocation = new Location() { Description = newVal == null ? null : new LocationDescription(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Description", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal, resultChanges[0].OldValue);
                Assert.AreEqual(newVal, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(LocationControlledBy.Torque, LocationControlledBy.Angle, true)]
        [TestCase(LocationControlledBy.Angle, LocationControlledBy.Torque, true)]
        [TestCase(LocationControlledBy.Torque, LocationControlledBy.Torque, false)]
        [TestCase(LocationControlledBy.Angle, LocationControlledBy.Angle, false)]
        public void ControlledByTest(LocationControlledBy oldVal, LocationControlledBy newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { ControlledBy = oldVal },
                NewLocation = new Location() { ControlledBy = newVal }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContextList[0]);
                Assert.AreEqual("Controlled by", environment.CatalogProxy.GetParticularStringTextList[0]);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContextList[1]);
                Assert.AreEqual(oldVal.ToString(), environment.CatalogProxy.GetParticularStringTextList[1]);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContextList[2]);
                Assert.AreEqual(newVal.ToString(), environment.CatalogProxy.GetParticularStringTextList[2]);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].OldValue);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, 2, false)]
        public void SetpointTorqueTest(double oldVal, double newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { SetPointTorque = Torque.FromNm(oldVal) },
                NewLocation = new Location() { SetPointTorque = Torque.FromNm(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Setpoint torque", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal.ToString(), resultChanges[0].OldValue);
                Assert.AreEqual(newVal.ToString(), resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, null, true)]
        [TestCase(2, 2, false)]
        [TestCase(null, null, false)]
        public void ToleranceClassTorqueTest(long? oldVal, long? newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { ToleranceClassTorque = oldVal == null ? null : new ToleranceClass() { Id = new ToleranceClassId(oldVal.Value), Name = DateTime.Now.AddDays(2).ToString() } },
                NewLocation = new Location() { ToleranceClassTorque = newVal == null ? null : new ToleranceClass() { Id = new ToleranceClassId(newVal.Value), Name = DateTime.Now.AddDays(3).ToString() } }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Tolerance class torque", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(diff.OldLocation.ToleranceClassTorque?.Name, resultChanges[0].OldValue);
                Assert.AreEqual(diff.NewLocation.ToleranceClassTorque?.Name, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, null, true)]
        [TestCase(2, 2, false)]
        [TestCase(null, null, false)]
        public void ToleranceClassAngleTest(long? oldVal, long? newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { ToleranceClassAngle = oldVal == null ? null : new ToleranceClass() { Id = new ToleranceClassId(oldVal.Value), Name = DateTime.Now.AddDays(2).ToString() } },
                NewLocation = new Location() { ToleranceClassAngle = newVal == null ? null : new ToleranceClass() { Id = new ToleranceClassId(newVal.Value), Name = DateTime.Now.AddDays(3).ToString() } }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Tolerance class angle", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(diff.OldLocation.ToleranceClassAngle?.Name, resultChanges[0].OldValue);
                Assert.AreEqual(diff.NewLocation.ToleranceClassAngle?.Name, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, 2, false)]
        public void MinimumTorqueTest(double oldVal, double newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { MinimumTorque = Torque.FromNm(oldVal) },
                NewLocation = new Location() { MinimumTorque = Torque.FromNm(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Minimum torque", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal.ToString(), resultChanges[0].OldValue);
                Assert.AreEqual(newVal.ToString(), resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, 2, false)]
        public void MaximumTorqueTest(double oldVal, double newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { MaximumTorque = Torque.FromNm(oldVal) },
                NewLocation = new Location() { MaximumTorque = Torque.FromNm(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Maximum torque", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal.ToString(), resultChanges[0].OldValue);
                Assert.AreEqual(newVal.ToString(), resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, 2, false)]
        public void ThresholdTorqueTest(double oldVal, double newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { ThresholdTorque = Torque.FromNm(oldVal) },
                NewLocation = new Location() { ThresholdTorque = Torque.FromNm(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Threshold torque", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal.ToString(), resultChanges[0].OldValue);
                Assert.AreEqual(newVal.ToString(), resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, 2, false)]
        public void SetPointAngleTest(double oldVal, double newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { SetPointAngle = Angle.FromDegree(oldVal) },
                NewLocation = new Location() { SetPointAngle = Angle.FromDegree(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Setpoint angle", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal.ToString(), resultChanges[0].OldValue);
                Assert.AreEqual(newVal.ToString(), resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, 2, false)]
        public void MinimumAngleTest(double oldVal, double newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { MinimumAngle = Angle.FromDegree(oldVal) },
                NewLocation = new Location() { MinimumAngle = Angle.FromDegree(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Minimum angle", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal.ToString(), resultChanges[0].OldValue);
                Assert.AreEqual(newVal.ToString(), resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(2, 3, true)]
        [TestCase(2, 2, false)]
        public void MaximumAngleTest(double oldVal, double newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { MaximumAngle = Angle.FromDegree(oldVal) },
                NewLocation = new Location() { MaximumAngle = Angle.FromDegree(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Maximum angle", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal.ToString(), resultChanges[0].OldValue);
                Assert.AreEqual(newVal.ToString(), resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase("qw", "as", true)]
        [TestCase(null, "as", true)]
        [TestCase("qw", null, true)]
        [TestCase("qw", "qw", false)]
        [TestCase(null, null, false)]
        public void ConfigurableField1Test(string oldVal, string newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { ConfigurableField1 = oldVal == null ? null : new LocationConfigurableField1(oldVal) },
                NewLocation = new Location() { ConfigurableField1 = newVal == null ? null : new LocationConfigurableField1(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Cost center", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal, resultChanges[0].OldValue);
                Assert.AreEqual(newVal, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase("q", "a", true)]
        [TestCase(null, "a", true)]
        [TestCase("q", null, true)]
        [TestCase("q", "q", false)]
        [TestCase(null, null, false)]
        public void ConfigurableField2Test(string oldVal, string newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { ConfigurableField2 = oldVal == null ? null : new LocationConfigurableField2(oldVal) },
                NewLocation = new Location() { ConfigurableField2 = newVal == null ? null : new LocationConfigurableField2(newVal) }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Category", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal, resultChanges[0].OldValue);
                Assert.AreEqual(newVal, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, false, false)]
        [TestCase(true, true, false)]
        public void ConfigurableField3Test(bool oldVal, bool newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { ConfigurableField3 = oldVal },
                NewLocation = new Location() { ConfigurableField3 = newVal }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();
            environment.CatalogProxy.GetStringReturnValue = DateTime.Now.AddDays(2).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContextList[0]);
                Assert.AreEqual("Documentation", environment.CatalogProxy.GetParticularStringTextList[0]);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal ? "Yes" : "No", environment.CatalogProxy.GetStringTextList[0]);
                Assert.AreEqual(newVal ? "Yes" : "No", environment.CatalogProxy.GetStringTextList[1]);
                Assert.AreEqual(environment.CatalogProxy.GetStringReturnValue, resultChanges[0].OldValue);
                Assert.AreEqual(environment.CatalogProxy.GetStringReturnValue, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }

        [TestCase("q", "a", true)]
        [TestCase(null, "a", true)]
        [TestCase("q", null, true)]
        [TestCase("q", "q", false)]
        [TestCase(null, null, false)]
        public void CommentTest(string oldVal, string newVal, bool successful)
        {
            var environment = CreateChangesGeneratorEnvironment();
            var diff = new LocationDiff()
            {
                OldLocation = new Location() { Comment = oldVal },
                NewLocation = new Location() { Comment = newVal }
            };
            environment.LocationDisplayFormatter.DisplayString = DateTime.Now.ToString();
            environment.CatalogProxy.GetParticularStringReturnValue = DateTime.Now.AddDays(1).ToString();

            var resultChanges = environment.ChangesGenerator.GetLocationChanges(diff).ToList();

            if (successful)
            {
                Assert.AreEqual(1, resultChanges.Count);
                Assert.AreSame(diff.NewLocation, environment.LocationDisplayFormatter.FormatLocation);
                Assert.AreEqual(environment.LocationDisplayFormatter.DisplayString, resultChanges[0].AffectedEntity);
                Assert.AreEqual("LocationAttribute", environment.CatalogProxy.GetParticularStringContext);
                Assert.AreEqual("Comment", environment.CatalogProxy.GetParticularStringText);
                Assert.AreEqual(environment.CatalogProxy.GetParticularStringReturnValue, resultChanges[0].ChangedAttribute);
                Assert.AreEqual(oldVal, resultChanges[0].OldValue);
                Assert.AreEqual(newVal, resultChanges[0].NewValue);
            }
            else
            {
                Assert.AreEqual(0, resultChanges.Count);
            }
        }



        private static Environment CreateChangesGeneratorEnvironment()
        {
            var environment = new Environment();
            environment.CatalogProxy = new CatalogProxyMock();
            environment.LocationDisplayFormatter = new MockLocationDisplayFormatter();
            environment.ChangesGenerator = new LocationChangesGenerator(environment.CatalogProxy, environment.LocationDisplayFormatter);
            return environment;
        }

        class Environment
        {
            public CatalogProxyMock CatalogProxy { get; set; }
            public MockLocationDisplayFormatter LocationDisplayFormatter { get; set; }
            public LocationChangesGenerator ChangesGenerator { get; set; }
        }
    }
}
