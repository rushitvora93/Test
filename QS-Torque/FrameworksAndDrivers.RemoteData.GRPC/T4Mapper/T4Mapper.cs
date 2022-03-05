
using System.Reflection;
using Core.Entities;
using T4Mapper;

namespace FrameworksAndDrivers.RemoteData.GRPC.T4Mapper
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
		public Core.Entities.ClassicChkTest DirectPropertyMapping(DtoTypes.ClassicChkTest source)
		{
			var target = new Core.Entities.ClassicChkTest();
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
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicChkTest source, Core.Entities.ClassicChkTest target)
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
		}

		public Core.Entities.ClassicChkTest ReflectedPropertyMapping(DtoTypes.ClassicChkTest source)
		{
			var target = new Core.Entities.ClassicChkTest();
			typeof(Core.Entities.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicChkTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Core.Entities.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Core.Entities.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Core.Entities.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Core.Entities.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Core.Entities.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Core.Entities.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Core.Entities.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Core.Entities.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Core.Entities.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Core.Entities.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Core.Entities.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Core.Entities.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicChkTest source, Core.Entities.ClassicChkTest target)
		{
			typeof(Core.Entities.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicChkTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Core.Entities.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Core.Entities.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Core.Entities.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Core.Entities.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Core.Entities.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Core.Entities.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Core.Entities.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Core.Entities.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Core.Entities.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Core.Entities.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Core.Entities.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Core.Entities.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Core.Entities.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
		}

		// ClassicChkTestValueDtoToClassicChkTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public Core.Entities.ClassicChkTestValue DirectPropertyMapping(DtoTypes.ClassicChkTestValue source)
		{
			var target = new Core.Entities.ClassicChkTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicChkTestValue source, Core.Entities.ClassicChkTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public Core.Entities.ClassicChkTestValue ReflectedPropertyMapping(DtoTypes.ClassicChkTestValue source)
		{
			var target = new Core.Entities.ClassicChkTestValue();
			typeof(Core.Entities.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Core.Entities.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Core.Entities.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicChkTestValue source, Core.Entities.ClassicChkTestValue target)
		{
			typeof(Core.Entities.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Core.Entities.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Core.Entities.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
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
			// PropertyMapping: Cmk -> Cmk
			// PropertyMapping: Cm -> Cm
			// PropertyMapping: LimitCm -> LimitCm
			// PropertyMapping: LimitCmk -> LimitCmk
			// PropertyMapping: TestLocation -> TestLocation
			// PropertyMapping: TestEquipment -> TestEquipment
		public Core.Entities.ClassicMfuTest DirectPropertyMapping(DtoTypes.ClassicMfuTest source)
		{
			var target = new Core.Entities.ClassicMfuTest();
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
			_assigner.Assign((value) => {target.Cmk = value;}, source.Cmk);
			_assigner.Assign((value) => {target.Cm = value;}, source.Cm);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicMfuTest source, Core.Entities.ClassicMfuTest target)
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
			_assigner.Assign((value) => {target.Cmk = value;}, source.Cmk);
			_assigner.Assign((value) => {target.Cm = value;}, source.Cm);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.TestLocation);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
		}

		public Core.Entities.ClassicMfuTest ReflectedPropertyMapping(DtoTypes.ClassicMfuTest source)
		{
			var target = new Core.Entities.ClassicMfuTest();
			typeof(Core.Entities.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicMfuTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Core.Entities.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Core.Entities.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Core.Entities.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Core.Entities.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Core.Entities.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Core.Entities.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Core.Entities.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(Core.Entities.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(Core.Entities.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(Core.Entities.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicMfuTest source, Core.Entities.ClassicMfuTest target)
		{
			typeof(Core.Entities.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicMfuTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Core.Entities.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Core.Entities.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Core.Entities.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Core.Entities.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Core.Entities.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(Core.Entities.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(Core.Entities.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(Core.Entities.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(Core.Entities.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(Core.Entities.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(Core.Entities.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Core.Entities.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
		}

		// ClassicMfuTestValueDtoToClassicMfuTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public Core.Entities.ClassicMfuTestValue DirectPropertyMapping(DtoTypes.ClassicMfuTestValue source)
		{
			var target = new Core.Entities.ClassicMfuTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicMfuTestValue source, Core.Entities.ClassicMfuTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public Core.Entities.ClassicMfuTestValue ReflectedPropertyMapping(DtoTypes.ClassicMfuTestValue source)
		{
			var target = new Core.Entities.ClassicMfuTestValue();
			typeof(Core.Entities.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Core.Entities.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Core.Entities.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicMfuTestValue source, Core.Entities.ClassicMfuTestValue target)
		{
			typeof(Core.Entities.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Core.Entities.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Core.Entities.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
		}

		// ClassicProcessTestDtoToClassicProcessTest.txt
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
		public Client.Core.Entities.ClassicProcessTest DirectPropertyMapping(DtoTypes.ClassicProcessTest source)
		{
			var target = new Client.Core.Entities.ClassicProcessTest();
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

		public void DirectPropertyMapping(DtoTypes.ClassicProcessTest source, Client.Core.Entities.ClassicProcessTest target)
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

		public Client.Core.Entities.ClassicProcessTest ReflectedPropertyMapping(DtoTypes.ClassicProcessTest source)
		{
			var target = new Client.Core.Entities.ClassicProcessTest();
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicProcessTest source, Client.Core.Entities.ClassicProcessTest target)
		{
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Timestamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Timestamp);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimitUnit2);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLocation);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(Client.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
		}

		// ClassicProcessTestValueDtoToClassicProcessTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Position -> Position
			// PropertyMapping: ValueUnit1 -> ValueUnit1
			// PropertyMapping: ValueUnit2 -> ValueUnit2
		public Client.Core.Entities.ClassicProcessTestValue DirectPropertyMapping(DtoTypes.ClassicProcessTestValue source)
		{
			var target = new Client.Core.Entities.ClassicProcessTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ClassicProcessTestValue source, Client.Core.Entities.ClassicProcessTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Position = value;}, source.Position);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.ValueUnit1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.ValueUnit2);
		}

		public Client.Core.Entities.ClassicProcessTestValue ReflectedPropertyMapping(DtoTypes.ClassicProcessTestValue source)
		{
			var target = new Client.Core.Entities.ClassicProcessTestValue();
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ClassicProcessTestValue source, Client.Core.Entities.ClassicProcessTestValue target)
		{
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Position);
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit1);
			typeof(Client.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ValueUnit2);
		}

		// ConfigurableFieldToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(ConfigurableField source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(ConfigurableField source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(ConfigurableField source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(ConfigurableField source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// ConstructionTypeToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(ConstructionType source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(ConstructionType source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(ConstructionType source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(ConstructionType source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// CostCenterToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(CostCenter source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(CostCenter source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(CostCenter source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(CostCenter source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// DriveSizeToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(DriveSize source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(DriveSize source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(DriveSize source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(DriveSize source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// DriveTypeToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(DriveType source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(DriveType source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(DriveType source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(DriveType source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// ExtensionDtoToExtension.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Description -> Description
			// PropertyMapping: Length -> Length
			// PropertyMapping: FactorTorque -> FactorTorque
			// PropertyMapping: Bending -> Bending
			// PropertyMapping: InventoryNumber -> InventoryNumber
		public Extension DirectPropertyMapping(DtoTypes.Extension source)
		{
			var target = new Extension();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Extension source, Extension target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
		}

		public Extension ReflectedPropertyMapping(DtoTypes.Extension source)
		{
			var target = new Extension();
			typeof(Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Extension source, Extension target)
		{
			typeof(Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
		}

		// ExtensionToExtensionDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: Description -> Description
			// PropertyMapping: Length -> Length
			// PropertyMapping: FactorTorque -> FactorTorque
			// PropertyMapping: Bending -> Bending
		public DtoTypes.Extension DirectPropertyMapping(Extension source)
		{
			var target = new DtoTypes.Extension();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
			return target;
		}

		public void DirectPropertyMapping(Extension source, DtoTypes.Extension target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Length = value;}, source.Length);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.Bending = value;}, source.Bending);
		}

		public DtoTypes.Extension ReflectedPropertyMapping(Extension source)
		{
			var target = new DtoTypes.Extension();
			typeof(DtoTypes.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(DtoTypes.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(DtoTypes.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			return target;
		}

		public void ReflectedPropertyMapping(Extension source, DtoTypes.Extension target)
		{
			typeof(DtoTypes.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(DtoTypes.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(DtoTypes.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
		}

		// HelperTableEntityDtoToConfigurableField.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, ConfigurableField target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, ConfigurableField target)
		{
			typeof(ConfigurableField).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(ConfigurableField).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToConstructionType.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, ConstructionType target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, ConstructionType target)
		{
			typeof(ConstructionType).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(ConstructionType).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToCostCenter.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, CostCenter target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, CostCenter target)
		{
			typeof(CostCenter).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(CostCenter).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToDriveSize.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, DriveSize target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, DriveSize target)
		{
			typeof(DriveSize).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DriveSize).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToDriveType.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, DriveType target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, DriveType target)
		{
			typeof(DriveType).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DriveType).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToReasonForToolChange.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, ReasonForToolChange target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, ReasonForToolChange target)
		{
			typeof(ReasonForToolChange).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(ReasonForToolChange).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToShutOff.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, ShutOff target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, ShutOff target)
		{
			typeof(ShutOff).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(ShutOff).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToSwitchOff.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, SwitchOff target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, SwitchOff target)
		{
			typeof(SwitchOff).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(SwitchOff).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// HelperTableEntityDtoToToolType.txt
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public void DirectPropertyMapping(DtoTypes.HelperTableEntity source, ToolType target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public void ReflectedPropertyMapping(DtoTypes.HelperTableEntity source, ToolType target)
		{
			typeof(ToolType).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(ToolType).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// LocationDiffDtoToLocationDiff.txt
			// hasDefaultConstructor
			// PropertyMapping: OldLocation -> OldLocation
			// PropertyMapping: NewLocation -> NewLocation
			// PropertyMapping: User -> User
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: Type -> Type
			// PropertyMapping: TimeStamp -> TimeStamp
		public Core.Diffs.LocationDiff DirectPropertyMapping(DtoTypes.LocationDiff source)
		{
			var target = new Core.Diffs.LocationDiff();
			_assigner.Assign((value) => {target.OldLocation = value;}, source.OldLocation);
			_assigner.Assign((value) => {target.NewLocation = value;}, source.NewLocation);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TimeStamp = value;}, source.TimeStamp);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.LocationDiff source, Core.Diffs.LocationDiff target)
		{
			_assigner.Assign((value) => {target.OldLocation = value;}, source.OldLocation);
			_assigner.Assign((value) => {target.NewLocation = value;}, source.NewLocation);
			_assigner.Assign((value) => {target.User = value;}, source.User);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.Type = value;}, source.Type);
			_assigner.Assign((value) => {target.TimeStamp = value;}, source.TimeStamp);
		}

		public Core.Diffs.LocationDiff ReflectedPropertyMapping(DtoTypes.LocationDiff source)
		{
			var target = new Core.Diffs.LocationDiff();
			typeof(Core.Diffs.LocationDiff).GetField("OldLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.OldLocation);
			typeof(Core.Diffs.LocationDiff).GetField("NewLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NewLocation);
			typeof(Core.Diffs.LocationDiff).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(Core.Diffs.LocationDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Core.Diffs.LocationDiff).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(Core.Diffs.LocationDiff).GetField("TimeStamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TimeStamp);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.LocationDiff source, Core.Diffs.LocationDiff target)
		{
			typeof(Core.Diffs.LocationDiff).GetField("OldLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.OldLocation);
			typeof(Core.Diffs.LocationDiff).GetField("NewLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NewLocation);
			typeof(Core.Diffs.LocationDiff).GetField("User", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(Core.Diffs.LocationDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Core.Diffs.LocationDiff).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(Core.Diffs.LocationDiff).GetField("TimeStamp", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TimeStamp);
		}

		// LocationDirectoryDtoToLocationDirectory.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: ParentId -> ParentId
		public LocationDirectory DirectPropertyMapping(DtoTypes.LocationDirectory source)
		{
			var target = new LocationDirectory();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.LocationDirectory source, LocationDirectory target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
		}

		public LocationDirectory ReflectedPropertyMapping(DtoTypes.LocationDirectory source)
		{
			var target = new LocationDirectory();
			typeof(LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.LocationDirectory source, LocationDirectory target)
		{
			typeof(LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
		}

		// LocationDirectoryToLocationDirectoryDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: ParentId -> ParentId
		public DtoTypes.LocationDirectory DirectPropertyMapping(LocationDirectory source)
		{
			var target = new DtoTypes.LocationDirectory();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
			return target;
		}

		public void DirectPropertyMapping(LocationDirectory source, DtoTypes.LocationDirectory target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.ParentId = value;}, source.ParentId);
		}

		public DtoTypes.LocationDirectory ReflectedPropertyMapping(LocationDirectory source)
		{
			var target = new DtoTypes.LocationDirectory();
			typeof(DtoTypes.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			return target;
		}

		public void ReflectedPropertyMapping(LocationDirectory source, DtoTypes.LocationDirectory target)
		{
			typeof(DtoTypes.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
		}

		// LocationDtoToLocation.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Number -> Number
			// PropertyMapping: Description -> Description
			// PropertyMapping: ParentDirectoryId -> ParentDirectoryId
			// PropertyMapping: ControlledBy -> ControlledBy
			// PropertyMapping: SetPoint1 -> SetPointTorque
			// PropertyMapping: Minimum1 -> MinimumTorque
			// PropertyMapping: Maximum1 -> MaximumTorque
			// PropertyMapping: Threshold1 -> ThresholdTorque
			// PropertyMapping: SetPoint2 -> SetPointAngle
			// PropertyMapping: Minimum2 -> MinimumAngle
			// PropertyMapping: Maximum2 -> MaximumAngle
			// PropertyMapping: ConfigurableField1 -> ConfigurableField1
			// PropertyMapping: ConfigurableField2 -> ConfigurableField2
			// PropertyMapping: ConfigurableField3 -> ConfigurableField3
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: ToleranceClass1 -> ToleranceClassTorque
			// PropertyMapping: ToleranceClass2 -> ToleranceClassAngle
		public Location DirectPropertyMapping(DtoTypes.Location source)
		{
			var target = new Location();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPointTorque = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.MinimumTorque = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.MaximumTorque = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPointAngle = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.MinimumAngle = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.MaximumAngle = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.ToleranceClassTorque = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.ToleranceClassAngle = value;}, source.ToleranceClass2);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Location source, Location target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPointTorque = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.MinimumTorque = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.MaximumTorque = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPointAngle = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.MinimumAngle = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.MaximumAngle = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.ToleranceClassTorque = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.ToleranceClassAngle = value;}, source.ToleranceClass2);
		}

		public Location ReflectedPropertyMapping(DtoTypes.Location source)
		{
			var target = new Location();
			typeof(Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(Location).GetField("SetPointTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(Location).GetField("MinimumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(Location).GetField("MaximumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(Location).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(Location).GetField("SetPointAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(Location).GetField("MinimumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(Location).GetField("MaximumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Location).GetField("ToleranceClassTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(Location).GetField("ToleranceClassAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Location source, Location target)
		{
			typeof(Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(Location).GetField("SetPointTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(Location).GetField("MinimumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(Location).GetField("MaximumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(Location).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(Location).GetField("SetPointAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(Location).GetField("MinimumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(Location).GetField("MaximumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Location).GetField("ToleranceClassTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(Location).GetField("ToleranceClassAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
		}

		// LocationToLocationDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Number -> Number
			// PropertyMapping: Description -> Description
			// PropertyMapping: ParentDirectoryId -> ParentDirectoryId
			// PropertyMapping: ControlledBy -> ControlledBy
			// PropertyMapping: SetPointTorque -> SetPoint1
			// PropertyMapping: MinimumTorque -> Minimum1
			// PropertyMapping: MaximumTorque -> Maximum1
			// PropertyMapping: ThresholdTorque -> Threshold1
			// PropertyMapping: SetPointAngle -> SetPoint2
			// PropertyMapping: MinimumAngle -> Minimum2
			// PropertyMapping: MaximumAngle -> Maximum2
			// PropertyMapping: ConfigurableField1 -> ConfigurableField1
			// PropertyMapping: ConfigurableField2 -> ConfigurableField2
			// PropertyMapping: ConfigurableField3 -> ConfigurableField3
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: ToleranceClassTorque -> ToleranceClass1
			// PropertyMapping: ToleranceClassAngle -> ToleranceClass2
		public DtoTypes.Location DirectPropertyMapping(Location source)
		{
			var target = new DtoTypes.Location();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPointTorque);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.MinimumTorque);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.MaximumTorque);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPointAngle);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.MinimumAngle);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.MaximumAngle);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClassTorque);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClassAngle);
			return target;
		}

		public void DirectPropertyMapping(Location source, DtoTypes.Location target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Number = value;}, source.Number);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPointTorque);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.MinimumTorque);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.MaximumTorque);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPointAngle);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.MinimumAngle);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.MaximumAngle);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClassTorque);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClassAngle);
		}

		public DtoTypes.Location ReflectedPropertyMapping(Location source)
		{
			var target = new DtoTypes.Location();
			typeof(DtoTypes.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DtoTypes.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(DtoTypes.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(DtoTypes.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointTorque);
			typeof(DtoTypes.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorque);
			typeof(DtoTypes.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumTorque);
			typeof(DtoTypes.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointAngle);
			typeof(DtoTypes.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumAngle);
			typeof(DtoTypes.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumAngle);
			typeof(DtoTypes.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(DtoTypes.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(DtoTypes.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(DtoTypes.Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DtoTypes.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassTorque);
			typeof(DtoTypes.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassAngle);
			return target;
		}

		public void ReflectedPropertyMapping(Location source, DtoTypes.Location target)
		{
			typeof(DtoTypes.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DtoTypes.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(DtoTypes.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(DtoTypes.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointTorque);
			typeof(DtoTypes.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorque);
			typeof(DtoTypes.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumTorque);
			typeof(DtoTypes.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointAngle);
			typeof(DtoTypes.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumAngle);
			typeof(DtoTypes.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumAngle);
			typeof(DtoTypes.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(DtoTypes.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(DtoTypes.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(DtoTypes.Location).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DtoTypes.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassTorque);
			typeof(DtoTypes.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassAngle);
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
			// PropertyMapping: StartDateMfu -> StartDateMfu
			// PropertyMapping: StartDateChk -> StartDateChk
			// PropertyMapping: TestOperationActiveMfu -> TestOperationActiveMfu
			// PropertyMapping: TestOperationActiveChk -> TestOperationActiveChk
		public LocationToolAssignment DirectPropertyMapping(DtoTypes.LocationToolAssignment source)
		{
			var target = new LocationToolAssignment();
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
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.LocationToolAssignment source, LocationToolAssignment target)
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
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
		}

		public LocationToolAssignment ReflectedPropertyMapping(DtoTypes.LocationToolAssignment source)
		{
			var target = new LocationToolAssignment();
			typeof(LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(LocationToolAssignment).GetField("AssignedLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedLocation);
			typeof(LocationToolAssignment).GetField("AssignedTool", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedTool);
			typeof(LocationToolAssignment).GetField("ToolUsage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsage);
			typeof(LocationToolAssignment).GetField("TestParameters", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestParameters);
			typeof(LocationToolAssignment).GetField("TestTechnique", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestTechnique);
			typeof(LocationToolAssignment).GetField("TestLevelSetMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetMfu);
			typeof(LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(LocationToolAssignment).GetField("TestLevelSetChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetChk);
			typeof(LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(LocationToolAssignment).GetField("NextTestShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(LocationToolAssignment).GetField("NextTestShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.LocationToolAssignment source, LocationToolAssignment target)
		{
			typeof(LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(LocationToolAssignment).GetField("AssignedLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedLocation);
			typeof(LocationToolAssignment).GetField("AssignedTool", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AssignedTool);
			typeof(LocationToolAssignment).GetField("ToolUsage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolUsage);
			typeof(LocationToolAssignment).GetField("TestParameters", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestParameters);
			typeof(LocationToolAssignment).GetField("TestTechnique", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestTechnique);
			typeof(LocationToolAssignment).GetField("TestLevelSetMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetMfu);
			typeof(LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(LocationToolAssignment).GetField("TestLevelSetChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSetChk);
			typeof(LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(LocationToolAssignment).GetField("NextTestShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(LocationToolAssignment).GetField("NextTestShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
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
			// PropertyMapping: StartDateMfu -> StartDateMfu
			// PropertyMapping: StartDateChk -> StartDateChk
			// PropertyMapping: TestOperationActiveMfu -> TestOperationActiveMfu
			// PropertyMapping: TestOperationActiveChk -> TestOperationActiveChk
		public DtoTypes.LocationToolAssignment DirectPropertyMapping(LocationToolAssignment source)
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
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
			return target;
		}

		public void DirectPropertyMapping(LocationToolAssignment source, DtoTypes.LocationToolAssignment target)
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
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.TestOperationActiveChk);
		}

		public DtoTypes.LocationToolAssignment ReflectedPropertyMapping(LocationToolAssignment source)
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
			typeof(DtoTypes.LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(DtoTypes.LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
			return target;
		}

		public void ReflectedPropertyMapping(LocationToolAssignment source, DtoTypes.LocationToolAssignment target)
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
		public Manufacturer DirectPropertyMapping(DtoTypes.Manufacturer source)
		{
			var target = new Manufacturer();
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
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Manufacturer source, Manufacturer target)
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
		}

		public Manufacturer ReflectedPropertyMapping(DtoTypes.Manufacturer source)
		{
			var target = new Manufacturer();
			typeof(Manufacturer).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(Manufacturer).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufactuerName);
			typeof(Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(Manufacturer).GetField("Plz", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ZipCode);
			typeof(Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
			typeof(Manufacturer).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Manufacturer source, Manufacturer target)
		{
			typeof(Manufacturer).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(Manufacturer).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufactuerName);
			typeof(Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(Manufacturer).GetField("Plz", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ZipCode);
			typeof(Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
			typeof(Manufacturer).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
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
		public DtoTypes.Manufacturer DirectPropertyMapping(Manufacturer source)
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

		public void DirectPropertyMapping(Manufacturer source, DtoTypes.Manufacturer target)
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

		public DtoTypes.Manufacturer ReflectedPropertyMapping(Manufacturer source)
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

		public void ReflectedPropertyMapping(Manufacturer source, DtoTypes.Manufacturer target)
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

		// PictureDtoToPicture.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> SeqId
			// PropertyMapping: Nodeid -> NodeId
			// PropertyMapping: Nodeseqid -> NodeSeqId
			// PropertyMapping: FileName -> FileName
			// PropertyMapping: FileType -> FileType
		public Core.Entities.Picture DirectPropertyMapping(DtoTypes.Picture source)
		{
			var target = new Core.Entities.Picture();
			_assigner.Assign((value) => {target.SeqId = value;}, source.Id);
			_assigner.Assign((value) => {target.NodeId = value;}, source.Nodeid);
			_assigner.Assign((value) => {target.NodeSeqId = value;}, source.Nodeseqid);
			_assigner.Assign((value) => {target.FileName = value;}, source.FileName);
			_assigner.Assign((value) => {target.FileType = value;}, source.FileType);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Picture source, Core.Entities.Picture target)
		{
			_assigner.Assign((value) => {target.SeqId = value;}, source.Id);
			_assigner.Assign((value) => {target.NodeId = value;}, source.Nodeid);
			_assigner.Assign((value) => {target.NodeSeqId = value;}, source.Nodeseqid);
			_assigner.Assign((value) => {target.FileName = value;}, source.FileName);
			_assigner.Assign((value) => {target.FileType = value;}, source.FileType);
		}

		public Core.Entities.Picture ReflectedPropertyMapping(DtoTypes.Picture source)
		{
			var target = new Core.Entities.Picture();
			typeof(Core.Entities.Picture).GetField("SeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.Picture).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Nodeid);
			typeof(Core.Entities.Picture).GetField("NodeSeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Nodeseqid);
			typeof(Core.Entities.Picture).GetField("FileName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileName);
			typeof(Core.Entities.Picture).GetField("FileType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileType);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Picture source, Core.Entities.Picture target)
		{
			typeof(Core.Entities.Picture).GetField("SeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.Picture).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Nodeid);
			typeof(Core.Entities.Picture).GetField("NodeSeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Nodeseqid);
			typeof(Core.Entities.Picture).GetField("FileName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileName);
			typeof(Core.Entities.Picture).GetField("FileType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FileType);
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
			// PropertyMapping: ProcessControlTech -> ProcessControlTech
			// PropertyMapping: NextTestDate -> NextTestDate
			// PropertyMapping: NextTestShift -> NextTestShift
		public Client.Core.Entities.ProcessControlCondition DirectPropertyMapping(DtoTypes.ProcessControlCondition source)
		{
			var target = new Client.Core.Entities.ProcessControlCondition();
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
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ProcessControlCondition source, Client.Core.Entities.ProcessControlCondition target)
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
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
		}

		public Client.Core.Entities.ProcessControlCondition ReflectedPropertyMapping(DtoTypes.ProcessControlCondition source)
		{
			var target = new Client.Core.Entities.ProcessControlCondition();
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("Location", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ProcessControlCondition source, Client.Core.Entities.ProcessControlCondition target)
		{
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("Location", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(Client.Core.Entities.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
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
			// PropertyMapping: ProcessControlTech -> ProcessControlTech
			// PropertyMapping: NextTestDate -> NextTestDate
			// PropertyMapping: NextTestShift -> NextTestShift
		public DtoTypes.ProcessControlCondition DirectPropertyMapping(Client.Core.Entities.ProcessControlCondition source)
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
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
			return target;
		}

		public void DirectPropertyMapping(Client.Core.Entities.ProcessControlCondition source, DtoTypes.ProcessControlCondition target)
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
			_assigner.Assign((value) => {target.ProcessControlTech = value;}, source.ProcessControlTech);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NextTestShift);
		}

		public DtoTypes.ProcessControlCondition ReflectedPropertyMapping(Client.Core.Entities.ProcessControlCondition source)
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
			typeof(DtoTypes.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
			return target;
		}

		public void ReflectedPropertyMapping(Client.Core.Entities.ProcessControlCondition source, DtoTypes.ProcessControlCondition target)
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
			typeof(DtoTypes.ProcessControlCondition).GetField("ProcessControlTech", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlTech);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(DtoTypes.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
		}

		// QstProcessControlTechDtoToQstProcessControlTech.txt
			// PropertyMapping: Id -> Id
			// PropertyMapping: ProcessControlConditionId -> ProcessControlConditionId
			// PropertyMapping: ManufacturerId -> ManufacturerId
			// PropertyMapping: TestMethod -> TestMethod
			// PropertyMapping: Extension -> Extension
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
		public void DirectPropertyMapping(DtoTypes.QstProcessControlTech source, Client.Core.Entities.QstProcessControlTech target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.Extension = value;}, source.Extension);
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

		public void ReflectedPropertyMapping(DtoTypes.QstProcessControlTech source, Client.Core.Entities.QstProcessControlTech target)
		{
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("AngleLimitMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAngleLimitMt);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("StartMeasurementPeak", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartMeasurementPeak);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("StartAngleCountingPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartAngleCountingPa);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("AngleForFurtherTurningPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAngleForFurtherTurningPa);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("TargetAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstTargetAnglePa);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("StartMeasurementPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartMeasurementPa);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("AlarmTorquePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmTorquePa);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("AlarmAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmAnglePa);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("MinimumTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstMinimumTorqueMt);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("StartAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartAngleMt);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("StartMeasurementMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstStartMeasurementMt);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("AlarmTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmTorqueMt);
			typeof(Client.Core.Entities.QstProcessControlTech).GetField("AlarmAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.QstAlarmAngleMt);
		}

		// QstProcessControlTechToQstProcessControlTechDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: ProcessControlConditionId -> ProcessControlConditionId
			// PropertyMapping: ManufacturerId -> ManufacturerId
			// PropertyMapping: TestMethod -> TestMethod
			// PropertyMapping: Extension -> Extension
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
		public DtoTypes.QstProcessControlTech DirectPropertyMapping(Client.Core.Entities.QstProcessControlTech source)
		{
			var target = new DtoTypes.QstProcessControlTech();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.Extension = value;}, source.Extension);
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

		public void DirectPropertyMapping(Client.Core.Entities.QstProcessControlTech source, DtoTypes.QstProcessControlTech target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.Extension = value;}, source.Extension);
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

		public DtoTypes.QstProcessControlTech ReflectedPropertyMapping(Client.Core.Entities.QstProcessControlTech source)
		{
			var target = new DtoTypes.QstProcessControlTech();
			typeof(DtoTypes.QstProcessControlTech).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.QstProcessControlTech).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(DtoTypes.QstProcessControlTech).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(DtoTypes.QstProcessControlTech).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(DtoTypes.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
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

		public void ReflectedPropertyMapping(Client.Core.Entities.QstProcessControlTech source, DtoTypes.QstProcessControlTech target)
		{
			typeof(DtoTypes.QstProcessControlTech).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.QstProcessControlTech).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(DtoTypes.QstProcessControlTech).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(DtoTypes.QstProcessControlTech).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(DtoTypes.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
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

		// ReasonForToolChangeToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(ReasonForToolChange source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(ReasonForToolChange source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(ReasonForToolChange source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(ReasonForToolChange source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// ShiftManagementDiffToShiftManagementDiffDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Old -> Old
			// PropertyMapping: New -> New
			// PropertyMapping: User -> UserId
			// PropertyMapping: Comment -> Comment
		public DtoTypes.ShiftManagementDiff DirectPropertyMapping(Client.Core.Diffs.ShiftManagementDiff source)
		{
			var target = new DtoTypes.ShiftManagementDiff();
			_assigner.Assign((value) => {target.Old = value;}, source.Old);
			_assigner.Assign((value) => {target.New = value;}, source.New);
			_assigner.Assign((value) => {target.UserId = value;}, source.User);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
			return target;
		}

		public void DirectPropertyMapping(Client.Core.Diffs.ShiftManagementDiff source, DtoTypes.ShiftManagementDiff target)
		{
			_assigner.Assign((value) => {target.Old = value;}, source.Old);
			_assigner.Assign((value) => {target.New = value;}, source.New);
			_assigner.Assign((value) => {target.UserId = value;}, source.User);
			_assigner.Assign((value) => {target.Comment = value;}, source.Comment);
		}

		public DtoTypes.ShiftManagementDiff ReflectedPropertyMapping(Client.Core.Diffs.ShiftManagementDiff source)
		{
			var target = new DtoTypes.ShiftManagementDiff();
			typeof(DtoTypes.ShiftManagementDiff).GetField("Old", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Old);
			typeof(DtoTypes.ShiftManagementDiff).GetField("New", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.New);
			typeof(DtoTypes.ShiftManagementDiff).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.ShiftManagementDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			return target;
		}

		public void ReflectedPropertyMapping(Client.Core.Diffs.ShiftManagementDiff source, DtoTypes.ShiftManagementDiff target)
		{
			typeof(DtoTypes.ShiftManagementDiff).GetField("Old", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Old);
			typeof(DtoTypes.ShiftManagementDiff).GetField("New", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.New);
			typeof(DtoTypes.ShiftManagementDiff).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DtoTypes.ShiftManagementDiff).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
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
		public Core.Entities.ShiftManagement DirectPropertyMapping(DtoTypes.ShiftManagement source)
		{
			var target = new Core.Entities.ShiftManagement();
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

		public void DirectPropertyMapping(DtoTypes.ShiftManagement source, Core.Entities.ShiftManagement target)
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

		public Core.Entities.ShiftManagement ReflectedPropertyMapping(DtoTypes.ShiftManagement source)
		{
			var target = new Core.Entities.ShiftManagement();
			typeof(Core.Entities.ShiftManagement).GetField("FirstShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftStart);
			typeof(Core.Entities.ShiftManagement).GetField("FirstShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftEnd);
			typeof(Core.Entities.ShiftManagement).GetField("SecondShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftStart);
			typeof(Core.Entities.ShiftManagement).GetField("SecondShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftEnd);
			typeof(Core.Entities.ShiftManagement).GetField("ThirdShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftStart);
			typeof(Core.Entities.ShiftManagement).GetField("ThirdShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftEnd);
			typeof(Core.Entities.ShiftManagement).GetField("IsSecondShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsSecondShiftActive);
			typeof(Core.Entities.ShiftManagement).GetField("IsThirdShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsThirdShiftActive);
			typeof(Core.Entities.ShiftManagement).GetField("ChangeOfDay", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ChangeOfDay);
			typeof(Core.Entities.ShiftManagement).GetField("FirstDayOfWeek", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstDayOfWeek);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ShiftManagement source, Core.Entities.ShiftManagement target)
		{
			typeof(Core.Entities.ShiftManagement).GetField("FirstShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftStart);
			typeof(Core.Entities.ShiftManagement).GetField("FirstShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstShiftEnd);
			typeof(Core.Entities.ShiftManagement).GetField("SecondShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftStart);
			typeof(Core.Entities.ShiftManagement).GetField("SecondShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SecondShiftEnd);
			typeof(Core.Entities.ShiftManagement).GetField("ThirdShiftStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftStart);
			typeof(Core.Entities.ShiftManagement).GetField("ThirdShiftEnd", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThirdShiftEnd);
			typeof(Core.Entities.ShiftManagement).GetField("IsSecondShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsSecondShiftActive);
			typeof(Core.Entities.ShiftManagement).GetField("IsThirdShiftActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsThirdShiftActive);
			typeof(Core.Entities.ShiftManagement).GetField("ChangeOfDay", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ChangeOfDay);
			typeof(Core.Entities.ShiftManagement).GetField("FirstDayOfWeek", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FirstDayOfWeek);
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
		public DtoTypes.ShiftManagement DirectPropertyMapping(Core.Entities.ShiftManagement source)
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

		public void DirectPropertyMapping(Core.Entities.ShiftManagement source, DtoTypes.ShiftManagement target)
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

		public DtoTypes.ShiftManagement ReflectedPropertyMapping(Core.Entities.ShiftManagement source)
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

		public void ReflectedPropertyMapping(Core.Entities.ShiftManagement source, DtoTypes.ShiftManagement target)
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

		// ShutOffToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(ShutOff source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(ShutOff source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(ShutOff source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(ShutOff source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
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
			// PropertyMapping: Description -> Value
			// PropertyMapping: Id -> ListId
		public Status DirectPropertyMapping(DtoTypes.Status source)
		{
			var target = new Status();
			_assigner.Assign((value) => {target.Value = value;}, source.Description);
			_assigner.Assign((value) => {target.ListId = value;}, source.Id);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.Status source, Status target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Description);
			_assigner.Assign((value) => {target.ListId = value;}, source.Id);
		}

		public Status ReflectedPropertyMapping(DtoTypes.Status source)
		{
			var target = new Status();
			typeof(Status).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Status).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Status source, Status target)
		{
			typeof(Status).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Status).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
		}

		// StatusToStatusDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Description
			// PropertyMapping: ListId -> Id
		public DtoTypes.Status DirectPropertyMapping(Status source)
		{
			var target = new DtoTypes.Status();
			_assigner.Assign((value) => {target.Description = value;}, source.Value);
			_assigner.Assign((value) => {target.Id = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(Status source, DtoTypes.Status target)
		{
			_assigner.Assign((value) => {target.Description = value;}, source.Value);
			_assigner.Assign((value) => {target.Id = value;}, source.ListId);
		}

		public DtoTypes.Status ReflectedPropertyMapping(Status source)
		{
			var target = new DtoTypes.Status();
			typeof(DtoTypes.Status).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(Status source, DtoTypes.Status target)
		{
			typeof(DtoTypes.Status).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// SwitchOffToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(SwitchOff source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(SwitchOff source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(SwitchOff source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(SwitchOff source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
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
		public Core.Entities.TestEquipment DirectPropertyMapping(DtoTypes.TestEquipment source)
		{
			var target = new Core.Entities.TestEquipment();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
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

		public void DirectPropertyMapping(DtoTypes.TestEquipment source, Core.Entities.TestEquipment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
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

		public Core.Entities.TestEquipment ReflectedPropertyMapping(DtoTypes.TestEquipment source)
		{
			var target = new Core.Entities.TestEquipment();
			typeof(Core.Entities.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Core.Entities.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(Core.Entities.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Core.Entities.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Core.Entities.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Core.Entities.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Core.Entities.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Core.Entities.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Core.Entities.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Core.Entities.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Core.Entities.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Core.Entities.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Core.Entities.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Core.Entities.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Core.Entities.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Core.Entities.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Core.Entities.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Core.Entities.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Core.Entities.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(Core.Entities.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(Core.Entities.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(Core.Entities.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(Core.Entities.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(Core.Entities.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(Core.Entities.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestEquipment source, Core.Entities.TestEquipment target)
		{
			typeof(Core.Entities.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Core.Entities.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(Core.Entities.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Core.Entities.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Core.Entities.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Core.Entities.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Core.Entities.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Core.Entities.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Core.Entities.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Core.Entities.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Core.Entities.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Core.Entities.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Core.Entities.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Core.Entities.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Core.Entities.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Core.Entities.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Core.Entities.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Core.Entities.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Core.Entities.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(Core.Entities.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(Core.Entities.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(Core.Entities.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(Core.Entities.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(Core.Entities.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(Core.Entities.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestEquipmentModelDtoToTestEquipmentModel.txt
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
			// PropertyMapping: UseForRot -> UseForRot
			// PropertyMapping: UseForCtl -> UseForCtl
			// PropertyMapping: DataGateVersion -> DataGateVersion
			// PropertyMapping: CanUseQstStandard -> CanUseQstStandard
		public Core.Entities.TestEquipmentModel DirectPropertyMapping(DtoTypes.TestEquipmentModel source)
		{
			var target = new Core.Entities.TestEquipmentModel();
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
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestEquipmentModel source, Core.Entities.TestEquipmentModel target)
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
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
		}

		public Core.Entities.TestEquipmentModel ReflectedPropertyMapping(DtoTypes.TestEquipmentModel source)
		{
			var target = new Core.Entities.TestEquipmentModel();
			typeof(Core.Entities.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(Core.Entities.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(Core.Entities.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(Core.Entities.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(Core.Entities.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Core.Entities.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Core.Entities.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Core.Entities.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Core.Entities.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Core.Entities.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Core.Entities.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Core.Entities.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Core.Entities.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Core.Entities.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Core.Entities.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(Core.Entities.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestEquipmentModel source, Core.Entities.TestEquipmentModel target)
		{
			typeof(Core.Entities.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(Core.Entities.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(Core.Entities.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(Core.Entities.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(Core.Entities.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Core.Entities.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(Core.Entities.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(Core.Entities.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(Core.Entities.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(Core.Entities.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(Core.Entities.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(Core.Entities.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(Core.Entities.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(Core.Entities.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(Core.Entities.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(Core.Entities.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(Core.Entities.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestEquipmentModelToTestEquipmentModelDto.txt
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
			// PropertyMapping: UseForRot -> UseForRot
			// PropertyMapping: UseForCtl -> UseForCtl
			// PropertyMapping: DataGateVersion -> DataGateVersion
			// PropertyMapping: CanUseQstStandard -> CanUseQstStandard
		public DtoTypes.TestEquipmentModel DirectPropertyMapping(Core.Entities.TestEquipmentModel source)
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
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(Core.Entities.TestEquipmentModel source, DtoTypes.TestEquipmentModel target)
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
			_assigner.Assign((value) => {target.UseForRot = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CanUseQstStandard);
		}

		public DtoTypes.TestEquipmentModel ReflectedPropertyMapping(Core.Entities.TestEquipmentModel source)
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
			typeof(DtoTypes.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DtoTypes.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DtoTypes.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(DtoTypes.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(Core.Entities.TestEquipmentModel source, DtoTypes.TestEquipmentModel target)
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
		public DtoTypes.TestEquipment DirectPropertyMapping(Core.Entities.TestEquipment source)
		{
			var target = new DtoTypes.TestEquipment();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
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

		public void DirectPropertyMapping(Core.Entities.TestEquipment source, DtoTypes.TestEquipment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
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

		public DtoTypes.TestEquipment ReflectedPropertyMapping(Core.Entities.TestEquipment source)
		{
			var target = new DtoTypes.TestEquipment();
			typeof(DtoTypes.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
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

		public void ReflectedPropertyMapping(Core.Entities.TestEquipment source, DtoTypes.TestEquipment target)
		{
			typeof(DtoTypes.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DtoTypes.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
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
		public Core.Entities.TestLevel DirectPropertyMapping(DtoTypes.TestLevel source)
		{
			var target = new Core.Entities.TestLevel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestLevel source, Core.Entities.TestLevel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
		}

		public Core.Entities.TestLevel ReflectedPropertyMapping(DtoTypes.TestLevel source)
		{
			var target = new Core.Entities.TestLevel();
			typeof(Core.Entities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(Core.Entities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(Core.Entities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(Core.Entities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestLevel source, Core.Entities.TestLevel target)
		{
			typeof(Core.Entities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(Core.Entities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(Core.Entities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(Core.Entities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
		}

		// TestLevelSetDtoToTestLevelSet.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: TestLevel1 -> TestLevel1
			// PropertyMapping: TestLevel2 -> TestLevel2
			// PropertyMapping: TestLevel3 -> TestLevel3
		public Core.Entities.TestLevelSet DirectPropertyMapping(DtoTypes.TestLevelSet source)
		{
			var target = new Core.Entities.TestLevelSet();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestLevelSet source, Core.Entities.TestLevelSet target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
		}

		public Core.Entities.TestLevelSet ReflectedPropertyMapping(DtoTypes.TestLevelSet source)
		{
			var target = new Core.Entities.TestLevelSet();
			typeof(Core.Entities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Core.Entities.TestLevelSet).GetField("TestLevel1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel1);
			typeof(Core.Entities.TestLevelSet).GetField("TestLevel2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel2);
			typeof(Core.Entities.TestLevelSet).GetField("TestLevel3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel3);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestLevelSet source, Core.Entities.TestLevelSet target)
		{
			typeof(Core.Entities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Core.Entities.TestLevelSet).GetField("TestLevel1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel1);
			typeof(Core.Entities.TestLevelSet).GetField("TestLevel2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel2);
			typeof(Core.Entities.TestLevelSet).GetField("TestLevel3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel3);
		}

		// TestLevelSetToTestLevelSetDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: TestLevel1 -> TestLevel1
			// PropertyMapping: TestLevel2 -> TestLevel2
			// PropertyMapping: TestLevel3 -> TestLevel3
		public DtoTypes.TestLevelSet DirectPropertyMapping(Core.Entities.TestLevelSet source)
		{
			var target = new DtoTypes.TestLevelSet();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
			return target;
		}

		public void DirectPropertyMapping(Core.Entities.TestLevelSet source, DtoTypes.TestLevelSet target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.TestLevel1 = value;}, source.TestLevel1);
			_assigner.Assign((value) => {target.TestLevel2 = value;}, source.TestLevel2);
			_assigner.Assign((value) => {target.TestLevel3 = value;}, source.TestLevel3);
		}

		public DtoTypes.TestLevelSet ReflectedPropertyMapping(Core.Entities.TestLevelSet source)
		{
			var target = new DtoTypes.TestLevelSet();
			typeof(DtoTypes.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel1);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel2);
			typeof(DtoTypes.TestLevelSet).GetField("TestLevel3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevel3);
			return target;
		}

		public void ReflectedPropertyMapping(Core.Entities.TestLevelSet source, DtoTypes.TestLevelSet target)
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
		public DtoTypes.TestLevel DirectPropertyMapping(Core.Entities.TestLevel source)
		{
			var target = new DtoTypes.TestLevel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
			return target;
		}

		public void DirectPropertyMapping(Core.Entities.TestLevel source, DtoTypes.TestLevel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.TestInterval = value;}, source.TestInterval);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
		}

		public DtoTypes.TestLevel ReflectedPropertyMapping(Core.Entities.TestLevel source)
		{
			var target = new DtoTypes.TestLevel();
			typeof(DtoTypes.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(DtoTypes.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(DtoTypes.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(DtoTypes.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
			return target;
		}

		public void ReflectedPropertyMapping(Core.Entities.TestLevel source, DtoTypes.TestLevel target)
		{
			typeof(DtoTypes.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.TestLevel).GetField("TestInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval);
			typeof(DtoTypes.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(DtoTypes.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(DtoTypes.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
		}

		// TestParametersDtoToTestTestParameters.txt
			// hasDefaultConstructor
			// PropertyMapping: SetPoint1 -> SetPointTorque
			// PropertyMapping: Minimum1 -> MinimumTorque
			// PropertyMapping: Maximum1 -> MaximumTorque
			// PropertyMapping: Threshold1 -> ThresholdTorque
			// PropertyMapping: SetPoint2 -> SetPointAngle
			// PropertyMapping: Minimum2 -> MinimumAngle
			// PropertyMapping: Maximum2 -> MaximumAngle
			// PropertyMapping: ControlledBy -> ControlledBy
		public TestParameters DirectPropertyMapping(DtoTypes.TestParameters source)
		{
			var target = new TestParameters();
			_assigner.Assign((value) => {target.SetPointTorque = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.MinimumTorque = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.MaximumTorque = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPointAngle = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.MinimumAngle = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.MaximumAngle = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.TestParameters source, TestParameters target)
		{
			_assigner.Assign((value) => {target.SetPointTorque = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.MinimumTorque = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.MaximumTorque = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.SetPointAngle = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.MinimumAngle = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.MaximumAngle = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
		}

		public TestParameters ReflectedPropertyMapping(DtoTypes.TestParameters source)
		{
			var target = new TestParameters();
			typeof(TestParameters).GetField("SetPointTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(TestParameters).GetField("MinimumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(TestParameters).GetField("MaximumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(TestParameters).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(TestParameters).GetField("SetPointAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(TestParameters).GetField("MinimumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(TestParameters).GetField("MaximumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(TestParameters).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestParameters source, TestParameters target)
		{
			typeof(TestParameters).GetField("SetPointTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(TestParameters).GetField("MinimumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(TestParameters).GetField("MaximumTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(TestParameters).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(TestParameters).GetField("SetPointAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(TestParameters).GetField("MinimumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(TestParameters).GetField("MaximumAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(TestParameters).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
		}

		// TestParametersToTestTestParametersDto.txt
			// hasDefaultConstructor
			// PropertyMapping: SetPointTorque -> SetPoint1
			// PropertyMapping: MinimumTorque -> Minimum1
			// PropertyMapping: MaximumTorque -> Maximum1
			// PropertyMapping: ThresholdTorque -> Threshold1
			// PropertyMapping: SetPointAngle -> SetPoint2
			// PropertyMapping: MinimumAngle -> Minimum2
			// PropertyMapping: MaximumAngle -> Maximum2
			// PropertyMapping: ControlledBy -> ControlledBy
		public DtoTypes.TestParameters DirectPropertyMapping(TestParameters source)
		{
			var target = new DtoTypes.TestParameters();
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPointTorque);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.MinimumTorque);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.MaximumTorque);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPointAngle);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.MinimumAngle);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.MaximumAngle);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
			return target;
		}

		public void DirectPropertyMapping(TestParameters source, DtoTypes.TestParameters target)
		{
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.SetPointTorque);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.MinimumTorque);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.MaximumTorque);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.SetPointAngle);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.MinimumAngle);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.MaximumAngle);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.ControlledBy);
		}

		public DtoTypes.TestParameters ReflectedPropertyMapping(TestParameters source)
		{
			var target = new DtoTypes.TestParameters();
			typeof(DtoTypes.TestParameters).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointTorque);
			typeof(DtoTypes.TestParameters).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorque);
			typeof(DtoTypes.TestParameters).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumTorque);
			typeof(DtoTypes.TestParameters).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.TestParameters).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointAngle);
			typeof(DtoTypes.TestParameters).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumAngle);
			typeof(DtoTypes.TestParameters).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumAngle);
			typeof(DtoTypes.TestParameters).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			return target;
		}

		public void ReflectedPropertyMapping(TestParameters source, DtoTypes.TestParameters target)
		{
			typeof(DtoTypes.TestParameters).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointTorque);
			typeof(DtoTypes.TestParameters).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorque);
			typeof(DtoTypes.TestParameters).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumTorque);
			typeof(DtoTypes.TestParameters).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DtoTypes.TestParameters).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPointAngle);
			typeof(DtoTypes.TestParameters).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumAngle);
			typeof(DtoTypes.TestParameters).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumAngle);
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
		public TestTechnique DirectPropertyMapping(DtoTypes.TestTechnique source)
		{
			var target = new TestTechnique();
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

		public void DirectPropertyMapping(DtoTypes.TestTechnique source, TestTechnique target)
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

		public TestTechnique ReflectedPropertyMapping(DtoTypes.TestTechnique source)
		{
			var target = new TestTechnique();
			typeof(TestTechnique).GetField("EndCycleTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndCycleTime);
			typeof(TestTechnique).GetField("FilterFrequency", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FilterFrequency);
			typeof(TestTechnique).GetField("CycleComplete", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleComplete);
			typeof(TestTechnique).GetField("MeasureDelayTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MeasureDelayTime);
			typeof(TestTechnique).GetField("ResetTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResetTime);
			typeof(TestTechnique).GetField("MustTorqueAndAngleBeInLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MustTorqueAndAngleBeInLimits);
			typeof(TestTechnique).GetField("CycleStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleStart);
			typeof(TestTechnique).GetField("StartFinalAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartFinalAngle);
			typeof(TestTechnique).GetField("SlipTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SlipTorque);
			typeof(TestTechnique).GetField("TorqueCoefficient", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TorqueCoefficient);
			typeof(TestTechnique).GetField("MinimumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumPulse);
			typeof(TestTechnique).GetField("MaximumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumPulse);
			typeof(TestTechnique).GetField("Threshold", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.TestTechnique source, TestTechnique target)
		{
			typeof(TestTechnique).GetField("EndCycleTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndCycleTime);
			typeof(TestTechnique).GetField("FilterFrequency", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FilterFrequency);
			typeof(TestTechnique).GetField("CycleComplete", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleComplete);
			typeof(TestTechnique).GetField("MeasureDelayTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MeasureDelayTime);
			typeof(TestTechnique).GetField("ResetTime", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResetTime);
			typeof(TestTechnique).GetField("MustTorqueAndAngleBeInLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MustTorqueAndAngleBeInLimits);
			typeof(TestTechnique).GetField("CycleStart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CycleStart);
			typeof(TestTechnique).GetField("StartFinalAngle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartFinalAngle);
			typeof(TestTechnique).GetField("SlipTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SlipTorque);
			typeof(TestTechnique).GetField("TorqueCoefficient", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TorqueCoefficient);
			typeof(TestTechnique).GetField("MinimumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumPulse);
			typeof(TestTechnique).GetField("MaximumPulse", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaximumPulse);
			typeof(TestTechnique).GetField("Threshold", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold);
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
		public DtoTypes.TestTechnique DirectPropertyMapping(TestTechnique source)
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

		public void DirectPropertyMapping(TestTechnique source, DtoTypes.TestTechnique target)
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

		public DtoTypes.TestTechnique ReflectedPropertyMapping(TestTechnique source)
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

		public void ReflectedPropertyMapping(TestTechnique source, DtoTypes.TestTechnique target)
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
		public ToleranceClass DirectPropertyMapping(DtoTypes.ToleranceClass source)
		{
			var target = new ToleranceClass();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ToleranceClass source, ToleranceClass target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
		}

		public ToleranceClass ReflectedPropertyMapping(DtoTypes.ToleranceClass source)
		{
			var target = new ToleranceClass();
			typeof(ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ToleranceClass source, ToleranceClass target)
		{
			typeof(ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
		}

		// ToleranceClassToToleranceClassDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: Relative -> Relative
			// PropertyMapping: LowerLimit -> LowerLimit
			// PropertyMapping: UpperLimit -> UpperLimit
		public DtoTypes.ToleranceClass DirectPropertyMapping(ToleranceClass source)
		{
			var target = new DtoTypes.ToleranceClass();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
			return target;
		}

		public void DirectPropertyMapping(ToleranceClass source, DtoTypes.ToleranceClass target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.Relative = value;}, source.Relative);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.UpperLimit);
		}

		public DtoTypes.ToleranceClass ReflectedPropertyMapping(ToleranceClass source)
		{
			var target = new DtoTypes.ToleranceClass();
			typeof(DtoTypes.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(DtoTypes.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(DtoTypes.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
			return target;
		}

		public void ReflectedPropertyMapping(ToleranceClass source, DtoTypes.ToleranceClass target)
		{
			typeof(DtoTypes.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(DtoTypes.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(DtoTypes.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
		}

		// ToolDtoToTool.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: SerialNumber -> SerialNumber
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: ToolModel -> ToolModel
			// PropertyMapping: Status -> Status
			// PropertyMapping: CostCenter -> CostCenter
			// PropertyMapping: ConfigurableField -> ConfigurableField
			// PropertyMapping: Accessory -> Accessory
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: AdditionalConfigurableField1 -> AdditionalConfigurableField1
			// PropertyMapping: AdditionalConfigurableField2 -> AdditionalConfigurableField2
			// PropertyMapping: AdditionalConfigurableField3 -> AdditionalConfigurableField3
		public Tool DirectPropertyMapping(DtoTypes.Tool source)
		{
			var target = new Tool();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
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

		public void DirectPropertyMapping(DtoTypes.Tool source, Tool target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
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

		public Tool ReflectedPropertyMapping(DtoTypes.Tool source)
		{
			var target = new Tool();
			typeof(Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(Tool).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.Tool source, Tool target)
		{
			typeof(Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(Tool).GetField("Comment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
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
			// PropertyMapping: ModelType -> ModelType
			// PropertyMapping: Class -> Class
		public ToolModel DirectPropertyMapping(DtoTypes.ToolModel source)
		{
			var target = new ToolModel();
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
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ToolModel source, ToolModel target)
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
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
		}

		public ToolModel ReflectedPropertyMapping(DtoTypes.ToolModel source)
		{
			var target = new ToolModel();
			typeof(ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(ToolModel).GetField("CmLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmLimit);
			typeof(ToolModel).GetField("CmkLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmkLimit);
			typeof(ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ToolModel source, ToolModel target)
		{
			typeof(ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(ToolModel).GetField("CmLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmLimit);
			typeof(ToolModel).GetField("CmkLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CmkLimit);
			typeof(ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
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
			// PropertyMapping: ModelType -> ModelType
			// PropertyMapping: Class -> Class
		public DtoTypes.ToolModel DirectPropertyMapping(ToolModel source)
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
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
			return target;
		}

		public void DirectPropertyMapping(ToolModel source, DtoTypes.ToolModel target)
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
			_assigner.Assign((value) => {target.ModelType = value;}, source.ModelType);
			_assigner.Assign((value) => {target.Class = value;}, source.Class);
		}

		public DtoTypes.ToolModel ReflectedPropertyMapping(ToolModel source)
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
			typeof(DtoTypes.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(DtoTypes.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
			return target;
		}

		public void ReflectedPropertyMapping(ToolModel source, DtoTypes.ToolModel target)
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
			typeof(DtoTypes.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(DtoTypes.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
		}

		// ToolToToolDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: SerialNumber -> SerialNumber
			// PropertyMapping: InventoryNumber -> InventoryNumber
			// PropertyMapping: ToolModel -> ToolModel
			// PropertyMapping: Status -> Status
			// PropertyMapping: CostCenter -> CostCenter
			// PropertyMapping: ConfigurableField -> ConfigurableField
			// PropertyMapping: Accessory -> Accessory
			// PropertyMapping: Comment -> Comment
			// PropertyMapping: AdditionalConfigurableField1 -> AdditionalConfigurableField1
			// PropertyMapping: AdditionalConfigurableField2 -> AdditionalConfigurableField2
			// PropertyMapping: AdditionalConfigurableField3 -> AdditionalConfigurableField3
		public DtoTypes.Tool DirectPropertyMapping(Tool source)
		{
			var target = new DtoTypes.Tool();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
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

		public void DirectPropertyMapping(Tool source, DtoTypes.Tool target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.InventoryNumber);
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

		public DtoTypes.Tool ReflectedPropertyMapping(Tool source)
		{
			var target = new DtoTypes.Tool();
			typeof(DtoTypes.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
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

		public void ReflectedPropertyMapping(Tool source, DtoTypes.Tool target)
		{
			typeof(DtoTypes.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DtoTypes.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
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

		// ToolTypeToHelperTableEntityDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Value -> Value
			// PropertyMapping: ListId -> ListId
		public DtoTypes.HelperTableEntity DirectPropertyMapping(ToolType source)
		{
			var target = new DtoTypes.HelperTableEntity();
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
			return target;
		}

		public void DirectPropertyMapping(ToolType source, DtoTypes.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.Value = value;}, source.Value);
			_assigner.Assign((value) => {target.ListId = value;}, source.ListId);
		}

		public DtoTypes.HelperTableEntity ReflectedPropertyMapping(ToolType source)
		{
			var target = new DtoTypes.HelperTableEntity();
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			return target;
		}

		public void ReflectedPropertyMapping(ToolType source, DtoTypes.HelperTableEntity target)
		{
			typeof(DtoTypes.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DtoTypes.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
		}

		// ToolUsageDtoToToolUsage.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> ListId
			// PropertyMapping: Description -> Value
		public ToolUsage DirectPropertyMapping(DtoTypes.ToolUsage source)
		{
			var target = new ToolUsage();
			_assigner.Assign((value) => {target.ListId = value;}, source.Id);
			_assigner.Assign((value) => {target.Value = value;}, source.Description);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.ToolUsage source, ToolUsage target)
		{
			_assigner.Assign((value) => {target.ListId = value;}, source.Id);
			_assigner.Assign((value) => {target.Value = value;}, source.Description);
		}

		public ToolUsage ReflectedPropertyMapping(DtoTypes.ToolUsage source)
		{
			var target = new ToolUsage();
			typeof(ToolUsage).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(ToolUsage).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.ToolUsage source, ToolUsage target)
		{
			typeof(ToolUsage).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(ToolUsage).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
		}

		// ToolUsageToToolUsageDto.txt
			// hasDefaultConstructor
			// PropertyMapping: ListId -> Id
			// PropertyMapping: Value -> Description
		public DtoTypes.ToolUsage DirectPropertyMapping(ToolUsage source)
		{
			var target = new DtoTypes.ToolUsage();
			_assigner.Assign((value) => {target.Id = value;}, source.ListId);
			_assigner.Assign((value) => {target.Description = value;}, source.Value);
			return target;
		}

		public void DirectPropertyMapping(ToolUsage source, DtoTypes.ToolUsage target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.ListId);
			_assigner.Assign((value) => {target.Description = value;}, source.Value);
		}

		public DtoTypes.ToolUsage ReflectedPropertyMapping(ToolUsage source)
		{
			var target = new DtoTypes.ToolUsage();
			typeof(DtoTypes.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(DtoTypes.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			return target;
		}

		public void ReflectedPropertyMapping(ToolUsage source, DtoTypes.ToolUsage target)
		{
			typeof(DtoTypes.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(DtoTypes.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
		}

		// UserDtoToUser.txt
			// hasDefaultConstructor
			// PropertyMapping: UserId -> UserId
			// PropertyMapping: UserName -> UserName
		public User DirectPropertyMapping(DtoTypes.User source)
		{
			var target = new User();
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.User source, User target)
		{
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
		}

		public User ReflectedPropertyMapping(DtoTypes.User source)
		{
			var target = new User();
			typeof(User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.User source, User target)
		{
			typeof(User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
		}

		// UserToUserDto.txt
			// hasDefaultConstructor
			// PropertyMapping: UserId -> UserId
			// PropertyMapping: UserName -> UserName
		public DtoTypes.User DirectPropertyMapping(User source)
		{
			var target = new DtoTypes.User();
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
			return target;
		}

		public void DirectPropertyMapping(User source, DtoTypes.User target)
		{
			_assigner.Assign((value) => {target.UserId = value;}, source.UserId);
			_assigner.Assign((value) => {target.UserName = value;}, source.UserName);
		}

		public DtoTypes.User ReflectedPropertyMapping(User source)
		{
			var target = new DtoTypes.User();
			typeof(DtoTypes.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(DtoTypes.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
			return target;
		}

		public void ReflectedPropertyMapping(User source, DtoTypes.User target)
		{
			typeof(DtoTypes.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(DtoTypes.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserName);
		}

		// WorkingCalendarDtoToWorkingCalendar.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: AreSaturdaysFree -> AreSaturdaysFree
			// PropertyMapping: AreSundaysFree -> AreSundaysFree
		public Core.Entities.WorkingCalendar DirectPropertyMapping(DtoTypes.WorkingCalendar source)
		{
			var target = new Core.Entities.WorkingCalendar();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.AreSaturdaysFree = value;}, source.AreSaturdaysFree);
			_assigner.Assign((value) => {target.AreSundaysFree = value;}, source.AreSundaysFree);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.WorkingCalendar source, Core.Entities.WorkingCalendar target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.AreSaturdaysFree = value;}, source.AreSaturdaysFree);
			_assigner.Assign((value) => {target.AreSundaysFree = value;}, source.AreSundaysFree);
		}

		public Core.Entities.WorkingCalendar ReflectedPropertyMapping(DtoTypes.WorkingCalendar source)
		{
			var target = new Core.Entities.WorkingCalendar();
			typeof(Core.Entities.WorkingCalendar).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Core.Entities.WorkingCalendar).GetField("AreSaturdaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSaturdaysFree);
			typeof(Core.Entities.WorkingCalendar).GetField("AreSundaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSundaysFree);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.WorkingCalendar source, Core.Entities.WorkingCalendar target)
		{
			typeof(Core.Entities.WorkingCalendar).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Core.Entities.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Core.Entities.WorkingCalendar).GetField("AreSaturdaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSaturdaysFree);
			typeof(Core.Entities.WorkingCalendar).GetField("AreSundaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSundaysFree);
		}

		// WorkingCalendarEntryDtoToWorkingCalendarEntry.txt
			// hasDefaultConstructor
			// PropertyMapping: Date -> Date
			// PropertyMapping: Description -> Description
			// PropertyMapping: Repeated -> Repetition
			// PropertyMapping: IsFree -> Type
		public Core.Entities.WorkingCalendarEntry DirectPropertyMapping(DtoTypes.WorkingCalendarEntry source)
		{
			var target = new Core.Entities.WorkingCalendarEntry();
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repetition = value;}, source.Repeated);
			_assigner.Assign((value) => {target.Type = value;}, source.IsFree);
			return target;
		}

		public void DirectPropertyMapping(DtoTypes.WorkingCalendarEntry source, Core.Entities.WorkingCalendarEntry target)
		{
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repetition = value;}, source.Repeated);
			_assigner.Assign((value) => {target.Type = value;}, source.IsFree);
		}

		public Core.Entities.WorkingCalendarEntry ReflectedPropertyMapping(DtoTypes.WorkingCalendarEntry source)
		{
			var target = new Core.Entities.WorkingCalendarEntry();
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Repetition", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repeated);
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsFree);
			return target;
		}

		public void ReflectedPropertyMapping(DtoTypes.WorkingCalendarEntry source, Core.Entities.WorkingCalendarEntry target)
		{
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Repetition", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repeated);
			typeof(Core.Entities.WorkingCalendarEntry).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsFree);
		}

		// WorkingCalendarEntryToWorkingCalendarEntryDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Date -> Date
			// PropertyMapping: Description -> Description
			// PropertyMapping: Repetition -> Repeated
			// PropertyMapping: Type -> IsFree
		public DtoTypes.WorkingCalendarEntry DirectPropertyMapping(Core.Entities.WorkingCalendarEntry source)
		{
			var target = new DtoTypes.WorkingCalendarEntry();
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repeated = value;}, source.Repetition);
			_assigner.Assign((value) => {target.IsFree = value;}, source.Type);
			return target;
		}

		public void DirectPropertyMapping(Core.Entities.WorkingCalendarEntry source, DtoTypes.WorkingCalendarEntry target)
		{
			_assigner.Assign((value) => {target.Date = value;}, source.Date);
			_assigner.Assign((value) => {target.Description = value;}, source.Description);
			_assigner.Assign((value) => {target.Repeated = value;}, source.Repetition);
			_assigner.Assign((value) => {target.IsFree = value;}, source.Type);
		}

		public DtoTypes.WorkingCalendarEntry ReflectedPropertyMapping(Core.Entities.WorkingCalendarEntry source)
		{
			var target = new DtoTypes.WorkingCalendarEntry();
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Repeated", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repetition);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("IsFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			return target;
		}

		public void ReflectedPropertyMapping(Core.Entities.WorkingCalendarEntry source, DtoTypes.WorkingCalendarEntry target)
		{
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("Repeated", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repetition);
			typeof(DtoTypes.WorkingCalendarEntry).GetField("IsFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
		}

		// WorkingCalendarToWorkingCalendarDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: AreSaturdaysFree -> AreSaturdaysFree
			// PropertyMapping: AreSundaysFree -> AreSundaysFree
		public DtoTypes.WorkingCalendar DirectPropertyMapping(Core.Entities.WorkingCalendar source)
		{
			var target = new DtoTypes.WorkingCalendar();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.AreSaturdaysFree = value;}, source.AreSaturdaysFree);
			_assigner.Assign((value) => {target.AreSundaysFree = value;}, source.AreSundaysFree);
			return target;
		}

		public void DirectPropertyMapping(Core.Entities.WorkingCalendar source, DtoTypes.WorkingCalendar target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.AreSaturdaysFree = value;}, source.AreSaturdaysFree);
			_assigner.Assign((value) => {target.AreSundaysFree = value;}, source.AreSundaysFree);
		}

		public DtoTypes.WorkingCalendar ReflectedPropertyMapping(Core.Entities.WorkingCalendar source)
		{
			var target = new DtoTypes.WorkingCalendar();
			typeof(DtoTypes.WorkingCalendar).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.WorkingCalendar).GetField("AreSaturdaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSaturdaysFree);
			typeof(DtoTypes.WorkingCalendar).GetField("AreSundaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSundaysFree);
			return target;
		}

		public void ReflectedPropertyMapping(Core.Entities.WorkingCalendar source, DtoTypes.WorkingCalendar target)
		{
			typeof(DtoTypes.WorkingCalendar).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DtoTypes.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DtoTypes.WorkingCalendar).GetField("AreSaturdaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSaturdaysFree);
			typeof(DtoTypes.WorkingCalendar).GetField("AreSundaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSundaysFree);
		}

	}
}

////////////
//

