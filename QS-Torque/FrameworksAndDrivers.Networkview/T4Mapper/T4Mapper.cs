
using System.Reflection;
using T4Mapper;

namespace FrameworksAndDrivers.NetworkView.T4Mapper
{
	public class Mapper
	{
		private readonly Assigner _assigner = new Assigner();


			// ClassicChkTestDtoToClassicChkTest.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Timestamp -> Timestamp
			// PropertyMapping: NumberOfTests -> NumberOfTests
			// PropertyMapping: LowerLimitUnit1 -> LowerLimitUnit1
			// PropertyMapping: NominalValueUnit1 -> NominalValueUnit1
			// PropertyMapping: UpperLimitUnit1 -> UpperLimitUnit1
			// PropertyMapping: Unit1Id -> Unit1Id
			// PropertyMapping: LowerLimitUnit2 -> LowerLimitUnit2
			// PropertyMapping: NominalValueUnit2 -> NominalValueUnit2
			// PropertyMapping: UpperLimitUnit2 -> UpperLimitUnit2
			// PropertyMapping: Unit2Id -> Unit2Id
			// PropertyMapping: TestValueMinimum -> TestValueMinimum
			// PropertyMapping: TestValueMaximum -> TestValueMaximum
			// PropertyMapping: Average -> Average
			// PropertyMapping: StandardDeviation -> StandardDeviation
			// PropertyMapping: ControlledByUnitId -> ControlledByUnitId
			// PropertyMapping: ThresholdTorque -> ThresholdTorque
			// PropertyMapping: SensorSerialNumber -> SensorSerialNumber
			// PropertyMapping: Result -> Result
			// PropertyMapping: ToleranceClassUnit1 -> ToleranceClassUnit1
			// PropertyMapping: ToleranceClassUnit2 -> ToleranceClassUnit2
			// PropertyMapping: TestLocation -> TestLocation
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: ToolId -> ToolId
			// PropertyMapping: User -> User
			// PropertyMapping: TestValues -> TestValues
			// PropertyMapping: LocationToolAssignmentId -> LocationToolAssignmentId
		public Server.Core.Entities.ClassicChkTest DirectPropertyMapping(DtoTypes.ClassicChkTest source)
		{
			var target = new Server.Core.Entities.ClassicChkTest();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicChkTest source, Server.Core.Entities.ClassicChkTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
		}

		public Server.Core.Entities.ClassicChkTest ReflectedPropertyMapping(DtoTypes.ClassicChkTest source)
		{
			var target = new Server.Core.Entities.ClassicChkTest();
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicChkTest source, Server.Core.Entities.ClassicChkTest target)
		{
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
		}

		// ClassicChkTestToClassicChkTestDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Timestamp -> Timestamp
			// PropertyMapping: NumberOfTests -> NumberOfTests
			// PropertyMapping: LowerLimitUnit1 -> LowerLimitUnit1
			// PropertyMapping: NominalValueUnit1 -> NominalValueUnit1
			// PropertyMapping: UpperLimitUnit1 -> UpperLimitUnit1
			// PropertyMapping: Unit1Id -> Unit1Id
			// PropertyMapping: LowerLimitUnit2 -> LowerLimitUnit2
			// PropertyMapping: NominalValueUnit2 -> NominalValueUnit2
			// PropertyMapping: UpperLimitUnit2 -> UpperLimitUnit2
			// PropertyMapping: Unit2Id -> Unit2Id
			// PropertyMapping: TestValueMinimum -> TestValueMinimum
			// PropertyMapping: TestValueMaximum -> TestValueMaximum
			// PropertyMapping: Average -> Average
			// PropertyMapping: StandardDeviation -> StandardDeviation
			// PropertyMapping: ControlledByUnitId -> ControlledByUnitId
			// PropertyMapping: ThresholdTorque -> ThresholdTorque
			// PropertyMapping: SensorSerialNumber -> SensorSerialNumber
			// PropertyMapping: Result -> Result
			// PropertyMapping: ToleranceClassUnit1 -> ToleranceClassUnit1
			// PropertyMapping: ToleranceClassUnit2 -> ToleranceClassUnit2
			// PropertyMapping: TestLocation -> TestLocation
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: ToolId -> ToolId
			// PropertyMapping: User -> User
			// PropertyMapping: TestValues -> TestValues
			// PropertyMapping: LocationToolAssignmentId -> LocationToolAssignmentId
		public DtoTypes.ClassicChkTest DirectPropertyMapping(Server.Core.Entities.ClassicChkTest source)
		{
			var target = new DtoTypes.ClassicChkTest();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicChkTest source, DtoTypes.ClassicChkTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
		}

		public DtoTypes.ClassicChkTest ReflectedPropertyMapping(Server.Core.Entities.ClassicChkTest source)
		{
			var target = new DtoTypes.ClassicChkTest();
			typeof(DtoTypes.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicChkTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(DtoTypes.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DtoTypes.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DtoTypes.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DtoTypes.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DtoTypes.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DtoTypes.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DtoTypes.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DtoTypes.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DtoTypes.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DtoTypes.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DtoTypes.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(DtoTypes.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DtoTypes.ClassicChkTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DtoTypes.ClassicChkTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.ClassicChkTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(DtoTypes.ClassicChkTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicChkTest source, DtoTypes.ClassicChkTest target)
		{
			typeof(DtoTypes.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicChkTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(DtoTypes.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DtoTypes.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DtoTypes.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DtoTypes.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DtoTypes.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DtoTypes.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DtoTypes.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DtoTypes.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DtoTypes.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DtoTypes.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DtoTypes.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DtoTypes.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DtoTypes.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(DtoTypes.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DtoTypes.ClassicChkTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DtoTypes.ClassicChkTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.ClassicChkTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(DtoTypes.ClassicChkTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
		}

		// ClassicChkTestValueDtoToClassicChkTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public Server.Core.Entities.ClassicChkTestValue DirectPropertyMapping(DtoTypes.ClassicChkTestValue source)
		{
			var target = new Server.Core.Entities.ClassicChkTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicChkTestValue source, Server.Core.Entities.ClassicChkTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public Server.Core.Entities.ClassicChkTestValue ReflectedPropertyMapping(DtoTypes.ClassicChkTestValue source)
		{
			var target = new Server.Core.Entities.ClassicChkTestValue();
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicChkTestValue source, Server.Core.Entities.ClassicChkTestValue target)
		{
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
		}

		// ClassicChkTestValueToClassicChkTestValueDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public DtoTypes.ClassicChkTestValue DirectPropertyMapping(Server.Core.Entities.ClassicChkTestValue source)
		{
			var target = new DtoTypes.ClassicChkTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicChkTestValue source, DtoTypes.ClassicChkTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public DtoTypes.ClassicChkTestValue ReflectedPropertyMapping(Server.Core.Entities.ClassicChkTestValue source)
		{
			var target = new DtoTypes.ClassicChkTestValue();
			typeof(DtoTypes.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(DtoTypes.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(DtoTypes.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicChkTestValue source, DtoTypes.ClassicChkTestValue target)
		{
			typeof(DtoTypes.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(DtoTypes.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(DtoTypes.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
		}

		// ClassicMfuTestDtoToClassicMfuTest.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Timestamp -> Timestamp
			// PropertyMapping: NumberOfTests -> NumberOfTests
			// PropertyMapping: LowerLimitUnit1 -> LowerLimitUnit1
			// PropertyMapping: NominalValueUnit1 -> NominalValueUnit1
			// PropertyMapping: UpperLimitUnit1 -> UpperLimitUnit1
			// PropertyMapping: Unit1Id -> Unit1Id
			// PropertyMapping: LowerLimitUnit2 -> LowerLimitUnit2
			// PropertyMapping: NominalValueUnit2 -> NominalValueUnit2
			// PropertyMapping: UpperLimitUnit2 -> UpperLimitUnit2
			// PropertyMapping: Unit2Id -> Unit2Id
			// PropertyMapping: TestValueMinimum -> TestValueMinimum
			// PropertyMapping: TestValueMaximum -> TestValueMaximum
			// PropertyMapping: Average -> Average
			// PropertyMapping: StandardDeviation -> StandardDeviation
			// PropertyMapping: ControlledByUnitId -> ControlledByUnitId
			// PropertyMapping: ThresholdTorque -> ThresholdTorque
			// PropertyMapping: SensorSerialNumber -> SensorSerialNumber
			// PropertyMapping: Result -> Result
			// PropertyMapping: ToleranceClassUnit1 -> ToleranceClassUnit1
			// PropertyMapping: ToleranceClassUnit2 -> ToleranceClassUnit2
			// PropertyMapping: TestLocation -> TestLocation
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: Cm -> Cm
			// PropertyMapping: Cmk -> Cmk
			// PropertyMapping: LimitCm -> LimitCm
			// PropertyMapping: LimitCmk -> LimitCmk
			// PropertyMapping: ToolId -> ToolId
			// PropertyMapping: User -> User
			// PropertyMapping: TestValues -> TestValues
			// PropertyMapping: LocationToolAssignmentId -> LocationToolAssignmentId
		public Server.Core.Entities.ClassicMfuTest DirectPropertyMapping(DtoTypes.ClassicMfuTest source)
		{
			var target = new Server.Core.Entities.ClassicMfuTest();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.Cm = value;}, source.Cm);
			_assigner.Assign((value) => {target.Cmk = value;}, source.Cmk);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicMfuTest source, Server.Core.Entities.ClassicMfuTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.Cm = value;}, source.Cm);
			_assigner.Assign((value) => {target.Cmk = value;}, source.Cmk);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
		}

		public Server.Core.Entities.ClassicMfuTest ReflectedPropertyMapping(DtoTypes.ClassicMfuTest source)
		{
			var target = new Server.Core.Entities.ClassicMfuTest();
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicMfuTest source, Server.Core.Entities.ClassicMfuTest target)
		{
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
		}

		// ClassicMfuTestToClassicMfuTestDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Timestamp -> Timestamp
			// PropertyMapping: NumberOfTests -> NumberOfTests
			// PropertyMapping: LowerLimitUnit1 -> LowerLimitUnit1
			// PropertyMapping: NominalValueUnit1 -> NominalValueUnit1
			// PropertyMapping: UpperLimitUnit1 -> UpperLimitUnit1
			// PropertyMapping: Unit1Id -> Unit1Id
			// PropertyMapping: LowerLimitUnit2 -> LowerLimitUnit2
			// PropertyMapping: NominalValueUnit2 -> NominalValueUnit2
			// PropertyMapping: UpperLimitUnit2 -> UpperLimitUnit2
			// PropertyMapping: Unit2Id -> Unit2Id
			// PropertyMapping: TestValueMinimum -> TestValueMinimum
			// PropertyMapping: TestValueMaximum -> TestValueMaximum
			// PropertyMapping: Average -> Average
			// PropertyMapping: StandardDeviation -> StandardDeviation
			// PropertyMapping: ControlledByUnitId -> ControlledByUnitId
			// PropertyMapping: ThresholdTorque -> ThresholdTorque
			// PropertyMapping: SensorSerialNumber -> SensorSerialNumber
			// PropertyMapping: Result -> Result
			// PropertyMapping: ToleranceClassUnit1 -> ToleranceClassUnit1
			// PropertyMapping: ToleranceClassUnit2 -> ToleranceClassUnit2
			// PropertyMapping: TestLocation -> TestLocation
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: Cm -> Cm
			// PropertyMapping: Cmk -> Cmk
			// PropertyMapping: LimitCm -> LimitCm
			// PropertyMapping: LimitCmk -> LimitCmk
			// PropertyMapping: ToolId -> ToolId
			// PropertyMapping: User -> User
			// PropertyMapping: TestValues -> TestValues
			// PropertyMapping: LocationToolAssignmentId -> LocationToolAssignmentId
		public DtoTypes.ClassicMfuTest DirectPropertyMapping(Server.Core.Entities.ClassicMfuTest source)
		{
			var target = new DtoTypes.ClassicMfuTest();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.Cm = value;}, source.Cm);
			_assigner.Assign((value) => {target.Cmk = value;}, source.Cmk);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicMfuTest source, DtoTypes.ClassicMfuTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.Cm = value;}, source.Cm);
			_assigner.Assign((value) => {target.Cmk = value;}, source.Cmk);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.TestValues = value;}, source.TestValues);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
		}

		public DtoTypes.ClassicMfuTest ReflectedPropertyMapping(Server.Core.Entities.ClassicMfuTest source)
		{
			var target = new DtoTypes.ClassicMfuTest();
			typeof(DtoTypes.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicMfuTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(DtoTypes.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DtoTypes.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DtoTypes.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DtoTypes.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DtoTypes.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DtoTypes.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DtoTypes.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DtoTypes.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DtoTypes.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DtoTypes.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(DtoTypes.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(DtoTypes.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(DtoTypes.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(DtoTypes.ClassicMfuTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DtoTypes.ClassicMfuTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(DtoTypes.ClassicMfuTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicMfuTest source, DtoTypes.ClassicMfuTest target)
		{
			typeof(DtoTypes.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicMfuTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(DtoTypes.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DtoTypes.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DtoTypes.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DtoTypes.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DtoTypes.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DtoTypes.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DtoTypes.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DtoTypes.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DtoTypes.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DtoTypes.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DtoTypes.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(DtoTypes.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(DtoTypes.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(DtoTypes.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(DtoTypes.ClassicMfuTest).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DtoTypes.ClassicMfuTest).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.ClassicMfuTest).GetField("TestValues", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValues);
			typeof(DtoTypes.ClassicMfuTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
		}

		// ClassicMfuTestValueDtoToClassicMfuTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public Server.Core.Entities.ClassicMfuTestValue DirectPropertyMapping(DtoTypes.ClassicMfuTestValue source)
		{
			var target = new Server.Core.Entities.ClassicMfuTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicMfuTestValue source, Server.Core.Entities.ClassicMfuTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public Server.Core.Entities.ClassicMfuTestValue ReflectedPropertyMapping(DtoTypes.ClassicMfuTestValue source)
		{
			var target = new Server.Core.Entities.ClassicMfuTestValue();
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicMfuTestValue source, Server.Core.Entities.ClassicMfuTestValue target)
		{
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
		}

		// ClassicMfuTestValueToClassicMfuTestValueDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public DtoTypes.ClassicMfuTestValue DirectPropertyMapping(Server.Core.Entities.ClassicMfuTestValue source)
		{
			var target = new DtoTypes.ClassicMfuTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicMfuTestValue source, DtoTypes.ClassicMfuTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public DtoTypes.ClassicMfuTestValue ReflectedPropertyMapping(Server.Core.Entities.ClassicMfuTestValue source)
		{
			var target = new DtoTypes.ClassicMfuTestValue();
			typeof(DtoTypes.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(DtoTypes.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(DtoTypes.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicMfuTestValue source, DtoTypes.ClassicMfuTestValue target)
		{
			typeof(DtoTypes.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(DtoTypes.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(DtoTypes.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
		}

		// ClassicProcessTestToClassicProcessTestDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Timestamp -> Timestamp
			// PropertyMapping: NumberOfTests -> NumberOfTests
			// PropertyMapping: ControlledByUnitId -> ControlledByUnitId
			// PropertyMapping: Unit1Id -> Unit1Id
			// PropertyMapping: LowerLimitUnit1 -> LowerLimitUnit1
			// PropertyMapping: NominalValueUnit1 -> NominalValueUnit1
			// PropertyMapping: UpperLimitUnit1 -> UpperLimitUnit1
			// PropertyMapping: LowerLimitUnit2 -> LowerLimitUnit2
			// PropertyMapping: NominalValueUnit2 -> NominalValueUnit2
			// PropertyMapping: UpperLimitUnit2 -> UpperLimitUnit2
			// PropertyMapping: LowerInterventionLimitUnit1 -> LowerInterventionLimitUnit1
			// PropertyMapping: UpperInterventionLimitUnit1 -> UpperInterventionLimitUnit1
			// PropertyMapping: LowerInterventionLimitUnit2 -> LowerInterventionLimitUnit2
			// PropertyMapping: UpperInterventionLimitUnit2 -> UpperInterventionLimitUnit2
			// PropertyMapping: Result -> Result
			// PropertyMapping: Unit2Id -> Unit2Id
			// PropertyMapping: TestValueMinimum -> TestValueMinimum
			// PropertyMapping: TestValueMaximum -> TestValueMaximum
			// PropertyMapping: Average -> Average
			// PropertyMapping: StandardDeviation -> StandardDeviation
			// PropertyMapping: TestLocation -> TestLocation
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: ToleranceClassUnit1 -> ToleranceClassUnit1
			// PropertyMapping: ToleranceClassUnit2 -> ToleranceClassUnit2
		public DtoTypes.ClassicProcessTest DirectPropertyMapping(Server.Core.Entities.ClassicProcessTest source)
		{
			var target = new DtoTypes.ClassicProcessTest();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit1 = value;}, source.LowerInterventionLimitUnit1);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit1 = value;}, source.UpperInterventionLimitUnit1);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit2 = value;}, source.LowerInterventionLimitUnit2);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit2 = value;}, source.UpperInterventionLimitUnit2);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicProcessTest source, DtoTypes.ClassicProcessTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Timestamp = value;}, source.Timestamp);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit1 = value;}, source.LowerInterventionLimitUnit1);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit1 = value;}, source.UpperInterventionLimitUnit1);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit2 = value;}, source.LowerInterventionLimitUnit2);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit2 = value;}, source.UpperInterventionLimitUnit2);
			_assigner.Assign((value) => {target.Result = value;}, source.Result);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.Average = value;}, source.Average);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.ToleranceClassUnit2);
		}

		public DtoTypes.ClassicProcessTest ReflectedPropertyMapping(Server.Core.Entities.ClassicProcessTest source)
		{
			var target = new DtoTypes.ClassicProcessTest();
			typeof(DtoTypes.ClassicProcessTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicProcessTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(DtoTypes.ClassicProcessTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DtoTypes.ClassicProcessTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DtoTypes.ClassicProcessTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DtoTypes.ClassicProcessTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DtoTypes.ClassicProcessTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DtoTypes.ClassicProcessTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DtoTypes.ClassicProcessTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicProcessTest source, DtoTypes.ClassicProcessTest target)
		{
			typeof(DtoTypes.ClassicProcessTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicProcessTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(DtoTypes.ClassicProcessTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DtoTypes.ClassicProcessTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DtoTypes.ClassicProcessTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("LowerInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("UpperInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit2);
			typeof(DtoTypes.ClassicProcessTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DtoTypes.ClassicProcessTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DtoTypes.ClassicProcessTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DtoTypes.ClassicProcessTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(DtoTypes.ClassicProcessTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DtoTypes.ClassicProcessTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DtoTypes.ClassicProcessTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
		}

		// ClassicProcessTestValueToClassicProcessTestValueDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public DtoTypes.ClassicProcessTestValue DirectPropertyMapping(Server.Core.Entities.ClassicProcessTestValue source)
		{
			var target = new DtoTypes.ClassicProcessTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicProcessTestValue source, DtoTypes.ClassicProcessTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public DtoTypes.ClassicProcessTestValue ReflectedPropertyMapping(Server.Core.Entities.ClassicProcessTestValue source)
		{
			var target = new DtoTypes.ClassicProcessTestValue();
			typeof(DtoTypes.ClassicProcessTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicProcessTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(DtoTypes.ClassicProcessTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(DtoTypes.ClassicProcessTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicProcessTestValue source, DtoTypes.ClassicProcessTestValue target)
		{
			typeof(DtoTypes.ClassicProcessTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ClassicProcessTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(DtoTypes.ClassicProcessTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(DtoTypes.ClassicProcessTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
		}

		// ClassicTestLocationDtoToClassicTestLocation.txt
			// hasDefaultConstructor
			// PropertyMapping: LocationId -> LocationId
			// PropertyMapping: LocationDirectoryId -> LocationDirectoryId
			// PropertyMapping: TreePath -> LocationTreePath
		public Server.Core.Entities.ClassicTestLocation DirectPropertyMapping(DtoTypes.ClassicTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LocationDirectoryId);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.TreePath);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LocationDirectoryId);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.TreePath);
		}

		public Server.Core.Entities.ClassicTestLocation ReflectedPropertyMapping(DtoTypes.ClassicTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDirectoryId);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TreePath);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDirectoryId);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TreePath);
		}

		// ClassicTestLocationToClassicTestLocationDto.txt
			// hasDefaultConstructor
			// PropertyMapping: LocationId -> LocationId
			// PropertyMapping: LocationDirectoryId -> LocationDirectoryId
			// PropertyMapping: LocationTreePath -> TreePath
		public DtoTypes.ClassicTestLocation DirectPropertyMapping(Server.Core.Entities.ClassicTestLocation source)
		{
			var target = new DtoTypes.ClassicTestLocation();
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LocationDirectoryId);
			_assigner.Assign((value) => {target.TreePath = value;}, source.LocationTreePath);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicTestLocation source, DtoTypes.ClassicTestLocation target)
		{
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LocationDirectoryId);
			_assigner.Assign((value) => {target.TreePath = value;}, source.LocationTreePath);
		}

		public DtoTypes.ClassicTestLocation ReflectedPropertyMapping(Server.Core.Entities.ClassicTestLocation source)
		{
			var target = new DtoTypes.ClassicTestLocation();
			typeof(DtoTypes.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(DtoTypes.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDirectoryId);
			typeof(DtoTypes.ClassicTestLocation).GetField("TreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationTreePath);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicTestLocation source, DtoTypes.ClassicTestLocation target)
		{
			typeof(DtoTypes.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(DtoTypes.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDirectoryId);
			typeof(DtoTypes.ClassicTestLocation).GetField("TreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationTreePath);
		}

		// ExtensionDtoToExtension.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Description
			// PropertyMapping: Length -> Length
			// PropertyMapping: FactorTorque -> FactorTorque
			// PropertyMapping: Bending -> Bending
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: Alive -> Alive
		public Server.Core.Entities.Extension DirectPropertyMapping(DtoTypes.Extension source)
		{
			var target = new Server.Core.Entities.Extension();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Extension source, Server.Core.Entities.Extension target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public Server.Core.Entities.Extension ReflectedPropertyMapping(DtoTypes.Extension source)
		{
			var target = new Server.Core.Entities.Extension();
			typeof(Server.Core.Entities.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(Server.Core.Entities.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(Server.Core.Entities.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(Server.Core.Entities.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Server.Core.Entities.Extension).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Extension source, Server.Core.Entities.Extension target)
		{
			typeof(Server.Core.Entities.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(Server.Core.Entities.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(Server.Core.Entities.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(Server.Core.Entities.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Server.Core.Entities.Extension).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// ExtensionToExtensionDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Description
			// PropertyMapping: Length -> Length
			// PropertyMapping: FactorTorque -> FactorTorque
			// PropertyMapping: Bending -> Bending
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: Alive -> Alive
		public DtoTypes.Extension DirectPropertyMapping(Server.Core.Entities.Extension source)
		{
			var target = new DtoTypes.Extension();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Extension source, DtoTypes.Extension target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public DtoTypes.Extension ReflectedPropertyMapping(Server.Core.Entities.Extension source)
		{
			var target = new DtoTypes.Extension();
			typeof(DtoTypes.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(DtoTypes.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(DtoTypes.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(DtoTypes.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.Extension).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Extension source, DtoTypes.Extension target)
		{
			typeof(DtoTypes.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(DtoTypes.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(DtoTypes.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(DtoTypes.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.Extension).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// HelperTableEntityDtoToHelperTableEntity.txt
			// hasDefaultConstructor
			// PropertyMapping: ListId -> ListId
			// PropertyMapping: Value -> Value
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: NodeId -> NodeId
		public Server.Core.Entities.HelperTableEntity DirectPropertyMapping(DtoTypes.HelperTableEntity source)
		{
			var target = new Server.Core.Entities.HelperTableEntity();
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NodeId);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, Server.Core.Entities.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NodeId);
		}

		public Server.Core.Entities.HelperTableEntity ReflectedPropertyMapping(DtoTypes.HelperTableEntity source)
		{
			var target = new Server.Core.Entities.HelperTableEntity();
			typeof(Server.Core.Entities.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, Server.Core.Entities.HelperTableEntity target)
		{
			typeof(Server.Core.Entities.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
		}

		// HelperTableEntityToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: ListId -> ListId
			// PropertyMapping: Value -> Value
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: NodeId -> NodeId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(Server.Core.Entities.HelperTableEntity source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NodeId);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.HelperTableEntity source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NodeId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(Server.Core.Entities.HelperTableEntity source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.HelperTableEntity).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.HelperTableEntity source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.HelperTableEntity).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
		}

		// LocationDiffToLocationDiffDto.txt
			// hasDefaultConstructor
			// PropertyMapping: OldLocation -> OldLocation
			// PropertyMapping: NewLocation -> NewLocation
			// PropertyMapping: User -> User
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: Type -> Type
			// PropertyMapping: TimeStamp -> TimeStamp
		public DtoTypes.LocationDiff DirectPropertyMapping(Server.Core.Diffs.LocationDiff source)
		{
			var target = new DtoTypes.LocationDiff();
			_assigner.Assign((value) => {target.OldLocation = value;}, source.OldLocation);
			_assigner.Assign((value) => {target.NewLocation = value;}, source.NewLocation);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TimeStamp = value;}, source.TimeStamp);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Diffs.LocationDiff source, DtoTypes.LocationDiff target)
		{
			_assigner.Assign((value) => {target.OldLocation = value;}, source.OldLocation);
			_assigner.Assign((value) => {target.NewLocation = value;}, source.NewLocation);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TimeStamp = value;}, source.TimeStamp);
		}

		public DtoTypes.LocationDiff ReflectedPropertyMapping(Server.Core.Diffs.LocationDiff source)
		{
			var target = new DtoTypes.LocationDiff();
			typeof(DtoTypes.LocationDiff).GetField("OldLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.OldLocation);
			typeof(DtoTypes.LocationDiff).GetField("NewLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NewLocation);
			typeof(DtoTypes.LocationDiff).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.LocationDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DtoTypes.LocationDiff).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(DtoTypes.LocationDiff).GetField("TimeStamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TimeStamp);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Diffs.LocationDiff source, DtoTypes.LocationDiff target)
		{
			typeof(DtoTypes.LocationDiff).GetField("OldLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.OldLocation);
			typeof(DtoTypes.LocationDiff).GetField("NewLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NewLocation);
			typeof(DtoTypes.LocationDiff).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.LocationDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DtoTypes.LocationDiff).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(DtoTypes.LocationDiff).GetField("TimeStamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TimeStamp);
		}

		// LocationDirectoryDtoToLocationDirectory.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: ParentId -> ParentId
			// PropertyMapping: Alive -> Alive
		public Server.Core.Entities.LocationDirectory DirectPropertyMapping(DtoTypes.LocationDirectory source)
		{
			var target = new Server.Core.Entities.LocationDirectory();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.LocationDirectory source, Server.Core.Entities.LocationDirectory target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public Server.Core.Entities.LocationDirectory ReflectedPropertyMapping(DtoTypes.LocationDirectory source)
		{
			var target = new Server.Core.Entities.LocationDirectory();
			typeof(Server.Core.Entities.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.LocationDirectory source, Server.Core.Entities.LocationDirectory target)
		{
			typeof(Server.Core.Entities.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// LocationDirectoryToLocationDirectoryDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: ParentId -> ParentId
			// PropertyMapping: Alive -> Alive
		public DtoTypes.LocationDirectory DirectPropertyMapping(Server.Core.Entities.LocationDirectory source)
		{
			var target = new DtoTypes.LocationDirectory();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.LocationDirectory source, DtoTypes.LocationDirectory target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public DtoTypes.LocationDirectory ReflectedPropertyMapping(Server.Core.Entities.LocationDirectory source)
		{
			var target = new DtoTypes.LocationDirectory();
			typeof(DtoTypes.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			typeof(DtoTypes.LocationDirectory).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.LocationDirectory source, DtoTypes.LocationDirectory target)
		{
			typeof(DtoTypes.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			typeof(DtoTypes.LocationDirectory).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// LocationDtoToLocation.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Number -> Number
			// PropertyMapping: Description -> Description
			// PropertyMapping: ParentDirectoryId -> ParentDirectoryId
			// PropertyMapping: ControlledBy -> ControlledBy
			// PropertyMapping: SetPoint1 -> SetPoint1
			// PropertyMapping: ToleranceClass1 -> ToleranceClass1
			// PropertyMapping: Minimum1 -> Minimum1
			// PropertyMapping: Maximum1 -> Maximum1
			// PropertyMapping: Threshold1 -> Threshold1
			// PropertyMapping: SetPoint2 -> SetPoint2
			// PropertyMapping: ToleranceClass2 -> ToleranceClass2
			// PropertyMapping: Minimum2 -> Minimum2
			// PropertyMapping: Maximum2 -> Maximum2
			// PropertyMapping: ConfigurableField1 -> ConfigurableField1
			// PropertyMapping: ConfigurableField2 -> ConfigurableField2
			// PropertyMapping: ConfigurableField3 -> ConfigurableField3
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: Comment -> Comment
		public Server.Core.Entities.Location DirectPropertyMapping(DtoTypes.Location source)
		{
			var target = new Server.Core.Entities.Location();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Location source, Server.Core.Entities.Location target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
		}

		public Server.Core.Entities.Location ReflectedPropertyMapping(DtoTypes.Location source)
		{
			var target = new Server.Core.Entities.Location();
			typeof(Server.Core.Entities.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(Server.Core.Entities.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(Server.Core.Entities.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(Server.Core.Entities.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(Server.Core.Entities.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(Server.Core.Entities.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(Server.Core.Entities.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(Server.Core.Entities.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(Server.Core.Entities.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(Server.Core.Entities.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(Server.Core.Entities.Location).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Location source, Server.Core.Entities.Location target)
		{
			typeof(Server.Core.Entities.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(Server.Core.Entities.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(Server.Core.Entities.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(Server.Core.Entities.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(Server.Core.Entities.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(Server.Core.Entities.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(Server.Core.Entities.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(Server.Core.Entities.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(Server.Core.Entities.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(Server.Core.Entities.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(Server.Core.Entities.Location).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
		}

		// LocationReferenceLinkToLocationLinkDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Number -> Number
			// PropertyMapping: Description -> Description
		public DtoTypes.LocationLink DirectPropertyMapping(Server.Core.Entities.ReferenceLink.LocationReferenceLink source)
		{
			var target = new DtoTypes.LocationLink();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ReferenceLink.LocationReferenceLink source, DtoTypes.LocationLink target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
		}

		public DtoTypes.LocationLink ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.LocationReferenceLink source)
		{
			var target = new DtoTypes.LocationLink();
			typeof(DtoTypes.LocationLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationLink).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DtoTypes.LocationLink).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.LocationReferenceLink source, DtoTypes.LocationLink target)
		{
			typeof(DtoTypes.LocationLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationLink).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DtoTypes.LocationLink).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
		}

		// LocationToLocationDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Number -> Number
			// PropertyMapping: Description -> Description
			// PropertyMapping: ParentDirectoryId -> ParentDirectoryId
			// PropertyMapping: ControlledBy -> ControlledBy
			// PropertyMapping: SetPoint1 -> SetPoint1
			// PropertyMapping: ToleranceClass1 -> ToleranceClass1
			// PropertyMapping: Minimum1 -> Minimum1
			// PropertyMapping: Maximum1 -> Maximum1
			// PropertyMapping: Threshold1 -> Threshold1
			// PropertyMapping: SetPoint2 -> SetPoint2
			// PropertyMapping: ToleranceClass2 -> ToleranceClass2
			// PropertyMapping: Minimum2 -> Minimum2
			// PropertyMapping: Maximum2 -> Maximum2
			// PropertyMapping: ConfigurableField1 -> ConfigurableField1
			// PropertyMapping: ConfigurableField2 -> ConfigurableField2
			// PropertyMapping: ConfigurableField3 -> ConfigurableField3
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: Comment -> Comment
		public DtoTypes.Location DirectPropertyMapping(Server.Core.Entities.Location source)
		{
			var target = new DtoTypes.Location();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Location source, DtoTypes.Location target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
		}

		public DtoTypes.Location ReflectedPropertyMapping(Server.Core.Entities.Location source)
		{
			var target = new DtoTypes.Location();
			typeof(DtoTypes.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DtoTypes.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(DtoTypes.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(DtoTypes.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(DtoTypes.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(DtoTypes.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(DtoTypes.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(DtoTypes.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(DtoTypes.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(DtoTypes.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(DtoTypes.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(DtoTypes.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(DtoTypes.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(DtoTypes.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(DtoTypes.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(DtoTypes.Location).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Location source, DtoTypes.Location target)
		{
			typeof(DtoTypes.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DtoTypes.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(DtoTypes.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(DtoTypes.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(DtoTypes.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(DtoTypes.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(DtoTypes.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(DtoTypes.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(DtoTypes.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(DtoTypes.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(DtoTypes.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(DtoTypes.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(DtoTypes.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(DtoTypes.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(DtoTypes.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(DtoTypes.Location).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
		}

		// LocationToolAssignmentDtoToLocationToolAssignment.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: AssignedLocation -> AssignedLocation
			// PropertyMapping: AssignedTool -> AssignedTool
			// PropertyMapping: ToolUsage -> ToolUsage
			// PropertyMapping: TestParameters -> TestParameters
			// PropertyMapping: TestTechnique -> TestTechnique
			// PropertyMapping: TestLevelSetMfu -> TestLevelSetMfu
			// PropertyMapping: TestLevelNumberMfu -> TestLevelNumberMfu
			// PropertyMapping: TestLevelSetChk -> TestLevelSetChk
			// PropertyMapping: TestLevelNumberChk -> TestLevelNumberChk
			// PropertyMapping: NextTestDateMfu -> NextTestDateMfu
			// PropertyMapping: NextTestShiftMfu -> NextTestShiftMfu
			// PropertyMapping: NextTestDateChk -> NextTestDateChk
			// PropertyMapping: NextTestShiftChk -> NextTestShiftChk
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: StartDateMfu -> StartDateMfu
			// PropertyMapping: StartDateChk -> StartDateChk
			// PropertyMapping: TestOperationActiveMfu -> TestOperationActiveMfu
			// PropertyMapping: TestOperationActiveChk -> TestOperationActiveChk
		public Server.Core.Entities.LocationToolAssignment DirectPropertyMapping(DtoTypes.LocationToolAssignment source)
		{
			var target = new Server.Core.Entities.LocationToolAssignment();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.AssignedLocation = value;}, source.AssignedLocation);
			_assigner.Assign((value) => {target.AssignedTool = value;}, source.AssignedTool);
			_assigner.Assign((value) => {target.ToolUsage = value;}, source.ToolUsage);
			_assigner.Assign((value) => {target.TestParameters = value;}, source.TestParameters);
			_assigner.Assign((value) => {target.TestTechnique = value;}, source.TestTechnique);
			_assigner.Assign((value) => {target.TestLevelSetMfu = value;}, source.TestLevelSetMfu);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelSetChk = value;}, source.TestLevelSetChk);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.NextTestDateMfu = value;}, source.NextTestDateMfu);
			_assigner.Assign((value) => {target.NextTestShiftMfu = value;}, source.NextTestShiftMfu);
			_assigner.Assign((value) => {target.NextTestDateChk = value;}, source.NextTestDateChk);
			_assigner.Assign((value) => {target.NextTestShiftChk = value;}, source.NextTestShiftChk);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.LocationToolAssignment source, Server.Core.Entities.LocationToolAssignment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.AssignedLocation = value;}, source.AssignedLocation);
			_assigner.Assign((value) => {target.AssignedTool = value;}, source.AssignedTool);
			_assigner.Assign((value) => {target.ToolUsage = value;}, source.ToolUsage);
			_assigner.Assign((value) => {target.TestParameters = value;}, source.TestParameters);
			_assigner.Assign((value) => {target.TestTechnique = value;}, source.TestTechnique);
			_assigner.Assign((value) => {target.TestLevelSetMfu = value;}, source.TestLevelSetMfu);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelSetChk = value;}, source.TestLevelSetChk);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.NextTestDateMfu = value;}, source.NextTestDateMfu);
			_assigner.Assign((value) => {target.NextTestShiftMfu = value;}, source.NextTestShiftMfu);
			_assigner.Assign((value) => {target.NextTestDateChk = value;}, source.NextTestDateChk);
			_assigner.Assign((value) => {target.NextTestShiftChk = value;}, source.NextTestShiftChk);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
		}

		public Server.Core.Entities.LocationToolAssignment ReflectedPropertyMapping(DtoTypes.LocationToolAssignment source)
		{
			var target = new Server.Core.Entities.LocationToolAssignment();
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("AssignedLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedLocation);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("AssignedTool", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedTool);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("ToolUsage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsage);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestParameters", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestParameters);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestTechnique", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestTechnique);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelSetMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelSetChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.LocationToolAssignment source, Server.Core.Entities.LocationToolAssignment target)
		{
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("AssignedLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedLocation);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("AssignedTool", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedTool);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("ToolUsage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsage);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestParameters", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestParameters);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestTechnique", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestTechnique);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelSetMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelSetChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
		}

		// LocationToolAssignmentReferenceLinkToDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: LocationId -> LocationId
			// PropertyMapping: LocationName -> LocationName
			// PropertyMapping: LocationNumber -> LocationNumber
			// PropertyMapping: ToolId -> ToolId
			// PropertyMapping: ToolInventoryNumber -> ToolInventoryNumber
			// PropertyMapping: ToolSerialNumber -> ToolSerialNumber
		public DtoTypes.LocationToolAssignmentReferenceLink DirectPropertyMapping(Server.Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink source)
		{
			var target = new DtoTypes.LocationToolAssignmentReferenceLink();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationName = value;}, source.LocationName);
			_assigner.Assign((value) => {target.LocationNumber = value;}, source.LocationNumber);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.ToolInventoryNumber = value;}, source.ToolInventoryNumber);
			_assigner.Assign((value) => {target.ToolSerialNumber = value;}, source.ToolSerialNumber);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink source, DtoTypes.LocationToolAssignmentReferenceLink target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationName = value;}, source.LocationName);
			_assigner.Assign((value) => {target.LocationNumber = value;}, source.LocationNumber);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.ToolInventoryNumber = value;}, source.ToolInventoryNumber);
			_assigner.Assign((value) => {target.ToolSerialNumber = value;}, source.ToolSerialNumber);
		}

		public DtoTypes.LocationToolAssignmentReferenceLink ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink source)
		{
			var target = new DtoTypes.LocationToolAssignmentReferenceLink();
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("LocationName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationName);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("LocationNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationNumber);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("ToolInventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolInventoryNumber);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("ToolSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolSerialNumber);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.LocationToolAssignmentReferenceLink source, DtoTypes.LocationToolAssignmentReferenceLink target)
		{
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("LocationName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationName);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("LocationNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationNumber);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("ToolInventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolInventoryNumber);
			typeof(DtoTypes.LocationToolAssignmentReferenceLink).GetField("ToolSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolSerialNumber);
		}

		// LocationToolAssignmentsForTransferToLocationToolAssignmentsForTransferDto.txt
			// hasDefaultConstructor
			// PropertyMapping: LocationToolAssignmentId -> LocationToolAssignmentId
			// PropertyMapping: LocationNumber -> LocationNumber
			// PropertyMapping: LocationId -> LocationId
			// PropertyMapping: LocationDescription -> LocationDescription
			// PropertyMapping: LocationFreeFieldCategory -> LocationFreeFieldCategory
			// PropertyMapping: LocationFreeFieldDocumentation -> LocationFreeFieldDocumentation
			// PropertyMapping: ToolUsageId -> ToolUsageId
			// PropertyMapping: ToolUsageDescription -> ToolUsageDescription
			// PropertyMapping: ToolId -> ToolId
			// PropertyMapping: ToolSerialNumber -> ToolSerialNumber
			// PropertyMapping: ToolInventoryNumber -> ToolInventoryNumber
			// PropertyMapping: SampleNumber -> SampleNumber
			// PropertyMapping: TestInterval -> TestInterval
			// PropertyMapping: NextTestDate -> NextTestDate
			// PropertyMapping: LastTestDate -> LastTestDate
			// PropertyMapping: NextTestDateShift -> NextTestDateShift
		public TransferToTestEquipmentService.LocationToolAssignmentForTransfer DirectPropertyMapping(Server.Core.Entities.LocationToolAssignmentForTransfer source)
		{
			var target = new TransferToTestEquipmentService.LocationToolAssignmentForTransfer();
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
			_assigner.Assign((value) => {target.LocationNumber = value;}, source.LocationNumber);
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationDescription = value;}, source.LocationDescription);
			_assigner.Assign((value) => {target.LocationFreeFieldCategory = value;}, source.LocationFreeFieldCategory);
			_assigner.Assign((value) => {target.LocationFreeFieldDocumentation = value;}, source.LocationFreeFieldDocumentation);
			_assigner.Assign((value) => {target.ToolUsageId = value;}, source.ToolUsageId);
			_assigner.Assign((value) => {target.ToolUsageDescription = value;}, source.ToolUsageDescription);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.ToolSerialNumber = value;}, source.ToolSerialNumber);
			_assigner.Assign((value) => {target.ToolInventoryNumber = value;}, source.ToolInventoryNumber);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.LastTestDate = value;}, source.LastTestDate);
			_assigner.Assign((value) => {target.NextTestDateShift = value;}, source.NextTestDateShift);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.LocationToolAssignmentForTransfer source, TransferToTestEquipmentService.LocationToolAssignmentForTransfer target)
		{
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocationToolAssignmentId);
			_assigner.Assign((value) => {target.LocationNumber = value;}, source.LocationNumber);
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationDescription = value;}, source.LocationDescription);
			_assigner.Assign((value) => {target.LocationFreeFieldCategory = value;}, source.LocationFreeFieldCategory);
			_assigner.Assign((value) => {target.LocationFreeFieldDocumentation = value;}, source.LocationFreeFieldDocumentation);
			_assigner.Assign((value) => {target.ToolUsageId = value;}, source.ToolUsageId);
			_assigner.Assign((value) => {target.ToolUsageDescription = value;}, source.ToolUsageDescription);
			_assigner.Assign((value) => {target.ToolId = value;}, source.ToolId);
			_assigner.Assign((value) => {target.ToolSerialNumber = value;}, source.ToolSerialNumber);
			_assigner.Assign((value) => {target.ToolInventoryNumber = value;}, source.ToolInventoryNumber);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.LastTestDate = value;}, source.LastTestDate);
			_assigner.Assign((value) => {target.NextTestDateShift = value;}, source.NextTestDateShift);
		}

		public TransferToTestEquipmentService.LocationToolAssignmentForTransfer ReflectedPropertyMapping(Server.Core.Entities.LocationToolAssignmentForTransfer source)
		{
			var target = new TransferToTestEquipmentService.LocationToolAssignmentForTransfer();
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationDescription", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDescription);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationFreeFieldCategory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationFreeFieldCategory);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationFreeFieldDocumentation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationFreeFieldDocumentation);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolUsageId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsageId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolUsageDescription", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsageDescription);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolSerialNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolInventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolInventoryNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LastTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastTestDate);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("NextTestDateShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateShift);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.LocationToolAssignmentForTransfer source, TransferToTestEquipmentService.LocationToolAssignmentForTransfer target)
		{
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationDescription", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDescription);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationFreeFieldCategory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationFreeFieldCategory);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LocationFreeFieldDocumentation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationFreeFieldDocumentation);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolUsageId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsageId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolUsageDescription", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsageDescription);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolSerialNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("ToolInventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolInventoryNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("LastTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastTestDate);
			typeof(TransferToTestEquipmentService.LocationToolAssignmentForTransfer).GetField("NextTestDateShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateShift);
		}

		// LocationToolAssignmentToLocationToolAssignmentDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: AssignedLocation -> AssignedLocation
			// PropertyMapping: AssignedTool -> AssignedTool
			// PropertyMapping: ToolUsage -> ToolUsage
			// PropertyMapping: TestParameters -> TestParameters
			// PropertyMapping: TestTechnique -> TestTechnique
			// PropertyMapping: TestLevelSetMfu -> TestLevelSetMfu
			// PropertyMapping: TestLevelNumberMfu -> TestLevelNumberMfu
			// PropertyMapping: TestLevelSetChk -> TestLevelSetChk
			// PropertyMapping: TestLevelNumberChk -> TestLevelNumberChk
			// PropertyMapping: NextTestDateMfu -> NextTestDateMfu
			// PropertyMapping: NextTestShiftMfu -> NextTestShiftMfu
			// PropertyMapping: NextTestDateChk -> NextTestDateChk
			// PropertyMapping: NextTestShiftChk -> NextTestShiftChk
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: StartDateMfu -> StartDateMfu
			// PropertyMapping: StartDateChk -> StartDateChk
			// PropertyMapping: TestOperationActiveMfu -> TestOperationActiveMfu
			// PropertyMapping: TestOperationActiveChk -> TestOperationActiveChk
		public DtoTypes.LocationToolAssignment DirectPropertyMapping(Server.Core.Entities.LocationToolAssignment source)
		{
			var target = new DtoTypes.LocationToolAssignment();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.AssignedLocation = value;}, source.AssignedLocation);
			_assigner.Assign((value) => {target.AssignedTool = value;}, source.AssignedTool);
			_assigner.Assign((value) => {target.ToolUsage = value;}, source.ToolUsage);
			_assigner.Assign((value) => {target.TestParameters = value;}, source.TestParameters);
			_assigner.Assign((value) => {target.TestTechnique = value;}, source.TestTechnique);
			_assigner.Assign((value) => {target.TestLevelSetMfu = value;}, source.TestLevelSetMfu);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelSetChk = value;}, source.TestLevelSetChk);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.NextTestDateMfu = value;}, source.NextTestDateMfu);
			_assigner.Assign((value) => {target.NextTestShiftMfu = value;}, source.NextTestShiftMfu);
			_assigner.Assign((value) => {target.NextTestDateChk = value;}, source.NextTestDateChk);
			_assigner.Assign((value) => {target.NextTestShiftChk = value;}, source.NextTestShiftChk);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.LocationToolAssignment source, DtoTypes.LocationToolAssignment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.AssignedLocation = value;}, source.AssignedLocation);
			_assigner.Assign((value) => {target.AssignedTool = value;}, source.AssignedTool);
			_assigner.Assign((value) => {target.ToolUsage = value;}, source.ToolUsage);
			_assigner.Assign((value) => {target.TestParameters = value;}, source.TestParameters);
			_assigner.Assign((value) => {target.TestTechnique = value;}, source.TestTechnique);
			_assigner.Assign((value) => {target.TestLevelSetMfu = value;}, source.TestLevelSetMfu);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelSetChk = value;}, source.TestLevelSetChk);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.NextTestDateMfu = value;}, source.NextTestDateMfu);
			_assigner.Assign((value) => {target.NextTestShiftMfu = value;}, source.NextTestShiftMfu);
			_assigner.Assign((value) => {target.NextTestDateChk = value;}, source.NextTestDateChk);
			_assigner.Assign((value) => {target.NextTestShiftChk = value;}, source.NextTestShiftChk);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
		}

		public DtoTypes.LocationToolAssignment ReflectedPropertyMapping(Server.Core.Entities.LocationToolAssignment source)
		{
			var target = new DtoTypes.LocationToolAssignment();
			typeof(DtoTypes.LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationToolAssignment).GetField("AssignedLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedLocation);
			typeof(DtoTypes.LocationToolAssignment).GetField("AssignedTool", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedTool);
			typeof(DtoTypes.LocationToolAssignment).GetField("ToolUsage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsage);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestParameters", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestParameters);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestTechnique", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestTechnique);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelSetMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelSetChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.LocationToolAssignment source, DtoTypes.LocationToolAssignment target)
		{
			typeof(DtoTypes.LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationToolAssignment).GetField("AssignedLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedLocation);
			typeof(DtoTypes.LocationToolAssignment).GetField("AssignedTool", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedTool);
			typeof(DtoTypes.LocationToolAssignment).GetField("ToolUsage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsage);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestParameters", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestParameters);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestTechnique", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestTechnique);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelSetMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelSetChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("NextTestShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
		}

		// ManufacturerDtoToManufacturer.txt
			// hasDefaultConstructor
			// PropertyMapping: ManufacturerId -> Id
			// PropertyMapping: ManufactuerName -> Name
			// PropertyMapping: Street -> Street
			// PropertyMapping: City -> City
			// PropertyMapping: PhoneNumber -> PhoneNumber
			// PropertyMapping: Fax -> Fax
			// PropertyMapping: Person -> Person
			// PropertyMapping: Country -> Country
			// PropertyMapping: ZipCode -> Plz
			// PropertyMapping: HouseNumber -> HouseNumber
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: Alive -> Alive
		public Server.Core.Entities.Manufacturer DirectPropertyMapping(DtoTypes.Manufacturer source)
		{
			var target = new Server.Core.Entities.Manufacturer();
			_assigner.Assign((value) => {target.Id = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.Name = value;}, source.ManufactuerName);
			_assigner.Assign((value) => {target.Street = value;}, source.Street);
			_assigner.Assign((value) => {target.City = value;}, source.City);
			_assigner.Assign((value) => {target.PhoneNumber = value;}, source.PhoneNumber);
			_assigner.Assign((value) => {target.Fax = value;}, source.Fax);
			_assigner.Assign((value) => {target.Person = value;}, source.Person);
			_assigner.Assign((value) => {target.Country = value;}, source.Country);
			_assigner.Assign((value) => {target.Plz = value;}, source.ZipCode);
			_assigner.Assign((value) => {target.HouseNumber = value;}, source.HouseNumber);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Manufacturer source, Server.Core.Entities.Manufacturer target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.Name = value;}, source.ManufactuerName);
			_assigner.Assign((value) => {target.Street = value;}, source.Street);
			_assigner.Assign((value) => {target.City = value;}, source.City);
			_assigner.Assign((value) => {target.PhoneNumber = value;}, source.PhoneNumber);
			_assigner.Assign((value) => {target.Fax = value;}, source.Fax);
			_assigner.Assign((value) => {target.Person = value;}, source.Person);
			_assigner.Assign((value) => {target.Country = value;}, source.Country);
			_assigner.Assign((value) => {target.Plz = value;}, source.ZipCode);
			_assigner.Assign((value) => {target.HouseNumber = value;}, source.HouseNumber);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public Server.Core.Entities.Manufacturer ReflectedPropertyMapping(DtoTypes.Manufacturer source)
		{
			var target = new Server.Core.Entities.Manufacturer();
			typeof(Server.Core.Entities.Manufacturer).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(Server.Core.Entities.Manufacturer).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufactuerName);
			typeof(Server.Core.Entities.Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(Server.Core.Entities.Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(Server.Core.Entities.Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(Server.Core.Entities.Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(Server.Core.Entities.Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(Server.Core.Entities.Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(Server.Core.Entities.Manufacturer).GetField("Plz", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ZipCode);
			typeof(Server.Core.Entities.Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
			typeof(Server.Core.Entities.Manufacturer).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Server.Core.Entities.Manufacturer).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Manufacturer source, Server.Core.Entities.Manufacturer target)
		{
			typeof(Server.Core.Entities.Manufacturer).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(Server.Core.Entities.Manufacturer).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufactuerName);
			typeof(Server.Core.Entities.Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(Server.Core.Entities.Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(Server.Core.Entities.Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(Server.Core.Entities.Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(Server.Core.Entities.Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(Server.Core.Entities.Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(Server.Core.Entities.Manufacturer).GetField("Plz", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ZipCode);
			typeof(Server.Core.Entities.Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
			typeof(Server.Core.Entities.Manufacturer).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Server.Core.Entities.Manufacturer).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// ManufacturerToManufacturerDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> ManufacturerId
			// PropertyMapping: Name -> ManufactuerName
			// PropertyMapping: Street -> Street
			// PropertyMapping: City -> City
			// PropertyMapping: PhoneNumber -> PhoneNumber
			// PropertyMapping: Fax -> Fax
			// PropertyMapping: Person -> Person
			// PropertyMapping: Country -> Country
			// PropertyMapping: Plz -> ZipCode
			// PropertyMapping: HouseNumber -> HouseNumber
			// PropertyMapping: Comment -> Comment
		public DtoTypes.Manufacturer DirectPropertyMapping(Server.Core.Entities.Manufacturer source)
		{
			var target = new DtoTypes.Manufacturer();
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.Id);
			_assigner.Assign((value) => {target.ManufactuerName = value;}, source.Name);
			_assigner.Assign((value) => {target.Street = value;}, source.Street);
			_assigner.Assign((value) => {target.City = value;}, source.City);
			_assigner.Assign((value) => {target.PhoneNumber = value;}, source.PhoneNumber);
			_assigner.Assign((value) => {target.Fax = value;}, source.Fax);
			_assigner.Assign((value) => {target.Person = value;}, source.Person);
			_assigner.Assign((value) => {target.Country = value;}, source.Country);
			_assigner.Assign((value) => {target.ZipCode = value;}, source.Plz);
			_assigner.Assign((value) => {target.HouseNumber = value;}, source.HouseNumber);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Manufacturer source, DtoTypes.Manufacturer target)
		{
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.Id);
			_assigner.Assign((value) => {target.ManufactuerName = value;}, source.Name);
			_assigner.Assign((value) => {target.Street = value;}, source.Street);
			_assigner.Assign((value) => {target.City = value;}, source.City);
			_assigner.Assign((value) => {target.PhoneNumber = value;}, source.PhoneNumber);
			_assigner.Assign((value) => {target.Fax = value;}, source.Fax);
			_assigner.Assign((value) => {target.Person = value;}, source.Person);
			_assigner.Assign((value) => {target.Country = value;}, source.Country);
			_assigner.Assign((value) => {target.ZipCode = value;}, source.Plz);
			_assigner.Assign((value) => {target.HouseNumber = value;}, source.HouseNumber);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
		}

		public DtoTypes.Manufacturer ReflectedPropertyMapping(Server.Core.Entities.Manufacturer source)
		{
			var target = new DtoTypes.Manufacturer();
			typeof(DtoTypes.Manufacturer).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Manufacturer).GetField("ManufactuerName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(DtoTypes.Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(DtoTypes.Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(DtoTypes.Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(DtoTypes.Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(DtoTypes.Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(DtoTypes.Manufacturer).GetField("ZipCode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Plz);
			typeof(DtoTypes.Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
			typeof(DtoTypes.Manufacturer).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Manufacturer source, DtoTypes.Manufacturer target)
		{
			typeof(DtoTypes.Manufacturer).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Manufacturer).GetField("ManufactuerName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(DtoTypes.Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(DtoTypes.Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(DtoTypes.Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(DtoTypes.Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(DtoTypes.Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(DtoTypes.Manufacturer).GetField("ZipCode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Plz);
			typeof(DtoTypes.Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
			typeof(DtoTypes.Manufacturer).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
		}

		// PictureToPictureDto.txt
			// hasDefaultConstructor
			// PropertyMapping: SeqId -> Id
			// PropertyMapping: NodeId -> Nodeid
			// PropertyMapping: NodeSeqId -> Nodeseqid
			// PropertyMapping: FileName -> FileName
			// PropertyMapping: FileType -> FileType
		public DtoTypes.Picture DirectPropertyMapping(Server.Core.Entities.Picture source)
		{
			var target = new DtoTypes.Picture();
			_assigner.Assign((value) => {target.Id = value;}, source.SeqId);
			_assigner.Assign((value) => {target.Nodeid = value;}, source.NodeId);
			_assigner.Assign((value) => {target.Nodeseqid = value;}, source.NodeSeqId);
			_assigner.Assign((value) => {target.FileName = value;}, source.FileName);
			_assigner.Assign((value) => {target.FileType = value;}, source.FileType);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Picture source, DtoTypes.Picture target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.SeqId);
			_assigner.Assign((value) => {target.Nodeid = value;}, source.NodeId);
			_assigner.Assign((value) => {target.Nodeseqid = value;}, source.NodeSeqId);
			_assigner.Assign((value) => {target.FileName = value;}, source.FileName);
			_assigner.Assign((value) => {target.FileType = value;}, source.FileType);
		}

		public DtoTypes.Picture ReflectedPropertyMapping(Server.Core.Entities.Picture source)
		{
			var target = new DtoTypes.Picture();
			typeof(DtoTypes.Picture).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SeqId);
			typeof(DtoTypes.Picture).GetField("Nodeid", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			typeof(DtoTypes.Picture).GetField("Nodeseqid", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeSeqId);
			typeof(DtoTypes.Picture).GetField("FileName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileName);
			typeof(DtoTypes.Picture).GetField("FileType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileType);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Picture source, DtoTypes.Picture target)
		{
			typeof(DtoTypes.Picture).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SeqId);
			typeof(DtoTypes.Picture).GetField("Nodeid", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			typeof(DtoTypes.Picture).GetField("Nodeseqid", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeSeqId);
			typeof(DtoTypes.Picture).GetField("FileName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileName);
			typeof(DtoTypes.Picture).GetField("FileType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileType);
		}

		// ProcessControlConditionDtoToProcessControlCondition.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Location -> Location
			// PropertyMapping: UpperMeasuringLimit -> UpperMeasuringLimit
			// PropertyMapping: LowerMeasuringLimit -> LowerMeasuringLimit
			// PropertyMapping: UpperInterventionLimit -> UpperInterventionLimit
			// PropertyMapping: LowerInterventionLimit -> LowerInterventionLimit
			// PropertyMapping: TestLevelSet -> TestLevelSet
			// PropertyMapping: TestLevelNumber -> TestLevelNumber
			// PropertyMapping: TestOperationActive -> TestOperationActive
			// PropertyMapping: StartDate -> StartDate
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: ProcessControlTech -> ProcessControlTech
			// PropertyMapping: NextTestDate -> NextTestDate
			// PropertyMapping: NextTestShift -> NextTestShift
		public Server.Core.Entities.ProcessControlCondition DirectPropertyMapping(DtoTypes.ProcessControlCondition source)
		{
			var target = new Server.Core.Entities.ProcessControlCondition();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Location = value;}, source.Location);
			_assigner.Assign((value) => {target.UpperMeasuringLimit = value;}, source.UpperMeasuringLimit);
			_assigner.Assign((value) => {target.LowerMeasuringLimit = value;}, source.LowerMeasuringLimit);
			_assigner.Assign((value) => {target.UpperInterventionLimit = value;}, source.UpperInterventionLimit);
			_assigner.Assign((value) => {target.LowerInterventionLimit = value;}, source.LowerInterventionLimit);
			_assigner.Assign((value) => {target.TestLevelSet = value;}, source.TestLevelSet);
			_assigner.Assign((value) => {target.TestLevelNumber = value;}, source.TestLevelNumber);
			_assigner.Assign((value) => {target.TestOperationActive = value;}, source.TestOperationActive);
			_assigner.Assign((value) => {target.StartDate = value;}, source.StartDate);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ProcessControlCondition source, Server.Core.Entities.ProcessControlCondition target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Location = value;}, source.Location);
			_assigner.Assign((value) => {target.UpperMeasuringLimit = value;}, source.UpperMeasuringLimit);
			_assigner.Assign((value) => {target.LowerMeasuringLimit = value;}, source.LowerMeasuringLimit);
			_assigner.Assign((value) => {target.UpperInterventionLimit = value;}, source.UpperInterventionLimit);
			_assigner.Assign((value) => {target.LowerInterventionLimit = value;}, source.LowerInterventionLimit);
			_assigner.Assign((value) => {target.TestLevelSet = value;}, source.TestLevelSet);
			_assigner.Assign((value) => {target.TestLevelNumber = value;}, source.TestLevelNumber);
			_assigner.Assign((value) => {target.TestOperationActive = value;}, source.TestOperationActive);
			_assigner.Assign((value) => {target.StartDate = value;}, source.StartDate);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
		}

		public Server.Core.Entities.ProcessControlCondition ReflectedPropertyMapping(DtoTypes.ProcessControlCondition source)
		{
			var target = new Server.Core.Entities.ProcessControlCondition();
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Location", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ProcessControlCondition source, Server.Core.Entities.ProcessControlCondition target)
		{
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Location", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
		}

		// ProcessControlConditionToProcessControlConditionDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Location -> Location
			// PropertyMapping: UpperMeasuringLimit -> UpperMeasuringLimit
			// PropertyMapping: LowerMeasuringLimit -> LowerMeasuringLimit
			// PropertyMapping: UpperInterventionLimit -> UpperInterventionLimit
			// PropertyMapping: LowerInterventionLimit -> LowerInterventionLimit
			// PropertyMapping: TestLevelSet -> TestLevelSet
			// PropertyMapping: TestLevelNumber -> TestLevelNumber
			// PropertyMapping: TestOperationActive -> TestOperationActive
			// PropertyMapping: StartDate -> StartDate
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: ProcessControlTech -> ProcessControlTech
			// PropertyMapping: NextTestDate -> NextTestDate
			// PropertyMapping: NextTestShift -> NextTestShift
		public DtoTypes.ProcessControlCondition DirectPropertyMapping(Server.Core.Entities.ProcessControlCondition source)
		{
			var target = new DtoTypes.ProcessControlCondition();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Location = value;}, source.Location);
			_assigner.Assign((value) => {target.UpperMeasuringLimit = value;}, source.UpperMeasuringLimit);
			_assigner.Assign((value) => {target.LowerMeasuringLimit = value;}, source.LowerMeasuringLimit);
			_assigner.Assign((value) => {target.UpperInterventionLimit = value;}, source.UpperInterventionLimit);
			_assigner.Assign((value) => {target.LowerInterventionLimit = value;}, source.LowerInterventionLimit);
			_assigner.Assign((value) => {target.TestLevelSet = value;}, source.TestLevelSet);
			_assigner.Assign((value) => {target.TestLevelNumber = value;}, source.TestLevelNumber);
			_assigner.Assign((value) => {target.TestOperationActive = value;}, source.TestOperationActive);
			_assigner.Assign((value) => {target.StartDate = value;}, source.StartDate);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ProcessControlCondition source, DtoTypes.ProcessControlCondition target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Location = value;}, source.Location);
			_assigner.Assign((value) => {target.UpperMeasuringLimit = value;}, source.UpperMeasuringLimit);
			_assigner.Assign((value) => {target.LowerMeasuringLimit = value;}, source.LowerMeasuringLimit);
			_assigner.Assign((value) => {target.UpperInterventionLimit = value;}, source.UpperInterventionLimit);
			_assigner.Assign((value) => {target.LowerInterventionLimit = value;}, source.LowerInterventionLimit);
			_assigner.Assign((value) => {target.TestLevelSet = value;}, source.TestLevelSet);
			_assigner.Assign((value) => {target.TestLevelNumber = value;}, source.TestLevelNumber);
			_assigner.Assign((value) => {target.TestOperationActive = value;}, source.TestOperationActive);
			_assigner.Assign((value) => {target.StartDate = value;}, source.StartDate);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
		}

		public DtoTypes.ProcessControlCondition ReflectedPropertyMapping(Server.Core.Entities.ProcessControlCondition source)
		{
			var target = new DtoTypes.ProcessControlCondition();
			typeof(DtoTypes.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ProcessControlCondition).GetField("Location", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location);
			typeof(DtoTypes.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(DtoTypes.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(DtoTypes.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(DtoTypes.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(DtoTypes.ProcessControlCondition).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ProcessControlCondition source, DtoTypes.ProcessControlCondition target)
		{
			typeof(DtoTypes.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ProcessControlCondition).GetField("Location", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location);
			typeof(DtoTypes.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(DtoTypes.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(DtoTypes.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(DtoTypes.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(DtoTypes.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(DtoTypes.ProcessControlCondition).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
		}

		// ProcessControlForTransferToProcessControlDataForTransferDto.txt
			// hasDefaultConstructor
			// PropertyMapping: LocationId -> LocationId
			// PropertyMapping: LocationNumber -> LocationNumber
			// PropertyMapping: LocationDescription -> LocationDescription
			// PropertyMapping: ProcessControlConditionId -> ProcessControlConditionId
			// PropertyMapping: ProcessControlTechId -> ProcessControlTechId
			// PropertyMapping: SetPointTorque -> SetPointTorque
			// PropertyMapping: MinimumTorque -> MinimumTorque
			// PropertyMapping: MaximumTorque -> MaximumTorque
			// PropertyMapping: TestMethod -> TestMethod
			// PropertyMapping: LastTestDate -> LastTestDate
			// PropertyMapping: NextTestDate -> NextTestDate
			// PropertyMapping: TestInterval -> TestInterval
			// PropertyMapping: SampleNumber -> SampleNumber
			// PropertyMapping: NextTestDateShift -> NextTestDateShift
		public TransferToTestEquipmentService.ProcessControlDataForTransfer DirectPropertyMapping(Server.Core.Entities.ProcessControlForTransfer source)
		{
			var target = new TransferToTestEquipmentService.ProcessControlDataForTransfer();
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationNumber = value;}, source.LocationNumber);
			_assigner.Assign((value) => {target.LocationDescription = value;}, source.LocationDescription);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ProcessControlTechId = value;}, source.ProcessControlTechId);
			_assigner.Assign((value) => {target.SetPointTorque = value;}, source.SetPointTorque);
			_assigner.Assign((value) => {target.MinimumTorque = value;}, source.MinimumTorque);
			_assigner.Assign((value) => {target.MaximumTorque = value;}, source.MaximumTorque);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.LastTestDate = value;}, source.LastTestDate);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.NextTestDateShift = value;}, source.NextTestDateShift);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ProcessControlForTransfer source, TransferToTestEquipmentService.ProcessControlDataForTransfer target)
		{
			_assigner.Assign((value) => {target.LocationId = value;}, source.LocationId);
			_assigner.Assign((value) => {target.LocationNumber = value;}, source.LocationNumber);
			_assigner.Assign((value) => {target.LocationDescription = value;}, source.LocationDescription);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ProcessControlTechId = value;}, source.ProcessControlTechId);
			_assigner.Assign((value) => {target.SetPointTorque = value;}, source.SetPointTorque);
			_assigner.Assign((value) => {target.MinimumTorque = value;}, source.MinimumTorque);
			_assigner.Assign((value) => {target.MaximumTorque = value;}, source.MaximumTorque);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.LastTestDate = value;}, source.LastTestDate);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.NextTestDateShift = value;}, source.NextTestDateShift);
		}

		public TransferToTestEquipmentService.ProcessControlDataForTransfer ReflectedPropertyMapping(Server.Core.Entities.ProcessControlForTransfer source)
		{
			var target = new TransferToTestEquipmentService.ProcessControlDataForTransfer();
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LocationNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationNumber);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LocationDescription", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDescription);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("ProcessControlTechId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTechId);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("SetPointTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointTorque);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("MinimumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorque);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("MaximumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumTorque);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LastTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastTestDate);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("NextTestDateShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateShift);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ProcessControlForTransfer source, TransferToTestEquipmentService.ProcessControlDataForTransfer target)
		{
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationId);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LocationNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationNumber);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LocationDescription", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationDescription);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("ProcessControlTechId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTechId);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("SetPointTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointTorque);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("MinimumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorque);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("MaximumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumTorque);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("LastTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastTestDate);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(TransferToTestEquipmentService.ProcessControlDataForTransfer).GetField("NextTestDateShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateShift);
		}

		// QstProcessControlTechDtoToQstProcessControlTech.txt
			// PropertyMapping: Id -> Id
			// PropertyMapping: ProcessControlConditionId -> ProcessControlConditionId
			// PropertyMapping: ManufacturerId -> ManufacturerId
			// PropertyMapping: TestMethod -> TestMethod
			// PropertyMapping: Extension -> Extension
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: QstAngleLimitMt -> AngleLimitMt
			// PropertyMapping: QstStartMeasurementPeak -> StartMeasurementPeak
			// PropertyMapping: QstStartAngleCountingPa -> StartAngleCountingPa
			// PropertyMapping: QstAngleForFurtherTurningPa -> AngleForFurtherTurningPa
			// PropertyMapping: QstTargetAnglePa -> TargetAnglePa
			// PropertyMapping: QstStartMeasurementPa -> StartMeasurementPa
			// PropertyMapping: QstAlarmTorquePa -> AlarmTorquePa
			// PropertyMapping: QstAlarmAnglePa -> AlarmAnglePa
			// PropertyMapping: QstMinimumTorqueMt -> MinimumTorqueMt
			// PropertyMapping: QstStartAngleMt -> StartAngleMt
			// PropertyMapping: QstStartMeasurementMt -> StartMeasurementMt
			// PropertyMapping: QstAlarmTorqueMt -> AlarmTorqueMt
			// PropertyMapping: QstAlarmAngleMt -> AlarmAngleMt
		public void DirectPropertyMapping(DtoTypes.QstProcessControlTech source, Server.Core.Entities.QstProcessControlTech target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.Extension = value;}, source.Extension);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.AngleLimitMt = value;}, source.QstAngleLimitMt);
			_assigner.Assign((value) => {target.StartMeasurementPeak = value;}, source.QstStartMeasurementPeak);
			_assigner.Assign((value) => {target.StartAngleCountingPa = value;}, source.QstStartAngleCountingPa);
			_assigner.Assign((value) => {target.AngleForFurtherTurningPa = value;}, source.QstAngleForFurtherTurningPa);
			_assigner.Assign((value) => {target.TargetAnglePa = value;}, source.QstTargetAnglePa);
			_assigner.Assign((value) => {target.StartMeasurementPa = value;}, source.QstStartMeasurementPa);
			_assigner.Assign((value) => {target.AlarmTorquePa = value;}, source.QstAlarmTorquePa);
			_assigner.Assign((value) => {target.AlarmAnglePa = value;}, source.QstAlarmAnglePa);
			_assigner.Assign((value) => {target.MinimumTorqueMt = value;}, source.QstMinimumTorqueMt);
			_assigner.Assign((value) => {target.StartAngleMt = value;}, source.QstStartAngleMt);
			_assigner.Assign((value) => {target.StartMeasurementMt = value;}, source.QstStartMeasurementMt);
			_assigner.Assign((value) => {target.AlarmTorqueMt = value;}, source.QstAlarmTorqueMt);
			_assigner.Assign((value) => {target.AlarmAngleMt = value;}, source.QstAlarmAngleMt);
		}

		public void ReflectedPropertyMapping(DtoTypes.QstProcessControlTech source, Server.Core.Entities.QstProcessControlTech target)
		{
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AngleLimitMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAngleLimitMt);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartMeasurementPeak", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartMeasurementPeak);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartAngleCountingPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartAngleCountingPa);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AngleForFurtherTurningPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAngleForFurtherTurningPa);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("TargetAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstTargetAnglePa);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartMeasurementPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartMeasurementPa);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmTorquePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmTorquePa);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmAnglePa);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("MinimumTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstMinimumTorqueMt);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartAngleMt);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartMeasurementMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartMeasurementMt);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmTorqueMt);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmAngleMt);
		}

		// QstProcessControlTechToQstProcessControlTechDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: ProcessControlConditionId -> ProcessControlConditionId
			// PropertyMapping: ManufacturerId -> ManufacturerId
			// PropertyMapping: TestMethod -> TestMethod
			// PropertyMapping: Extension -> Extension
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: AngleLimitMt -> QstAngleLimitMt
			// PropertyMapping: StartMeasurementPeak -> QstStartMeasurementPeak
			// PropertyMapping: StartAngleCountingPa -> QstStartAngleCountingPa
			// PropertyMapping: AngleForFurtherTurningPa -> QstAngleForFurtherTurningPa
			// PropertyMapping: TargetAnglePa -> QstTargetAnglePa
			// PropertyMapping: StartMeasurementPa -> QstStartMeasurementPa
			// PropertyMapping: AlarmTorquePa -> QstAlarmTorquePa
			// PropertyMapping: AlarmAnglePa -> QstAlarmAnglePa
			// PropertyMapping: MinimumTorqueMt -> QstMinimumTorqueMt
			// PropertyMapping: StartAngleMt -> QstStartAngleMt
			// PropertyMapping: StartMeasurementMt -> QstStartMeasurementMt
			// PropertyMapping: AlarmTorqueMt -> QstAlarmTorqueMt
			// PropertyMapping: AlarmAngleMt -> QstAlarmAngleMt
		public DtoTypes.QstProcessControlTech DirectPropertyMapping(Server.Core.Entities.QstProcessControlTech source)
		{
			var target = new DtoTypes.QstProcessControlTech();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.Extension = value;}, source.Extension);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.QstAngleLimitMt = value;}, source.AngleLimitMt);
			_assigner.Assign((value) => {target.QstStartMeasurementPeak = value;}, source.StartMeasurementPeak);
			_assigner.Assign((value) => {target.QstStartAngleCountingPa = value;}, source.StartAngleCountingPa);
			_assigner.Assign((value) => {target.QstAngleForFurtherTurningPa = value;}, source.AngleForFurtherTurningPa);
			_assigner.Assign((value) => {target.QstTargetAnglePa = value;}, source.TargetAnglePa);
			_assigner.Assign((value) => {target.QstStartMeasurementPa = value;}, source.StartMeasurementPa);
			_assigner.Assign((value) => {target.QstAlarmTorquePa = value;}, source.AlarmTorquePa);
			_assigner.Assign((value) => {target.QstAlarmAnglePa = value;}, source.AlarmAnglePa);
			_assigner.Assign((value) => {target.QstMinimumTorqueMt = value;}, source.MinimumTorqueMt);
			_assigner.Assign((value) => {target.QstStartAngleMt = value;}, source.StartAngleMt);
			_assigner.Assign((value) => {target.QstStartMeasurementMt = value;}, source.StartMeasurementMt);
			_assigner.Assign((value) => {target.QstAlarmTorqueMt = value;}, source.AlarmTorqueMt);
			_assigner.Assign((value) => {target.QstAlarmAngleMt = value;}, source.AlarmAngleMt);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.QstProcessControlTech source, DtoTypes.QstProcessControlTech target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.Extension = value;}, source.Extension);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.QstAngleLimitMt = value;}, source.AngleLimitMt);
			_assigner.Assign((value) => {target.QstStartMeasurementPeak = value;}, source.StartMeasurementPeak);
			_assigner.Assign((value) => {target.QstStartAngleCountingPa = value;}, source.StartAngleCountingPa);
			_assigner.Assign((value) => {target.QstAngleForFurtherTurningPa = value;}, source.AngleForFurtherTurningPa);
			_assigner.Assign((value) => {target.QstTargetAnglePa = value;}, source.TargetAnglePa);
			_assigner.Assign((value) => {target.QstStartMeasurementPa = value;}, source.StartMeasurementPa);
			_assigner.Assign((value) => {target.QstAlarmTorquePa = value;}, source.AlarmTorquePa);
			_assigner.Assign((value) => {target.QstAlarmAnglePa = value;}, source.AlarmAnglePa);
			_assigner.Assign((value) => {target.QstMinimumTorqueMt = value;}, source.MinimumTorqueMt);
			_assigner.Assign((value) => {target.QstStartAngleMt = value;}, source.StartAngleMt);
			_assigner.Assign((value) => {target.QstStartMeasurementMt = value;}, source.StartMeasurementMt);
			_assigner.Assign((value) => {target.QstAlarmTorqueMt = value;}, source.AlarmTorqueMt);
			_assigner.Assign((value) => {target.QstAlarmAngleMt = value;}, source.AlarmAngleMt);
		}

		public DtoTypes.QstProcessControlTech ReflectedPropertyMapping(Server.Core.Entities.QstProcessControlTech source)
		{
			var target = new DtoTypes.QstProcessControlTech();
			typeof(DtoTypes.QstProcessControlTech).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.QstProcessControlTech).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(DtoTypes.QstProcessControlTech).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(DtoTypes.QstProcessControlTech).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(DtoTypes.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
			typeof(DtoTypes.QstProcessControlTech).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAngleLimitMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleLimitMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartMeasurementPeak", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPeak);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartAngleCountingPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleCountingPa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAngleForFurtherTurningPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleForFurtherTurningPa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstTargetAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TargetAnglePa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartMeasurementPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmTorquePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorquePa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAnglePa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstMinimumTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorqueMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartMeasurementMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorqueMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAngleMt);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.QstProcessControlTech source, DtoTypes.QstProcessControlTech target)
		{
			typeof(DtoTypes.QstProcessControlTech).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.QstProcessControlTech).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(DtoTypes.QstProcessControlTech).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(DtoTypes.QstProcessControlTech).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(DtoTypes.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
			typeof(DtoTypes.QstProcessControlTech).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAngleLimitMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleLimitMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartMeasurementPeak", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPeak);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartAngleCountingPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleCountingPa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAngleForFurtherTurningPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleForFurtherTurningPa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstTargetAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TargetAnglePa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartMeasurementPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmTorquePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorquePa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAnglePa);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstMinimumTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorqueMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstStartMeasurementMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorqueMt);
			typeof(DtoTypes.QstProcessControlTech).GetField("QstAlarmAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAngleMt);
		}

		// QstSetupDtoToQstSetup.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: Value -> Value
			// PropertyMapping: UserId -> UserId
		public Server.Core.Entities.QstSetup DirectPropertyMapping(DtoTypes.QstSetup source)
		{
			var target = new Server.Core.Entities.QstSetup();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.QstSetup source, Server.Core.Entities.QstSetup target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
		}

		public Server.Core.Entities.QstSetup ReflectedPropertyMapping(DtoTypes.QstSetup source)
		{
			var target = new Server.Core.Entities.QstSetup();
			typeof(Server.Core.Entities.QstSetup).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.QstSetup).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.QstSetup).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(Server.Core.Entities.QstSetup).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.QstSetup source, Server.Core.Entities.QstSetup target)
		{
			typeof(Server.Core.Entities.QstSetup).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.QstSetup).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.QstSetup).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(Server.Core.Entities.QstSetup).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
		}

		// QstSetupToQstSetupDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: Value -> Value
			// PropertyMapping: UserId -> UserId
		public DtoTypes.QstSetup DirectPropertyMapping(Server.Core.Entities.QstSetup source)
		{
			var target = new DtoTypes.QstSetup();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.QstSetup source, DtoTypes.QstSetup target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
		}

		public DtoTypes.QstSetup ReflectedPropertyMapping(Server.Core.Entities.QstSetup source)
		{
			var target = new DtoTypes.QstSetup();
			typeof(DtoTypes.QstSetup).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.QstSetup).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.QstSetup).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.QstSetup).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.QstSetup source, DtoTypes.QstSetup target)
		{
			typeof(DtoTypes.QstSetup).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.QstSetup).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.QstSetup).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.QstSetup).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
		}

		// ShiftManagementDiffDtoToShiftManagementDiff.txt
			// hasDefaultConstructor
			// PropertyMapping: Old -> Old
			// PropertyMapping: New -> New
			// PropertyMapping: UserId -> User
			// PropertyMapping: Comment -> Comment
		public Server.Core.Diffs.ShiftManagementDiff DirectPropertyMapping(DtoTypes.ShiftManagementDiff source)
		{
			var target = new Server.Core.Diffs.ShiftManagementDiff();
			_assigner.Assign((value) => {target.Old = value;}, source.Old);
			_assigner.Assign((value) => {target.New = value;}, source.New);
			_assigner.Assign((value) => {target.User = value;}, source.UserId);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ShiftManagementDiff source, Server.Core.Diffs.ShiftManagementDiff target)
		{
			_assigner.Assign((value) => {target.Old = value;}, source.Old);
			_assigner.Assign((value) => {target.New = value;}, source.New);
			_assigner.Assign((value) => {target.User = value;}, source.UserId);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
		}

		public Server.Core.Diffs.ShiftManagementDiff ReflectedPropertyMapping(DtoTypes.ShiftManagementDiff source)
		{
			var target = new Server.Core.Diffs.ShiftManagementDiff();
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("Old", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Old);
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("New", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.New);
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ShiftManagementDiff source, Server.Core.Diffs.ShiftManagementDiff target)
		{
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("Old", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Old);
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("New", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.New);
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(Server.Core.Diffs.ShiftManagementDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
		}

		// ShiftManagementDtoToShiftManagement.txt
			// hasDefaultConstructor
			// PropertyMapping: FirstShiftStart -> FirstShiftStart
			// PropertyMapping: FirstShiftEnd -> FirstShiftEnd
			// PropertyMapping: SecondShiftStart -> SecondShiftStart
			// PropertyMapping: SecondShiftEnd -> SecondShiftEnd
			// PropertyMapping: ThirdShiftStart -> ThirdShiftStart
			// PropertyMapping: ThirdShiftEnd -> ThirdShiftEnd
			// PropertyMapping: IsSecondShiftActive -> IsSecondShiftActive
			// PropertyMapping: IsThirdShiftActive -> IsThirdShiftActive
			// PropertyMapping: ChangeOfDay -> ChangeOfDay
			// PropertyMapping: FirstDayOfWeek -> FirstDayOfWeek
		public Server.Core.Entities.ShiftManagement DirectPropertyMapping(DtoTypes.ShiftManagement source)
		{
			var target = new Server.Core.Entities.ShiftManagement();
			_assigner.Assign((value) => {target.FirstShiftStart = value;}, source.FirstShiftStart);
			_assigner.Assign((value) => {target.FirstShiftEnd = value;}, source.FirstShiftEnd);
			_assigner.Assign((value) => {target.SecondShiftStart = value;}, source.SecondShiftStart);
			_assigner.Assign((value) => {target.SecondShiftEnd = value;}, source.SecondShiftEnd);
			_assigner.Assign((value) => {target.ThirdShiftStart = value;}, source.ThirdShiftStart);
			_assigner.Assign((value) => {target.ThirdShiftEnd = value;}, source.ThirdShiftEnd);
			_assigner.Assign((value) => {target.IsSecondShiftActive = value;}, source.IsSecondShiftActive);
			_assigner.Assign((value) => {target.IsThirdShiftActive = value;}, source.IsThirdShiftActive);
			_assigner.Assign((value) => {target.ChangeOfDay = value;}, source.ChangeOfDay);
			_assigner.Assign((value) => {target.FirstDayOfWeek = value;}, source.FirstDayOfWeek);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ShiftManagement source, Server.Core.Entities.ShiftManagement target)
		{
			_assigner.Assign((value) => {target.FirstShiftStart = value;}, source.FirstShiftStart);
			_assigner.Assign((value) => {target.FirstShiftEnd = value;}, source.FirstShiftEnd);
			_assigner.Assign((value) => {target.SecondShiftStart = value;}, source.SecondShiftStart);
			_assigner.Assign((value) => {target.SecondShiftEnd = value;}, source.SecondShiftEnd);
			_assigner.Assign((value) => {target.ThirdShiftStart = value;}, source.ThirdShiftStart);
			_assigner.Assign((value) => {target.ThirdShiftEnd = value;}, source.ThirdShiftEnd);
			_assigner.Assign((value) => {target.IsSecondShiftActive = value;}, source.IsSecondShiftActive);
			_assigner.Assign((value) => {target.IsThirdShiftActive = value;}, source.IsThirdShiftActive);
			_assigner.Assign((value) => {target.ChangeOfDay = value;}, source.ChangeOfDay);
			_assigner.Assign((value) => {target.FirstDayOfWeek = value;}, source.FirstDayOfWeek);
		}

		public Server.Core.Entities.ShiftManagement ReflectedPropertyMapping(DtoTypes.ShiftManagement source)
		{
			var target = new Server.Core.Entities.ShiftManagement();
			typeof(Server.Core.Entities.ShiftManagement).GetField("FirstShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftStart);
			typeof(Server.Core.Entities.ShiftManagement).GetField("FirstShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftEnd);
			typeof(Server.Core.Entities.ShiftManagement).GetField("SecondShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftStart);
			typeof(Server.Core.Entities.ShiftManagement).GetField("SecondShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftEnd);
			typeof(Server.Core.Entities.ShiftManagement).GetField("ThirdShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftStart);
			typeof(Server.Core.Entities.ShiftManagement).GetField("ThirdShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftEnd);
			typeof(Server.Core.Entities.ShiftManagement).GetField("IsSecondShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsSecondShiftActive);
			typeof(Server.Core.Entities.ShiftManagement).GetField("IsThirdShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsThirdShiftActive);
			typeof(Server.Core.Entities.ShiftManagement).GetField("ChangeOfDay", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ChangeOfDay);
			typeof(Server.Core.Entities.ShiftManagement).GetField("FirstDayOfWeek", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstDayOfWeek);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ShiftManagement source, Server.Core.Entities.ShiftManagement target)
		{
			typeof(Server.Core.Entities.ShiftManagement).GetField("FirstShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftStart);
			typeof(Server.Core.Entities.ShiftManagement).GetField("FirstShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftEnd);
			typeof(Server.Core.Entities.ShiftManagement).GetField("SecondShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftStart);
			typeof(Server.Core.Entities.ShiftManagement).GetField("SecondShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftEnd);
			typeof(Server.Core.Entities.ShiftManagement).GetField("ThirdShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftStart);
			typeof(Server.Core.Entities.ShiftManagement).GetField("ThirdShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftEnd);
			typeof(Server.Core.Entities.ShiftManagement).GetField("IsSecondShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsSecondShiftActive);
			typeof(Server.Core.Entities.ShiftManagement).GetField("IsThirdShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsThirdShiftActive);
			typeof(Server.Core.Entities.ShiftManagement).GetField("ChangeOfDay", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ChangeOfDay);
			typeof(Server.Core.Entities.ShiftManagement).GetField("FirstDayOfWeek", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstDayOfWeek);
		}

		// ShiftManagementToShiftManagementDto.txt
			// hasDefaultConstructor
			// PropertyMapping: FirstShiftStart -> FirstShiftStart
			// PropertyMapping: FirstShiftEnd -> FirstShiftEnd
			// PropertyMapping: SecondShiftStart -> SecondShiftStart
			// PropertyMapping: SecondShiftEnd -> SecondShiftEnd
			// PropertyMapping: ThirdShiftStart -> ThirdShiftStart
			// PropertyMapping: ThirdShiftEnd -> ThirdShiftEnd
			// PropertyMapping: IsSecondShiftActive -> IsSecondShiftActive
			// PropertyMapping: IsThirdShiftActive -> IsThirdShiftActive
			// PropertyMapping: ChangeOfDay -> ChangeOfDay
			// PropertyMapping: FirstDayOfWeek -> FirstDayOfWeek
		public DtoTypes.ShiftManagement DirectPropertyMapping(Server.Core.Entities.ShiftManagement source)
		{
			var target = new DtoTypes.ShiftManagement();
			_assigner.Assign((value) => {target.FirstShiftStart = value;}, source.FirstShiftStart);
			_assigner.Assign((value) => {target.FirstShiftEnd = value;}, source.FirstShiftEnd);
			_assigner.Assign((value) => {target.SecondShiftStart = value;}, source.SecondShiftStart);
			_assigner.Assign((value) => {target.SecondShiftEnd = value;}, source.SecondShiftEnd);
			_assigner.Assign((value) => {target.ThirdShiftStart = value;}, source.ThirdShiftStart);
			_assigner.Assign((value) => {target.ThirdShiftEnd = value;}, source.ThirdShiftEnd);
			_assigner.Assign((value) => {target.IsSecondShiftActive = value;}, source.IsSecondShiftActive);
			_assigner.Assign((value) => {target.IsThirdShiftActive = value;}, source.IsThirdShiftActive);
			_assigner.Assign((value) => {target.ChangeOfDay = value;}, source.ChangeOfDay);
			_assigner.Assign((value) => {target.FirstDayOfWeek = value;}, source.FirstDayOfWeek);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ShiftManagement source, DtoTypes.ShiftManagement target)
		{
			_assigner.Assign((value) => {target.FirstShiftStart = value;}, source.FirstShiftStart);
			_assigner.Assign((value) => {target.FirstShiftEnd = value;}, source.FirstShiftEnd);
			_assigner.Assign((value) => {target.SecondShiftStart = value;}, source.SecondShiftStart);
			_assigner.Assign((value) => {target.SecondShiftEnd = value;}, source.SecondShiftEnd);
			_assigner.Assign((value) => {target.ThirdShiftStart = value;}, source.ThirdShiftStart);
			_assigner.Assign((value) => {target.ThirdShiftEnd = value;}, source.ThirdShiftEnd);
			_assigner.Assign((value) => {target.IsSecondShiftActive = value;}, source.IsSecondShiftActive);
			_assigner.Assign((value) => {target.IsThirdShiftActive = value;}, source.IsThirdShiftActive);
			_assigner.Assign((value) => {target.ChangeOfDay = value;}, source.ChangeOfDay);
			_assigner.Assign((value) => {target.FirstDayOfWeek = value;}, source.FirstDayOfWeek);
		}

		public DtoTypes.ShiftManagement ReflectedPropertyMapping(Server.Core.Entities.ShiftManagement source)
		{
			var target = new DtoTypes.ShiftManagement();
			typeof(DtoTypes.ShiftManagement).GetField("FirstShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftStart);
			typeof(DtoTypes.ShiftManagement).GetField("FirstShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftEnd);
			typeof(DtoTypes.ShiftManagement).GetField("SecondShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftStart);
			typeof(DtoTypes.ShiftManagement).GetField("SecondShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftEnd);
			typeof(DtoTypes.ShiftManagement).GetField("ThirdShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftStart);
			typeof(DtoTypes.ShiftManagement).GetField("ThirdShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftEnd);
			typeof(DtoTypes.ShiftManagement).GetField("IsSecondShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsSecondShiftActive);
			typeof(DtoTypes.ShiftManagement).GetField("IsThirdShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsThirdShiftActive);
			typeof(DtoTypes.ShiftManagement).GetField("ChangeOfDay", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ChangeOfDay);
			typeof(DtoTypes.ShiftManagement).GetField("FirstDayOfWeek", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstDayOfWeek);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ShiftManagement source, DtoTypes.ShiftManagement target)
		{
			typeof(DtoTypes.ShiftManagement).GetField("FirstShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftStart);
			typeof(DtoTypes.ShiftManagement).GetField("FirstShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftEnd);
			typeof(DtoTypes.ShiftManagement).GetField("SecondShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftStart);
			typeof(DtoTypes.ShiftManagement).GetField("SecondShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftEnd);
			typeof(DtoTypes.ShiftManagement).GetField("ThirdShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftStart);
			typeof(DtoTypes.ShiftManagement).GetField("ThirdShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftEnd);
			typeof(DtoTypes.ShiftManagement).GetField("IsSecondShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsSecondShiftActive);
			typeof(DtoTypes.ShiftManagement).GetField("IsThirdShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsThirdShiftActive);
			typeof(DtoTypes.ShiftManagement).GetField("ChangeOfDay", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ChangeOfDay);
			typeof(DtoTypes.ShiftManagement).GetField("FirstDayOfWeek", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstDayOfWeek);
		}

		// SourceStubToTargetStub.txt
			// hasDefaultConstructor
			// PropertyMapping: ITEM -> count
			// PropertyMapping: UNAME -> name
			// ConstructorMapping: 0
			// ConstructorMapping: UNAME
			// ConstructorMapping: ITEM
			// ConstructorMapping: null
		public TargetStub DirectPropertyMapping(SourceStub source)
		{
			var target = new TargetStub();
			_assigner.Assign((value) => {target.count = value;}, source.ITEM);
			_assigner.Assign((value) => {target.name = value;}, source.UNAME);
			return target;
		}

		public void DirectPropertyMapping(SourceStub source, TargetStub target)
		{
			_assigner.Assign((value) => {target.count = value;}, source.ITEM);
			_assigner.Assign((value) => {target.name = value;}, source.UNAME);
		}

		public TargetStub ReflectedPropertyMapping(SourceStub source)
		{
			var target = new TargetStub();
			typeof(TargetStub).GetField("count", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ITEM);
			typeof(TargetStub).GetField("name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNAME);
			return target;
		}

		public void ReflectedPropertyMapping(SourceStub source, TargetStub target)
		{
			typeof(TargetStub).GetField("count", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ITEM);
			typeof(TargetStub).GetField("name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNAME);
		}

		public TargetStub DirectConstructorMapping(SourceStub source)
		{
			return new TargetStub(0, source.UNAME, source.ITEM, null);
		}

		// StatusDtoToStatus.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Value
			// PropertyMapping: Alive -> Alive
		public Server.Core.Entities.Status DirectPropertyMapping(DtoTypes.Status source)
		{
			var target = new Server.Core.Entities.Status();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Value = value;}, source.Description);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Status source, Server.Core.Entities.Status target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Value = value;}, source.Description);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public Server.Core.Entities.Status ReflectedPropertyMapping(DtoTypes.Status source)
		{
			var target = new Server.Core.Entities.Status();
			typeof(Server.Core.Entities.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Status).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.Status).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Status source, Server.Core.Entities.Status target)
		{
			typeof(Server.Core.Entities.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Status).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.Status).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// StatusToStatusDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Value -> Description
			// PropertyMapping: Alive -> Alive
		public DtoTypes.Status DirectPropertyMapping(Server.Core.Entities.Status source)
		{
			var target = new DtoTypes.Status();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Value);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Status source, DtoTypes.Status target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Value);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public DtoTypes.Status ReflectedPropertyMapping(Server.Core.Entities.Status source)
		{
			var target = new DtoTypes.Status();
			typeof(DtoTypes.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Status).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.Status).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Status source, DtoTypes.Status target)
		{
			typeof(DtoTypes.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Status).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.Status).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// TargetStubToSourceStub.txt
			// PropertyMapping: count -> ITEM
			// PropertyMapping: name -> UNAME
		public void DirectPropertyMapping(TargetStub source, SourceStub target)
		{
			_assigner.Assign((value) => {target.ITEM = value;}, source.count);
			_assigner.Assign((value) => {target.UNAME = value;}, source.name);
		}

		public void ReflectedPropertyMapping(TargetStub source, SourceStub target)
		{
			typeof(SourceStub).GetField("ITEM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.count);
			typeof(SourceStub).GetField("UNAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.name);
		}

		// TestEquipmentDtoToTestEquipment.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: SerialNumber -> SerialNumber
			// PropertyMapping: TestEquipmentModel -> TestEquipmentModel
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: TransferUser -> TransferUser
			// PropertyMapping: TransferAdapter -> TransferAdapter
			// PropertyMapping: TransferTransducer -> TransferTransducer
			// PropertyMapping: AskForIdent -> AskForIdent
			// PropertyMapping: TransferCurves -> TransferCurves
			// PropertyMapping: UseErrorCodes -> UseErrorCodes
			// PropertyMapping: AskForSign -> AskForSign
			// PropertyMapping: DoLoseCheck -> DoLoseCheck
			// PropertyMapping: CanDeleteMeasurements -> CanDeleteMeasurements
			// PropertyMapping: ConfirmMeasurements -> ConfirmMeasurements
			// PropertyMapping: TransferLocationPictures -> TransferLocationPictures
			// PropertyMapping: TransferNewLimits -> TransferNewLimits
			// PropertyMapping: TransferAttributes -> TransferAttributes
			// PropertyMapping: UseForRot -> UseForRot
			// PropertyMapping: UseForCtl -> UseForCtl
			// PropertyMapping: Status -> Status
			// PropertyMapping: Version -> Version
			// PropertyMapping: LastCalibration -> LastCalibration
			// PropertyMapping: CalibrationInterval -> CalibrationInterval
			// PropertyMapping: CapacityMin -> CapacityMin
			// PropertyMapping: CapacityMax -> CapacityMax
			// PropertyMapping: CalibrationNorm -> CalibrationNorm
			// PropertyMapping: CanUseQstStandard -> CanUseQstStandard
		public Server.Core.Entities.TestEquipment DirectPropertyMapping(DtoTypes.TestEquipment source)
		{
			var target = new Server.Core.Entities.TestEquipment();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.Version = value;}, source.Version);
			_assigner.Assign((value) => {target.LastCalibration = value;}, source.LastCalibration);
			_assigner.Assign((value) => {target.CalibrationInterval = value;}, source.CalibrationInterval);
			_assigner.Assign((value) => {target.CapacityMin = value;}, source.CapacityMin);
			_assigner.Assign((value) => {target.CapacityMax = value;}, source.CapacityMax);
			_assigner.Assign((value) => {target.CalibrationNorm = value;}, source.CalibrationNorm);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestEquipment source, Server.Core.Entities.TestEquipment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.Version = value;}, source.Version);
			_assigner.Assign((value) => {target.LastCalibration = value;}, source.LastCalibration);
			_assigner.Assign((value) => {target.CalibrationInterval = value;}, source.CalibrationInterval);
			_assigner.Assign((value) => {target.CapacityMin = value;}, source.CapacityMin);
			_assigner.Assign((value) => {target.CapacityMax = value;}, source.CapacityMax);
			_assigner.Assign((value) => {target.CalibrationNorm = value;}, source.CalibrationNorm);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
		}

		public Server.Core.Entities.TestEquipment ReflectedPropertyMapping(DtoTypes.TestEquipment source)
		{
			var target = new Server.Core.Entities.TestEquipment();
			typeof(Server.Core.Entities.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Server.Core.Entities.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Server.Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(Server.Core.Entities.TestEquipment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Server.Core.Entities.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Server.Core.Entities.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Server.Core.Entities.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Server.Core.Entities.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(Server.Core.Entities.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestEquipment source, Server.Core.Entities.TestEquipment target)
		{
			typeof(Server.Core.Entities.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Server.Core.Entities.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Server.Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(Server.Core.Entities.TestEquipment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Server.Core.Entities.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Server.Core.Entities.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Server.Core.Entities.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Server.Core.Entities.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(Server.Core.Entities.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestEquipmentModelDtoToTestEquipmenModel.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: TestEquipmentModelName -> TestEquipmentModelName
			// PropertyMapping: DriverProgramPath -> DriverProgramPath
			// PropertyMapping: CommunicationFilePath -> CommunicationFilePath
			// PropertyMapping: StatusFilePath -> StatusFilePath
			// PropertyMapping: ResultFilePath -> ResultFilePath
			// PropertyMapping: Manufacturer -> Manufacturer
			// PropertyMapping: Type -> Type
			// PropertyMapping: TransferUser -> TransferUser
			// PropertyMapping: TransferAdapter -> TransferAdapter
			// PropertyMapping: TransferTransducer -> TransferTransducer
			// PropertyMapping: AskForIdent -> AskForIdent
			// PropertyMapping: TransferCurves -> TransferCurves
			// PropertyMapping: UseErrorCodes -> UseErrorCodes
			// PropertyMapping: AskForSign -> AskForSign
			// PropertyMapping: DoLoseCheck -> DoLoseCheck
			// PropertyMapping: CanDeleteMeasurements -> CanDeleteMeasurements
			// PropertyMapping: ConfirmMeasurements -> ConfirmMeasurements
			// PropertyMapping: TransferLocationPictures -> TransferLocationPictures
			// PropertyMapping: TransferNewLimits -> TransferNewLimits
			// PropertyMapping: TransferAttributes -> TransferAttributes
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: UseForRot -> UseForRot
			// PropertyMapping: UseForCtl -> UseForCtl
			// PropertyMapping: DataGateVersion -> DataGateVersion
			// PropertyMapping: CanUseQstStandard -> CanUseQstStandard
		public Server.Core.Entities.TestEquipmentModel DirectPropertyMapping(DtoTypes.TestEquipmentModel source)
		{
			var target = new Server.Core.Entities.TestEquipmentModel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestEquipmentModelName = value;}, source.TestEquipmentModelName);
			_assigner.Assign((value) => {target.DriverProgramPath = value;}, source.DriverProgramPath);
			_assigner.Assign((value) => {target.CommunicationFilePath = value;}, source.CommunicationFilePath);
			_assigner.Assign((value) => {target.StatusFilePath = value;}, source.StatusFilePath);
			_assigner.Assign((value) => {target.ResultFilePath = value;}, source.ResultFilePath);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestEquipmentModel source, Server.Core.Entities.TestEquipmentModel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestEquipmentModelName = value;}, source.TestEquipmentModelName);
			_assigner.Assign((value) => {target.DriverProgramPath = value;}, source.DriverProgramPath);
			_assigner.Assign((value) => {target.CommunicationFilePath = value;}, source.CommunicationFilePath);
			_assigner.Assign((value) => {target.StatusFilePath = value;}, source.StatusFilePath);
			_assigner.Assign((value) => {target.ResultFilePath = value;}, source.ResultFilePath);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
		}

		public Server.Core.Entities.TestEquipmentModel ReflectedPropertyMapping(DtoTypes.TestEquipmentModel source)
		{
			var target = new Server.Core.Entities.TestEquipmentModel();
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestEquipmentModel source, Server.Core.Entities.TestEquipmentModel target)
		{
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestEquipmentModelToTestEquipmenModelDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: TestEquipmentModelName -> TestEquipmentModelName
			// PropertyMapping: DriverProgramPath -> DriverProgramPath
			// PropertyMapping: CommunicationFilePath -> CommunicationFilePath
			// PropertyMapping: StatusFilePath -> StatusFilePath
			// PropertyMapping: ResultFilePath -> ResultFilePath
			// PropertyMapping: Manufacturer -> Manufacturer
			// PropertyMapping: Type -> Type
			// PropertyMapping: TransferUser -> TransferUser
			// PropertyMapping: TransferAdapter -> TransferAdapter
			// PropertyMapping: TransferTransducer -> TransferTransducer
			// PropertyMapping: AskForIdent -> AskForIdent
			// PropertyMapping: TransferCurves -> TransferCurves
			// PropertyMapping: UseErrorCodes -> UseErrorCodes
			// PropertyMapping: AskForSign -> AskForSign
			// PropertyMapping: DoLoseCheck -> DoLoseCheck
			// PropertyMapping: CanDeleteMeasurements -> CanDeleteMeasurements
			// PropertyMapping: ConfirmMeasurements -> ConfirmMeasurements
			// PropertyMapping: TransferLocationPictures -> TransferLocationPictures
			// PropertyMapping: TransferNewLimits -> TransferNewLimits
			// PropertyMapping: TransferAttributes -> TransferAttributes
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: UseForRot -> UseForRot
			// PropertyMapping: UseForCtl -> UseForCtl
			// PropertyMapping: DataGateVersion -> DataGateVersion
			// PropertyMapping: CanUseQstStandard -> CanUseQstStandard
		public DtoTypes.TestEquipmentModel DirectPropertyMapping(Server.Core.Entities.TestEquipmentModel source)
		{
			var target = new DtoTypes.TestEquipmentModel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestEquipmentModelName = value;}, source.TestEquipmentModelName);
			_assigner.Assign((value) => {target.DriverProgramPath = value;}, source.DriverProgramPath);
			_assigner.Assign((value) => {target.CommunicationFilePath = value;}, source.CommunicationFilePath);
			_assigner.Assign((value) => {target.StatusFilePath = value;}, source.StatusFilePath);
			_assigner.Assign((value) => {target.ResultFilePath = value;}, source.ResultFilePath);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestEquipmentModel source, DtoTypes.TestEquipmentModel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestEquipmentModelName = value;}, source.TestEquipmentModelName);
			_assigner.Assign((value) => {target.DriverProgramPath = value;}, source.DriverProgramPath);
			_assigner.Assign((value) => {target.CommunicationFilePath = value;}, source.CommunicationFilePath);
			_assigner.Assign((value) => {target.StatusFilePath = value;}, source.StatusFilePath);
			_assigner.Assign((value) => {target.ResultFilePath = value;}, source.ResultFilePath);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
		}

		public DtoTypes.TestEquipmentModel ReflectedPropertyMapping(Server.Core.Entities.TestEquipmentModel source)
		{
			var target = new DtoTypes.TestEquipmentModel();
			typeof(DtoTypes.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(DtoTypes.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(DtoTypes.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(DtoTypes.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(DtoTypes.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(DtoTypes.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DtoTypes.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DtoTypes.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DtoTypes.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DtoTypes.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DtoTypes.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DtoTypes.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DtoTypes.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DtoTypes.TestEquipmentModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DtoTypes.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DtoTypes.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(DtoTypes.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestEquipmentModel source, DtoTypes.TestEquipmentModel target)
		{
			typeof(DtoTypes.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(DtoTypes.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(DtoTypes.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(DtoTypes.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(DtoTypes.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(DtoTypes.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DtoTypes.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DtoTypes.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DtoTypes.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DtoTypes.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DtoTypes.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DtoTypes.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DtoTypes.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DtoTypes.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DtoTypes.TestEquipmentModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DtoTypes.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DtoTypes.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(DtoTypes.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestEquipmentToTestEquipmentDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: SerialNumber -> SerialNumber
			// PropertyMapping: TestEquipmentModel -> TestEquipmentModel
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: TransferUser -> TransferUser
			// PropertyMapping: TransferAdapter -> TransferAdapter
			// PropertyMapping: TransferTransducer -> TransferTransducer
			// PropertyMapping: AskForIdent -> AskForIdent
			// PropertyMapping: TransferCurves -> TransferCurves
			// PropertyMapping: UseErrorCodes -> UseErrorCodes
			// PropertyMapping: AskForSign -> AskForSign
			// PropertyMapping: DoLoseCheck -> DoLoseCheck
			// PropertyMapping: CanDeleteMeasurements -> CanDeleteMeasurements
			// PropertyMapping: ConfirmMeasurements -> ConfirmMeasurements
			// PropertyMapping: TransferLocationPictures -> TransferLocationPictures
			// PropertyMapping: TransferNewLimits -> TransferNewLimits
			// PropertyMapping: TransferAttributes -> TransferAttributes
			// PropertyMapping: UseForRot -> UseForRot
			// PropertyMapping: UseForCtl -> UseForCtl
			// PropertyMapping: Status -> Status
			// PropertyMapping: Version -> Version
			// PropertyMapping: LastCalibration -> LastCalibration
			// PropertyMapping: CalibrationInterval -> CalibrationInterval
			// PropertyMapping: CapacityMin -> CapacityMin
			// PropertyMapping: CapacityMax -> CapacityMax
			// PropertyMapping: CalibrationNorm -> CalibrationNorm
			// PropertyMapping: CanUseQstStandard -> CanUseQstStandard
		public DtoTypes.TestEquipment DirectPropertyMapping(Server.Core.Entities.TestEquipment source)
		{
			var target = new DtoTypes.TestEquipment();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.Version = value;}, source.Version);
			_assigner.Assign((value) => {target.LastCalibration = value;}, source.LastCalibration);
			_assigner.Assign((value) => {target.CalibrationInterval = value;}, source.CalibrationInterval);
			_assigner.Assign((value) => {target.CapacityMin = value;}, source.CapacityMin);
			_assigner.Assign((value) => {target.CapacityMax = value;}, source.CapacityMax);
			_assigner.Assign((value) => {target.CalibrationNorm = value;}, source.CalibrationNorm);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestEquipment source, DtoTypes.TestEquipment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.Version = value;}, source.Version);
			_assigner.Assign((value) => {target.LastCalibration = value;}, source.LastCalibration);
			_assigner.Assign((value) => {target.CalibrationInterval = value;}, source.CalibrationInterval);
			_assigner.Assign((value) => {target.CapacityMin = value;}, source.CapacityMin);
			_assigner.Assign((value) => {target.CapacityMax = value;}, source.CapacityMax);
			_assigner.Assign((value) => {target.CalibrationNorm = value;}, source.CalibrationNorm);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
		}

		public DtoTypes.TestEquipment ReflectedPropertyMapping(Server.Core.Entities.TestEquipment source)
		{
			var target = new DtoTypes.TestEquipment();
			typeof(DtoTypes.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(DtoTypes.TestEquipment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DtoTypes.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DtoTypes.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DtoTypes.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DtoTypes.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DtoTypes.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DtoTypes.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DtoTypes.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DtoTypes.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DtoTypes.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DtoTypes.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DtoTypes.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DtoTypes.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DtoTypes.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DtoTypes.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DtoTypes.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DtoTypes.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(DtoTypes.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(DtoTypes.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(DtoTypes.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(DtoTypes.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(DtoTypes.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(DtoTypes.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestEquipment source, DtoTypes.TestEquipment target)
		{
			typeof(DtoTypes.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(DtoTypes.TestEquipment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DtoTypes.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DtoTypes.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DtoTypes.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DtoTypes.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DtoTypes.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DtoTypes.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DtoTypes.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DtoTypes.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DtoTypes.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DtoTypes.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DtoTypes.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DtoTypes.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DtoTypes.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DtoTypes.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DtoTypes.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DtoTypes.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(DtoTypes.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(DtoTypes.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(DtoTypes.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(DtoTypes.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(DtoTypes.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(DtoTypes.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestLevelDtoToTestLevel.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: TestInterval -> TestInterval
			// PropertyMapping: SampleNumber -> SampleNumber
			// PropertyMapping: ConsiderWorkingCalendar -> ConsiderWorkingCalendar
			// PropertyMapping: IsActive -> IsActive
		public Server.Core.Entities.TestLevel DirectPropertyMapping(DtoTypes.TestLevel source)
		{
			var target = new Server.Core.Entities.TestLevel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestLevel source, Server.Core.Entities.TestLevel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
		}

		public Server.Core.Entities.TestLevel ReflectedPropertyMapping(DtoTypes.TestLevel source)
		{
			var target = new Server.Core.Entities.TestLevel();
			typeof(Server.Core.Entities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(Server.Core.Entities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(Server.Core.Entities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(Server.Core.Entities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestLevel source, Server.Core.Entities.TestLevel target)
		{
			typeof(Server.Core.Entities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(Server.Core.Entities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(Server.Core.Entities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(Server.Core.Entities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
		}

		// TestLevelSetDtoToTestLevelSet.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: TestLevel1 -> TestLevel1
			// PropertyMapping: TestLevel2 -> TestLevel2
			// PropertyMapping: TestLevel3 -> TestLevel3
		public Server.Core.Entities.TestLevelSet DirectPropertyMapping(DtoTypes.TestLevelSet source)
		{
			var target = new Server.Core.Entities.TestLevelSet();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestLevelSet source, Server.Core.Entities.TestLevelSet target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
		}

		public Server.Core.Entities.TestLevelSet ReflectedPropertyMapping(DtoTypes.TestLevelSet source)
		{
			var target = new Server.Core.Entities.TestLevelSet();
			typeof(Server.Core.Entities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.TestLevelSet).GetField("TestLevel1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel1);
			typeof(Server.Core.Entities.TestLevelSet).GetField("TestLevel2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel2);
			typeof(Server.Core.Entities.TestLevelSet).GetField("TestLevel3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel3);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestLevelSet source, Server.Core.Entities.TestLevelSet target)
		{
			typeof(Server.Core.Entities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.TestLevelSet).GetField("TestLevel1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel1);
			typeof(Server.Core.Entities.TestLevelSet).GetField("TestLevel2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel2);
			typeof(Server.Core.Entities.TestLevelSet).GetField("TestLevel3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel3);
		}

		// TestLevelSetToTestLevelSetDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: TestLevel1 -> TestLevel1
			// PropertyMapping: TestLevel2 -> TestLevel2
			// PropertyMapping: TestLevel3 -> TestLevel3
		public DtoTypes.TestLevelSet DirectPropertyMapping(Server.Core.Entities.TestLevelSet source)
		{
			var target = new DtoTypes.TestLevelSet();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestLevelSet source, DtoTypes.TestLevelSet target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
		}

		public DtoTypes.TestLevelSet ReflectedPropertyMapping(Server.Core.Entities.TestLevelSet source)
		{
			var target = new DtoTypes.TestLevelSet();
			typeof(DtoTypes.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel1);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel2);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel3);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestLevelSet source, DtoTypes.TestLevelSet target)
		{
			typeof(DtoTypes.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel1);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel2);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel3);
		}

		// TestLevelToTestLevelDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: TestInterval -> TestInterval
			// PropertyMapping: SampleNumber -> SampleNumber
			// PropertyMapping: ConsiderWorkingCalendar -> ConsiderWorkingCalendar
			// PropertyMapping: IsActive -> IsActive
		public DtoTypes.TestLevel DirectPropertyMapping(Server.Core.Entities.TestLevel source)
		{
			var target = new DtoTypes.TestLevel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestLevel source, DtoTypes.TestLevel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
		}

		public DtoTypes.TestLevel ReflectedPropertyMapping(Server.Core.Entities.TestLevel source)
		{
			var target = new DtoTypes.TestLevel();
			typeof(DtoTypes.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(DtoTypes.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(DtoTypes.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(DtoTypes.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestLevel source, DtoTypes.TestLevel target)
		{
			typeof(DtoTypes.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(DtoTypes.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(DtoTypes.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(DtoTypes.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
		}

		// TestParametersDtoToTestTestParameters.txt
			// hasDefaultConstructor
			// PropertyMapping: SetPoint1 -> SetPoint1
			// PropertyMapping: Minimum1 -> Minimum1
			// PropertyMapping: Maximum1 -> Maximum1
			// PropertyMapping: Threshold1 -> Threshold1
			// PropertyMapping: SetPoint2 -> SetPoint2
			// PropertyMapping: Minimum2 -> Minimum2
			// PropertyMapping: Maximum2 -> Maximum2
			// PropertyMapping: ControlledBy -> ControlledBy
			// PropertyMapping: Alive -> Alive
		public Server.Core.Entities.TestParameters DirectPropertyMapping(DtoTypes.TestParameters source)
		{
			var target = new Server.Core.Entities.TestParameters();
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestParameters source, Server.Core.Entities.TestParameters target)
		{
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public Server.Core.Entities.TestParameters ReflectedPropertyMapping(DtoTypes.TestParameters source)
		{
			var target = new Server.Core.Entities.TestParameters();
			typeof(Server.Core.Entities.TestParameters).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(Server.Core.Entities.TestParameters).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(Server.Core.Entities.TestParameters).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(Server.Core.Entities.TestParameters).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(Server.Core.Entities.TestParameters).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(Server.Core.Entities.TestParameters).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(Server.Core.Entities.TestParameters).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(Server.Core.Entities.TestParameters).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(Server.Core.Entities.TestParameters).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestParameters source, Server.Core.Entities.TestParameters target)
		{
			typeof(Server.Core.Entities.TestParameters).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(Server.Core.Entities.TestParameters).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(Server.Core.Entities.TestParameters).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(Server.Core.Entities.TestParameters).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(Server.Core.Entities.TestParameters).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(Server.Core.Entities.TestParameters).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(Server.Core.Entities.TestParameters).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(Server.Core.Entities.TestParameters).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(Server.Core.Entities.TestParameters).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// TestParametersToTestTestParametersDto.txt
			// hasDefaultConstructor
			// PropertyMapping: SetPoint1 -> SetPoint1
			// PropertyMapping: Minimum1 -> Minimum1
			// PropertyMapping: Maximum1 -> Maximum1
			// PropertyMapping: Threshold1 -> Threshold1
			// PropertyMapping: SetPoint2 -> SetPoint2
			// PropertyMapping: Minimum2 -> Minimum2
			// PropertyMapping: Maximum2 -> Maximum2
			// PropertyMapping: ControlledBy -> ControlledBy
		public DtoTypes.TestParameters DirectPropertyMapping(Server.Core.Entities.TestParameters source)
		{
			var target = new DtoTypes.TestParameters();
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestParameters source, DtoTypes.TestParameters target)
		{
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
		}

		public DtoTypes.TestParameters ReflectedPropertyMapping(Server.Core.Entities.TestParameters source)
		{
			var target = new DtoTypes.TestParameters();
			typeof(DtoTypes.TestParameters).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(DtoTypes.TestParameters).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(DtoTypes.TestParameters).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(DtoTypes.TestParameters).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(DtoTypes.TestParameters).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(DtoTypes.TestParameters).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(DtoTypes.TestParameters).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(DtoTypes.TestParameters).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestParameters source, DtoTypes.TestParameters target)
		{
			typeof(DtoTypes.TestParameters).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(DtoTypes.TestParameters).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(DtoTypes.TestParameters).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(DtoTypes.TestParameters).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(DtoTypes.TestParameters).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(DtoTypes.TestParameters).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(DtoTypes.TestParameters).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(DtoTypes.TestParameters).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
		}

		// TestTechniqueDtoToTestTechnique.txt
			// hasDefaultConstructor
			// PropertyMapping: EndCycleTime -> EndCycleTime
			// PropertyMapping: FilterFrequency -> FilterFrequency
			// PropertyMapping: CycleComplete -> CycleComplete
			// PropertyMapping: MeasureDelayTime -> MeasureDelayTime
			// PropertyMapping: ResetTime -> ResetTime
			// PropertyMapping: MustTorqueAndAngleBeInLimits -> MustTorqueAndAngleBeInLimits
			// PropertyMapping: CycleStart -> CycleStart
			// PropertyMapping: StartFinalAngle -> StartFinalAngle
			// PropertyMapping: SlipTorque -> SlipTorque
			// PropertyMapping: TorqueCoefficient -> TorqueCoefficient
			// PropertyMapping: MinimumPulse -> MinimumPulse
			// PropertyMapping: MaximumPulse -> MaximumPulse
			// PropertyMapping: Threshold -> Threshold
		public Server.Core.Entities.TestTechnique DirectPropertyMapping(DtoTypes.TestTechnique source)
		{
			var target = new Server.Core.Entities.TestTechnique();
			_assigner.Assign((value) => {target.EndCycleTime = value;}, source.EndCycleTime);
			_assigner.Assign((value) => {target.FilterFrequency = value;}, source.FilterFrequency);
			_assigner.Assign((value) => {target.CycleComplete = value;}, source.CycleComplete);
			_assigner.Assign((value) => {target.MeasureDelayTime = value;}, source.MeasureDelayTime);
			_assigner.Assign((value) => {target.ResetTime = value;}, source.ResetTime);
			_assigner.Assign((value) => {target.MustTorqueAndAngleBeInLimits = value;}, source.MustTorqueAndAngleBeInLimits);
			_assigner.Assign((value) => {target.CycleStart = value;}, source.CycleStart);
			_assigner.Assign((value) => {target.StartFinalAngle = value;}, source.StartFinalAngle);
			_assigner.Assign((value) => {target.SlipTorque = value;}, source.SlipTorque);
			_assigner.Assign((value) => {target.TorqueCoefficient = value;}, source.TorqueCoefficient);
			_assigner.Assign((value) => {target.MinimumPulse = value;}, source.MinimumPulse);
			_assigner.Assign((value) => {target.MaximumPulse = value;}, source.MaximumPulse);
			_assigner.Assign((value) => {target.Threshold = value;}, source.Threshold);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestTechnique source, Server.Core.Entities.TestTechnique target)
		{
			_assigner.Assign((value) => {target.EndCycleTime = value;}, source.EndCycleTime);
			_assigner.Assign((value) => {target.FilterFrequency = value;}, source.FilterFrequency);
			_assigner.Assign((value) => {target.CycleComplete = value;}, source.CycleComplete);
			_assigner.Assign((value) => {target.MeasureDelayTime = value;}, source.MeasureDelayTime);
			_assigner.Assign((value) => {target.ResetTime = value;}, source.ResetTime);
			_assigner.Assign((value) => {target.MustTorqueAndAngleBeInLimits = value;}, source.MustTorqueAndAngleBeInLimits);
			_assigner.Assign((value) => {target.CycleStart = value;}, source.CycleStart);
			_assigner.Assign((value) => {target.StartFinalAngle = value;}, source.StartFinalAngle);
			_assigner.Assign((value) => {target.SlipTorque = value;}, source.SlipTorque);
			_assigner.Assign((value) => {target.TorqueCoefficient = value;}, source.TorqueCoefficient);
			_assigner.Assign((value) => {target.MinimumPulse = value;}, source.MinimumPulse);
			_assigner.Assign((value) => {target.MaximumPulse = value;}, source.MaximumPulse);
			_assigner.Assign((value) => {target.Threshold = value;}, source.Threshold);
		}

		public Server.Core.Entities.TestTechnique ReflectedPropertyMapping(DtoTypes.TestTechnique source)
		{
			var target = new Server.Core.Entities.TestTechnique();
			typeof(Server.Core.Entities.TestTechnique).GetField("EndCycleTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndCycleTime);
			typeof(Server.Core.Entities.TestTechnique).GetField("FilterFrequency", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FilterFrequency);
			typeof(Server.Core.Entities.TestTechnique).GetField("CycleComplete", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleComplete);
			typeof(Server.Core.Entities.TestTechnique).GetField("MeasureDelayTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MeasureDelayTime);
			typeof(Server.Core.Entities.TestTechnique).GetField("ResetTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResetTime);
			typeof(Server.Core.Entities.TestTechnique).GetField("MustTorqueAndAngleBeInLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MustTorqueAndAngleBeInLimits);
			typeof(Server.Core.Entities.TestTechnique).GetField("CycleStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleStart);
			typeof(Server.Core.Entities.TestTechnique).GetField("StartFinalAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartFinalAngle);
			typeof(Server.Core.Entities.TestTechnique).GetField("SlipTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SlipTorque);
			typeof(Server.Core.Entities.TestTechnique).GetField("TorqueCoefficient", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TorqueCoefficient);
			typeof(Server.Core.Entities.TestTechnique).GetField("MinimumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumPulse);
			typeof(Server.Core.Entities.TestTechnique).GetField("MaximumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumPulse);
			typeof(Server.Core.Entities.TestTechnique).GetField("Threshold", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestTechnique source, Server.Core.Entities.TestTechnique target)
		{
			typeof(Server.Core.Entities.TestTechnique).GetField("EndCycleTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndCycleTime);
			typeof(Server.Core.Entities.TestTechnique).GetField("FilterFrequency", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FilterFrequency);
			typeof(Server.Core.Entities.TestTechnique).GetField("CycleComplete", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleComplete);
			typeof(Server.Core.Entities.TestTechnique).GetField("MeasureDelayTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MeasureDelayTime);
			typeof(Server.Core.Entities.TestTechnique).GetField("ResetTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResetTime);
			typeof(Server.Core.Entities.TestTechnique).GetField("MustTorqueAndAngleBeInLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MustTorqueAndAngleBeInLimits);
			typeof(Server.Core.Entities.TestTechnique).GetField("CycleStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleStart);
			typeof(Server.Core.Entities.TestTechnique).GetField("StartFinalAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartFinalAngle);
			typeof(Server.Core.Entities.TestTechnique).GetField("SlipTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SlipTorque);
			typeof(Server.Core.Entities.TestTechnique).GetField("TorqueCoefficient", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TorqueCoefficient);
			typeof(Server.Core.Entities.TestTechnique).GetField("MinimumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumPulse);
			typeof(Server.Core.Entities.TestTechnique).GetField("MaximumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumPulse);
			typeof(Server.Core.Entities.TestTechnique).GetField("Threshold", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold);
		}

		// TestTechniqueToTestTechniqueDto.txt
			// hasDefaultConstructor
			// PropertyMapping: EndCycleTime -> EndCycleTime
			// PropertyMapping: FilterFrequency -> FilterFrequency
			// PropertyMapping: CycleComplete -> CycleComplete
			// PropertyMapping: MeasureDelayTime -> MeasureDelayTime
			// PropertyMapping: ResetTime -> ResetTime
			// PropertyMapping: MustTorqueAndAngleBeInLimits -> MustTorqueAndAngleBeInLimits
			// PropertyMapping: CycleStart -> CycleStart
			// PropertyMapping: StartFinalAngle -> StartFinalAngle
			// PropertyMapping: SlipTorque -> SlipTorque
			// PropertyMapping: TorqueCoefficient -> TorqueCoefficient
			// PropertyMapping: MinimumPulse -> MinimumPulse
			// PropertyMapping: MaximumPulse -> MaximumPulse
			// PropertyMapping: Threshold -> Threshold
		public DtoTypes.TestTechnique DirectPropertyMapping(Server.Core.Entities.TestTechnique source)
		{
			var target = new DtoTypes.TestTechnique();
			_assigner.Assign((value) => {target.EndCycleTime = value;}, source.EndCycleTime);
			_assigner.Assign((value) => {target.FilterFrequency = value;}, source.FilterFrequency);
			_assigner.Assign((value) => {target.CycleComplete = value;}, source.CycleComplete);
			_assigner.Assign((value) => {target.MeasureDelayTime = value;}, source.MeasureDelayTime);
			_assigner.Assign((value) => {target.ResetTime = value;}, source.ResetTime);
			_assigner.Assign((value) => {target.MustTorqueAndAngleBeInLimits = value;}, source.MustTorqueAndAngleBeInLimits);
			_assigner.Assign((value) => {target.CycleStart = value;}, source.CycleStart);
			_assigner.Assign((value) => {target.StartFinalAngle = value;}, source.StartFinalAngle);
			_assigner.Assign((value) => {target.SlipTorque = value;}, source.SlipTorque);
			_assigner.Assign((value) => {target.TorqueCoefficient = value;}, source.TorqueCoefficient);
			_assigner.Assign((value) => {target.MinimumPulse = value;}, source.MinimumPulse);
			_assigner.Assign((value) => {target.MaximumPulse = value;}, source.MaximumPulse);
			_assigner.Assign((value) => {target.Threshold = value;}, source.Threshold);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestTechnique source, DtoTypes.TestTechnique target)
		{
			_assigner.Assign((value) => {target.EndCycleTime = value;}, source.EndCycleTime);
			_assigner.Assign((value) => {target.FilterFrequency = value;}, source.FilterFrequency);
			_assigner.Assign((value) => {target.CycleComplete = value;}, source.CycleComplete);
			_assigner.Assign((value) => {target.MeasureDelayTime = value;}, source.MeasureDelayTime);
			_assigner.Assign((value) => {target.ResetTime = value;}, source.ResetTime);
			_assigner.Assign((value) => {target.MustTorqueAndAngleBeInLimits = value;}, source.MustTorqueAndAngleBeInLimits);
			_assigner.Assign((value) => {target.CycleStart = value;}, source.CycleStart);
			_assigner.Assign((value) => {target.StartFinalAngle = value;}, source.StartFinalAngle);
			_assigner.Assign((value) => {target.SlipTorque = value;}, source.SlipTorque);
			_assigner.Assign((value) => {target.TorqueCoefficient = value;}, source.TorqueCoefficient);
			_assigner.Assign((value) => {target.MinimumPulse = value;}, source.MinimumPulse);
			_assigner.Assign((value) => {target.MaximumPulse = value;}, source.MaximumPulse);
			_assigner.Assign((value) => {target.Threshold = value;}, source.Threshold);
		}

		public DtoTypes.TestTechnique ReflectedPropertyMapping(Server.Core.Entities.TestTechnique source)
		{
			var target = new DtoTypes.TestTechnique();
			typeof(DtoTypes.TestTechnique).GetField("EndCycleTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndCycleTime);
			typeof(DtoTypes.TestTechnique).GetField("FilterFrequency", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FilterFrequency);
			typeof(DtoTypes.TestTechnique).GetField("CycleComplete", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleComplete);
			typeof(DtoTypes.TestTechnique).GetField("MeasureDelayTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MeasureDelayTime);
			typeof(DtoTypes.TestTechnique).GetField("ResetTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResetTime);
			typeof(DtoTypes.TestTechnique).GetField("MustTorqueAndAngleBeInLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MustTorqueAndAngleBeInLimits);
			typeof(DtoTypes.TestTechnique).GetField("CycleStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleStart);
			typeof(DtoTypes.TestTechnique).GetField("StartFinalAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartFinalAngle);
			typeof(DtoTypes.TestTechnique).GetField("SlipTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SlipTorque);
			typeof(DtoTypes.TestTechnique).GetField("TorqueCoefficient", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TorqueCoefficient);
			typeof(DtoTypes.TestTechnique).GetField("MinimumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumPulse);
			typeof(DtoTypes.TestTechnique).GetField("MaximumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumPulse);
			typeof(DtoTypes.TestTechnique).GetField("Threshold", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestTechnique source, DtoTypes.TestTechnique target)
		{
			typeof(DtoTypes.TestTechnique).GetField("EndCycleTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndCycleTime);
			typeof(DtoTypes.TestTechnique).GetField("FilterFrequency", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FilterFrequency);
			typeof(DtoTypes.TestTechnique).GetField("CycleComplete", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleComplete);
			typeof(DtoTypes.TestTechnique).GetField("MeasureDelayTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MeasureDelayTime);
			typeof(DtoTypes.TestTechnique).GetField("ResetTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResetTime);
			typeof(DtoTypes.TestTechnique).GetField("MustTorqueAndAngleBeInLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MustTorqueAndAngleBeInLimits);
			typeof(DtoTypes.TestTechnique).GetField("CycleStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleStart);
			typeof(DtoTypes.TestTechnique).GetField("StartFinalAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartFinalAngle);
			typeof(DtoTypes.TestTechnique).GetField("SlipTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SlipTorque);
			typeof(DtoTypes.TestTechnique).GetField("TorqueCoefficient", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TorqueCoefficient);
			typeof(DtoTypes.TestTechnique).GetField("MinimumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumPulse);
			typeof(DtoTypes.TestTechnique).GetField("MaximumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumPulse);
			typeof(DtoTypes.TestTechnique).GetField("Threshold", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold);
		}

		// ToleranceClassDtoToToleranceClass.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: Relative -> Relative
			// PropertyMapping: LowerLimit -> LowerLimit
			// PropertyMapping: UpperLimit -> UpperLimit
			// PropertyMapping: Alive -> Alive
		public Server.Core.Entities.ToleranceClass DirectPropertyMapping(DtoTypes.ToleranceClass source)
		{
			var target = new Server.Core.Entities.ToleranceClass();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ToleranceClass source, Server.Core.Entities.ToleranceClass target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public Server.Core.Entities.ToleranceClass ReflectedPropertyMapping(DtoTypes.ToleranceClass source)
		{
			var target = new Server.Core.Entities.ToleranceClass();
			typeof(Server.Core.Entities.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(Server.Core.Entities.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(Server.Core.Entities.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ToleranceClass source, Server.Core.Entities.ToleranceClass target)
		{
			typeof(Server.Core.Entities.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(Server.Core.Entities.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(Server.Core.Entities.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// ToleranceClassToToleranceClassDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: Relative -> Relative
			// PropertyMapping: LowerLimit -> LowerLimit
			// PropertyMapping: UpperLimit -> UpperLimit
			// PropertyMapping: Alive -> Alive
		public DtoTypes.ToleranceClass DirectPropertyMapping(Server.Core.Entities.ToleranceClass source)
		{
			var target = new DtoTypes.ToleranceClass();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ToleranceClass source, DtoTypes.ToleranceClass target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public DtoTypes.ToleranceClass ReflectedPropertyMapping(Server.Core.Entities.ToleranceClass source)
		{
			var target = new DtoTypes.ToleranceClass();
			typeof(DtoTypes.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(DtoTypes.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(DtoTypes.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
			typeof(DtoTypes.ToleranceClass).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ToleranceClass source, DtoTypes.ToleranceClass target)
		{
			typeof(DtoTypes.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(DtoTypes.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(DtoTypes.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
			typeof(DtoTypes.ToleranceClass).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// ToolDtoToTool.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: SerialNumber -> SerialNumber
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: ToolModel -> ToolModel
			// PropertyMapping: Status -> Status
			// PropertyMapping: CostCenter -> CostCenter
			// PropertyMapping: ConfigurableField -> ConfigurableField
			// PropertyMapping: Accessory -> Accessory
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: AdditionalConfigurableField1 -> AdditionalConfigurableField1
			// PropertyMapping: AdditionalConfigurableField2 -> AdditionalConfigurableField2
			// PropertyMapping: AdditionalConfigurableField3 -> AdditionalConfigurableField3
		public Server.Core.Entities.Tool DirectPropertyMapping(DtoTypes.Tool source)
		{
			var target = new Server.Core.Entities.Tool();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.CostCenter = value;}, source.CostCenter);
			_assigner.Assign((value) => {target.ConfigurableField = value;}, source.ConfigurableField);
			_assigner.Assign((value) => {target.Accessory = value;}, source.Accessory);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.AdditionalConfigurableField1 = value;}, source.AdditionalConfigurableField1);
			_assigner.Assign((value) => {target.AdditionalConfigurableField2 = value;}, source.AdditionalConfigurableField2);
			_assigner.Assign((value) => {target.AdditionalConfigurableField3 = value;}, source.AdditionalConfigurableField3);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Tool source, Server.Core.Entities.Tool target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.CostCenter = value;}, source.CostCenter);
			_assigner.Assign((value) => {target.ConfigurableField = value;}, source.ConfigurableField);
			_assigner.Assign((value) => {target.Accessory = value;}, source.Accessory);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.AdditionalConfigurableField1 = value;}, source.AdditionalConfigurableField1);
			_assigner.Assign((value) => {target.AdditionalConfigurableField2 = value;}, source.AdditionalConfigurableField2);
			_assigner.Assign((value) => {target.AdditionalConfigurableField3 = value;}, source.AdditionalConfigurableField3);
		}

		public Server.Core.Entities.Tool ReflectedPropertyMapping(DtoTypes.Tool source)
		{
			var target = new Server.Core.Entities.Tool();
			typeof(Server.Core.Entities.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Server.Core.Entities.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Server.Core.Entities.Tool).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(Server.Core.Entities.Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Server.Core.Entities.Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(Server.Core.Entities.Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(Server.Core.Entities.Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(Server.Core.Entities.Tool).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Tool source, Server.Core.Entities.Tool target)
		{
			typeof(Server.Core.Entities.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Server.Core.Entities.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Server.Core.Entities.Tool).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(Server.Core.Entities.Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Server.Core.Entities.Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(Server.Core.Entities.Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(Server.Core.Entities.Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(Server.Core.Entities.Tool).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
		}

		// ToolModelDtoToToolModel.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Description
			// PropertyMapping: Manufacturer -> Manufacturer
			// PropertyMapping: MinPower -> MinPower
			// PropertyMapping: MaxPower -> MaxPower
			// PropertyMapping: AirPressure -> AirPressure
			// PropertyMapping: ToolType -> ToolType
			// PropertyMapping: Weight -> Weight
			// PropertyMapping: BatteryVoltage -> BatteryVoltage
			// PropertyMapping: MaxRotationSpeed -> MaxRotationSpeed
			// PropertyMapping: AirConsumption -> AirConsumption
			// PropertyMapping: ShutOff -> ShutOff
			// PropertyMapping: DriveSize -> DriveSize
			// PropertyMapping: SwitchOff -> SwitchOff
			// PropertyMapping: DriveType -> DriveType
			// PropertyMapping: ConstructionType -> ConstructionType
			// PropertyMapping: CmLimit -> CmLimit
			// PropertyMapping: CmkLimit -> CmkLimit
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: ModelType -> ModelType
			// PropertyMapping: Class -> Class
		public Server.Core.Entities.ToolModel DirectPropertyMapping(DtoTypes.ToolModel source)
		{
			var target = new Server.Core.Entities.ToolModel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.MinPower = value;}, source.MinPower);
			_assigner.Assign((value) => {target.MaxPower = value;}, source.MaxPower);
			_assigner.Assign((value) => {target.AirPressure = value;}, source.AirPressure);
			_assigner.Assign((value) => {target.ToolType = value;}, source.ToolType);
			_assigner.Assign((value) => {target.Weight = value;}, source.Weight);
			_assigner.Assign((value) => {target.BatteryVoltage = value;}, source.BatteryVoltage);
			_assigner.Assign((value) => {target.MaxRotationSpeed = value;}, source.MaxRotationSpeed);
			_assigner.Assign((value) => {target.AirConsumption = value;}, source.AirConsumption);
			_assigner.Assign((value) => {target.ShutOff = value;}, source.ShutOff);
			_assigner.Assign((value) => {target.DriveSize = value;}, source.DriveSize);
			_assigner.Assign((value) => {target.SwitchOff = value;}, source.SwitchOff);
			_assigner.Assign((value) => {target.DriveType = value;}, source.DriveType);
			_assigner.Assign((value) => {target.ConstructionType = value;}, source.ConstructionType);
			_assigner.Assign((value) => {target.CmLimit = value;}, source.CmLimit);
			_assigner.Assign((value) => {target.CmkLimit = value;}, source.CmkLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ToolModel source, Server.Core.Entities.ToolModel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.MinPower = value;}, source.MinPower);
			_assigner.Assign((value) => {target.MaxPower = value;}, source.MaxPower);
			_assigner.Assign((value) => {target.AirPressure = value;}, source.AirPressure);
			_assigner.Assign((value) => {target.ToolType = value;}, source.ToolType);
			_assigner.Assign((value) => {target.Weight = value;}, source.Weight);
			_assigner.Assign((value) => {target.BatteryVoltage = value;}, source.BatteryVoltage);
			_assigner.Assign((value) => {target.MaxRotationSpeed = value;}, source.MaxRotationSpeed);
			_assigner.Assign((value) => {target.AirConsumption = value;}, source.AirConsumption);
			_assigner.Assign((value) => {target.ShutOff = value;}, source.ShutOff);
			_assigner.Assign((value) => {target.DriveSize = value;}, source.DriveSize);
			_assigner.Assign((value) => {target.SwitchOff = value;}, source.SwitchOff);
			_assigner.Assign((value) => {target.DriveType = value;}, source.DriveType);
			_assigner.Assign((value) => {target.ConstructionType = value;}, source.ConstructionType);
			_assigner.Assign((value) => {target.CmLimit = value;}, source.CmLimit);
			_assigner.Assign((value) => {target.CmkLimit = value;}, source.CmkLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
		}

		public Server.Core.Entities.ToolModel ReflectedPropertyMapping(DtoTypes.ToolModel source)
		{
			var target = new Server.Core.Entities.ToolModel();
			typeof(Server.Core.Entities.ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(Server.Core.Entities.ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(Server.Core.Entities.ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(Server.Core.Entities.ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(Server.Core.Entities.ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(Server.Core.Entities.ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(Server.Core.Entities.ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(Server.Core.Entities.ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(Server.Core.Entities.ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(Server.Core.Entities.ToolModel).GetField("CmLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmLimit);
			typeof(Server.Core.Entities.ToolModel).GetField("CmkLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmkLimit);
			typeof(Server.Core.Entities.ToolModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(Server.Core.Entities.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ToolModel source, Server.Core.Entities.ToolModel target)
		{
			typeof(Server.Core.Entities.ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(Server.Core.Entities.ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(Server.Core.Entities.ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(Server.Core.Entities.ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(Server.Core.Entities.ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(Server.Core.Entities.ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(Server.Core.Entities.ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(Server.Core.Entities.ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(Server.Core.Entities.ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(Server.Core.Entities.ToolModel).GetField("CmLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmLimit);
			typeof(Server.Core.Entities.ToolModel).GetField("CmkLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmkLimit);
			typeof(Server.Core.Entities.ToolModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(Server.Core.Entities.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(Server.Core.Entities.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
		}

		// ToolModelReferenceLinkToModelLinkDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: DisplayName -> Model
		public DtoTypes.ModelLink DirectPropertyMapping(Server.Core.Entities.ReferenceLink.ToolModelReferenceLink source)
		{
			var target = new DtoTypes.ModelLink();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Model = value;}, source.DisplayName);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ReferenceLink.ToolModelReferenceLink source, DtoTypes.ModelLink target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Model = value;}, source.DisplayName);
		}

		public DtoTypes.ModelLink ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.ToolModelReferenceLink source)
		{
			var target = new DtoTypes.ModelLink();
			typeof(DtoTypes.ModelLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ModelLink).GetField("Model", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DisplayName);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.ToolModelReferenceLink source, DtoTypes.ModelLink target)
		{
			typeof(DtoTypes.ModelLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ModelLink).GetField("Model", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DisplayName);
		}

		// ToolModelToToolModelDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Description
			// PropertyMapping: Manufacturer -> Manufacturer
			// PropertyMapping: MinPower -> MinPower
			// PropertyMapping: MaxPower -> MaxPower
			// PropertyMapping: AirPressure -> AirPressure
			// PropertyMapping: ToolType -> ToolType
			// PropertyMapping: Weight -> Weight
			// PropertyMapping: BatteryVoltage -> BatteryVoltage
			// PropertyMapping: MaxRotationSpeed -> MaxRotationSpeed
			// PropertyMapping: AirConsumption -> AirConsumption
			// PropertyMapping: ShutOff -> ShutOff
			// PropertyMapping: DriveSize -> DriveSize
			// PropertyMapping: SwitchOff -> SwitchOff
			// PropertyMapping: DriveType -> DriveType
			// PropertyMapping: ConstructionType -> ConstructionType
			// PropertyMapping: CmLimit -> CmLimit
			// PropertyMapping: CmkLimit -> CmkLimit
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: ModelType -> ModelType
			// PropertyMapping: Class -> Class
		public DtoTypes.ToolModel DirectPropertyMapping(Server.Core.Entities.ToolModel source)
		{
			var target = new DtoTypes.ToolModel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.MinPower = value;}, source.MinPower);
			_assigner.Assign((value) => {target.MaxPower = value;}, source.MaxPower);
			_assigner.Assign((value) => {target.AirPressure = value;}, source.AirPressure);
			_assigner.Assign((value) => {target.ToolType = value;}, source.ToolType);
			_assigner.Assign((value) => {target.Weight = value;}, source.Weight);
			_assigner.Assign((value) => {target.BatteryVoltage = value;}, source.BatteryVoltage);
			_assigner.Assign((value) => {target.MaxRotationSpeed = value;}, source.MaxRotationSpeed);
			_assigner.Assign((value) => {target.AirConsumption = value;}, source.AirConsumption);
			_assigner.Assign((value) => {target.ShutOff = value;}, source.ShutOff);
			_assigner.Assign((value) => {target.DriveSize = value;}, source.DriveSize);
			_assigner.Assign((value) => {target.SwitchOff = value;}, source.SwitchOff);
			_assigner.Assign((value) => {target.DriveType = value;}, source.DriveType);
			_assigner.Assign((value) => {target.ConstructionType = value;}, source.ConstructionType);
			_assigner.Assign((value) => {target.CmLimit = value;}, source.CmLimit);
			_assigner.Assign((value) => {target.CmkLimit = value;}, source.CmkLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ToolModel source, DtoTypes.ToolModel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.MinPower = value;}, source.MinPower);
			_assigner.Assign((value) => {target.MaxPower = value;}, source.MaxPower);
			_assigner.Assign((value) => {target.AirPressure = value;}, source.AirPressure);
			_assigner.Assign((value) => {target.ToolType = value;}, source.ToolType);
			_assigner.Assign((value) => {target.Weight = value;}, source.Weight);
			_assigner.Assign((value) => {target.BatteryVoltage = value;}, source.BatteryVoltage);
			_assigner.Assign((value) => {target.MaxRotationSpeed = value;}, source.MaxRotationSpeed);
			_assigner.Assign((value) => {target.AirConsumption = value;}, source.AirConsumption);
			_assigner.Assign((value) => {target.ShutOff = value;}, source.ShutOff);
			_assigner.Assign((value) => {target.DriveSize = value;}, source.DriveSize);
			_assigner.Assign((value) => {target.SwitchOff = value;}, source.SwitchOff);
			_assigner.Assign((value) => {target.DriveType = value;}, source.DriveType);
			_assigner.Assign((value) => {target.ConstructionType = value;}, source.ConstructionType);
			_assigner.Assign((value) => {target.CmLimit = value;}, source.CmLimit);
			_assigner.Assign((value) => {target.CmkLimit = value;}, source.CmkLimit);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
		}

		public DtoTypes.ToolModel ReflectedPropertyMapping(Server.Core.Entities.ToolModel source)
		{
			var target = new DtoTypes.ToolModel();
			typeof(DtoTypes.ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DtoTypes.ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(DtoTypes.ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(DtoTypes.ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(DtoTypes.ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(DtoTypes.ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(DtoTypes.ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(DtoTypes.ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(DtoTypes.ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(DtoTypes.ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(DtoTypes.ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(DtoTypes.ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(DtoTypes.ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(DtoTypes.ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(DtoTypes.ToolModel).GetField("CmLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmLimit);
			typeof(DtoTypes.ToolModel).GetField("CmkLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmkLimit);
			typeof(DtoTypes.ToolModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(DtoTypes.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ToolModel source, DtoTypes.ToolModel target)
		{
			typeof(DtoTypes.ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DtoTypes.ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(DtoTypes.ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(DtoTypes.ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(DtoTypes.ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(DtoTypes.ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(DtoTypes.ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(DtoTypes.ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(DtoTypes.ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(DtoTypes.ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(DtoTypes.ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(DtoTypes.ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(DtoTypes.ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(DtoTypes.ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(DtoTypes.ToolModel).GetField("CmLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmLimit);
			typeof(DtoTypes.ToolModel).GetField("CmkLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmkLimit);
			typeof(DtoTypes.ToolModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(DtoTypes.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
		}

		// ToolReferenceLinkToToolReferenceLinkDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: SerialNumber -> SerialNumber
		public DtoTypes.ToolReferenceLink DirectPropertyMapping(Server.Core.Entities.ReferenceLink.ToolReferenceLink source)
		{
			var target = new DtoTypes.ToolReferenceLink();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ReferenceLink.ToolReferenceLink source, DtoTypes.ToolReferenceLink target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
		}

		public DtoTypes.ToolReferenceLink ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.ToolReferenceLink source)
		{
			var target = new DtoTypes.ToolReferenceLink();
			typeof(DtoTypes.ToolReferenceLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToolReferenceLink).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.ToolReferenceLink).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ReferenceLink.ToolReferenceLink source, DtoTypes.ToolReferenceLink target)
		{
			typeof(DtoTypes.ToolReferenceLink).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToolReferenceLink).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.ToolReferenceLink).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
		}

		// ToolToToolDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: SerialNumber -> SerialNumber
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: Alive -> Alive
			// PropertyMapping: ToolModel -> ToolModel
			// PropertyMapping: Status -> Status
			// PropertyMapping: CostCenter -> CostCenter
			// PropertyMapping: ConfigurableField -> ConfigurableField
			// PropertyMapping: Accessory -> Accessory
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: AdditionalConfigurableField1 -> AdditionalConfigurableField1
			// PropertyMapping: AdditionalConfigurableField2 -> AdditionalConfigurableField2
			// PropertyMapping: AdditionalConfigurableField3 -> AdditionalConfigurableField3
		public DtoTypes.Tool DirectPropertyMapping(Server.Core.Entities.Tool source)
		{
			var target = new DtoTypes.Tool();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.CostCenter = value;}, source.CostCenter);
			_assigner.Assign((value) => {target.ConfigurableField = value;}, source.ConfigurableField);
			_assigner.Assign((value) => {target.Accessory = value;}, source.Accessory);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.AdditionalConfigurableField1 = value;}, source.AdditionalConfigurableField1);
			_assigner.Assign((value) => {target.AdditionalConfigurableField2 = value;}, source.AdditionalConfigurableField2);
			_assigner.Assign((value) => {target.AdditionalConfigurableField3 = value;}, source.AdditionalConfigurableField3);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Tool source, DtoTypes.Tool target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.Status = value;}, source.Status);
			_assigner.Assign((value) => {target.CostCenter = value;}, source.CostCenter);
			_assigner.Assign((value) => {target.ConfigurableField = value;}, source.ConfigurableField);
			_assigner.Assign((value) => {target.Accessory = value;}, source.Accessory);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.AdditionalConfigurableField1 = value;}, source.AdditionalConfigurableField1);
			_assigner.Assign((value) => {target.AdditionalConfigurableField2 = value;}, source.AdditionalConfigurableField2);
			_assigner.Assign((value) => {target.AdditionalConfigurableField3 = value;}, source.AdditionalConfigurableField3);
		}

		public DtoTypes.Tool ReflectedPropertyMapping(Server.Core.Entities.Tool source)
		{
			var target = new DtoTypes.Tool();
			typeof(DtoTypes.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.Tool).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(DtoTypes.Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DtoTypes.Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(DtoTypes.Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(DtoTypes.Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(DtoTypes.Tool).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DtoTypes.Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(DtoTypes.Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(DtoTypes.Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Tool source, DtoTypes.Tool target)
		{
			typeof(DtoTypes.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.Tool).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DtoTypes.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(DtoTypes.Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DtoTypes.Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(DtoTypes.Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(DtoTypes.Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(DtoTypes.Tool).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DtoTypes.Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(DtoTypes.Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(DtoTypes.Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
		}

		// ToolUsageDtoToToolUsage.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Description
			// PropertyMapping: Alive -> Alive
		public Server.Core.Entities.ToolUsage DirectPropertyMapping(DtoTypes.ToolUsage source)
		{
			var target = new Server.Core.Entities.ToolUsage();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ToolUsage source, Server.Core.Entities.ToolUsage target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public Server.Core.Entities.ToolUsage ReflectedPropertyMapping(DtoTypes.ToolUsage source)
		{
			var target = new Server.Core.Entities.ToolUsage();
			typeof(Server.Core.Entities.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.ToolUsage).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ToolUsage source, Server.Core.Entities.ToolUsage target)
		{
			typeof(Server.Core.Entities.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.ToolUsage).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// ToolUsageToToolUsageDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Description
			// PropertyMapping: Alive -> Alive
		public DtoTypes.ToolUsage DirectPropertyMapping(Server.Core.Entities.ToolUsage source)
		{
			var target = new DtoTypes.ToolUsage();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ToolUsage source, DtoTypes.ToolUsage target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Alive = value;}, source.Alive);
		}

		public DtoTypes.ToolUsage ReflectedPropertyMapping(Server.Core.Entities.ToolUsage source)
		{
			var target = new DtoTypes.ToolUsage();
			typeof(DtoTypes.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.ToolUsage).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ToolUsage source, DtoTypes.ToolUsage target)
		{
			typeof(DtoTypes.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.ToolUsage).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// UserDtoToUser.txt
			// hasDefaultConstructor
			// PropertyMapping: UserId -> UserId
			// PropertyMapping: UserName -> UserName
		public Server.Core.Entities.User DirectPropertyMapping(DtoTypes.User source)
		{
			var target = new Server.Core.Entities.User();
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.User source, Server.Core.Entities.User target)
		{
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
		}

		public Server.Core.Entities.User ReflectedPropertyMapping(DtoTypes.User source)
		{
			var target = new Server.Core.Entities.User();
			typeof(Server.Core.Entities.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(Server.Core.Entities.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.User source, Server.Core.Entities.User target)
		{
			typeof(Server.Core.Entities.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(Server.Core.Entities.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
		}

		// UserToUserDto.txt
			// hasDefaultConstructor
			// PropertyMapping: UserId -> UserId
			// PropertyMapping: UserName -> UserName
		public DtoTypes.User DirectPropertyMapping(Server.Core.Entities.User source)
		{
			var target = new DtoTypes.User();
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.User source, DtoTypes.User target)
		{
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
		}

		public DtoTypes.User ReflectedPropertyMapping(Server.Core.Entities.User source)
		{
			var target = new DtoTypes.User();
			typeof(DtoTypes.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(DtoTypes.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.User source, DtoTypes.User target)
		{
			typeof(DtoTypes.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(DtoTypes.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
		}

		// WorkingCalendarEntryDtoToWorkingCalendarEntry.txt
			// hasDefaultConstructor
			// PropertyMapping: Date -> Date
			// PropertyMapping: Description -> Description
			// PropertyMapping: Repeated -> Repetition
			// PropertyMapping: IsFree -> Type
		public Server.Core.Entities.WorkingCalendarEntry DirectPropertyMapping(DtoTypes.WorkingCalendarEntry source)
		{
			var target = new Server.Core.Entities.WorkingCalendarEntry();
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repetition = value;}, source.Repeated);
			_assigner.Assign((value) => {target.Type = value;}, source.IsFree);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.WorkingCalendarEntry source, Server.Core.Entities.WorkingCalendarEntry target)
		{
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repetition = value;}, source.Repeated);
			_assigner.Assign((value) => {target.Type = value;}, source.IsFree);
		}

		public Server.Core.Entities.WorkingCalendarEntry ReflectedPropertyMapping(DtoTypes.WorkingCalendarEntry source)
		{
			var target = new Server.Core.Entities.WorkingCalendarEntry();
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Repetition", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repeated);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsFree);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.WorkingCalendarEntry source, Server.Core.Entities.WorkingCalendarEntry target)
		{
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Repetition", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repeated);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsFree);
		}

		// WorkingCalendarEntryToWorkingCalendarEntryDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Date -> Date
			// PropertyMapping: Description -> Description
			// PropertyMapping: Repetition -> Repeated
			// PropertyMapping: Type -> IsFree
		public DtoTypes.WorkingCalendarEntry DirectPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source)
		{
			var target = new DtoTypes.WorkingCalendarEntry();
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repeated = value;}, source.Repetition);
			_assigner.Assign((value) => {target.IsFree = value;}, source.Type);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source, DtoTypes.WorkingCalendarEntry target)
		{
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repeated = value;}, source.Repetition);
			_assigner.Assign((value) => {target.IsFree = value;}, source.Type);
		}

		public DtoTypes.WorkingCalendarEntry ReflectedPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source)
		{
			var target = new DtoTypes.WorkingCalendarEntry();
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Repeated", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repetition);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("IsFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source, DtoTypes.WorkingCalendarEntry target)
		{
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Repeated", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repetition);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("IsFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
		}

	}
}

////////////
//

