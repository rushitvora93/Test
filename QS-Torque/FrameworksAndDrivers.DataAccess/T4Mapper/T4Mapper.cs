
using System.Reflection;
using Server.Core;
using T4Mapper;

namespace FrameworksAndDrivers.DataAccess.T4Mapper
{
	public class Mapper
	{
		private readonly Assigner _assigner = new Assigner();


			// ClassicChkTestDbToClassicChkTest.txt
			// hasDefaultConstructor
			// PropertyMapping: GlobalHistoryId -> Id
			// PropertyMapping: Shift -> Shift
			// PropertyMapping: NUMBER_OF_TESTS -> NumberOfTests
			// PropertyMapping: LOWER_LIMIT_UNIT1 -> LowerLimitUnit1
			// PropertyMapping: NOMINAL_UNIT1 -> NominalValueUnit1
			// PropertyMapping: UPPER_LIMIT_UNIT1 -> UpperLimitUnit1
			// PropertyMapping: TOLERANCE_CLASS_UNIT1_ID -> ToleranceClassUnit1
			// PropertyMapping: UNIT1_ID -> Unit1Id
			// PropertyMapping: LOWER_LIMIT_UNIT2 -> LowerLimitUnit2
			// PropertyMapping: NOMINAL_UNIT2 -> NominalValueUnit2
			// PropertyMapping: UPPER_LIMIT_UNIT2 -> UpperLimitUnit2
			// PropertyMapping: TOLERANCE_CLASS_UNIT2_ID -> ToleranceClassUnit2
			// PropertyMapping: UNIT2_ID -> Unit2Id
			// PropertyMapping: MINIMUM -> TestValueMinimum
			// PropertyMapping: MAXIMUM -> TestValueMaximum
			// PropertyMapping: X -> Average
			// PropertyMapping: S -> StandardDeviation
			// PropertyMapping: CONTROLED_BY_UNIT_ID -> ControlledByUnitId
			// PropertyMapping: START_COUNT -> ThresholdTorque
			// PropertyMapping: SENSOR_SERIAL_NUMBER -> SensorSerialNumber
			// PropertyMapping: RESULT -> Result
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: ClassicChkTestLocation -> TestLocation
			// PropertyMapping: LocPowId -> LocationToolAssignmentId
		public Server.Core.Entities.ClassicChkTest DirectPropertyMapping(DbEntities.ClassicChkTest source)
		{
			var target = new Server.Core.Entities.ClassicChkTest();
			_assigner.Assign((value) => {target.Id = value;}, source.GlobalHistoryId);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NUMBER_OF_TESTS);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LOWER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NOMINAL_UNIT1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UPPER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.TOLERANCE_CLASS_UNIT1_ID);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.UNIT1_ID);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LOWER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NOMINAL_UNIT2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UPPER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.TOLERANCE_CLASS_UNIT2_ID);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.UNIT2_ID);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.MINIMUM);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.MAXIMUM);
			_assigner.Assign((value) => {target.Average = value;}, source.X);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.S);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.CONTROLED_BY_UNIT_ID);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.START_COUNT);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SENSOR_SERIAL_NUMBER);
			_assigner.Assign((value) => {target.Result = value;}, source.RESULT);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.ClassicChkTestLocation);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocPowId);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicChkTest source, Server.Core.Entities.ClassicChkTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.GlobalHistoryId);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NUMBER_OF_TESTS);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LOWER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NOMINAL_UNIT1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UPPER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.TOLERANCE_CLASS_UNIT1_ID);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.UNIT1_ID);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LOWER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NOMINAL_UNIT2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UPPER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.TOLERANCE_CLASS_UNIT2_ID);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.UNIT2_ID);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.MINIMUM);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.MAXIMUM);
			_assigner.Assign((value) => {target.Average = value;}, source.X);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.S);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.CONTROLED_BY_UNIT_ID);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.START_COUNT);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SENSOR_SERIAL_NUMBER);
			_assigner.Assign((value) => {target.Result = value;}, source.RESULT);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.ClassicChkTestLocation);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocPowId);
		}

		public Server.Core.Entities.ClassicChkTest ReflectedPropertyMapping(DbEntities.ClassicChkTest source)
		{
			var target = new Server.Core.Entities.ClassicChkTest();
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GlobalHistoryId);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NUMBER_OF_TESTS);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT1_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT1_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT2_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT2_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MINIMUM);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAXIMUM);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.X);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.S);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROLED_BY_UNIT_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.START_COUNT);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SENSOR_SERIAL_NUMBER);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.RESULT);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ClassicChkTestLocation);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocPowId);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicChkTest source, Server.Core.Entities.ClassicChkTest target)
		{
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GlobalHistoryId);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NUMBER_OF_TESTS);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT1_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT1_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT2_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT2_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MINIMUM);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAXIMUM);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.X);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.S);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROLED_BY_UNIT_ID);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.START_COUNT);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SENSOR_SERIAL_NUMBER);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.RESULT);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ClassicChkTestLocation);
			typeof(Server.Core.Entities.ClassicChkTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocPowId);
		}

		// ClassicChkTestLocationDbToClassicTestLocation.txt
			// hasDefaultConstructor
			// PropertyMapping: LOCATION_ID -> LocationId
			// PropertyMapping: LOCTREE_ID -> LocationDirectoryId
			// PropertyMapping: LOCATION_TREE_PATH -> LocationTreePath
		public Server.Core.Entities.ClassicTestLocation DirectPropertyMapping(DbEntities.ClassicChkTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			_assigner.Assign((value) => {target.LocationId = value;}, source.LOCATION_ID);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LOCTREE_ID);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.LOCATION_TREE_PATH);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicChkTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			_assigner.Assign((value) => {target.LocationId = value;}, source.LOCATION_ID);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LOCTREE_ID);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.LOCATION_TREE_PATH);
		}

		public Server.Core.Entities.ClassicTestLocation ReflectedPropertyMapping(DbEntities.ClassicChkTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREE_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_TREE_PATH);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicChkTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREE_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_TREE_PATH);
		}

		// ClassicChkTestToClassicChkTestDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> GlobalHistoryId
			// PropertyMapping: Shift -> Shift
			// PropertyMapping: NumberOfTests -> NUMBER_OF_TESTS
			// PropertyMapping: LowerLimitUnit1 -> LOWER_LIMIT_UNIT1
			// PropertyMapping: NominalValueUnit1 -> NOMINAL_UNIT1
			// PropertyMapping: UpperLimitUnit1 -> UPPER_LIMIT_UNIT1
			// PropertyMapping: ToleranceClassUnit1 -> TOLERANCE_CLASS_UNIT1_ID
			// PropertyMapping: Unit1Id -> UNIT1_ID
			// PropertyMapping: LowerLimitUnit2 -> LOWER_LIMIT_UNIT2
			// PropertyMapping: NominalValueUnit2 -> NOMINAL_UNIT2
			// PropertyMapping: UpperLimitUnit2 -> UPPER_LIMIT_UNIT2
			// PropertyMapping: ToleranceClassUnit2 -> TOLERANCE_CLASS_UNIT2_ID
			// PropertyMapping: Unit2Id -> UNIT2_ID
			// PropertyMapping: TestValueMinimum -> MINIMUM
			// PropertyMapping: TestValueMaximum -> MAXIMUM
			// PropertyMapping: Average -> X
			// PropertyMapping: StandardDeviation -> S
			// PropertyMapping: ControlledByUnitId -> CONTROLED_BY_UNIT_ID
			// PropertyMapping: ThresholdTorque -> START_COUNT
			// PropertyMapping: SensorSerialNumber -> SENSOR_SERIAL_NUMBER
			// PropertyMapping: Result -> RESULT
			// PropertyMapping: TestEquipment -> TEST_DEVICE_ID
			// PropertyMapping: User -> TESTER_ID
			// PropertyMapping: ToolId -> POW_TOOL_ID
			// PropertyMapping: LocationToolAssignmentId -> LocPowId
		public DbEntities.ClassicChkTest DirectPropertyMapping(Server.Core.Entities.ClassicChkTest source)
		{
			var target = new DbEntities.ClassicChkTest();
			_assigner.Assign((value) => {target.GlobalHistoryId = value;}, source.Id);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NUMBER_OF_TESTS = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NOMINAL_UNIT1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT1_ID = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.UNIT1_ID = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NOMINAL_UNIT2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT2_ID = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.UNIT2_ID = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.MINIMUM = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.MAXIMUM = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.X = value;}, source.Average);
			_assigner.Assign((value) => {target.S = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.CONTROLED_BY_UNIT_ID = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.START_COUNT = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SENSOR_SERIAL_NUMBER = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.RESULT = value;}, source.Result);
			_assigner.Assign((value) => {target.TEST_DEVICE_ID = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TESTER_ID = value;}, source.User);
			_assigner.Assign((value) => {target.POW_TOOL_ID = value;}, source.ToolId);
			_assigner.Assign((value) => {target.LocPowId = value;}, source.LocationToolAssignmentId);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicChkTest source, DbEntities.ClassicChkTest target)
		{
			_assigner.Assign((value) => {target.GlobalHistoryId = value;}, source.Id);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NUMBER_OF_TESTS = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NOMINAL_UNIT1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT1_ID = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.UNIT1_ID = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NOMINAL_UNIT2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT2_ID = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.UNIT2_ID = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.MINIMUM = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.MAXIMUM = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.X = value;}, source.Average);
			_assigner.Assign((value) => {target.S = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.CONTROLED_BY_UNIT_ID = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.START_COUNT = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SENSOR_SERIAL_NUMBER = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.RESULT = value;}, source.Result);
			_assigner.Assign((value) => {target.TEST_DEVICE_ID = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TESTER_ID = value;}, source.User);
			_assigner.Assign((value) => {target.POW_TOOL_ID = value;}, source.ToolId);
			_assigner.Assign((value) => {target.LocPowId = value;}, source.LocationToolAssignmentId);
		}

		public DbEntities.ClassicChkTest ReflectedPropertyMapping(Server.Core.Entities.ClassicChkTest source)
		{
			var target = new DbEntities.ClassicChkTest();
			typeof(DbEntities.ClassicChkTest).GetField("GlobalHistoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ClassicChkTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(DbEntities.ClassicChkTest).GetField("NUMBER_OF_TESTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DbEntities.ClassicChkTest).GetField("LOWER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("NOMINAL_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("UPPER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("TOLERANCE_CLASS_UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DbEntities.ClassicChkTest).GetField("LOWER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("NOMINAL_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("UPPER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("TOLERANCE_CLASS_UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DbEntities.ClassicChkTest).GetField("MINIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DbEntities.ClassicChkTest).GetField("MAXIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DbEntities.ClassicChkTest).GetField("X", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DbEntities.ClassicChkTest).GetField("S", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DbEntities.ClassicChkTest).GetField("CONTROLED_BY_UNIT_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DbEntities.ClassicChkTest).GetField("START_COUNT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DbEntities.ClassicChkTest).GetField("SENSOR_SERIAL_NUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DbEntities.ClassicChkTest).GetField("RESULT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DbEntities.ClassicChkTest).GetField("TEST_DEVICE_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DbEntities.ClassicChkTest).GetField("TESTER_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DbEntities.ClassicChkTest).GetField("POW_TOOL_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DbEntities.ClassicChkTest).GetField("LocPowId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicChkTest source, DbEntities.ClassicChkTest target)
		{
			typeof(DbEntities.ClassicChkTest).GetField("GlobalHistoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ClassicChkTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(DbEntities.ClassicChkTest).GetField("NUMBER_OF_TESTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DbEntities.ClassicChkTest).GetField("LOWER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("NOMINAL_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("UPPER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("TOLERANCE_CLASS_UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DbEntities.ClassicChkTest).GetField("UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DbEntities.ClassicChkTest).GetField("LOWER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("NOMINAL_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("UPPER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("TOLERANCE_CLASS_UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DbEntities.ClassicChkTest).GetField("UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DbEntities.ClassicChkTest).GetField("MINIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DbEntities.ClassicChkTest).GetField("MAXIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DbEntities.ClassicChkTest).GetField("X", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DbEntities.ClassicChkTest).GetField("S", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DbEntities.ClassicChkTest).GetField("CONTROLED_BY_UNIT_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DbEntities.ClassicChkTest).GetField("START_COUNT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DbEntities.ClassicChkTest).GetField("SENSOR_SERIAL_NUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DbEntities.ClassicChkTest).GetField("RESULT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DbEntities.ClassicChkTest).GetField("TEST_DEVICE_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DbEntities.ClassicChkTest).GetField("TESTER_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DbEntities.ClassicChkTest).GetField("POW_TOOL_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DbEntities.ClassicChkTest).GetField("LocPowId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
		}

		// ClassicChkTestValueDbToClassicChkTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: GLOBALHISTORYID -> Id
			// PropertyMapping: POSITION -> Position
			// PropertyMapping: VALUE_UNIT1 -> ValueUnit1
			// PropertyMapping: VALUE_UNIT2 -> ValueUnit2
		public Server.Core.Entities.ClassicChkTestValue DirectPropertyMapping(DbEntities.ClassicChkTestValue source)
		{
			var target = new Server.Core.Entities.ClassicChkTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.GLOBALHISTORYID);
			_assigner.Assign((value) => {target.Position = value;}, source.POSITION);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.VALUE_UNIT1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.VALUE_UNIT2);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicChkTestValue source, Server.Core.Entities.ClassicChkTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.GLOBALHISTORYID);
			_assigner.Assign((value) => {target.Position = value;}, source.POSITION);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.VALUE_UNIT1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.VALUE_UNIT2);
		}

		public Server.Core.Entities.ClassicChkTestValue ReflectedPropertyMapping(DbEntities.ClassicChkTestValue source)
		{
			var target = new Server.Core.Entities.ClassicChkTestValue();
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GLOBALHISTORYID);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSITION);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT2);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicChkTestValue source, Server.Core.Entities.ClassicChkTestValue target)
		{
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GLOBALHISTORYID);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSITION);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT1);
			typeof(Server.Core.Entities.ClassicChkTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT2);
		}

		// ClassicMfuTestDbToClassicMfuTest.txt
			// hasDefaultConstructor
			// PropertyMapping: GlobalHistoryId -> Id
			// PropertyMapping: Shift -> Shift
			// PropertyMapping: NUMBER_OF_TESTS -> NumberOfTests
			// PropertyMapping: LOWER_LIMIT_UNIT1 -> LowerLimitUnit1
			// PropertyMapping: NOMINAL_UNIT1 -> NominalValueUnit1
			// PropertyMapping: UPPER_LIMIT_UNIT1 -> UpperLimitUnit1
			// PropertyMapping: TOLERANCE_CLASS_UNIT1_ID -> ToleranceClassUnit1
			// PropertyMapping: UNIT1_ID -> Unit1Id
			// PropertyMapping: LOWER_LIMIT_UNIT2 -> LowerLimitUnit2
			// PropertyMapping: NOMINAL_UNIT2 -> NominalValueUnit2
			// PropertyMapping: UPPER_LIMIT_UNIT2 -> UpperLimitUnit2
			// PropertyMapping: TOLERANCE_CLASS_UNIT2_ID -> ToleranceClassUnit2
			// PropertyMapping: UNIT2_ID -> Unit2Id
			// PropertyMapping: MINIMUM -> TestValueMinimum
			// PropertyMapping: MAXIMUM -> TestValueMaximum
			// PropertyMapping: X -> Average
			// PropertyMapping: S -> StandardDeviation
			// PropertyMapping: CONTROLED_BY_UNIT_ID -> ControlledByUnitId
			// PropertyMapping: START_COUNT -> ThresholdTorque
			// PropertyMapping: SENSOR_SERIAL_NUMBER -> SensorSerialNumber
			// PropertyMapping: RESULT -> Result
			// PropertyMapping: CMK -> Cmk
			// PropertyMapping: CM -> Cm
			// PropertyMapping: LIMIT_CMK -> LimitCmk
			// PropertyMapping: LIMIT_CM -> LimitCm
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: ClassicMfuTestLocation -> TestLocation
			// PropertyMapping: LocPowId -> LocationToolAssignmentId
		public Server.Core.Entities.ClassicMfuTest DirectPropertyMapping(DbEntities.ClassicMfuTest source)
		{
			var target = new Server.Core.Entities.ClassicMfuTest();
			_assigner.Assign((value) => {target.Id = value;}, source.GlobalHistoryId);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NUMBER_OF_TESTS);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LOWER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NOMINAL_UNIT1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UPPER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.TOLERANCE_CLASS_UNIT1_ID);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.UNIT1_ID);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LOWER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NOMINAL_UNIT2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UPPER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.TOLERANCE_CLASS_UNIT2_ID);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.UNIT2_ID);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.MINIMUM);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.MAXIMUM);
			_assigner.Assign((value) => {target.Average = value;}, source.X);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.S);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.CONTROLED_BY_UNIT_ID);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.START_COUNT);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SENSOR_SERIAL_NUMBER);
			_assigner.Assign((value) => {target.Result = value;}, source.RESULT);
			_assigner.Assign((value) => {target.Cmk = value;}, source.CMK);
			_assigner.Assign((value) => {target.Cm = value;}, source.CM);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LIMIT_CMK);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LIMIT_CM);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.ClassicMfuTestLocation);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocPowId);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicMfuTest source, Server.Core.Entities.ClassicMfuTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.GlobalHistoryId);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NUMBER_OF_TESTS);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LOWER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NOMINAL_UNIT1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UPPER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.TOLERANCE_CLASS_UNIT1_ID);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.UNIT1_ID);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LOWER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NOMINAL_UNIT2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UPPER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.TOLERANCE_CLASS_UNIT2_ID);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.UNIT2_ID);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.MINIMUM);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.MAXIMUM);
			_assigner.Assign((value) => {target.Average = value;}, source.X);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.S);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.CONTROLED_BY_UNIT_ID);
			_assigner.Assign((value) => {target.ThresholdTorque = value;}, source.START_COUNT);
			_assigner.Assign((value) => {target.SensorSerialNumber = value;}, source.SENSOR_SERIAL_NUMBER);
			_assigner.Assign((value) => {target.Result = value;}, source.RESULT);
			_assigner.Assign((value) => {target.Cmk = value;}, source.CMK);
			_assigner.Assign((value) => {target.Cm = value;}, source.CM);
			_assigner.Assign((value) => {target.LimitCmk = value;}, source.LIMIT_CMK);
			_assigner.Assign((value) => {target.LimitCm = value;}, source.LIMIT_CM);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.ClassicMfuTestLocation);
			_assigner.Assign((value) => {target.LocationToolAssignmentId = value;}, source.LocPowId);
		}

		public Server.Core.Entities.ClassicMfuTest ReflectedPropertyMapping(DbEntities.ClassicMfuTest source)
		{
			var target = new Server.Core.Entities.ClassicMfuTest();
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GlobalHistoryId);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NUMBER_OF_TESTS);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT1_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT1_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT2_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT2_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MINIMUM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAXIMUM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.X);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.S);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROLED_BY_UNIT_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.START_COUNT);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SENSOR_SERIAL_NUMBER);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.RESULT);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CMK);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMIT_CMK);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMIT_CM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ClassicMfuTestLocation);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocPowId);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicMfuTest source, Server.Core.Entities.ClassicMfuTest target)
		{
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GlobalHistoryId);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NUMBER_OF_TESTS);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT1_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT1_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT2_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT2_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MINIMUM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAXIMUM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.X);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.S);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROLED_BY_UNIT_ID);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("ThresholdTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.START_COUNT);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("SensorSerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SENSOR_SERIAL_NUMBER);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.RESULT);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CMK);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("Cm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCmk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMIT_CMK);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LimitCm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMIT_CM);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ClassicMfuTestLocation);
			typeof(Server.Core.Entities.ClassicMfuTest).GetField("LocationToolAssignmentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocPowId);
		}

		// ClassicMfuTestLocationDbToClassicTestLocation.txt
			// hasDefaultConstructor
			// PropertyMapping: LOCATION_ID -> LocationId
			// PropertyMapping: LOCTREE_ID -> LocationDirectoryId
			// PropertyMapping: LOCATION_TREE_PATH -> LocationTreePath
		public Server.Core.Entities.ClassicTestLocation DirectPropertyMapping(DbEntities.ClassicMfuTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			_assigner.Assign((value) => {target.LocationId = value;}, source.LOCATION_ID);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LOCTREE_ID);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.LOCATION_TREE_PATH);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicMfuTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			_assigner.Assign((value) => {target.LocationId = value;}, source.LOCATION_ID);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LOCTREE_ID);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.LOCATION_TREE_PATH);
		}

		public Server.Core.Entities.ClassicTestLocation ReflectedPropertyMapping(DbEntities.ClassicMfuTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREE_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_TREE_PATH);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicMfuTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREE_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_TREE_PATH);
		}

		// ClassicMfuTestToClassicMfuTestDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> GlobalHistoryId
			// PropertyMapping: Shift -> Shift
			// PropertyMapping: NumberOfTests -> NUMBER_OF_TESTS
			// PropertyMapping: LowerLimitUnit1 -> LOWER_LIMIT_UNIT1
			// PropertyMapping: NominalValueUnit1 -> NOMINAL_UNIT1
			// PropertyMapping: UpperLimitUnit1 -> UPPER_LIMIT_UNIT1
			// PropertyMapping: ToleranceClassUnit1 -> TOLERANCE_CLASS_UNIT1_ID
			// PropertyMapping: Unit1Id -> UNIT1_ID
			// PropertyMapping: LowerLimitUnit2 -> LOWER_LIMIT_UNIT2
			// PropertyMapping: NominalValueUnit2 -> NOMINAL_UNIT2
			// PropertyMapping: UpperLimitUnit2 -> UPPER_LIMIT_UNIT2
			// PropertyMapping: ToleranceClassUnit2 -> TOLERANCE_CLASS_UNIT2_ID
			// PropertyMapping: Unit2Id -> UNIT2_ID
			// PropertyMapping: TestValueMinimum -> MINIMUM
			// PropertyMapping: TestValueMaximum -> MAXIMUM
			// PropertyMapping: Average -> X
			// PropertyMapping: StandardDeviation -> S
			// PropertyMapping: ControlledByUnitId -> CONTROLED_BY_UNIT_ID
			// PropertyMapping: ThresholdTorque -> START_COUNT
			// PropertyMapping: SensorSerialNumber -> SENSOR_SERIAL_NUMBER
			// PropertyMapping: Result -> RESULT
			// PropertyMapping: Cmk -> CMK
			// PropertyMapping: Cm -> CM
			// PropertyMapping: LimitCmk -> LIMIT_CMK
			// PropertyMapping: LimitCm -> LIMIT_CM
			// PropertyMapping: TestEquipment -> TEST_DEVICE_ID
			// PropertyMapping: User -> TESTER_ID
			// PropertyMapping: ToolId -> POW_TOOL_ID
			// PropertyMapping: LocationToolAssignmentId -> LocPowId
		public DbEntities.ClassicMfuTest DirectPropertyMapping(Server.Core.Entities.ClassicMfuTest source)
		{
			var target = new DbEntities.ClassicMfuTest();
			_assigner.Assign((value) => {target.GlobalHistoryId = value;}, source.Id);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NUMBER_OF_TESTS = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NOMINAL_UNIT1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT1_ID = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.UNIT1_ID = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NOMINAL_UNIT2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT2_ID = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.UNIT2_ID = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.MINIMUM = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.MAXIMUM = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.X = value;}, source.Average);
			_assigner.Assign((value) => {target.S = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.CONTROLED_BY_UNIT_ID = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.START_COUNT = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SENSOR_SERIAL_NUMBER = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.RESULT = value;}, source.Result);
			_assigner.Assign((value) => {target.CMK = value;}, source.Cmk);
			_assigner.Assign((value) => {target.CM = value;}, source.Cm);
			_assigner.Assign((value) => {target.LIMIT_CMK = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.LIMIT_CM = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.TEST_DEVICE_ID = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TESTER_ID = value;}, source.User);
			_assigner.Assign((value) => {target.POW_TOOL_ID = value;}, source.ToolId);
			_assigner.Assign((value) => {target.LocPowId = value;}, source.LocationToolAssignmentId);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ClassicMfuTest source, DbEntities.ClassicMfuTest target)
		{
			_assigner.Assign((value) => {target.GlobalHistoryId = value;}, source.Id);
			_assigner.Assign((value) => {target.Shift = value;}, source.Shift);
			_assigner.Assign((value) => {target.NUMBER_OF_TESTS = value;}, source.NumberOfTests);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT1 = value;}, source.LowerLimitUnit1);
			_assigner.Assign((value) => {target.NOMINAL_UNIT1 = value;}, source.NominalValueUnit1);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT1 = value;}, source.UpperLimitUnit1);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT1_ID = value;}, source.ToleranceClassUnit1);
			_assigner.Assign((value) => {target.UNIT1_ID = value;}, source.Unit1Id);
			_assigner.Assign((value) => {target.LOWER_LIMIT_UNIT2 = value;}, source.LowerLimitUnit2);
			_assigner.Assign((value) => {target.NOMINAL_UNIT2 = value;}, source.NominalValueUnit2);
			_assigner.Assign((value) => {target.UPPER_LIMIT_UNIT2 = value;}, source.UpperLimitUnit2);
			_assigner.Assign((value) => {target.TOLERANCE_CLASS_UNIT2_ID = value;}, source.ToleranceClassUnit2);
			_assigner.Assign((value) => {target.UNIT2_ID = value;}, source.Unit2Id);
			_assigner.Assign((value) => {target.MINIMUM = value;}, source.TestValueMinimum);
			_assigner.Assign((value) => {target.MAXIMUM = value;}, source.TestValueMaximum);
			_assigner.Assign((value) => {target.X = value;}, source.Average);
			_assigner.Assign((value) => {target.S = value;}, source.StandardDeviation);
			_assigner.Assign((value) => {target.CONTROLED_BY_UNIT_ID = value;}, source.ControlledByUnitId);
			_assigner.Assign((value) => {target.START_COUNT = value;}, source.ThresholdTorque);
			_assigner.Assign((value) => {target.SENSOR_SERIAL_NUMBER = value;}, source.SensorSerialNumber);
			_assigner.Assign((value) => {target.RESULT = value;}, source.Result);
			_assigner.Assign((value) => {target.CMK = value;}, source.Cmk);
			_assigner.Assign((value) => {target.CM = value;}, source.Cm);
			_assigner.Assign((value) => {target.LIMIT_CMK = value;}, source.LimitCmk);
			_assigner.Assign((value) => {target.LIMIT_CM = value;}, source.LimitCm);
			_assigner.Assign((value) => {target.TEST_DEVICE_ID = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TESTER_ID = value;}, source.User);
			_assigner.Assign((value) => {target.POW_TOOL_ID = value;}, source.ToolId);
			_assigner.Assign((value) => {target.LocPowId = value;}, source.LocationToolAssignmentId);
		}

		public DbEntities.ClassicMfuTest ReflectedPropertyMapping(Server.Core.Entities.ClassicMfuTest source)
		{
			var target = new DbEntities.ClassicMfuTest();
			typeof(DbEntities.ClassicMfuTest).GetField("GlobalHistoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ClassicMfuTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(DbEntities.ClassicMfuTest).GetField("NUMBER_OF_TESTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DbEntities.ClassicMfuTest).GetField("LOWER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("NOMINAL_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("UPPER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("TOLERANCE_CLASS_UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DbEntities.ClassicMfuTest).GetField("LOWER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("NOMINAL_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("UPPER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("TOLERANCE_CLASS_UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DbEntities.ClassicMfuTest).GetField("MINIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DbEntities.ClassicMfuTest).GetField("MAXIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DbEntities.ClassicMfuTest).GetField("X", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DbEntities.ClassicMfuTest).GetField("S", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DbEntities.ClassicMfuTest).GetField("CONTROLED_BY_UNIT_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DbEntities.ClassicMfuTest).GetField("START_COUNT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DbEntities.ClassicMfuTest).GetField("SENSOR_SERIAL_NUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DbEntities.ClassicMfuTest).GetField("RESULT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DbEntities.ClassicMfuTest).GetField("CMK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(DbEntities.ClassicMfuTest).GetField("CM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(DbEntities.ClassicMfuTest).GetField("LIMIT_CMK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(DbEntities.ClassicMfuTest).GetField("LIMIT_CM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(DbEntities.ClassicMfuTest).GetField("TEST_DEVICE_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DbEntities.ClassicMfuTest).GetField("TESTER_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DbEntities.ClassicMfuTest).GetField("POW_TOOL_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DbEntities.ClassicMfuTest).GetField("LocPowId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ClassicMfuTest source, DbEntities.ClassicMfuTest target)
		{
			typeof(DbEntities.ClassicMfuTest).GetField("GlobalHistoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ClassicMfuTest).GetField("Shift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Shift);
			typeof(DbEntities.ClassicMfuTest).GetField("NUMBER_OF_TESTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NumberOfTests);
			typeof(DbEntities.ClassicMfuTest).GetField("LOWER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("NOMINAL_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("UPPER_LIMIT_UNIT1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("TOLERANCE_CLASS_UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit1);
			typeof(DbEntities.ClassicMfuTest).GetField("UNIT1_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit1Id);
			typeof(DbEntities.ClassicMfuTest).GetField("LOWER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimitUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("NOMINAL_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NominalValueUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("UPPER_LIMIT_UNIT2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimitUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("TOLERANCE_CLASS_UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClassUnit2);
			typeof(DbEntities.ClassicMfuTest).GetField("UNIT2_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Unit2Id);
			typeof(DbEntities.ClassicMfuTest).GetField("MINIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMinimum);
			typeof(DbEntities.ClassicMfuTest).GetField("MAXIMUM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestValueMaximum);
			typeof(DbEntities.ClassicMfuTest).GetField("X", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Average);
			typeof(DbEntities.ClassicMfuTest).GetField("S", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StandardDeviation);
			typeof(DbEntities.ClassicMfuTest).GetField("CONTROLED_BY_UNIT_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledByUnitId);
			typeof(DbEntities.ClassicMfuTest).GetField("START_COUNT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ThresholdTorque);
			typeof(DbEntities.ClassicMfuTest).GetField("SENSOR_SERIAL_NUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SensorSerialNumber);
			typeof(DbEntities.ClassicMfuTest).GetField("RESULT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Result);
			typeof(DbEntities.ClassicMfuTest).GetField("CMK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cmk);
			typeof(DbEntities.ClassicMfuTest).GetField("CM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Cm);
			typeof(DbEntities.ClassicMfuTest).GetField("LIMIT_CMK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCmk);
			typeof(DbEntities.ClassicMfuTest).GetField("LIMIT_CM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LimitCm);
			typeof(DbEntities.ClassicMfuTest).GetField("TEST_DEVICE_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(DbEntities.ClassicMfuTest).GetField("TESTER_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.User);
			typeof(DbEntities.ClassicMfuTest).GetField("POW_TOOL_ID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolId);
			typeof(DbEntities.ClassicMfuTest).GetField("LocPowId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LocationToolAssignmentId);
		}

		// ClassicMfuTestValueDbToClassicMfuTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: GLOBALHISTORYID -> Id
			// PropertyMapping: POSITION -> Position
			// PropertyMapping: VALUE_UNIT1 -> ValueUnit1
			// PropertyMapping: VALUE_UNIT2 -> ValueUnit2
		public Server.Core.Entities.ClassicMfuTestValue DirectPropertyMapping(DbEntities.ClassicMfuTestValue source)
		{
			var target = new Server.Core.Entities.ClassicMfuTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.GLOBALHISTORYID);
			_assigner.Assign((value) => {target.Position = value;}, source.POSITION);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.VALUE_UNIT1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.VALUE_UNIT2);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicMfuTestValue source, Server.Core.Entities.ClassicMfuTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.GLOBALHISTORYID);
			_assigner.Assign((value) => {target.Position = value;}, source.POSITION);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.VALUE_UNIT1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.VALUE_UNIT2);
		}

		public Server.Core.Entities.ClassicMfuTestValue ReflectedPropertyMapping(DbEntities.ClassicMfuTestValue source)
		{
			var target = new Server.Core.Entities.ClassicMfuTestValue();
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GLOBALHISTORYID);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSITION);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT2);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicMfuTestValue source, Server.Core.Entities.ClassicMfuTestValue target)
		{
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GLOBALHISTORYID);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSITION);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT1);
			typeof(Server.Core.Entities.ClassicMfuTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT2);
		}

		// ClassicProcessTestDbToClassicProcessTest.txt
			// hasDefaultConstructor
			// PropertyMapping: GlobalHistoryId -> Id
			// PropertyMapping: NUMBER_OF_TESTS -> NumberOfTests
			// PropertyMapping: LOWER_LIMIT_UNIT1 -> LowerLimitUnit1
			// PropertyMapping: NOMINAL_UNIT1 -> NominalValueUnit1
			// PropertyMapping: UPPER_LIMIT_UNIT1 -> UpperLimitUnit1
			// PropertyMapping: LOWER_LIMIT_UNIT2 -> LowerLimitUnit2
			// PropertyMapping: NOMINAL_UNIT2 -> NominalValueUnit2
			// PropertyMapping: UPPER_LIMIT_UNIT2 -> UpperLimitUnit2
			// PropertyMapping: AVERAGE -> Average
			// PropertyMapping: STANDARDDEVIATION -> StandardDeviation
			// PropertyMapping: MINIMUM -> TestValueMinimum
			// PropertyMapping: MAXIMUM -> TestValueMaximum
			// PropertyMapping: CONTROLED_BY_UNIT_ID -> ControlledByUnitId
			// PropertyMapping: UNIT1_ID -> Unit1Id
			// PropertyMapping: UNIT2_ID -> Unit2Id
			// PropertyMapping: LOWER_INTERVENTION_LIMIT_UNIT1 -> LowerInterventionLimitUnit1
			// PropertyMapping: UPPER_INTERVENTION_LIMIT_UNIT1 -> UpperInterventionLimitUnit1
			// PropertyMapping: LOWER_INTERVENTION_LIMIT_UNIT2 -> LowerInterventionLimitUnit2
			// PropertyMapping: UPPER_INTERVENTION_LIMIT_UNIT2 -> UpperInterventionLimitUnit2
			// PropertyMapping: RESULT -> Result
			// PropertyMapping: TestEquipment -> TestEquipment
			// PropertyMapping: ClassicProcessTestLocation -> TestLocation
			// PropertyMapping: TOLERANCE_CLASS_UNIT1_ID -> ToleranceClassUnit1
			// PropertyMapping: TOLERANCE_CLASS_UNIT2_ID -> ToleranceClassUnit2
		public Server.Core.Entities.ClassicProcessTest DirectPropertyMapping(DbEntities.ClassicProcessTest source)
		{
			var target = new Server.Core.Entities.ClassicProcessTest();
			_assigner.Assign((value) => {target.Id = value;}, source.GlobalHistoryId);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NUMBER_OF_TESTS);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LOWER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NOMINAL_UNIT1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UPPER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LOWER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NOMINAL_UNIT2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UPPER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.Average = value;}, source.AVERAGE);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.STANDARDDEVIATION);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.MINIMUM);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.MAXIMUM);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.CONTROLED_BY_UNIT_ID);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.UNIT1_ID);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.UNIT2_ID);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit1 = value;}, source.LOWER_INTERVENTION_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit1 = value;}, source.UPPER_INTERVENTION_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit2 = value;}, source.LOWER_INTERVENTION_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit2 = value;}, source.UPPER_INTERVENTION_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.Result = value;}, source.RESULT);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.ClassicProcessTestLocation);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.TOLERANCE_CLASS_UNIT1_ID);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.TOLERANCE_CLASS_UNIT2_ID);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicProcessTest source, Server.Core.Entities.ClassicProcessTest target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.GlobalHistoryId);
			_assigner.Assign((value) => {target.NumberOfTests = value;}, source.NUMBER_OF_TESTS);
			_assigner.Assign((value) => {target.LowerLimitUnit1 = value;}, source.LOWER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.NominalValueUnit1 = value;}, source.NOMINAL_UNIT1);
			_assigner.Assign((value) => {target.UpperLimitUnit1 = value;}, source.UPPER_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.LowerLimitUnit2 = value;}, source.LOWER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.NominalValueUnit2 = value;}, source.NOMINAL_UNIT2);
			_assigner.Assign((value) => {target.UpperLimitUnit2 = value;}, source.UPPER_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.Average = value;}, source.AVERAGE);
			_assigner.Assign((value) => {target.StandardDeviation = value;}, source.STANDARDDEVIATION);
			_assigner.Assign((value) => {target.TestValueMinimum = value;}, source.MINIMUM);
			_assigner.Assign((value) => {target.TestValueMaximum = value;}, source.MAXIMUM);
			_assigner.Assign((value) => {target.ControlledByUnitId = value;}, source.CONTROLED_BY_UNIT_ID);
			_assigner.Assign((value) => {target.Unit1Id = value;}, source.UNIT1_ID);
			_assigner.Assign((value) => {target.Unit2Id = value;}, source.UNIT2_ID);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit1 = value;}, source.LOWER_INTERVENTION_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit1 = value;}, source.UPPER_INTERVENTION_LIMIT_UNIT1);
			_assigner.Assign((value) => {target.LowerInterventionLimitUnit2 = value;}, source.LOWER_INTERVENTION_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.UpperInterventionLimitUnit2 = value;}, source.UPPER_INTERVENTION_LIMIT_UNIT2);
			_assigner.Assign((value) => {target.Result = value;}, source.RESULT);
			_assigner.Assign((value) => {target.TestEquipment = value;}, source.TestEquipment);
			_assigner.Assign((value) => {target.TestLocation = value;}, source.ClassicProcessTestLocation);
			_assigner.Assign((value) => {target.ToleranceClassUnit1 = value;}, source.TOLERANCE_CLASS_UNIT1_ID);
			_assigner.Assign((value) => {target.ToleranceClassUnit2 = value;}, source.TOLERANCE_CLASS_UNIT2_ID);
		}

		public Server.Core.Entities.ClassicProcessTest ReflectedPropertyMapping(DbEntities.ClassicProcessTest source)
		{
			var target = new Server.Core.Entities.ClassicProcessTest();
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GlobalHistoryId);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NUMBER_OF_TESTS);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AVERAGE);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STANDARDDEVIATION);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MINIMUM);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAXIMUM);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROLED_BY_UNIT_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT1_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT2_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_INTERVENTION_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_INTERVENTION_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_INTERVENTION_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_INTERVENTION_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.RESULT);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ClassicProcessTestLocation);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT1_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT2_ID);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicProcessTest source, Server.Core.Entities.ClassicProcessTest target)
		{
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GlobalHistoryId);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("NumberOfTests", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NUMBER_OF_TESTS);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("NominalValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOMINAL_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Average", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AVERAGE);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("StandardDeviation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STANDARDDEVIATION);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestValueMinimum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MINIMUM);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestValueMaximum", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAXIMUM);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("ControlledByUnitId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROLED_BY_UNIT_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Unit1Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT1_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Unit2Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNIT2_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_INTERVENTION_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_INTERVENTION_LIMIT_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("LowerInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOWER_INTERVENTION_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("UpperInterventionLimitUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UPPER_INTERVENTION_LIMIT_UNIT2);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("Result", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.RESULT);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestEquipment", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipment);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("TestLocation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ClassicProcessTestLocation);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT1_ID);
			typeof(Server.Core.Entities.ClassicProcessTest).GetField("ToleranceClassUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOLERANCE_CLASS_UNIT2_ID);
		}

		// ClassicProcessTestLocationDbToClassicProcesstLocation.txt
			// hasDefaultConstructor
			// PropertyMapping: LOCATION_ID -> LocationId
			// PropertyMapping: LOCTREE_ID -> LocationDirectoryId
			// PropertyMapping: LOCATION_TREE_PATH -> LocationTreePath
		public Server.Core.Entities.ClassicTestLocation DirectPropertyMapping(DbEntities.ClassicProcessTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			_assigner.Assign((value) => {target.LocationId = value;}, source.LOCATION_ID);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LOCTREE_ID);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.LOCATION_TREE_PATH);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicProcessTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			_assigner.Assign((value) => {target.LocationId = value;}, source.LOCATION_ID);
			_assigner.Assign((value) => {target.LocationDirectoryId = value;}, source.LOCTREE_ID);
			_assigner.Assign((value) => {target.LocationTreePath = value;}, source.LOCATION_TREE_PATH);
		}

		public Server.Core.Entities.ClassicTestLocation ReflectedPropertyMapping(DbEntities.ClassicProcessTestLocation source)
		{
			var target = new Server.Core.Entities.ClassicTestLocation();
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREE_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_TREE_PATH);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicProcessTestLocation source, Server.Core.Entities.ClassicTestLocation target)
		{
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREE_ID);
			typeof(Server.Core.Entities.ClassicTestLocation).GetField("LocationTreePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCATION_TREE_PATH);
		}

		// ClassicProcessTestValueDbToClassicProcessTestValue.txt
			// hasDefaultConstructor
			// PropertyMapping: GLOBALHISTORYID -> Id
			// PropertyMapping: POSITION -> Position
			// PropertyMapping: VALUE_UNIT1 -> ValueUnit1
			// PropertyMapping: VALUE_UNIT2 -> ValueUnit2
		public Server.Core.Entities.ClassicProcessTestValue DirectPropertyMapping(DbEntities.ClassicProcessTestValue source)
		{
			var target = new Server.Core.Entities.ClassicProcessTestValue();
			_assigner.Assign((value) => {target.Id = value;}, source.GLOBALHISTORYID);
			_assigner.Assign((value) => {target.Position = value;}, source.POSITION);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.VALUE_UNIT1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.VALUE_UNIT2);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ClassicProcessTestValue source, Server.Core.Entities.ClassicProcessTestValue target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.GLOBALHISTORYID);
			_assigner.Assign((value) => {target.Position = value;}, source.POSITION);
			_assigner.Assign((value) => {target.ValueUnit1 = value;}, source.VALUE_UNIT1);
			_assigner.Assign((value) => {target.ValueUnit2 = value;}, source.VALUE_UNIT2);
		}

		public Server.Core.Entities.ClassicProcessTestValue ReflectedPropertyMapping(DbEntities.ClassicProcessTestValue source)
		{
			var target = new Server.Core.Entities.ClassicProcessTestValue();
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GLOBALHISTORYID);
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSITION);
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT2);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ClassicProcessTestValue source, Server.Core.Entities.ClassicProcessTestValue target)
		{
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.GLOBALHISTORYID);
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("Position", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSITION);
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT1);
			typeof(Server.Core.Entities.ClassicProcessTestValue).GetField("ValueUnit2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VALUE_UNIT2);
		}

		// CondLocDbToProcessControlCondition.txt
			// hasDefaultConstructor
			// PropertyMapping: SEQID -> Id
			// PropertyMapping: MDMAX -> UpperMeasuringLimit
			// PropertyMapping: MDMIN -> LowerMeasuringLimit
			// PropertyMapping: OEGN -> UpperInterventionLimit
			// PropertyMapping: UEGN -> LowerInterventionLimit
			// PropertyMapping: TESTLEVELNUMBER -> TestLevelNumber
			// PropertyMapping: TESTLEVELSETID -> TestLevelSet
			// PropertyMapping: PLANOK -> TestOperationActive
			// PropertyMapping: TESTSTART -> StartDate
			// PropertyMapping: ALIVE -> Alive
			// PropertyMapping: ENDOFLASTTESTPERIOD -> EndOfLastTestPeriod
			// PropertyMapping: ENDOFLASTTESTPERIODSHIFT -> EndOfLastTestPeriodShift
			// PropertyMapping: NEXT_CTL -> NextTestDate
			// PropertyMapping: NEXTSHIFT -> NextTestShift
		public Server.Core.Entities.ProcessControlCondition DirectPropertyMapping(DbEntities.CondLoc source)
		{
			var target = new Server.Core.Entities.ProcessControlCondition();
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.UpperMeasuringLimit = value;}, source.MDMAX);
			_assigner.Assign((value) => {target.LowerMeasuringLimit = value;}, source.MDMIN);
			_assigner.Assign((value) => {target.UpperInterventionLimit = value;}, source.OEGN);
			_assigner.Assign((value) => {target.LowerInterventionLimit = value;}, source.UEGN);
			_assigner.Assign((value) => {target.TestLevelNumber = value;}, source.TESTLEVELNUMBER);
			_assigner.Assign((value) => {target.TestLevelSet = value;}, source.TESTLEVELSETID);
			_assigner.Assign((value) => {target.TestOperationActive = value;}, source.PLANOK);
			_assigner.Assign((value) => {target.StartDate = value;}, source.TESTSTART);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			_assigner.Assign((value) => {target.EndOfLastTestPeriod = value;}, source.ENDOFLASTTESTPERIOD);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShift = value;}, source.ENDOFLASTTESTPERIODSHIFT);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NEXT_CTL);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NEXTSHIFT);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.CondLoc source, Server.Core.Entities.ProcessControlCondition target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.UpperMeasuringLimit = value;}, source.MDMAX);
			_assigner.Assign((value) => {target.LowerMeasuringLimit = value;}, source.MDMIN);
			_assigner.Assign((value) => {target.UpperInterventionLimit = value;}, source.OEGN);
			_assigner.Assign((value) => {target.LowerInterventionLimit = value;}, source.UEGN);
			_assigner.Assign((value) => {target.TestLevelNumber = value;}, source.TESTLEVELNUMBER);
			_assigner.Assign((value) => {target.TestLevelSet = value;}, source.TESTLEVELSETID);
			_assigner.Assign((value) => {target.TestOperationActive = value;}, source.PLANOK);
			_assigner.Assign((value) => {target.StartDate = value;}, source.TESTSTART);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			_assigner.Assign((value) => {target.EndOfLastTestPeriod = value;}, source.ENDOFLASTTESTPERIOD);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShift = value;}, source.ENDOFLASTTESTPERIODSHIFT);
			_assigner.Assign((value) => {target.NextTestDate = value;}, source.NEXT_CTL);
			_assigner.Assign((value) => {target.NextTestShift = value;}, source.NEXTSHIFT);
		}

		public Server.Core.Entities.ProcessControlCondition ReflectedPropertyMapping(DbEntities.CondLoc source)
		{
			var target = new Server.Core.Entities.ProcessControlCondition();
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MDMAX);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MDMIN);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.OEGN);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UEGN);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTLEVELNUMBER);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTLEVELSETID);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PLANOK);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTSTART);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("EndOfLastTestPeriod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ENDOFLASTTESTPERIOD);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("EndOfLastTestPeriodShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ENDOFLASTTESTPERIODSHIFT);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXT_CTL);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXTSHIFT);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.CondLoc source, Server.Core.Entities.ProcessControlCondition target)
		{
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MDMAX);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerMeasuringLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MDMIN);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("UpperInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.OEGN);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("LowerInterventionLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UEGN);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTLEVELNUMBER);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestLevelSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTLEVELSETID);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("TestOperationActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PLANOK);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("StartDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTSTART);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("EndOfLastTestPeriod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ENDOFLASTTESTPERIOD);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("EndOfLastTestPeriodShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ENDOFLASTTESTPERIODSHIFT);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXT_CTL);
			typeof(Server.Core.Entities.ProcessControlCondition).GetField("NextTestShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXTSHIFT);
		}

		// CondLocTechDbToQstProcessControlTech.txt
			// PropertyMapping: SEQID -> Id
			// PropertyMapping: CONDLOCID -> ProcessControlConditionId
			// PropertyMapping: HERSTELLERID -> ManufacturerId
			// PropertyMapping: METHODE -> TestMethod
			// PropertyMapping: EXTENSIONID -> Extension
			// PropertyMapping: Extension -> Extension
			// PropertyMapping: ALIVE -> Alive
			// PropertyMapping: I0 -> AngleLimitMt
			// PropertyMapping: F0 -> StartMeasurementPeak
			// PropertyMapping: F1 -> StartAngleCountingPa
			// PropertyMapping: F2 -> AngleForFurtherTurningPa
			// PropertyMapping: F3 -> TargetAnglePa
			// PropertyMapping: F4 -> StartMeasurementPa
			// PropertyMapping: F5 -> AlarmTorquePa
			// PropertyMapping: F6 -> AlarmAnglePa
			// PropertyMapping: F7 -> MinimumTorqueMt
			// PropertyMapping: F8 -> StartAngleMt
			// PropertyMapping: F9 -> StartMeasurementMt
			// PropertyMapping: F10 -> AlarmTorqueMt
			// PropertyMapping: F11 -> AlarmAngleMt
		public void DirectPropertyMapping(DbEntities.CondLocTech source, Server.Core.Entities.QstProcessControlTech target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.ProcessControlConditionId = value;}, source.CONDLOCID);
			_assigner.Assign((value) => {target.ManufacturerId = value;}, source.HERSTELLERID);
			_assigner.Assign((value) => {target.TestMethod = value;}, source.METHODE);
			_assigner.Assign((value) => {target.Extension = value;}, source.EXTENSIONID);
			_assigner.Assign((value) => {target.Extension = value;}, source.Extension);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			_assigner.Assign((value) => {target.AngleLimitMt = value;}, source.I0);
			_assigner.Assign((value) => {target.StartMeasurementPeak = value;}, source.F0);
			_assigner.Assign((value) => {target.StartAngleCountingPa = value;}, source.F1);
			_assigner.Assign((value) => {target.AngleForFurtherTurningPa = value;}, source.F2);
			_assigner.Assign((value) => {target.TargetAnglePa = value;}, source.F3);
			_assigner.Assign((value) => {target.StartMeasurementPa = value;}, source.F4);
			_assigner.Assign((value) => {target.AlarmTorquePa = value;}, source.F5);
			_assigner.Assign((value) => {target.AlarmAnglePa = value;}, source.F6);
			_assigner.Assign((value) => {target.MinimumTorqueMt = value;}, source.F7);
			_assigner.Assign((value) => {target.StartAngleMt = value;}, source.F8);
			_assigner.Assign((value) => {target.StartMeasurementMt = value;}, source.F9);
			_assigner.Assign((value) => {target.AlarmTorqueMt = value;}, source.F10);
			_assigner.Assign((value) => {target.AlarmAngleMt = value;}, source.F11);
		}

		public void ReflectedPropertyMapping(DbEntities.CondLocTech source, Server.Core.Entities.QstProcessControlTech target)
		{
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("ProcessControlConditionId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONDLOCID);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("ManufacturerId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HERSTELLERID);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("TestMethod", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.METHODE);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EXTENSIONID);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("Extension", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AngleLimitMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.I0);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartMeasurementPeak", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F0);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartAngleCountingPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F1);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AngleForFurtherTurningPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F2);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("TargetAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F3);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartMeasurementPa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F4);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmTorquePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F5);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmAnglePa", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F6);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("MinimumTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F7);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F8);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("StartMeasurementMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F9);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmTorqueMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F10);
			typeof(Server.Core.Entities.QstProcessControlTech).GetField("AlarmAngleMt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.F11);
		}

		// DbCondRotToLocationToolAssignment.txt
			// hasDefaultConstructor
			// PropertyMapping: LOCPOWID -> Id
			// PropertyMapping: TestLevelNumberMfu -> TestLevelNumberMfu
			// PropertyMapping: TestLevelNumberChk -> TestLevelNumberChk
			// PropertyMapping: EndOfLastTestPeriodMfu -> EndOfLastTestPeriodMfu
			// PropertyMapping: EndOfLastTestPeriodChk -> EndOfLastTestPeriodChk
			// PropertyMapping: EndOfLastTestPeriodShiftMfu -> EndOfLastTestPeriodShiftMfu
			// PropertyMapping: EndOfLastTestPeriodShiftChk -> EndOfLastTestPeriodShiftChk
			// PropertyMapping: NEXT_MFU -> NextTestDateMfu
			// PropertyMapping: NEXT_CHK -> NextTestDateChk
			// PropertyMapping: TESTSTART_MFU -> StartDateMfu
			// PropertyMapping: TESTSTART -> StartDateChk
			// PropertyMapping: PLANOK_MFU -> TestOperationActiveMfu
			// PropertyMapping: PLANOK -> TestOperationActiveChk
		public Server.Core.Entities.LocationToolAssignment DirectPropertyMapping(DbEntities.CondRot source)
		{
			var target = new Server.Core.Entities.LocationToolAssignment();
			_assigner.Assign((value) => {target.Id = value;}, source.LOCPOWID);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodMfu = value;}, source.EndOfLastTestPeriodMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodChk = value;}, source.EndOfLastTestPeriodChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftMfu = value;}, source.EndOfLastTestPeriodShiftMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftChk = value;}, source.EndOfLastTestPeriodShiftChk);
			_assigner.Assign((value) => {target.NextTestDateMfu = value;}, source.NEXT_MFU);
			_assigner.Assign((value) => {target.NextTestDateChk = value;}, source.NEXT_CHK);
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.TESTSTART_MFU);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.TESTSTART);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.PLANOK_MFU);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.PLANOK);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.CondRot source, Server.Core.Entities.LocationToolAssignment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.LOCPOWID);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodMfu = value;}, source.EndOfLastTestPeriodMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodChk = value;}, source.EndOfLastTestPeriodChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftMfu = value;}, source.EndOfLastTestPeriodShiftMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftChk = value;}, source.EndOfLastTestPeriodShiftChk);
			_assigner.Assign((value) => {target.NextTestDateMfu = value;}, source.NEXT_MFU);
			_assigner.Assign((value) => {target.NextTestDateChk = value;}, source.NEXT_CHK);
			_assigner.Assign((value) => {target.StartDateMfu = value;}, source.TESTSTART_MFU);
			_assigner.Assign((value) => {target.StartDateChk = value;}, source.TESTSTART);
			_assigner.Assign((value) => {target.TestOperationActiveMfu = value;}, source.PLANOK_MFU);
			_assigner.Assign((value) => {target.TestOperationActiveChk = value;}, source.PLANOK);
		}

		public Server.Core.Entities.LocationToolAssignment ReflectedPropertyMapping(DbEntities.CondRot source)
		{
			var target = new Server.Core.Entities.LocationToolAssignment();
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCPOWID);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXT_MFU);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXT_CHK);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTSTART_MFU);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTSTART);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PLANOK_MFU);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PLANOK);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.CondRot source, Server.Core.Entities.LocationToolAssignment target)
		{
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCPOWID);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftMfu);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("EndOfLastTestPeriodShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftChk);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXT_MFU);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("NextTestDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NEXT_CHK);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTSTART_MFU);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("StartDateChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TESTSTART);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PLANOK_MFU);
			typeof(Server.Core.Entities.LocationToolAssignment).GetField("TestOperationActiveChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PLANOK);
		}

		// DbTestLevelToTestLevel.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: SampleNumber -> SampleNumber
			// PropertyMapping: ConsiderWorkingCalendar -> ConsiderWorkingCalendar
			// PropertyMapping: IntervalValue -> TestInterval.IntervalValue
			// PropertyMapping: IntervalType -> TestInterval.Type
			// PropertyMapping: IsActive -> IsActive
		public Server.Core.Entities.TestLevel DirectPropertyMapping(DbEntities.TestLevel source)
		{
			var target = new Server.Core.Entities.TestLevel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.TestInterval.IntervalValue = value;}, source.IntervalValue);
			_assigner.Assign((value) => {target.TestInterval.Type = value;}, source.IntervalType);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.TestLevel source, Server.Core.Entities.TestLevel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.TestInterval.IntervalValue = value;}, source.IntervalValue);
			_assigner.Assign((value) => {target.TestInterval.Type = value;}, source.IntervalType);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
		}

		public Server.Core.Entities.TestLevel ReflectedPropertyMapping(DbEntities.TestLevel source)
		{
			var target = new Server.Core.Entities.TestLevel();
			typeof(Server.Core.Entities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(Server.Core.Entities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(Server.Core.Entities.TestLevel).GetField("TestInterval.IntervalValue", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IntervalValue);
			typeof(Server.Core.Entities.TestLevel).GetField("TestInterval.Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IntervalType);
			typeof(Server.Core.Entities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.TestLevel source, Server.Core.Entities.TestLevel target)
		{
			typeof(Server.Core.Entities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(Server.Core.Entities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(Server.Core.Entities.TestLevel).GetField("TestInterval.IntervalValue", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IntervalValue);
			typeof(Server.Core.Entities.TestLevel).GetField("TestInterval.Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IntervalType);
			typeof(Server.Core.Entities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
		}

		// DbUserToUser.txt
			// hasDefaultConstructor
			// PropertyMapping: USERID -> UserId
			// PropertyMapping: UNAME -> UserName
		public Server.Core.Entities.User DirectPropertyMapping(DbEntities.QstUser source)
		{
			var target = new Server.Core.Entities.User();
			_assigner.Assign((value) => {target.UserId = value;}, source.USERID);
			_assigner.Assign((value) => {target.UserName = value;}, source.UNAME);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.QstUser source, Server.Core.Entities.User target)
		{
			_assigner.Assign((value) => {target.UserId = value;}, source.USERID);
			_assigner.Assign((value) => {target.UserName = value;}, source.UNAME);
		}

		public Server.Core.Entities.User ReflectedPropertyMapping(DbEntities.QstUser source)
		{
			var target = new Server.Core.Entities.User();
			typeof(Server.Core.Entities.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USERID);
			typeof(Server.Core.Entities.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNAME);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.QstUser source, Server.Core.Entities.User target)
		{
			typeof(Server.Core.Entities.User).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USERID);
			typeof(Server.Core.Entities.User).GetField("UserName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UNAME);
		}

		// DbWorkingCalendarEntryToWorkingCalendarEntry.txt
			// hasDefaultConstructor
			// PropertyMapping: FDDate -> Date
			// PropertyMapping: Name -> Description
			// PropertyMapping: Repeat -> Repetition
			// PropertyMapping: IsFree -> Type
		public Server.Core.Entities.WorkingCalendarEntry DirectPropertyMapping(DbEntities.WorkingCalendarEntry source)
		{
			var target = new Server.Core.Entities.WorkingCalendarEntry();
			_assigner.Assign((value) => {target.Date = value;}, source.FDDate);
			_assigner.Assign((value) => {target.Description = value;}, source.Name);
			_assigner.Assign((value) => {target.Repetition = value;}, source.Repeat);
			_assigner.Assign((value) => {target.Type = value;}, source.IsFree);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.WorkingCalendarEntry source, Server.Core.Entities.WorkingCalendarEntry target)
		{
			_assigner.Assign((value) => {target.Date = value;}, source.FDDate);
			_assigner.Assign((value) => {target.Description = value;}, source.Name);
			_assigner.Assign((value) => {target.Repetition = value;}, source.Repeat);
			_assigner.Assign((value) => {target.Type = value;}, source.IsFree);
		}

		public Server.Core.Entities.WorkingCalendarEntry ReflectedPropertyMapping(DbEntities.WorkingCalendarEntry source)
		{
			var target = new Server.Core.Entities.WorkingCalendarEntry();
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FDDate);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Repetition", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repeat);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsFree);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.WorkingCalendarEntry source, Server.Core.Entities.WorkingCalendarEntry target)
		{
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Date", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FDDate);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Repetition", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repeat);
			typeof(Server.Core.Entities.WorkingCalendarEntry).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsFree);
		}

		// DbWorkingCalendarToWorkingCalendar.txt
			// hasDefaultConstructor
			// PropertyMapping: SeqId -> Id
			// PropertyMapping: Name -> Name
			// PropertyMapping: FdSat -> AreSaturdaysFree
			// PropertyMapping: FdSun -> AreSundaysFree
		public Server.Core.Entities.WorkingCalendar DirectPropertyMapping(DbEntities.WorkingCalendar source)
		{
			var target = new Server.Core.Entities.WorkingCalendar();
			_assigner.Assign((value) => {target.Id = value;}, source.SeqId);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.AreSaturdaysFree = value;}, source.FdSat);
			_assigner.Assign((value) => {target.AreSundaysFree = value;}, source.FdSun);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.WorkingCalendar source, Server.Core.Entities.WorkingCalendar target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.SeqId);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.AreSaturdaysFree = value;}, source.FdSat);
			_assigner.Assign((value) => {target.AreSundaysFree = value;}, source.FdSun);
		}

		public Server.Core.Entities.WorkingCalendar ReflectedPropertyMapping(DbEntities.WorkingCalendar source)
		{
			var target = new Server.Core.Entities.WorkingCalendar();
			typeof(Server.Core.Entities.WorkingCalendar).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SeqId);
			typeof(Server.Core.Entities.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.WorkingCalendar).GetField("AreSaturdaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FdSat);
			typeof(Server.Core.Entities.WorkingCalendar).GetField("AreSundaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FdSun);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.WorkingCalendar source, Server.Core.Entities.WorkingCalendar target)
		{
			typeof(Server.Core.Entities.WorkingCalendar).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SeqId);
			typeof(Server.Core.Entities.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(Server.Core.Entities.WorkingCalendar).GetField("AreSaturdaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FdSat);
			typeof(Server.Core.Entities.WorkingCalendar).GetField("AreSundaysFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FdSun);
		}

		// ExtensionDBToExtension.txt
			// hasDefaultConstructor
			// PropertyMapping: TOOLID -> Id
			// PropertyMapping: TOOL -> Description
			// PropertyMapping: INVENTORY_NUMBER -> InventoryNumber
			// PropertyMapping: FAKTOR -> FactorTorque
			// PropertyMapping: TOOLLEN -> Length
			// PropertyMapping: BENDING -> Bending
			// PropertyMapping: ALIVE -> Alive
		public Server.Core.Entities.Extension DirectPropertyMapping(DbEntities.Extension source)
		{
			var target = new Server.Core.Entities.Extension();
			_assigner.Assign((value) => {target.Id = value;}, source.TOOLID);
			_assigner.Assign((value) => {target.Description = value;}, source.TOOL);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.INVENTORY_NUMBER);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FAKTOR);
			_assigner.Assign((value) => {target.Length = value;}, source.TOOLLEN);
			_assigner.Assign((value) => {target.Bending = value;}, source.BENDING);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.Extension source, Server.Core.Entities.Extension target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.TOOLID);
			_assigner.Assign((value) => {target.Description = value;}, source.TOOL);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.INVENTORY_NUMBER);
			_assigner.Assign((value) => {target.FactorTorque = value;}, source.FAKTOR);
			_assigner.Assign((value) => {target.Length = value;}, source.TOOLLEN);
			_assigner.Assign((value) => {target.Bending = value;}, source.BENDING);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
		}

		public Server.Core.Entities.Extension ReflectedPropertyMapping(DbEntities.Extension source)
		{
			var target = new Server.Core.Entities.Extension();
			typeof(Server.Core.Entities.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOOLID);
			typeof(Server.Core.Entities.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOOL);
			typeof(Server.Core.Entities.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.INVENTORY_NUMBER);
			typeof(Server.Core.Entities.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FAKTOR);
			typeof(Server.Core.Entities.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOOLLEN);
			typeof(Server.Core.Entities.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BENDING);
			typeof(Server.Core.Entities.Extension).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.Extension source, Server.Core.Entities.Extension target)
		{
			typeof(Server.Core.Entities.Extension).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOOLID);
			typeof(Server.Core.Entities.Extension).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOOL);
			typeof(Server.Core.Entities.Extension).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.INVENTORY_NUMBER);
			typeof(Server.Core.Entities.Extension).GetField("FactorTorque", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FAKTOR);
			typeof(Server.Core.Entities.Extension).GetField("Length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOOLLEN);
			typeof(Server.Core.Entities.Extension).GetField("Bending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BENDING);
			typeof(Server.Core.Entities.Extension).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
		}

		// ExtensionToExtensionDB.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> TOOLID
			// PropertyMapping: Description -> TOOL
			// PropertyMapping: InventoryNumber -> INVENTORY_NUMBER
			// PropertyMapping: FactorTorque -> FAKTOR
			// PropertyMapping: Length -> TOOLLEN
			// PropertyMapping: Bending -> BENDING
			// PropertyMapping: Alive -> ALIVE
		public DbEntities.Extension DirectPropertyMapping(Server.Core.Entities.Extension source)
		{
			var target = new DbEntities.Extension();
			_assigner.Assign((value) => {target.TOOLID = value;}, source.Id);
			_assigner.Assign((value) => {target.TOOL = value;}, source.Description);
			_assigner.Assign((value) => {target.INVENTORY_NUMBER = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.FAKTOR = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.TOOLLEN = value;}, source.Length);
			_assigner.Assign((value) => {target.BENDING = value;}, source.Bending);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Extension source, DbEntities.Extension target)
		{
			_assigner.Assign((value) => {target.TOOLID = value;}, source.Id);
			_assigner.Assign((value) => {target.TOOL = value;}, source.Description);
			_assigner.Assign((value) => {target.INVENTORY_NUMBER = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.FAKTOR = value;}, source.FactorTorque);
			_assigner.Assign((value) => {target.TOOLLEN = value;}, source.Length);
			_assigner.Assign((value) => {target.BENDING = value;}, source.Bending);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
		}

		public DbEntities.Extension ReflectedPropertyMapping(Server.Core.Entities.Extension source)
		{
			var target = new DbEntities.Extension();
			typeof(DbEntities.Extension).GetField("TOOLID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Extension).GetField("TOOL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.Extension).GetField("INVENTORY_NUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DbEntities.Extension).GetField("FAKTOR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(DbEntities.Extension).GetField("TOOLLEN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(DbEntities.Extension).GetField("BENDING", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(DbEntities.Extension).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Extension source, DbEntities.Extension target)
		{
			typeof(DbEntities.Extension).GetField("TOOLID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Extension).GetField("TOOL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.Extension).GetField("INVENTORY_NUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DbEntities.Extension).GetField("FAKTOR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FactorTorque);
			typeof(DbEntities.Extension).GetField("TOOLLEN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Length);
			typeof(DbEntities.Extension).GetField("BENDING", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Bending);
			typeof(DbEntities.Extension).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// HelperTableEntityToQstList.txt
			// hasDefaultConstructor
			// PropertyMapping: ListId -> LISTID
			// PropertyMapping: Value -> INFO
			// PropertyMapping: NodeId -> NODEID
			// PropertyMapping: Alive -> ALIVE
		public DbEntities.QstList DirectPropertyMapping(Server.Core.Entities.HelperTableEntity source)
		{
			var target = new DbEntities.QstList();
			_assigner.Assign((value) => {target.LISTID = value;}, source.ListId);
			_assigner.Assign((value) => {target.INFO = value;}, source.Value);
			_assigner.Assign((value) => {target.NODEID = value;}, source.NodeId);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.HelperTableEntity source, DbEntities.QstList target)
		{
			_assigner.Assign((value) => {target.LISTID = value;}, source.ListId);
			_assigner.Assign((value) => {target.INFO = value;}, source.Value);
			_assigner.Assign((value) => {target.NODEID = value;}, source.NodeId);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
		}

		public DbEntities.QstList ReflectedPropertyMapping(Server.Core.Entities.HelperTableEntity source)
		{
			var target = new DbEntities.QstList();
			typeof(DbEntities.QstList).GetField("LISTID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(DbEntities.QstList).GetField("INFO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DbEntities.QstList).GetField("NODEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			typeof(DbEntities.QstList).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.HelperTableEntity source, DbEntities.QstList target)
		{
			typeof(DbEntities.QstList).GetField("LISTID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ListId);
			typeof(DbEntities.QstList).GetField("INFO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DbEntities.QstList).GetField("NODEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			typeof(DbEntities.QstList).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// LocationDbToLocation.txt
			// hasDefaultConstructor
			// PropertyMapping: SEQID -> Id
			// PropertyMapping: USERID -> Number
			// PropertyMapping: NAME -> Description
			// PropertyMapping: TREEID -> ParentDirectoryId
			// PropertyMapping: CONTROL -> ControlledBy
			// PropertyMapping: TNOM -> SetPoint1
			// PropertyMapping: CLASSID -> ToleranceClass1
			// PropertyMapping: ToleranceClass1 -> ToleranceClass1
			// PropertyMapping: TMIN -> Minimum1
			// PropertyMapping: TMAX -> Maximum1
			// PropertyMapping: TSTART -> Threshold1
			// PropertyMapping: NOM2 -> SetPoint2
			// PropertyMapping: CLASSID2 -> ToleranceClass2
			// PropertyMapping: ToleranceClass2 -> ToleranceClass2
			// PropertyMapping: MIN2 -> Minimum2
			// PropertyMapping: MAX2 -> Maximum2
			// PropertyMapping: KOST -> ConfigurableField1
			// PropertyMapping: KATEG -> ConfigurableField2
			// PropertyMapping: DOKU -> ConfigurableField3
			// PropertyMapping: ALIVE -> Alive
		public Server.Core.Entities.Location DirectPropertyMapping(DbEntities.Location source)
		{
			var target = new Server.Core.Entities.Location();
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.Number = value;}, source.USERID);
			_assigner.Assign((value) => {target.Description = value;}, source.NAME);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.TREEID);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.CONTROL);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.TNOM);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.CLASSID);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.TMIN);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.TMAX);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.TSTART);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.NOM2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.CLASSID2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.MIN2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.MAX2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.KOST);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.KATEG);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.DOKU);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.Location source, Server.Core.Entities.Location target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.Number = value;}, source.USERID);
			_assigner.Assign((value) => {target.Description = value;}, source.NAME);
			_assigner.Assign((value) => {target.ParentDirectoryId = value;}, source.TREEID);
			_assigner.Assign((value) => {target.ControlledBy = value;}, source.CONTROL);
			_assigner.Assign((value) => {target.SetPoint1 = value;}, source.TNOM);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.CLASSID);
			_assigner.Assign((value) => {target.ToleranceClass1 = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.Minimum1 = value;}, source.TMIN);
			_assigner.Assign((value) => {target.Maximum1 = value;}, source.TMAX);
			_assigner.Assign((value) => {target.Threshold1 = value;}, source.TSTART);
			_assigner.Assign((value) => {target.SetPoint2 = value;}, source.NOM2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.CLASSID2);
			_assigner.Assign((value) => {target.ToleranceClass2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.Minimum2 = value;}, source.MIN2);
			_assigner.Assign((value) => {target.Maximum2 = value;}, source.MAX2);
			_assigner.Assign((value) => {target.ConfigurableField1 = value;}, source.KOST);
			_assigner.Assign((value) => {target.ConfigurableField2 = value;}, source.KATEG);
			_assigner.Assign((value) => {target.ConfigurableField3 = value;}, source.DOKU);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
		}

		public Server.Core.Entities.Location ReflectedPropertyMapping(DbEntities.Location source)
		{
			var target = new Server.Core.Entities.Location();
			typeof(Server.Core.Entities.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USERID);
			typeof(Server.Core.Entities.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TREEID);
			typeof(Server.Core.Entities.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROL);
			typeof(Server.Core.Entities.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TNOM);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(Server.Core.Entities.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TMIN);
			typeof(Server.Core.Entities.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TMAX);
			typeof(Server.Core.Entities.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TSTART);
			typeof(Server.Core.Entities.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOM2);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID2);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(Server.Core.Entities.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MIN2);
			typeof(Server.Core.Entities.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAX2);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KOST);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KATEG);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DOKU);
			typeof(Server.Core.Entities.Location).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.Location source, Server.Core.Entities.Location target)
		{
			typeof(Server.Core.Entities.Location).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.Location).GetField("Number", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USERID);
			typeof(Server.Core.Entities.Location).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.Location).GetField("ParentDirectoryId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TREEID);
			typeof(Server.Core.Entities.Location).GetField("ControlledBy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONTROL);
			typeof(Server.Core.Entities.Location).GetField("SetPoint1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TNOM);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(Server.Core.Entities.Location).GetField("Minimum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TMIN);
			typeof(Server.Core.Entities.Location).GetField("Maximum1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TMAX);
			typeof(Server.Core.Entities.Location).GetField("Threshold1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TSTART);
			typeof(Server.Core.Entities.Location).GetField("SetPoint2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NOM2);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID2);
			typeof(Server.Core.Entities.Location).GetField("ToleranceClass2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(Server.Core.Entities.Location).GetField("Minimum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MIN2);
			typeof(Server.Core.Entities.Location).GetField("Maximum2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MAX2);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KOST);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KATEG);
			typeof(Server.Core.Entities.Location).GetField("ConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DOKU);
			typeof(Server.Core.Entities.Location).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
		}

		// LocationDirectoryDbToLocationDirectory.txt
			// hasDefaultConstructor
			// PropertyMapping: LOCTREEID -> Id
			// PropertyMapping: NAME -> Name
			// PropertyMapping: PARENTID -> ParentId
			// PropertyMapping: STATUS -> Alive
		public Server.Core.Entities.LocationDirectory DirectPropertyMapping(DbEntities.LocationDirectory source)
		{
			var target = new Server.Core.Entities.LocationDirectory();
			_assigner.Assign((value) => {target.Id = value;}, source.LOCTREEID);
			_assigner.Assign((value) => {target.Name = value;}, source.NAME);
			_assigner.Assign((value) => {target.ParentId = value;}, source.PARENTID);
			_assigner.Assign((value) => {target.Alive = value;}, source.STATUS);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.LocationDirectory source, Server.Core.Entities.LocationDirectory target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.LOCTREEID);
			_assigner.Assign((value) => {target.Name = value;}, source.NAME);
			_assigner.Assign((value) => {target.ParentId = value;}, source.PARENTID);
			_assigner.Assign((value) => {target.Alive = value;}, source.STATUS);
		}

		public Server.Core.Entities.LocationDirectory ReflectedPropertyMapping(DbEntities.LocationDirectory source)
		{
			var target = new Server.Core.Entities.LocationDirectory();
			typeof(Server.Core.Entities.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREEID);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PARENTID);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATUS);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.LocationDirectory source, Server.Core.Entities.LocationDirectory target)
		{
			typeof(Server.Core.Entities.LocationDirectory).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LOCTREEID);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.LocationDirectory).GetField("ParentId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PARENTID);
			typeof(Server.Core.Entities.LocationDirectory).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATUS);
		}

		// LocationDirectoryToLocationDirectoryDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> LOCTREEID
			// PropertyMapping: Name -> NAME
			// PropertyMapping: ParentId -> PARENTID
			// PropertyMapping: Alive -> STATUS
		public DbEntities.LocationDirectory DirectPropertyMapping(Server.Core.Entities.LocationDirectory source)
		{
			var target = new DbEntities.LocationDirectory();
			_assigner.Assign((value) => {target.LOCTREEID = value;}, source.Id);
			_assigner.Assign((value) => {target.NAME = value;}, source.Name);
			_assigner.Assign((value) => {target.PARENTID = value;}, source.ParentId);
			_assigner.Assign((value) => {target.STATUS = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.LocationDirectory source, DbEntities.LocationDirectory target)
		{
			_assigner.Assign((value) => {target.LOCTREEID = value;}, source.Id);
			_assigner.Assign((value) => {target.NAME = value;}, source.Name);
			_assigner.Assign((value) => {target.PARENTID = value;}, source.ParentId);
			_assigner.Assign((value) => {target.STATUS = value;}, source.Alive);
		}

		public DbEntities.LocationDirectory ReflectedPropertyMapping(Server.Core.Entities.LocationDirectory source)
		{
			var target = new DbEntities.LocationDirectory();
			typeof(DbEntities.LocationDirectory).GetField("LOCTREEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.LocationDirectory).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.LocationDirectory).GetField("PARENTID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			typeof(DbEntities.LocationDirectory).GetField("STATUS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.LocationDirectory source, DbEntities.LocationDirectory target)
		{
			typeof(DbEntities.LocationDirectory).GetField("LOCTREEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.LocationDirectory).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.LocationDirectory).GetField("PARENTID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentId);
			typeof(DbEntities.LocationDirectory).GetField("STATUS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// LocationToLocationDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> SEQID
			// PropertyMapping: Number -> USERID
			// PropertyMapping: Description -> NAME
			// PropertyMapping: ParentDirectoryId -> TREEID
			// PropertyMapping: ControlledBy -> CONTROL
			// PropertyMapping: SetPoint1 -> TNOM
			// PropertyMapping: ToleranceClass1 -> CLASSID
			// PropertyMapping: Minimum1 -> TMIN
			// PropertyMapping: Maximum1 -> TMAX
			// PropertyMapping: Threshold1 -> TSTART
			// PropertyMapping: SetPoint2 -> NOM2
			// PropertyMapping: ToleranceClass2 -> CLASSID2
			// PropertyMapping: Minimum2 -> MIN2
			// PropertyMapping: Maximum2 -> MAX2
			// PropertyMapping: ConfigurableField1 -> KOST
			// PropertyMapping: ConfigurableField2 -> KATEG
			// PropertyMapping: ConfigurableField3 -> DOKU
			// PropertyMapping: Alive -> ALIVE
		public DbEntities.Location DirectPropertyMapping(Server.Core.Entities.Location source)
		{
			var target = new DbEntities.Location();
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.USERID = value;}, source.Number);
			_assigner.Assign((value) => {target.NAME = value;}, source.Description);
			_assigner.Assign((value) => {target.TREEID = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.CONTROL = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.TNOM = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.CLASSID = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.TMIN = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.TMAX = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.TSTART = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.NOM2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.CLASSID2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.MIN2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.MAX2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.KOST = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.KATEG = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.DOKU = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Location source, DbEntities.Location target)
		{
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.USERID = value;}, source.Number);
			_assigner.Assign((value) => {target.NAME = value;}, source.Description);
			_assigner.Assign((value) => {target.TREEID = value;}, source.ParentDirectoryId);
			_assigner.Assign((value) => {target.CONTROL = value;}, source.ControlledBy);
			_assigner.Assign((value) => {target.TNOM = value;}, source.SetPoint1);
			_assigner.Assign((value) => {target.CLASSID = value;}, source.ToleranceClass1);
			_assigner.Assign((value) => {target.TMIN = value;}, source.Minimum1);
			_assigner.Assign((value) => {target.TMAX = value;}, source.Maximum1);
			_assigner.Assign((value) => {target.TSTART = value;}, source.Threshold1);
			_assigner.Assign((value) => {target.NOM2 = value;}, source.SetPoint2);
			_assigner.Assign((value) => {target.CLASSID2 = value;}, source.ToleranceClass2);
			_assigner.Assign((value) => {target.MIN2 = value;}, source.Minimum2);
			_assigner.Assign((value) => {target.MAX2 = value;}, source.Maximum2);
			_assigner.Assign((value) => {target.KOST = value;}, source.ConfigurableField1);
			_assigner.Assign((value) => {target.KATEG = value;}, source.ConfigurableField2);
			_assigner.Assign((value) => {target.DOKU = value;}, source.ConfigurableField3);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
		}

		public DbEntities.Location ReflectedPropertyMapping(Server.Core.Entities.Location source)
		{
			var target = new DbEntities.Location();
			typeof(DbEntities.Location).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Location).GetField("USERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DbEntities.Location).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.Location).GetField("TREEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(DbEntities.Location).GetField("CONTROL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(DbEntities.Location).GetField("TNOM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(DbEntities.Location).GetField("CLASSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(DbEntities.Location).GetField("TMIN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(DbEntities.Location).GetField("TMAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(DbEntities.Location).GetField("TSTART", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(DbEntities.Location).GetField("NOM2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(DbEntities.Location).GetField("CLASSID2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(DbEntities.Location).GetField("MIN2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(DbEntities.Location).GetField("MAX2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(DbEntities.Location).GetField("KOST", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(DbEntities.Location).GetField("KATEG", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(DbEntities.Location).GetField("DOKU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(DbEntities.Location).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Location source, DbEntities.Location target)
		{
			typeof(DbEntities.Location).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Location).GetField("USERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Number);
			typeof(DbEntities.Location).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.Location).GetField("TREEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ParentDirectoryId);
			typeof(DbEntities.Location).GetField("CONTROL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ControlledBy);
			typeof(DbEntities.Location).GetField("TNOM", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint1);
			typeof(DbEntities.Location).GetField("CLASSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass1);
			typeof(DbEntities.Location).GetField("TMIN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum1);
			typeof(DbEntities.Location).GetField("TMAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum1);
			typeof(DbEntities.Location).GetField("TSTART", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Threshold1);
			typeof(DbEntities.Location).GetField("NOM2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SetPoint2);
			typeof(DbEntities.Location).GetField("CLASSID2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToleranceClass2);
			typeof(DbEntities.Location).GetField("MIN2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Minimum2);
			typeof(DbEntities.Location).GetField("MAX2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Maximum2);
			typeof(DbEntities.Location).GetField("KOST", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField1);
			typeof(DbEntities.Location).GetField("KATEG", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField2);
			typeof(DbEntities.Location).GetField("DOKU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField3);
			typeof(DbEntities.Location).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// LocationToolAssignmentToDbCondRot.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> LOCPOWID
			// PropertyMapping: TestLevelNumberMfu -> TestLevelNumberMfu
			// PropertyMapping: TestLevelNumberChk -> TestLevelNumberChk
			// PropertyMapping: EndOfLastTestPeriodMfu -> EndOfLastTestPeriodMfu
			// PropertyMapping: EndOfLastTestPeriodChk -> EndOfLastTestPeriodChk
			// PropertyMapping: EndOfLastTestPeriodShiftMfu -> EndOfLastTestPeriodShiftMfu
			// PropertyMapping: EndOfLastTestPeriodShiftChk -> EndOfLastTestPeriodShiftChk
			// PropertyMapping: NextTestDateMfu -> NEXT_MFU
			// PropertyMapping: NextTestDateChk -> NEXT_CHK
			// PropertyMapping: NextTestShiftMfu -> NextMfuShift
			// PropertyMapping: NextTestShiftChk -> NextChkShift
			// PropertyMapping: StartDateMfu -> TESTSTART_MFU
			// PropertyMapping: StartDateChk -> TESTSTART
			// PropertyMapping: TestOperationActiveMfu -> PLANOK_MFU
			// PropertyMapping: TestOperationActiveChk -> PLANOK
		public DbEntities.CondRot DirectPropertyMapping(Server.Core.Entities.LocationToolAssignment source)
		{
			var target = new DbEntities.CondRot();
			_assigner.Assign((value) => {target.LOCPOWID = value;}, source.Id);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodMfu = value;}, source.EndOfLastTestPeriodMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodChk = value;}, source.EndOfLastTestPeriodChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftMfu = value;}, source.EndOfLastTestPeriodShiftMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftChk = value;}, source.EndOfLastTestPeriodShiftChk);
			_assigner.Assign((value) => {target.NEXT_MFU = value;}, source.NextTestDateMfu);
			_assigner.Assign((value) => {target.NEXT_CHK = value;}, source.NextTestDateChk);
			_assigner.Assign((value) => {target.NextMfuShift = value;}, source.NextTestShiftMfu);
			_assigner.Assign((value) => {target.NextChkShift = value;}, source.NextTestShiftChk);
			_assigner.Assign((value) => {target.TESTSTART_MFU = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.TESTSTART = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.PLANOK_MFU = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.PLANOK = value;}, source.TestOperationActiveChk);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.LocationToolAssignment source, DbEntities.CondRot target)
		{
			_assigner.Assign((value) => {target.LOCPOWID = value;}, source.Id);
			_assigner.Assign((value) => {target.TestLevelNumberMfu = value;}, source.TestLevelNumberMfu);
			_assigner.Assign((value) => {target.TestLevelNumberChk = value;}, source.TestLevelNumberChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodMfu = value;}, source.EndOfLastTestPeriodMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodChk = value;}, source.EndOfLastTestPeriodChk);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftMfu = value;}, source.EndOfLastTestPeriodShiftMfu);
			_assigner.Assign((value) => {target.EndOfLastTestPeriodShiftChk = value;}, source.EndOfLastTestPeriodShiftChk);
			_assigner.Assign((value) => {target.NEXT_MFU = value;}, source.NextTestDateMfu);
			_assigner.Assign((value) => {target.NEXT_CHK = value;}, source.NextTestDateChk);
			_assigner.Assign((value) => {target.NextMfuShift = value;}, source.NextTestShiftMfu);
			_assigner.Assign((value) => {target.NextChkShift = value;}, source.NextTestShiftChk);
			_assigner.Assign((value) => {target.TESTSTART_MFU = value;}, source.StartDateMfu);
			_assigner.Assign((value) => {target.TESTSTART = value;}, source.StartDateChk);
			_assigner.Assign((value) => {target.PLANOK_MFU = value;}, source.TestOperationActiveMfu);
			_assigner.Assign((value) => {target.PLANOK = value;}, source.TestOperationActiveChk);
		}

		public DbEntities.CondRot ReflectedPropertyMapping(Server.Core.Entities.LocationToolAssignment source)
		{
			var target = new DbEntities.CondRot();
			typeof(DbEntities.CondRot).GetField("LOCPOWID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.CondRot).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(DbEntities.CondRot).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodMfu);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodChk);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftMfu);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftChk);
			typeof(DbEntities.CondRot).GetField("NEXT_MFU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(DbEntities.CondRot).GetField("NEXT_CHK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(DbEntities.CondRot).GetField("NextMfuShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(DbEntities.CondRot).GetField("NextChkShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(DbEntities.CondRot).GetField("TESTSTART_MFU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(DbEntities.CondRot).GetField("TESTSTART", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(DbEntities.CondRot).GetField("PLANOK_MFU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(DbEntities.CondRot).GetField("PLANOK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.LocationToolAssignment source, DbEntities.CondRot target)
		{
			typeof(DbEntities.CondRot).GetField("LOCPOWID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.CondRot).GetField("TestLevelNumberMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberMfu);
			typeof(DbEntities.CondRot).GetField("TestLevelNumberChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumberChk);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodMfu);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodChk);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodShiftMfu", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftMfu);
			typeof(DbEntities.CondRot).GetField("EndOfLastTestPeriodShiftChk", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShiftChk);
			typeof(DbEntities.CondRot).GetField("NEXT_MFU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateMfu);
			typeof(DbEntities.CondRot).GetField("NEXT_CHK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDateChk);
			typeof(DbEntities.CondRot).GetField("NextMfuShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftMfu);
			typeof(DbEntities.CondRot).GetField("NextChkShift", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShiftChk);
			typeof(DbEntities.CondRot).GetField("TESTSTART_MFU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateMfu);
			typeof(DbEntities.CondRot).GetField("TESTSTART", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDateChk);
			typeof(DbEntities.CondRot).GetField("PLANOK_MFU", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveMfu);
			typeof(DbEntities.CondRot).GetField("PLANOK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActiveChk);
		}

		// ManufacturerDbToManufacturer.txt
			// hasDefaultConstructor
			// PropertyMapping: MANUID -> Id
			// PropertyMapping: NAME -> Name
			// PropertyMapping: STREET -> Street
			// PropertyMapping: CITY -> City
			// PropertyMapping: TEL -> PhoneNumber
			// PropertyMapping: FAX -> Fax
			// PropertyMapping: PERSON -> Person
			// PropertyMapping: COUNTRY -> Country
			// PropertyMapping: ZIPCODESTR -> Plz
			// PropertyMapping: HOUSENUMBERSTR -> HouseNumber
		public Server.Core.Entities.Manufacturer DirectPropertyMapping(DbEntities.Manufacturer source)
		{
			var target = new Server.Core.Entities.Manufacturer();
			_assigner.Assign((value) => {target.Id = value;}, source.MANUID);
			_assigner.Assign((value) => {target.Name = value;}, source.NAME);
			_assigner.Assign((value) => {target.Street = value;}, source.STREET);
			_assigner.Assign((value) => {target.City = value;}, source.CITY);
			_assigner.Assign((value) => {target.PhoneNumber = value;}, source.TEL);
			_assigner.Assign((value) => {target.Fax = value;}, source.FAX);
			_assigner.Assign((value) => {target.Person = value;}, source.PERSON);
			_assigner.Assign((value) => {target.Country = value;}, source.COUNTRY);
			_assigner.Assign((value) => {target.Plz = value;}, source.ZIPCODESTR);
			_assigner.Assign((value) => {target.HouseNumber = value;}, source.HOUSENUMBERSTR);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.Manufacturer source, Server.Core.Entities.Manufacturer target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.MANUID);
			_assigner.Assign((value) => {target.Name = value;}, source.NAME);
			_assigner.Assign((value) => {target.Street = value;}, source.STREET);
			_assigner.Assign((value) => {target.City = value;}, source.CITY);
			_assigner.Assign((value) => {target.PhoneNumber = value;}, source.TEL);
			_assigner.Assign((value) => {target.Fax = value;}, source.FAX);
			_assigner.Assign((value) => {target.Person = value;}, source.PERSON);
			_assigner.Assign((value) => {target.Country = value;}, source.COUNTRY);
			_assigner.Assign((value) => {target.Plz = value;}, source.ZIPCODESTR);
			_assigner.Assign((value) => {target.HouseNumber = value;}, source.HOUSENUMBERSTR);
		}

		public Server.Core.Entities.Manufacturer ReflectedPropertyMapping(DbEntities.Manufacturer source)
		{
			var target = new Server.Core.Entities.Manufacturer();
			typeof(Server.Core.Entities.Manufacturer).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MANUID);
			typeof(Server.Core.Entities.Manufacturer).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STREET);
			typeof(Server.Core.Entities.Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CITY);
			typeof(Server.Core.Entities.Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TEL);
			typeof(Server.Core.Entities.Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FAX);
			typeof(Server.Core.Entities.Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PERSON);
			typeof(Server.Core.Entities.Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.COUNTRY);
			typeof(Server.Core.Entities.Manufacturer).GetField("Plz", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ZIPCODESTR);
			typeof(Server.Core.Entities.Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HOUSENUMBERSTR);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.Manufacturer source, Server.Core.Entities.Manufacturer target)
		{
			typeof(Server.Core.Entities.Manufacturer).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MANUID);
			typeof(Server.Core.Entities.Manufacturer).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.Manufacturer).GetField("Street", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STREET);
			typeof(Server.Core.Entities.Manufacturer).GetField("City", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CITY);
			typeof(Server.Core.Entities.Manufacturer).GetField("PhoneNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TEL);
			typeof(Server.Core.Entities.Manufacturer).GetField("Fax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FAX);
			typeof(Server.Core.Entities.Manufacturer).GetField("Person", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PERSON);
			typeof(Server.Core.Entities.Manufacturer).GetField("Country", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.COUNTRY);
			typeof(Server.Core.Entities.Manufacturer).GetField("Plz", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ZIPCODESTR);
			typeof(Server.Core.Entities.Manufacturer).GetField("HouseNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HOUSENUMBERSTR);
		}

		// ManufacturerToManufacturerDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> MANUID
			// PropertyMapping: Name -> NAME
			// PropertyMapping: Street -> STREET
			// PropertyMapping: City -> CITY
			// PropertyMapping: PhoneNumber -> TEL
			// PropertyMapping: Fax -> FAX
			// PropertyMapping: Person -> PERSON
			// PropertyMapping: Country -> COUNTRY
			// PropertyMapping: Plz -> ZIPCODESTR
			// PropertyMapping: HouseNumber -> HOUSENUMBERSTR
		public DbEntities.Manufacturer DirectPropertyMapping(Server.Core.Entities.Manufacturer source)
		{
			var target = new DbEntities.Manufacturer();
			_assigner.Assign((value) => {target.MANUID = value;}, source.Id);
			_assigner.Assign((value) => {target.NAME = value;}, source.Name);
			_assigner.Assign((value) => {target.STREET = value;}, source.Street);
			_assigner.Assign((value) => {target.CITY = value;}, source.City);
			_assigner.Assign((value) => {target.TEL = value;}, source.PhoneNumber);
			_assigner.Assign((value) => {target.FAX = value;}, source.Fax);
			_assigner.Assign((value) => {target.PERSON = value;}, source.Person);
			_assigner.Assign((value) => {target.COUNTRY = value;}, source.Country);
			_assigner.Assign((value) => {target.ZIPCODESTR = value;}, source.Plz);
			_assigner.Assign((value) => {target.HOUSENUMBERSTR = value;}, source.HouseNumber);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Manufacturer source, DbEntities.Manufacturer target)
		{
			_assigner.Assign((value) => {target.MANUID = value;}, source.Id);
			_assigner.Assign((value) => {target.NAME = value;}, source.Name);
			_assigner.Assign((value) => {target.STREET = value;}, source.Street);
			_assigner.Assign((value) => {target.CITY = value;}, source.City);
			_assigner.Assign((value) => {target.TEL = value;}, source.PhoneNumber);
			_assigner.Assign((value) => {target.FAX = value;}, source.Fax);
			_assigner.Assign((value) => {target.PERSON = value;}, source.Person);
			_assigner.Assign((value) => {target.COUNTRY = value;}, source.Country);
			_assigner.Assign((value) => {target.ZIPCODESTR = value;}, source.Plz);
			_assigner.Assign((value) => {target.HOUSENUMBERSTR = value;}, source.HouseNumber);
		}

		public DbEntities.Manufacturer ReflectedPropertyMapping(Server.Core.Entities.Manufacturer source)
		{
			var target = new DbEntities.Manufacturer();
			typeof(DbEntities.Manufacturer).GetField("MANUID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Manufacturer).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.Manufacturer).GetField("STREET", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(DbEntities.Manufacturer).GetField("CITY", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(DbEntities.Manufacturer).GetField("TEL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(DbEntities.Manufacturer).GetField("FAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(DbEntities.Manufacturer).GetField("PERSON", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(DbEntities.Manufacturer).GetField("COUNTRY", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(DbEntities.Manufacturer).GetField("ZIPCODESTR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Plz);
			typeof(DbEntities.Manufacturer).GetField("HOUSENUMBERSTR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Manufacturer source, DbEntities.Manufacturer target)
		{
			typeof(DbEntities.Manufacturer).GetField("MANUID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Manufacturer).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.Manufacturer).GetField("STREET", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Street);
			typeof(DbEntities.Manufacturer).GetField("CITY", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.City);
			typeof(DbEntities.Manufacturer).GetField("TEL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PhoneNumber);
			typeof(DbEntities.Manufacturer).GetField("FAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Fax);
			typeof(DbEntities.Manufacturer).GetField("PERSON", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Person);
			typeof(DbEntities.Manufacturer).GetField("COUNTRY", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Country);
			typeof(DbEntities.Manufacturer).GetField("ZIPCODESTR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Plz);
			typeof(DbEntities.Manufacturer).GetField("HOUSENUMBERSTR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.HouseNumber);
		}

		// PictureDbToPicture.txt
			// hasDefaultConstructor
			// PropertyMapping: SEQID -> SeqId
			// PropertyMapping: NODEID -> NodeId
			// PropertyMapping: NODESEQID -> NodeSeqId
			// PropertyMapping: PICT -> FileName
			// PropertyMapping: PICTURE -> PictureBytes
			// PropertyMapping: FILETYPE -> FileType
		public Server.Core.Entities.Picture DirectPropertyMapping(DbEntities.Picture source)
		{
			var target = new Server.Core.Entities.Picture();
			_assigner.Assign((value) => {target.SeqId = value;}, source.SEQID);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NODEID);
			_assigner.Assign((value) => {target.NodeSeqId = value;}, source.NODESEQID);
			_assigner.Assign((value) => {target.FileName = value;}, source.PICT);
			_assigner.Assign((value) => {target.PictureBytes = value;}, source.PICTURE);
			_assigner.Assign((value) => {target.FileType = value;}, source.FILETYPE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.Picture source, Server.Core.Entities.Picture target)
		{
			_assigner.Assign((value) => {target.SeqId = value;}, source.SEQID);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NODEID);
			_assigner.Assign((value) => {target.NodeSeqId = value;}, source.NODESEQID);
			_assigner.Assign((value) => {target.FileName = value;}, source.PICT);
			_assigner.Assign((value) => {target.PictureBytes = value;}, source.PICTURE);
			_assigner.Assign((value) => {target.FileType = value;}, source.FILETYPE);
		}

		public Server.Core.Entities.Picture ReflectedPropertyMapping(DbEntities.Picture source)
		{
			var target = new Server.Core.Entities.Picture();
			typeof(Server.Core.Entities.Picture).GetField("SeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.Picture).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NODEID);
			typeof(Server.Core.Entities.Picture).GetField("NodeSeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NODESEQID);
			typeof(Server.Core.Entities.Picture).GetField("FileName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PICT);
			typeof(Server.Core.Entities.Picture).GetField("PictureBytes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PICTURE);
			typeof(Server.Core.Entities.Picture).GetField("FileType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FILETYPE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.Picture source, Server.Core.Entities.Picture target)
		{
			typeof(Server.Core.Entities.Picture).GetField("SeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.Picture).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NODEID);
			typeof(Server.Core.Entities.Picture).GetField("NodeSeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NODESEQID);
			typeof(Server.Core.Entities.Picture).GetField("FileName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PICT);
			typeof(Server.Core.Entities.Picture).GetField("PictureBytes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PICTURE);
			typeof(Server.Core.Entities.Picture).GetField("FileType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FILETYPE);
		}

		// ProcessControlConditionToCondLocDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> SEQID
			// PropertyMapping: Location.Id -> LOCID
			// PropertyMapping: UpperMeasuringLimit -> MDMAX
			// PropertyMapping: LowerMeasuringLimit -> MDMIN
			// PropertyMapping: UpperInterventionLimit -> OEGN
			// PropertyMapping: LowerInterventionLimit -> UEGN
			// PropertyMapping: TestLevelNumber -> TESTLEVELNUMBER
			// PropertyMapping: TestLevelSet -> TESTLEVELSETID
			// PropertyMapping: TestOperationActive -> PLANOK
			// PropertyMapping: StartDate -> TESTSTART
			// PropertyMapping: Alive -> ALIVE
			// PropertyMapping: EndOfLastTestPeriod -> ENDOFLASTTESTPERIOD
			// PropertyMapping: EndOfLastTestPeriodShift -> ENDOFLASTTESTPERIODSHIFT
			// PropertyMapping: NextTestDate -> NEXT_CTL
			// PropertyMapping: NextTestShift -> NEXTSHIFT
		public DbEntities.CondLoc DirectPropertyMapping(Server.Core.Entities.ProcessControlCondition source)
		{
			var target = new DbEntities.CondLoc();
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.LOCID = value;}, source.Location.Id);
			_assigner.Assign((value) => {target.MDMAX = value;}, source.UpperMeasuringLimit);
			_assigner.Assign((value) => {target.MDMIN = value;}, source.LowerMeasuringLimit);
			_assigner.Assign((value) => {target.OEGN = value;}, source.UpperInterventionLimit);
			_assigner.Assign((value) => {target.UEGN = value;}, source.LowerInterventionLimit);
			_assigner.Assign((value) => {target.TESTLEVELNUMBER = value;}, source.TestLevelNumber);
			_assigner.Assign((value) => {target.TESTLEVELSETID = value;}, source.TestLevelSet);
			_assigner.Assign((value) => {target.PLANOK = value;}, source.TestOperationActive);
			_assigner.Assign((value) => {target.TESTSTART = value;}, source.StartDate);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.ENDOFLASTTESTPERIOD = value;}, source.EndOfLastTestPeriod);
			_assigner.Assign((value) => {target.ENDOFLASTTESTPERIODSHIFT = value;}, source.EndOfLastTestPeriodShift);
			_assigner.Assign((value) => {target.NEXT_CTL = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NEXTSHIFT = value;}, source.NextTestShift);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ProcessControlCondition source, DbEntities.CondLoc target)
		{
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.LOCID = value;}, source.Location.Id);
			_assigner.Assign((value) => {target.MDMAX = value;}, source.UpperMeasuringLimit);
			_assigner.Assign((value) => {target.MDMIN = value;}, source.LowerMeasuringLimit);
			_assigner.Assign((value) => {target.OEGN = value;}, source.UpperInterventionLimit);
			_assigner.Assign((value) => {target.UEGN = value;}, source.LowerInterventionLimit);
			_assigner.Assign((value) => {target.TESTLEVELNUMBER = value;}, source.TestLevelNumber);
			_assigner.Assign((value) => {target.TESTLEVELSETID = value;}, source.TestLevelSet);
			_assigner.Assign((value) => {target.PLANOK = value;}, source.TestOperationActive);
			_assigner.Assign((value) => {target.TESTSTART = value;}, source.StartDate);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.ENDOFLASTTESTPERIOD = value;}, source.EndOfLastTestPeriod);
			_assigner.Assign((value) => {target.ENDOFLASTTESTPERIODSHIFT = value;}, source.EndOfLastTestPeriodShift);
			_assigner.Assign((value) => {target.NEXT_CTL = value;}, source.NextTestDate);
			_assigner.Assign((value) => {target.NEXTSHIFT = value;}, source.NextTestShift);
		}

		public DbEntities.CondLoc ReflectedPropertyMapping(Server.Core.Entities.ProcessControlCondition source)
		{
			var target = new DbEntities.CondLoc();
			typeof(DbEntities.CondLoc).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.CondLoc).GetField("LOCID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location.Id);
			typeof(DbEntities.CondLoc).GetField("MDMAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(DbEntities.CondLoc).GetField("MDMIN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(DbEntities.CondLoc).GetField("OEGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(DbEntities.CondLoc).GetField("UEGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(DbEntities.CondLoc).GetField("TESTLEVELNUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(DbEntities.CondLoc).GetField("TESTLEVELSETID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(DbEntities.CondLoc).GetField("PLANOK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(DbEntities.CondLoc).GetField("TESTSTART", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(DbEntities.CondLoc).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.CondLoc).GetField("ENDOFLASTTESTPERIOD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriod);
			typeof(DbEntities.CondLoc).GetField("ENDOFLASTTESTPERIODSHIFT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShift);
			typeof(DbEntities.CondLoc).GetField("NEXT_CTL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(DbEntities.CondLoc).GetField("NEXTSHIFT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ProcessControlCondition source, DbEntities.CondLoc target)
		{
			typeof(DbEntities.CondLoc).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.CondLoc).GetField("LOCID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Location.Id);
			typeof(DbEntities.CondLoc).GetField("MDMAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperMeasuringLimit);
			typeof(DbEntities.CondLoc).GetField("MDMIN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerMeasuringLimit);
			typeof(DbEntities.CondLoc).GetField("OEGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperInterventionLimit);
			typeof(DbEntities.CondLoc).GetField("UEGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerInterventionLimit);
			typeof(DbEntities.CondLoc).GetField("TESTLEVELNUMBER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelNumber);
			typeof(DbEntities.CondLoc).GetField("TESTLEVELSETID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestLevelSet);
			typeof(DbEntities.CondLoc).GetField("PLANOK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestOperationActive);
			typeof(DbEntities.CondLoc).GetField("TESTSTART", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartDate);
			typeof(DbEntities.CondLoc).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.CondLoc).GetField("ENDOFLASTTESTPERIOD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriod);
			typeof(DbEntities.CondLoc).GetField("ENDOFLASTTESTPERIODSHIFT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.EndOfLastTestPeriodShift);
			typeof(DbEntities.CondLoc).GetField("NEXT_CTL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestDate);
			typeof(DbEntities.CondLoc).GetField("NEXTSHIFT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NextTestShift);
		}

		// QstCommentToCommentDb.txt
			// hasDefaultConstructor
			// PropertyMapping: NodeId -> NODEID
			// PropertyMapping: Comment -> INFO
			// PropertyMapping: UserId -> USERID
			// PropertyMapping: NodeSeqid -> NODESEQID
		public DbEntities.QstComment DirectPropertyMapping(Server.UseCases.UseCases.QstComment source)
		{
			var target = new DbEntities.QstComment();
			_assigner.Assign((value) => {target.NODEID = value;}, source.NodeId);
			_assigner.Assign((value) => {target.INFO = value;}, source.Comment);
			_assigner.Assign((value) => {target.USERID = value;}, source.UserId);
			_assigner.Assign((value) => {target.NODESEQID = value;}, source.NodeSeqid);
			return target;
		}

		public void DirectPropertyMapping(Server.UseCases.UseCases.QstComment source, DbEntities.QstComment target)
		{
			_assigner.Assign((value) => {target.NODEID = value;}, source.NodeId);
			_assigner.Assign((value) => {target.INFO = value;}, source.Comment);
			_assigner.Assign((value) => {target.USERID = value;}, source.UserId);
			_assigner.Assign((value) => {target.NODESEQID = value;}, source.NodeSeqid);
		}

		public DbEntities.QstComment ReflectedPropertyMapping(Server.UseCases.UseCases.QstComment source)
		{
			var target = new DbEntities.QstComment();
			typeof(DbEntities.QstComment).GetField("NODEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			typeof(DbEntities.QstComment).GetField("INFO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DbEntities.QstComment).GetField("USERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(DbEntities.QstComment).GetField("NODESEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeSeqid);
			return target;
		}

		public void ReflectedPropertyMapping(Server.UseCases.UseCases.QstComment source, DbEntities.QstComment target)
		{
			typeof(DbEntities.QstComment).GetField("NODEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeId);
			typeof(DbEntities.QstComment).GetField("INFO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Comment);
			typeof(DbEntities.QstComment).GetField("USERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			typeof(DbEntities.QstComment).GetField("NODESEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NodeSeqid);
		}

		// QstListToHelperTableEntity.txt
			// hasDefaultConstructor
			// PropertyMapping: LISTID -> ListId
			// PropertyMapping: INFO -> Value
			// PropertyMapping: NODEID -> NodeId
			// PropertyMapping: ALIVE -> Alive
		public Server.Core.Entities.HelperTableEntity DirectPropertyMapping(DbEntities.QstList source)
		{
			var target = new Server.Core.Entities.HelperTableEntity();
			_assigner.Assign((value) => {target.ListId = value;}, source.LISTID);
			_assigner.Assign((value) => {target.Value = value;}, source.INFO);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NODEID);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.QstList source, Server.Core.Entities.HelperTableEntity target)
		{
			_assigner.Assign((value) => {target.ListId = value;}, source.LISTID);
			_assigner.Assign((value) => {target.Value = value;}, source.INFO);
			_assigner.Assign((value) => {target.NodeId = value;}, source.NODEID);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
		}

		public Server.Core.Entities.HelperTableEntity ReflectedPropertyMapping(DbEntities.QstList source)
		{
			var target = new Server.Core.Entities.HelperTableEntity();
			typeof(Server.Core.Entities.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LISTID);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.INFO);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NODEID);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.QstList source, Server.Core.Entities.HelperTableEntity target)
		{
			typeof(Server.Core.Entities.HelperTableEntity).GetField("ListId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LISTID);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.INFO);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("NodeId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NODEID);
			typeof(Server.Core.Entities.HelperTableEntity).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
		}

		// QstProcessControlTechToCondLocTechDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> SEQID
			// PropertyMapping: ProcessControlConditionId -> CONDLOCID
			// PropertyMapping: ManufacturerId -> HERSTELLERID
			// PropertyMapping: TestMethod -> METHODE
			// PropertyMapping: Extension -> EXTENSIONID
			// PropertyMapping: Alive -> ALIVE
			// PropertyMapping: AngleLimitMt -> I0
			// PropertyMapping: StartMeasurementPeak -> F0
			// PropertyMapping: StartAngleCountingPa -> F1
			// PropertyMapping: AngleForFurtherTurningPa -> F2
			// PropertyMapping: TargetAnglePa -> F3
			// PropertyMapping: StartMeasurementPa -> F4
			// PropertyMapping: AlarmTorquePa -> F5
			// PropertyMapping: AlarmAnglePa -> F6
			// PropertyMapping: MinimumTorqueMt -> F7
			// PropertyMapping: StartAngleMt -> F8
			// PropertyMapping: StartMeasurementMt -> F9
			// PropertyMapping: AlarmTorqueMt -> F10
			// PropertyMapping: AlarmAngleMt -> F11
		public DbEntities.CondLocTech DirectPropertyMapping(Server.Core.Entities.QstProcessControlTech source)
		{
			var target = new DbEntities.CondLocTech();
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.CONDLOCID = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.HERSTELLERID = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.METHODE = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.EXTENSIONID = value;}, source.Extension);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.I0 = value;}, source.AngleLimitMt);
			_assigner.Assign((value) => {target.F0 = value;}, source.StartMeasurementPeak);
			_assigner.Assign((value) => {target.F1 = value;}, source.StartAngleCountingPa);
			_assigner.Assign((value) => {target.F2 = value;}, source.AngleForFurtherTurningPa);
			_assigner.Assign((value) => {target.F3 = value;}, source.TargetAnglePa);
			_assigner.Assign((value) => {target.F4 = value;}, source.StartMeasurementPa);
			_assigner.Assign((value) => {target.F5 = value;}, source.AlarmTorquePa);
			_assigner.Assign((value) => {target.F6 = value;}, source.AlarmAnglePa);
			_assigner.Assign((value) => {target.F7 = value;}, source.MinimumTorqueMt);
			_assigner.Assign((value) => {target.F8 = value;}, source.StartAngleMt);
			_assigner.Assign((value) => {target.F9 = value;}, source.StartMeasurementMt);
			_assigner.Assign((value) => {target.F10 = value;}, source.AlarmTorqueMt);
			_assigner.Assign((value) => {target.F11 = value;}, source.AlarmAngleMt);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.QstProcessControlTech source, DbEntities.CondLocTech target)
		{
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.CONDLOCID = value;}, source.ProcessControlConditionId);
			_assigner.Assign((value) => {target.HERSTELLERID = value;}, source.ManufacturerId);
			_assigner.Assign((value) => {target.METHODE = value;}, source.TestMethod);
			_assigner.Assign((value) => {target.EXTENSIONID = value;}, source.Extension);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.I0 = value;}, source.AngleLimitMt);
			_assigner.Assign((value) => {target.F0 = value;}, source.StartMeasurementPeak);
			_assigner.Assign((value) => {target.F1 = value;}, source.StartAngleCountingPa);
			_assigner.Assign((value) => {target.F2 = value;}, source.AngleForFurtherTurningPa);
			_assigner.Assign((value) => {target.F3 = value;}, source.TargetAnglePa);
			_assigner.Assign((value) => {target.F4 = value;}, source.StartMeasurementPa);
			_assigner.Assign((value) => {target.F5 = value;}, source.AlarmTorquePa);
			_assigner.Assign((value) => {target.F6 = value;}, source.AlarmAnglePa);
			_assigner.Assign((value) => {target.F7 = value;}, source.MinimumTorqueMt);
			_assigner.Assign((value) => {target.F8 = value;}, source.StartAngleMt);
			_assigner.Assign((value) => {target.F9 = value;}, source.StartMeasurementMt);
			_assigner.Assign((value) => {target.F10 = value;}, source.AlarmTorqueMt);
			_assigner.Assign((value) => {target.F11 = value;}, source.AlarmAngleMt);
		}

		public DbEntities.CondLocTech ReflectedPropertyMapping(Server.Core.Entities.QstProcessControlTech source)
		{
			var target = new DbEntities.CondLocTech();
			typeof(DbEntities.CondLocTech).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.CondLocTech).GetField("CONDLOCID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(DbEntities.CondLocTech).GetField("HERSTELLERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(DbEntities.CondLocTech).GetField("METHODE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(DbEntities.CondLocTech).GetField("EXTENSIONID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
			typeof(DbEntities.CondLocTech).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.CondLocTech).GetField("I0", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleLimitMt);
			typeof(DbEntities.CondLocTech).GetField("F0", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPeak);
			typeof(DbEntities.CondLocTech).GetField("F1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleCountingPa);
			typeof(DbEntities.CondLocTech).GetField("F2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleForFurtherTurningPa);
			typeof(DbEntities.CondLocTech).GetField("F3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TargetAnglePa);
			typeof(DbEntities.CondLocTech).GetField("F4", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPa);
			typeof(DbEntities.CondLocTech).GetField("F5", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorquePa);
			typeof(DbEntities.CondLocTech).GetField("F6", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAnglePa);
			typeof(DbEntities.CondLocTech).GetField("F7", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorqueMt);
			typeof(DbEntities.CondLocTech).GetField("F8", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleMt);
			typeof(DbEntities.CondLocTech).GetField("F9", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementMt);
			typeof(DbEntities.CondLocTech).GetField("F10", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorqueMt);
			typeof(DbEntities.CondLocTech).GetField("F11", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAngleMt);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.QstProcessControlTech source, DbEntities.CondLocTech target)
		{
			typeof(DbEntities.CondLocTech).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.CondLocTech).GetField("CONDLOCID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ProcessControlConditionId);
			typeof(DbEntities.CondLocTech).GetField("HERSTELLERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ManufacturerId);
			typeof(DbEntities.CondLocTech).GetField("METHODE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestMethod);
			typeof(DbEntities.CondLocTech).GetField("EXTENSIONID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Extension);
			typeof(DbEntities.CondLocTech).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.CondLocTech).GetField("I0", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleLimitMt);
			typeof(DbEntities.CondLocTech).GetField("F0", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPeak);
			typeof(DbEntities.CondLocTech).GetField("F1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleCountingPa);
			typeof(DbEntities.CondLocTech).GetField("F2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AngleForFurtherTurningPa);
			typeof(DbEntities.CondLocTech).GetField("F3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TargetAnglePa);
			typeof(DbEntities.CondLocTech).GetField("F4", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementPa);
			typeof(DbEntities.CondLocTech).GetField("F5", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorquePa);
			typeof(DbEntities.CondLocTech).GetField("F6", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAnglePa);
			typeof(DbEntities.CondLocTech).GetField("F7", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinimumTorqueMt);
			typeof(DbEntities.CondLocTech).GetField("F8", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartAngleMt);
			typeof(DbEntities.CondLocTech).GetField("F9", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StartMeasurementMt);
			typeof(DbEntities.CondLocTech).GetField("F10", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmTorqueMt);
			typeof(DbEntities.CondLocTech).GetField("F11", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AlarmAngleMt);
		}

		// QstSetupDbToQstSetup.txt
			// hasDefaultConstructor
			// PropertyMapping: LID -> Id
			// PropertyMapping: SName -> Name
			// PropertyMapping: SText -> Value
			// PropertyMapping: LUserId -> UserId
		public Server.Core.Entities.QstSetup DirectPropertyMapping(DbEntities.QstSetup source)
		{
			var target = new Server.Core.Entities.QstSetup();
			_assigner.Assign((value) => {target.Id = value;}, source.LID);
			_assigner.Assign((value) => {target.Name = value;}, source.SName);
			_assigner.Assign((value) => {target.Value = value;}, source.SText);
			_assigner.Assign((value) => {target.UserId = value;}, source.LUserId);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.QstSetup source, Server.Core.Entities.QstSetup target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.LID);
			_assigner.Assign((value) => {target.Name = value;}, source.SName);
			_assigner.Assign((value) => {target.Value = value;}, source.SText);
			_assigner.Assign((value) => {target.UserId = value;}, source.LUserId);
		}

		public Server.Core.Entities.QstSetup ReflectedPropertyMapping(DbEntities.QstSetup source)
		{
			var target = new Server.Core.Entities.QstSetup();
			typeof(Server.Core.Entities.QstSetup).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LID);
			typeof(Server.Core.Entities.QstSetup).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SName);
			typeof(Server.Core.Entities.QstSetup).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SText);
			typeof(Server.Core.Entities.QstSetup).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LUserId);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.QstSetup source, Server.Core.Entities.QstSetup target)
		{
			typeof(Server.Core.Entities.QstSetup).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LID);
			typeof(Server.Core.Entities.QstSetup).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SName);
			typeof(Server.Core.Entities.QstSetup).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SText);
			typeof(Server.Core.Entities.QstSetup).GetField("UserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LUserId);
		}

		// QstSetupToQstSetupDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> LID
			// PropertyMapping: Name -> SName
			// PropertyMapping: Value -> SText
			// PropertyMapping: UserId -> LUserId
		public DbEntities.QstSetup DirectPropertyMapping(Server.Core.Entities.QstSetup source)
		{
			var target = new DbEntities.QstSetup();
			_assigner.Assign((value) => {target.LID = value;}, source.Id);
			_assigner.Assign((value) => {target.SName = value;}, source.Name);
			_assigner.Assign((value) => {target.SText = value;}, source.Value);
			_assigner.Assign((value) => {target.LUserId = value;}, source.UserId);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.QstSetup source, DbEntities.QstSetup target)
		{
			_assigner.Assign((value) => {target.LID = value;}, source.Id);
			_assigner.Assign((value) => {target.SName = value;}, source.Name);
			_assigner.Assign((value) => {target.SText = value;}, source.Value);
			_assigner.Assign((value) => {target.LUserId = value;}, source.UserId);
		}

		public DbEntities.QstSetup ReflectedPropertyMapping(Server.Core.Entities.QstSetup source)
		{
			var target = new DbEntities.QstSetup();
			typeof(DbEntities.QstSetup).GetField("LID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.QstSetup).GetField("SName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.QstSetup).GetField("SText", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DbEntities.QstSetup).GetField("LUserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.QstSetup source, DbEntities.QstSetup target)
		{
			typeof(DbEntities.QstSetup).GetField("LID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.QstSetup).GetField("SName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.QstSetup).GetField("SText", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DbEntities.QstSetup).GetField("LUserId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UserId);
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

		// StatusDbToStatus.txt
			// hasDefaultConstructor
			// PropertyMapping: STATEID -> Id
			// PropertyMapping: STATE -> Value
			// PropertyMapping: ALIVE -> Alive
		public Server.Core.Entities.Status DirectPropertyMapping(DbEntities.Status source)
		{
			var target = new Server.Core.Entities.Status();
			_assigner.Assign((value) => {target.Id = value;}, source.STATEID);
			_assigner.Assign((value) => {target.Value = value;}, source.STATE);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.Status source, Server.Core.Entities.Status target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.STATEID);
			_assigner.Assign((value) => {target.Value = value;}, source.STATE);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
		}

		public Server.Core.Entities.Status ReflectedPropertyMapping(DbEntities.Status source)
		{
			var target = new Server.Core.Entities.Status();
			typeof(Server.Core.Entities.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATEID);
			typeof(Server.Core.Entities.Status).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATE);
			typeof(Server.Core.Entities.Status).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.Status source, Server.Core.Entities.Status target)
		{
			typeof(Server.Core.Entities.Status).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATEID);
			typeof(Server.Core.Entities.Status).GetField("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATE);
			typeof(Server.Core.Entities.Status).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
		}

		// StatusToStatusDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> STATEID
			// PropertyMapping: Value -> STATE
			// PropertyMapping: Alive -> ALIVE
		public DbEntities.Status DirectPropertyMapping(Server.Core.Entities.Status source)
		{
			var target = new DbEntities.Status();
			_assigner.Assign((value) => {target.STATEID = value;}, source.Id);
			_assigner.Assign((value) => {target.STATE = value;}, source.Value);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Status source, DbEntities.Status target)
		{
			_assigner.Assign((value) => {target.STATEID = value;}, source.Id);
			_assigner.Assign((value) => {target.STATE = value;}, source.Value);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
		}

		public DbEntities.Status ReflectedPropertyMapping(Server.Core.Entities.Status source)
		{
			var target = new DbEntities.Status();
			typeof(DbEntities.Status).GetField("STATEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Status).GetField("STATE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DbEntities.Status).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Status source, DbEntities.Status target)
		{
			typeof(DbEntities.Status).GetField("STATEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Status).GetField("STATE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Value);
			typeof(DbEntities.Status).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
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

		// TestEquipmentDbToTestEquipment.txt
			// hasDefaultConstructor
			// PropertyMapping: SEQID -> Id
			// PropertyMapping: SERNO -> SerialNumber
			// PropertyMapping: USRNO -> InventoryNumber
			// PropertyMapping: MODELID -> TestEquipmentModel
			// PropertyMapping: TestEquipmentModel -> TestEquipmentModel
			// PropertyMapping: ALIVE -> Alive
			// PropertyMapping: TRANSFERUSER -> TransferUser
			// PropertyMapping: TRANSFERADAPTER -> TransferAdapter
			// PropertyMapping: TRANSFERTRANSDUCER -> TransferTransducer
			// PropertyMapping: ASKFORIDENT -> AskForIdent
			// PropertyMapping: TRANSFERCURVES -> TransferCurves
			// PropertyMapping: USEERRORCODES -> UseErrorCodes
			// PropertyMapping: ASKFORSIGN -> AskForSign
			// PropertyMapping: DOLOSECHECK -> DoLoseCheck
			// PropertyMapping: CANDELETEMEASUREMENTS -> CanDeleteMeasurements
			// PropertyMapping: CONFIRMMEASUREMENT -> ConfirmMeasurements
			// PropertyMapping: TRANSFERPICT -> TransferLocationPictures
			// PropertyMapping: TRANSFERNEWLIMITS -> TransferNewLimits
			// PropertyMapping: TRANSFERATTRIBUTES -> TransferAttributes
			// PropertyMapping: ISCTLTEST -> UseForCtl
			// PropertyMapping: ISROUTTEST -> UseForRot
			// PropertyMapping: STATEID -> Status
			// PropertyMapping: VERS -> Version
			// PropertyMapping: LASTCERT -> LastCalibration
			// PropertyMapping: PERIOD -> CalibrationInterval
			// PropertyMapping: CAPACITYMIN -> CapacityMin
			// PropertyMapping: CAPACITYMAX -> CapacityMax
			// PropertyMapping: CALIBRATION_NORM_TEXT -> CalibrationNorm
			// PropertyMapping: CANUSEQSTSTANDARD -> CanUseQstStandard
		public Server.Core.Entities.TestEquipment DirectPropertyMapping(DbEntities.TestEquipment source)
		{
			var target = new Server.Core.Entities.TestEquipment();
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SERNO);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.USRNO);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.MODELID);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TRANSFERUSER);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TRANSFERADAPTER);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TRANSFERTRANSDUCER);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.ASKFORIDENT);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TRANSFERCURVES);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.USEERRORCODES);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.ASKFORSIGN);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DOLOSECHECK);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CANDELETEMEASUREMENTS);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.CONFIRMMEASUREMENT);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TRANSFERPICT);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TRANSFERNEWLIMITS);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TRANSFERATTRIBUTES);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.ISCTLTEST);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.ISROUTTEST);
			_assigner.Assign((value) => {target.Status = value;}, source.STATEID);
			_assigner.Assign((value) => {target.Version = value;}, source.VERS);
			_assigner.Assign((value) => {target.LastCalibration = value;}, source.LASTCERT);
			_assigner.Assign((value) => {target.CalibrationInterval = value;}, source.PERIOD);
			_assigner.Assign((value) => {target.CapacityMin = value;}, source.CAPACITYMIN);
			_assigner.Assign((value) => {target.CapacityMax = value;}, source.CAPACITYMAX);
			_assigner.Assign((value) => {target.CalibrationNorm = value;}, source.CALIBRATION_NORM_TEXT);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CANUSEQSTSTANDARD);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.TestEquipment source, Server.Core.Entities.TestEquipment target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SERNO);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.USRNO);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.MODELID);
			_assigner.Assign((value) => {target.TestEquipmentModel = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TRANSFERUSER);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TRANSFERADAPTER);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TRANSFERTRANSDUCER);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.ASKFORIDENT);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TRANSFERCURVES);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.USEERRORCODES);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.ASKFORSIGN);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DOLOSECHECK);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CANDELETEMEASUREMENTS);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.CONFIRMMEASUREMENT);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TRANSFERPICT);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TRANSFERNEWLIMITS);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TRANSFERATTRIBUTES);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.ISCTLTEST);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.ISROUTTEST);
			_assigner.Assign((value) => {target.Status = value;}, source.STATEID);
			_assigner.Assign((value) => {target.Version = value;}, source.VERS);
			_assigner.Assign((value) => {target.LastCalibration = value;}, source.LASTCERT);
			_assigner.Assign((value) => {target.CalibrationInterval = value;}, source.PERIOD);
			_assigner.Assign((value) => {target.CapacityMin = value;}, source.CAPACITYMIN);
			_assigner.Assign((value) => {target.CapacityMax = value;}, source.CAPACITYMAX);
			_assigner.Assign((value) => {target.CalibrationNorm = value;}, source.CALIBRATION_NORM_TEXT);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CANUSEQSTSTANDARD);
		}

		public Server.Core.Entities.TestEquipment ReflectedPropertyMapping(DbEntities.TestEquipment source)
		{
			var target = new Server.Core.Entities.TestEquipment();
			typeof(Server.Core.Entities.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SERNO);
			typeof(Server.Core.Entities.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USRNO);
			typeof(Server.Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(Server.Core.Entities.TestEquipment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERUSER);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERADAPTER);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERTRANSDUCER);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORIDENT);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERCURVES);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEERRORCODES);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORSIGN);
			typeof(Server.Core.Entities.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DOLOSECHECK);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANDELETEMEASUREMENTS);
			typeof(Server.Core.Entities.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONFIRMMEASUREMENT);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERPICT);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERNEWLIMITS);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERATTRIBUTES);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ISCTLTEST);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ISROUTTEST);
			typeof(Server.Core.Entities.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATEID);
			typeof(Server.Core.Entities.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VERS);
			typeof(Server.Core.Entities.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LASTCERT);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PERIOD);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CAPACITYMIN);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CAPACITYMAX);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CALIBRATION_NORM_TEXT);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANUSEQSTSTANDARD);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.TestEquipment source, Server.Core.Entities.TestEquipment target)
		{
			typeof(Server.Core.Entities.TestEquipment).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.TestEquipment).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SERNO);
			typeof(Server.Core.Entities.TestEquipment).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USRNO);
			typeof(Server.Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.TestEquipment).GetField("TestEquipmentModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(Server.Core.Entities.TestEquipment).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERUSER);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERADAPTER);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERTRANSDUCER);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORIDENT);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERCURVES);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEERRORCODES);
			typeof(Server.Core.Entities.TestEquipment).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORSIGN);
			typeof(Server.Core.Entities.TestEquipment).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DOLOSECHECK);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANDELETEMEASUREMENTS);
			typeof(Server.Core.Entities.TestEquipment).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONFIRMMEASUREMENT);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERPICT);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERNEWLIMITS);
			typeof(Server.Core.Entities.TestEquipment).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERATTRIBUTES);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ISCTLTEST);
			typeof(Server.Core.Entities.TestEquipment).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ISROUTTEST);
			typeof(Server.Core.Entities.TestEquipment).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATEID);
			typeof(Server.Core.Entities.TestEquipment).GetField("Version", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VERS);
			typeof(Server.Core.Entities.TestEquipment).GetField("LastCalibration", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LASTCERT);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationInterval", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PERIOD);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMin", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CAPACITYMIN);
			typeof(Server.Core.Entities.TestEquipment).GetField("CapacityMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CAPACITYMAX);
			typeof(Server.Core.Entities.TestEquipment).GetField("CalibrationNorm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CALIBRATION_NORM_TEXT);
			typeof(Server.Core.Entities.TestEquipment).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANUSEQSTSTANDARD);
		}

		// TestEquipmentModelDbToTestEquipmentModel.txt
			// hasDefaultConstructor
			// PropertyMapping: MODELID -> Id
			// PropertyMapping: NAME -> TestEquipmentModelName
			// PropertyMapping: MANUID -> Manufacturer
			// PropertyMapping: Manufacturer -> Manufacturer
			// PropertyMapping: DRIVERPROGRAM_FILE_PATH -> DriverProgramPath
			// PropertyMapping: TOTESTEQUIPMENT_FILE_PATH -> CommunicationFilePath
			// PropertyMapping: STATUS_FILE_PATH -> StatusFilePath
			// PropertyMapping: FROMTESTEQUIPMENT_FILE_PATH -> ResultFilePath
			// PropertyMapping: TYPE -> Type
			// PropertyMapping: TRANSFERUSER -> TransferUser
			// PropertyMapping: TRANSFERADAPTER -> TransferAdapter
			// PropertyMapping: TRANSFERTRANSDUCER -> TransferTransducer
			// PropertyMapping: ASKFORIDENT -> AskForIdent
			// PropertyMapping: TRANSFERCURVES -> TransferCurves
			// PropertyMapping: USEERRORCODES -> UseErrorCodes
			// PropertyMapping: ASKFORSIGN -> AskForSign
			// PropertyMapping: DOLOSECHECK -> DoLoseCheck
			// PropertyMapping: CANDELETEMEASUREMENTS -> CanDeleteMeasurements
			// PropertyMapping: CONFIRMMEASUREMENT -> ConfirmMeasurements
			// PropertyMapping: TRANSFERPICTS -> TransferLocationPictures
			// PropertyMapping: TRANSFERNEWLIMITS -> TransferNewLimits
			// PropertyMapping: TRANSFERATTRIBUTES -> TransferAttributes
			// PropertyMapping: ALIVE -> Alive
			// PropertyMapping: USEFORCTL -> UseForCtl
			// PropertyMapping: USEFORROT -> UseForRot
			// PropertyMapping: DGVERSION -> DataGateVersion
			// PropertyMapping: CANUSEQSTSTANDARD -> CanUseQstStandard
		public Server.Core.Entities.TestEquipmentModel DirectPropertyMapping(DbEntities.TestEquipmentModel source)
		{
			var target = new Server.Core.Entities.TestEquipmentModel();
			_assigner.Assign((value) => {target.Id = value;}, source.MODELID);
			_assigner.Assign((value) => {target.TestEquipmentModelName = value;}, source.NAME);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.MANUID);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.DriverProgramPath = value;}, source.DRIVERPROGRAM_FILE_PATH);
			_assigner.Assign((value) => {target.CommunicationFilePath = value;}, source.TOTESTEQUIPMENT_FILE_PATH);
			_assigner.Assign((value) => {target.StatusFilePath = value;}, source.STATUS_FILE_PATH);
			_assigner.Assign((value) => {target.ResultFilePath = value;}, source.FROMTESTEQUIPMENT_FILE_PATH);
			_assigner.Assign((value) => {target.Type = value;}, source.TYPE);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TRANSFERUSER);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TRANSFERADAPTER);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TRANSFERTRANSDUCER);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.ASKFORIDENT);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TRANSFERCURVES);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.USEERRORCODES);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.ASKFORSIGN);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DOLOSECHECK);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CANDELETEMEASUREMENTS);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.CONFIRMMEASUREMENT);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TRANSFERPICTS);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TRANSFERNEWLIMITS);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TRANSFERATTRIBUTES);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.USEFORCTL);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.USEFORROT);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DGVERSION);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CANUSEQSTSTANDARD);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.TestEquipmentModel source, Server.Core.Entities.TestEquipmentModel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.MODELID);
			_assigner.Assign((value) => {target.TestEquipmentModelName = value;}, source.NAME);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.MANUID);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.DriverProgramPath = value;}, source.DRIVERPROGRAM_FILE_PATH);
			_assigner.Assign((value) => {target.CommunicationFilePath = value;}, source.TOTESTEQUIPMENT_FILE_PATH);
			_assigner.Assign((value) => {target.StatusFilePath = value;}, source.STATUS_FILE_PATH);
			_assigner.Assign((value) => {target.ResultFilePath = value;}, source.FROMTESTEQUIPMENT_FILE_PATH);
			_assigner.Assign((value) => {target.Type = value;}, source.TYPE);
			_assigner.Assign((value) => {target.TransferUser = value;}, source.TRANSFERUSER);
			_assigner.Assign((value) => {target.TransferAdapter = value;}, source.TRANSFERADAPTER);
			_assigner.Assign((value) => {target.TransferTransducer = value;}, source.TRANSFERTRANSDUCER);
			_assigner.Assign((value) => {target.AskForIdent = value;}, source.ASKFORIDENT);
			_assigner.Assign((value) => {target.TransferCurves = value;}, source.TRANSFERCURVES);
			_assigner.Assign((value) => {target.UseErrorCodes = value;}, source.USEERRORCODES);
			_assigner.Assign((value) => {target.AskForSign = value;}, source.ASKFORSIGN);
			_assigner.Assign((value) => {target.DoLoseCheck = value;}, source.DOLOSECHECK);
			_assigner.Assign((value) => {target.CanDeleteMeasurements = value;}, source.CANDELETEMEASUREMENTS);
			_assigner.Assign((value) => {target.ConfirmMeasurements = value;}, source.CONFIRMMEASUREMENT);
			_assigner.Assign((value) => {target.TransferLocationPictures = value;}, source.TRANSFERPICTS);
			_assigner.Assign((value) => {target.TransferNewLimits = value;}, source.TRANSFERNEWLIMITS);
			_assigner.Assign((value) => {target.TransferAttributes = value;}, source.TRANSFERATTRIBUTES);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			_assigner.Assign((value) => {target.UseForCtl = value;}, source.USEFORCTL);
			_assigner.Assign((value) => {target.UseForRot = value;}, source.USEFORROT);
			_assigner.Assign((value) => {target.DataGateVersion = value;}, source.DGVERSION);
			_assigner.Assign((value) => {target.CanUseQstStandard = value;}, source.CANUSEQSTSTANDARD);
		}

		public Server.Core.Entities.TestEquipmentModel ReflectedPropertyMapping(DbEntities.TestEquipmentModel source)
		{
			var target = new Server.Core.Entities.TestEquipmentModel();
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MANUID);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DRIVERPROGRAM_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOTESTEQUIPMENT_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATUS_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FROMTESTEQUIPMENT_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TYPE);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERUSER);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERADAPTER);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERTRANSDUCER);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORIDENT);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERCURVES);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEERRORCODES);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORSIGN);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DOLOSECHECK);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANDELETEMEASUREMENTS);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONFIRMMEASUREMENT);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERPICTS);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERNEWLIMITS);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERATTRIBUTES);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEFORCTL);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEFORROT);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DGVERSION);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANUSEQSTSTANDARD);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.TestEquipmentModel source, Server.Core.Entities.TestEquipmentModel target)
		{
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TestEquipmentModelName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.NAME);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MANUID);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DriverProgramPath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DRIVERPROGRAM_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CommunicationFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TOTESTEQUIPMENT_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("StatusFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATUS_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ResultFilePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FROMTESTEQUIPMENT_FILE_PATH);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TYPE);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferUser", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERUSER);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAdapter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERADAPTER);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferTransducer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERTRANSDUCER);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForIdent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORIDENT);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferCurves", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERCURVES);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseErrorCodes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEERRORCODES);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("AskForSign", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ASKFORSIGN);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DoLoseCheck", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DOLOSECHECK);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanDeleteMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANDELETEMEASUREMENTS);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("ConfirmMeasurements", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CONFIRMMEASUREMENT);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferLocationPictures", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERPICTS);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferNewLimits", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERNEWLIMITS);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("TransferAttributes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TRANSFERATTRIBUTES);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForCtl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEFORCTL);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("UseForRot", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USEFORROT);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("DataGateVersion", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DGVERSION);
			typeof(Server.Core.Entities.TestEquipmentModel).GetField("CanUseQstStandard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CANUSEQSTSTANDARD);
		}

		// TestEquipmentModelToTestEquipmentModelDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> MODELID
			// PropertyMapping: TestEquipmentModelName -> NAME
			// PropertyMapping: Manufacturer -> MANUID
			// PropertyMapping: DriverProgramPath -> DRIVERPROGRAM_FILE_PATH
			// PropertyMapping: CommunicationFilePath -> TOTESTEQUIPMENT_FILE_PATH
			// PropertyMapping: StatusFilePath -> STATUS_FILE_PATH
			// PropertyMapping: ResultFilePath -> FROMTESTEQUIPMENT_FILE_PATH
			// PropertyMapping: Type -> TYPE
			// PropertyMapping: TransferUser -> TRANSFERUSER
			// PropertyMapping: TransferAdapter -> TRANSFERADAPTER
			// PropertyMapping: TransferTransducer -> TRANSFERTRANSDUCER
			// PropertyMapping: AskForIdent -> ASKFORIDENT
			// PropertyMapping: TransferCurves -> TRANSFERCURVES
			// PropertyMapping: UseErrorCodes -> USEERRORCODES
			// PropertyMapping: AskForSign -> ASKFORSIGN
			// PropertyMapping: DoLoseCheck -> DOLOSECHECK
			// PropertyMapping: CanDeleteMeasurements -> CANDELETEMEASUREMENTS
			// PropertyMapping: ConfirmMeasurements -> CONFIRMMEASUREMENT
			// PropertyMapping: TransferLocationPictures -> TRANSFERPICTS
			// PropertyMapping: TransferNewLimits -> TRANSFERNEWLIMITS
			// PropertyMapping: TransferAttributes -> TRANSFERATTRIBUTES
			// PropertyMapping: Alive -> ALIVE
			// PropertyMapping: UseForCtl -> USEFORCTL
			// PropertyMapping: UseForRot -> USEFORROT
			// PropertyMapping: DataGateVersion -> DGVERSION
			// PropertyMapping: CanUseQstStandard -> CANUSEQSTSTANDARD
		public DbEntities.TestEquipmentModel DirectPropertyMapping(Server.Core.Entities.TestEquipmentModel source)
		{
			var target = new DbEntities.TestEquipmentModel();
			_assigner.Assign((value) => {target.MODELID = value;}, source.Id);
			_assigner.Assign((value) => {target.NAME = value;}, source.TestEquipmentModelName);
			_assigner.Assign((value) => {target.MANUID = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.DRIVERPROGRAM_FILE_PATH = value;}, source.DriverProgramPath);
			_assigner.Assign((value) => {target.TOTESTEQUIPMENT_FILE_PATH = value;}, source.CommunicationFilePath);
			_assigner.Assign((value) => {target.STATUS_FILE_PATH = value;}, source.StatusFilePath);
			_assigner.Assign((value) => {target.FROMTESTEQUIPMENT_FILE_PATH = value;}, source.ResultFilePath);
			_assigner.Assign((value) => {target.TYPE = value;}, source.Type);
			_assigner.Assign((value) => {target.TRANSFERUSER = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TRANSFERADAPTER = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TRANSFERTRANSDUCER = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.ASKFORIDENT = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TRANSFERCURVES = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.USEERRORCODES = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.ASKFORSIGN = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DOLOSECHECK = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CANDELETEMEASUREMENTS = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.CONFIRMMEASUREMENT = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TRANSFERPICTS = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TRANSFERNEWLIMITS = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TRANSFERATTRIBUTES = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.USEFORCTL = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.USEFORROT = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.DGVERSION = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CANUSEQSTSTANDARD = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestEquipmentModel source, DbEntities.TestEquipmentModel target)
		{
			_assigner.Assign((value) => {target.MODELID = value;}, source.Id);
			_assigner.Assign((value) => {target.NAME = value;}, source.TestEquipmentModelName);
			_assigner.Assign((value) => {target.MANUID = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.DRIVERPROGRAM_FILE_PATH = value;}, source.DriverProgramPath);
			_assigner.Assign((value) => {target.TOTESTEQUIPMENT_FILE_PATH = value;}, source.CommunicationFilePath);
			_assigner.Assign((value) => {target.STATUS_FILE_PATH = value;}, source.StatusFilePath);
			_assigner.Assign((value) => {target.FROMTESTEQUIPMENT_FILE_PATH = value;}, source.ResultFilePath);
			_assigner.Assign((value) => {target.TYPE = value;}, source.Type);
			_assigner.Assign((value) => {target.TRANSFERUSER = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TRANSFERADAPTER = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TRANSFERTRANSDUCER = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.ASKFORIDENT = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TRANSFERCURVES = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.USEERRORCODES = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.ASKFORSIGN = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DOLOSECHECK = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CANDELETEMEASUREMENTS = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.CONFIRMMEASUREMENT = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TRANSFERPICTS = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TRANSFERNEWLIMITS = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TRANSFERATTRIBUTES = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.USEFORCTL = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.USEFORROT = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.DGVERSION = value;}, source.DataGateVersion);
			_assigner.Assign((value) => {target.CANUSEQSTSTANDARD = value;}, source.CanUseQstStandard);
		}

		public DbEntities.TestEquipmentModel ReflectedPropertyMapping(Server.Core.Entities.TestEquipmentModel source)
		{
			var target = new DbEntities.TestEquipmentModel();
			typeof(DbEntities.TestEquipmentModel).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestEquipmentModel).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(DbEntities.TestEquipmentModel).GetField("MANUID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DbEntities.TestEquipmentModel).GetField("DRIVERPROGRAM_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(DbEntities.TestEquipmentModel).GetField("TOTESTEQUIPMENT_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(DbEntities.TestEquipmentModel).GetField("STATUS_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(DbEntities.TestEquipmentModel).GetField("FROMTESTEQUIPMENT_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(DbEntities.TestEquipmentModel).GetField("TYPE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERUSER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERADAPTER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERTRANSDUCER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DbEntities.TestEquipmentModel).GetField("ASKFORIDENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERCURVES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DbEntities.TestEquipmentModel).GetField("USEERRORCODES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DbEntities.TestEquipmentModel).GetField("ASKFORSIGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DbEntities.TestEquipmentModel).GetField("DOLOSECHECK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DbEntities.TestEquipmentModel).GetField("CANDELETEMEASUREMENTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DbEntities.TestEquipmentModel).GetField("CONFIRMMEASUREMENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERPICTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERNEWLIMITS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERATTRIBUTES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DbEntities.TestEquipmentModel).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.TestEquipmentModel).GetField("USEFORCTL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DbEntities.TestEquipmentModel).GetField("USEFORROT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DbEntities.TestEquipmentModel).GetField("DGVERSION", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(DbEntities.TestEquipmentModel).GetField("CANUSEQSTSTANDARD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestEquipmentModel source, DbEntities.TestEquipmentModel target)
		{
			typeof(DbEntities.TestEquipmentModel).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestEquipmentModel).GetField("NAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModelName);
			typeof(DbEntities.TestEquipmentModel).GetField("MANUID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DbEntities.TestEquipmentModel).GetField("DRIVERPROGRAM_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriverProgramPath);
			typeof(DbEntities.TestEquipmentModel).GetField("TOTESTEQUIPMENT_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CommunicationFilePath);
			typeof(DbEntities.TestEquipmentModel).GetField("STATUS_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.StatusFilePath);
			typeof(DbEntities.TestEquipmentModel).GetField("FROMTESTEQUIPMENT_FILE_PATH", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ResultFilePath);
			typeof(DbEntities.TestEquipmentModel).GetField("TYPE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERUSER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERADAPTER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERTRANSDUCER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DbEntities.TestEquipmentModel).GetField("ASKFORIDENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERCURVES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DbEntities.TestEquipmentModel).GetField("USEERRORCODES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DbEntities.TestEquipmentModel).GetField("ASKFORSIGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DbEntities.TestEquipmentModel).GetField("DOLOSECHECK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DbEntities.TestEquipmentModel).GetField("CANDELETEMEASUREMENTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DbEntities.TestEquipmentModel).GetField("CONFIRMMEASUREMENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERPICTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERNEWLIMITS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DbEntities.TestEquipmentModel).GetField("TRANSFERATTRIBUTES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DbEntities.TestEquipmentModel).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.TestEquipmentModel).GetField("USEFORCTL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DbEntities.TestEquipmentModel).GetField("USEFORROT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DbEntities.TestEquipmentModel).GetField("DGVERSION", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DataGateVersion);
			typeof(DbEntities.TestEquipmentModel).GetField("CANUSEQSTSTANDARD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestEquipmentToTestEquipmentDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> SEQID
			// PropertyMapping: SerialNumber -> SERNO
			// PropertyMapping: InventoryNumber -> USRNO
			// PropertyMapping: TestEquipmentModel -> MODELID
			// PropertyMapping: Alive -> ALIVE
			// PropertyMapping: TransferUser -> TRANSFERUSER
			// PropertyMapping: TransferAdapter -> TRANSFERADAPTER
			// PropertyMapping: TransferTransducer -> TRANSFERTRANSDUCER
			// PropertyMapping: AskForIdent -> ASKFORIDENT
			// PropertyMapping: TransferCurves -> TRANSFERCURVES
			// PropertyMapping: UseErrorCodes -> USEERRORCODES
			// PropertyMapping: AskForSign -> ASKFORSIGN
			// PropertyMapping: DoLoseCheck -> DOLOSECHECK
			// PropertyMapping: CanDeleteMeasurements -> CANDELETEMEASUREMENTS
			// PropertyMapping: ConfirmMeasurements -> CONFIRMMEASUREMENT
			// PropertyMapping: TransferLocationPictures -> TRANSFERPICT
			// PropertyMapping: TransferNewLimits -> TRANSFERNEWLIMITS
			// PropertyMapping: TransferAttributes -> TRANSFERATTRIBUTES
			// PropertyMapping: UseForCtl -> ISCTLTEST
			// PropertyMapping: UseForRot -> ISROUTTEST
			// PropertyMapping: Status -> STATEID
			// PropertyMapping: Version -> VERS
			// PropertyMapping: LastCalibration -> LASTCERT
			// PropertyMapping: CalibrationInterval -> PERIOD
			// PropertyMapping: CapacityMin -> CAPACITYMIN
			// PropertyMapping: CapacityMax -> CAPACITYMAX
			// PropertyMapping: CalibrationNorm -> CALIBRATION_NORM_TEXT
			// PropertyMapping: CanUseQstStandard -> CANUSEQSTSTANDARD
		public DbEntities.TestEquipment DirectPropertyMapping(Server.Core.Entities.TestEquipment source)
		{
			var target = new DbEntities.TestEquipment();
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.SERNO = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.USRNO = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.MODELID = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.TRANSFERUSER = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TRANSFERADAPTER = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TRANSFERTRANSDUCER = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.ASKFORIDENT = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TRANSFERCURVES = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.USEERRORCODES = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.ASKFORSIGN = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DOLOSECHECK = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CANDELETEMEASUREMENTS = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.CONFIRMMEASUREMENT = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TRANSFERPICT = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TRANSFERNEWLIMITS = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TRANSFERATTRIBUTES = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.ISCTLTEST = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.ISROUTTEST = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.STATEID = value;}, source.Status);
			_assigner.Assign((value) => {target.VERS = value;}, source.Version);
			_assigner.Assign((value) => {target.LASTCERT = value;}, source.LastCalibration);
			_assigner.Assign((value) => {target.PERIOD = value;}, source.CalibrationInterval);
			_assigner.Assign((value) => {target.CAPACITYMIN = value;}, source.CapacityMin);
			_assigner.Assign((value) => {target.CAPACITYMAX = value;}, source.CapacityMax);
			_assigner.Assign((value) => {target.CALIBRATION_NORM_TEXT = value;}, source.CalibrationNorm);
			_assigner.Assign((value) => {target.CANUSEQSTSTANDARD = value;}, source.CanUseQstStandard);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestEquipment source, DbEntities.TestEquipment target)
		{
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.SERNO = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.USRNO = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.MODELID = value;}, source.TestEquipmentModel);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			_assigner.Assign((value) => {target.TRANSFERUSER = value;}, source.TransferUser);
			_assigner.Assign((value) => {target.TRANSFERADAPTER = value;}, source.TransferAdapter);
			_assigner.Assign((value) => {target.TRANSFERTRANSDUCER = value;}, source.TransferTransducer);
			_assigner.Assign((value) => {target.ASKFORIDENT = value;}, source.AskForIdent);
			_assigner.Assign((value) => {target.TRANSFERCURVES = value;}, source.TransferCurves);
			_assigner.Assign((value) => {target.USEERRORCODES = value;}, source.UseErrorCodes);
			_assigner.Assign((value) => {target.ASKFORSIGN = value;}, source.AskForSign);
			_assigner.Assign((value) => {target.DOLOSECHECK = value;}, source.DoLoseCheck);
			_assigner.Assign((value) => {target.CANDELETEMEASUREMENTS = value;}, source.CanDeleteMeasurements);
			_assigner.Assign((value) => {target.CONFIRMMEASUREMENT = value;}, source.ConfirmMeasurements);
			_assigner.Assign((value) => {target.TRANSFERPICT = value;}, source.TransferLocationPictures);
			_assigner.Assign((value) => {target.TRANSFERNEWLIMITS = value;}, source.TransferNewLimits);
			_assigner.Assign((value) => {target.TRANSFERATTRIBUTES = value;}, source.TransferAttributes);
			_assigner.Assign((value) => {target.ISCTLTEST = value;}, source.UseForCtl);
			_assigner.Assign((value) => {target.ISROUTTEST = value;}, source.UseForRot);
			_assigner.Assign((value) => {target.STATEID = value;}, source.Status);
			_assigner.Assign((value) => {target.VERS = value;}, source.Version);
			_assigner.Assign((value) => {target.LASTCERT = value;}, source.LastCalibration);
			_assigner.Assign((value) => {target.PERIOD = value;}, source.CalibrationInterval);
			_assigner.Assign((value) => {target.CAPACITYMIN = value;}, source.CapacityMin);
			_assigner.Assign((value) => {target.CAPACITYMAX = value;}, source.CapacityMax);
			_assigner.Assign((value) => {target.CALIBRATION_NORM_TEXT = value;}, source.CalibrationNorm);
			_assigner.Assign((value) => {target.CANUSEQSTSTANDARD = value;}, source.CanUseQstStandard);
		}

		public DbEntities.TestEquipment ReflectedPropertyMapping(Server.Core.Entities.TestEquipment source)
		{
			var target = new DbEntities.TestEquipment();
			typeof(DbEntities.TestEquipment).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestEquipment).GetField("SERNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DbEntities.TestEquipment).GetField("USRNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DbEntities.TestEquipment).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(DbEntities.TestEquipment).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERUSER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERADAPTER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERTRANSDUCER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DbEntities.TestEquipment).GetField("ASKFORIDENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERCURVES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DbEntities.TestEquipment).GetField("USEERRORCODES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DbEntities.TestEquipment).GetField("ASKFORSIGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DbEntities.TestEquipment).GetField("DOLOSECHECK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DbEntities.TestEquipment).GetField("CANDELETEMEASUREMENTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DbEntities.TestEquipment).GetField("CONFIRMMEASUREMENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERPICT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERNEWLIMITS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERATTRIBUTES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DbEntities.TestEquipment).GetField("ISCTLTEST", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DbEntities.TestEquipment).GetField("ISROUTTEST", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DbEntities.TestEquipment).GetField("STATEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DbEntities.TestEquipment).GetField("VERS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(DbEntities.TestEquipment).GetField("LASTCERT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(DbEntities.TestEquipment).GetField("PERIOD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(DbEntities.TestEquipment).GetField("CAPACITYMIN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(DbEntities.TestEquipment).GetField("CAPACITYMAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(DbEntities.TestEquipment).GetField("CALIBRATION_NORM_TEXT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(DbEntities.TestEquipment).GetField("CANUSEQSTSTANDARD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestEquipment source, DbEntities.TestEquipment target)
		{
			typeof(DbEntities.TestEquipment).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestEquipment).GetField("SERNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DbEntities.TestEquipment).GetField("USRNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DbEntities.TestEquipment).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestEquipmentModel);
			typeof(DbEntities.TestEquipment).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERUSER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferUser);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERADAPTER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAdapter);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERTRANSDUCER", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferTransducer);
			typeof(DbEntities.TestEquipment).GetField("ASKFORIDENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForIdent);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERCURVES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferCurves);
			typeof(DbEntities.TestEquipment).GetField("USEERRORCODES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseErrorCodes);
			typeof(DbEntities.TestEquipment).GetField("ASKFORSIGN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AskForSign);
			typeof(DbEntities.TestEquipment).GetField("DOLOSECHECK", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DoLoseCheck);
			typeof(DbEntities.TestEquipment).GetField("CANDELETEMEASUREMENTS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanDeleteMeasurements);
			typeof(DbEntities.TestEquipment).GetField("CONFIRMMEASUREMENT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfirmMeasurements);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERPICT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferLocationPictures);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERNEWLIMITS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferNewLimits);
			typeof(DbEntities.TestEquipment).GetField("TRANSFERATTRIBUTES", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TransferAttributes);
			typeof(DbEntities.TestEquipment).GetField("ISCTLTEST", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForCtl);
			typeof(DbEntities.TestEquipment).GetField("ISROUTTEST", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UseForRot);
			typeof(DbEntities.TestEquipment).GetField("STATEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DbEntities.TestEquipment).GetField("VERS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Version);
			typeof(DbEntities.TestEquipment).GetField("LASTCERT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LastCalibration);
			typeof(DbEntities.TestEquipment).GetField("PERIOD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationInterval);
			typeof(DbEntities.TestEquipment).GetField("CAPACITYMIN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMin);
			typeof(DbEntities.TestEquipment).GetField("CAPACITYMAX", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CapacityMax);
			typeof(DbEntities.TestEquipment).GetField("CALIBRATION_NORM_TEXT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CalibrationNorm);
			typeof(DbEntities.TestEquipment).GetField("CANUSEQSTSTANDARD", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CanUseQstStandard);
		}

		// TestLevelSetDtoToTestLevelSet.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
		public Server.Core.Entities.TestLevelSet DirectPropertyMapping(DbEntities.TestLevelSet source)
		{
			var target = new Server.Core.Entities.TestLevelSet();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.TestLevelSet source, Server.Core.Entities.TestLevelSet target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
		}

		public Server.Core.Entities.TestLevelSet ReflectedPropertyMapping(DbEntities.TestLevelSet source)
		{
			var target = new Server.Core.Entities.TestLevelSet();
			typeof(Server.Core.Entities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.TestLevelSet source, Server.Core.Entities.TestLevelSet target)
		{
			typeof(Server.Core.Entities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(Server.Core.Entities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
		}

		// TestLevelSetToTestLevelSetDto.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: Name -> Name
		public DbEntities.TestLevelSet DirectPropertyMapping(Server.Core.Entities.TestLevelSet source)
		{
			var target = new DbEntities.TestLevelSet();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestLevelSet source, DbEntities.TestLevelSet target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
		}

		public DbEntities.TestLevelSet ReflectedPropertyMapping(Server.Core.Entities.TestLevelSet source)
		{
			var target = new DbEntities.TestLevelSet();
			typeof(DbEntities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestLevelSet source, DbEntities.TestLevelSet target)
		{
			typeof(DbEntities.TestLevelSet).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestLevelSet).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
		}

		// TestLevelToDbTestLevel.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> Id
			// PropertyMapping: SampleNumber -> SampleNumber
			// PropertyMapping: ConsiderWorkingCalendar -> ConsiderWorkingCalendar
			// PropertyMapping: TestInterval.IntervalValue -> IntervalValue
			// PropertyMapping: TestInterval.Type -> IntervalType
			// PropertyMapping: IsActive -> IsActive
		public DbEntities.TestLevel DirectPropertyMapping(Server.Core.Entities.TestLevel source)
		{
			var target = new DbEntities.TestLevel();
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IntervalValue = value;}, source.TestInterval.IntervalValue);
			_assigner.Assign((value) => {target.IntervalType = value;}, source.TestInterval.Type);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.TestLevel source, DbEntities.TestLevel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.Id);
			_assigner.Assign((value) => {target.SampleNumber = value;}, source.SampleNumber);
			_assigner.Assign((value) => {target.ConsiderWorkingCalendar = value;}, source.ConsiderWorkingCalendar);
			_assigner.Assign((value) => {target.IntervalValue = value;}, source.TestInterval.IntervalValue);
			_assigner.Assign((value) => {target.IntervalType = value;}, source.TestInterval.Type);
			_assigner.Assign((value) => {target.IsActive = value;}, source.IsActive);
		}

		public DbEntities.TestLevel ReflectedPropertyMapping(Server.Core.Entities.TestLevel source)
		{
			var target = new DbEntities.TestLevel();
			typeof(DbEntities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(DbEntities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(DbEntities.TestLevel).GetField("IntervalValue", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval.IntervalValue);
			typeof(DbEntities.TestLevel).GetField("IntervalType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval.Type);
			typeof(DbEntities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.TestLevel source, DbEntities.TestLevel target)
		{
			typeof(DbEntities.TestLevel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.TestLevel).GetField("SampleNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SampleNumber);
			typeof(DbEntities.TestLevel).GetField("ConsiderWorkingCalendar", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConsiderWorkingCalendar);
			typeof(DbEntities.TestLevel).GetField("IntervalValue", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval.IntervalValue);
			typeof(DbEntities.TestLevel).GetField("IntervalType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TestInterval.Type);
			typeof(DbEntities.TestLevel).GetField("IsActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.IsActive);
		}

		// ToleranceClassDbToToleranceClass.txt
			// hasDefaultConstructor
			// PropertyMapping: CLASSID -> Id
			// PropertyMapping: DESCRIPTION -> Name
			// PropertyMapping: CL_REL -> Relative
			// PropertyMapping: CMINUS -> LowerLimit
			// PropertyMapping: CPLUS -> UpperLimit
		public Server.Core.Entities.ToleranceClass DirectPropertyMapping(DbEntities.ToleranceClass source)
		{
			var target = new Server.Core.Entities.ToleranceClass();
			_assigner.Assign((value) => {target.Id = value;}, source.CLASSID);
			_assigner.Assign((value) => {target.Name = value;}, source.DESCRIPTION);
			_assigner.Assign((value) => {target.Relative = value;}, source.CL_REL);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.CMINUS);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.CPLUS);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ToleranceClass source, Server.Core.Entities.ToleranceClass target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.CLASSID);
			_assigner.Assign((value) => {target.Name = value;}, source.DESCRIPTION);
			_assigner.Assign((value) => {target.Relative = value;}, source.CL_REL);
			_assigner.Assign((value) => {target.LowerLimit = value;}, source.CMINUS);
			_assigner.Assign((value) => {target.UpperLimit = value;}, source.CPLUS);
		}

		public Server.Core.Entities.ToleranceClass ReflectedPropertyMapping(DbEntities.ToleranceClass source)
		{
			var target = new Server.Core.Entities.ToleranceClass();
			typeof(Server.Core.Entities.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DESCRIPTION);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CL_REL);
			typeof(Server.Core.Entities.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CMINUS);
			typeof(Server.Core.Entities.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CPLUS);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ToleranceClass source, Server.Core.Entities.ToleranceClass target)
		{
			typeof(Server.Core.Entities.ToleranceClass).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DESCRIPTION);
			typeof(Server.Core.Entities.ToleranceClass).GetField("Relative", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CL_REL);
			typeof(Server.Core.Entities.ToleranceClass).GetField("LowerLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CMINUS);
			typeof(Server.Core.Entities.ToleranceClass).GetField("UpperLimit", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CPLUS);
		}

		// ToleranceClassToToleranceClassDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> CLASSID
			// PropertyMapping: Name -> DESCRIPTION
			// PropertyMapping: Relative -> CL_REL
			// PropertyMapping: LowerLimit -> CMINUS
			// PropertyMapping: UpperLimit -> CPLUS
		public DbEntities.ToleranceClass DirectPropertyMapping(Server.Core.Entities.ToleranceClass source)
		{
			var target = new DbEntities.ToleranceClass();
			_assigner.Assign((value) => {target.CLASSID = value;}, source.Id);
			_assigner.Assign((value) => {target.DESCRIPTION = value;}, source.Name);
			_assigner.Assign((value) => {target.CL_REL = value;}, source.Relative);
			_assigner.Assign((value) => {target.CMINUS = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.CPLUS = value;}, source.UpperLimit);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ToleranceClass source, DbEntities.ToleranceClass target)
		{
			_assigner.Assign((value) => {target.CLASSID = value;}, source.Id);
			_assigner.Assign((value) => {target.DESCRIPTION = value;}, source.Name);
			_assigner.Assign((value) => {target.CL_REL = value;}, source.Relative);
			_assigner.Assign((value) => {target.CMINUS = value;}, source.LowerLimit);
			_assigner.Assign((value) => {target.CPLUS = value;}, source.UpperLimit);
		}

		public DbEntities.ToleranceClass ReflectedPropertyMapping(Server.Core.Entities.ToleranceClass source)
		{
			var target = new DbEntities.ToleranceClass();
			typeof(DbEntities.ToleranceClass).GetField("CLASSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ToleranceClass).GetField("DESCRIPTION", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.ToleranceClass).GetField("CL_REL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(DbEntities.ToleranceClass).GetField("CMINUS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(DbEntities.ToleranceClass).GetField("CPLUS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ToleranceClass source, DbEntities.ToleranceClass target)
		{
			typeof(DbEntities.ToleranceClass).GetField("CLASSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ToleranceClass).GetField("DESCRIPTION", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.ToleranceClass).GetField("CL_REL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Relative);
			typeof(DbEntities.ToleranceClass).GetField("CMINUS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LowerLimit);
			typeof(DbEntities.ToleranceClass).GetField("CPLUS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.UpperLimit);
		}

		// ToolDbToTool.txt
			// hasDefaultConstructor
			// PropertyMapping: SEQID -> Id
			// PropertyMapping: SERIALNO -> SerialNumber
			// PropertyMapping: USERNO -> InventoryNumber
			// PropertyMapping: MODELID -> ToolModel
			// PropertyMapping: ToolModel -> ToolModel
			// PropertyMapping: STATEID -> Status
			// PropertyMapping: PTACCESS -> Accessory
			// PropertyMapping: ORDERID -> ConfigurableField
			// PropertyMapping: KOSTID -> CostCenter
			// PropertyMapping: FREE_STR1 -> AdditionalConfigurableField1
			// PropertyMapping: FREE_STR2 -> AdditionalConfigurableField2
			// PropertyMapping: FREE_STR3 -> AdditionalConfigurableField3
			// PropertyMapping: ALIVE -> Alive
		public Server.Core.Entities.Tool DirectPropertyMapping(DbEntities.Tool source)
		{
			var target = new Server.Core.Entities.Tool();
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SERIALNO);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.USERNO);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.MODELID);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.Status = value;}, source.STATEID);
			_assigner.Assign((value) => {target.Accessory = value;}, source.PTACCESS);
			_assigner.Assign((value) => {target.ConfigurableField = value;}, source.ORDERID);
			_assigner.Assign((value) => {target.CostCenter = value;}, source.KOSTID);
			_assigner.Assign((value) => {target.AdditionalConfigurableField1 = value;}, source.FREE_STR1);
			_assigner.Assign((value) => {target.AdditionalConfigurableField2 = value;}, source.FREE_STR2);
			_assigner.Assign((value) => {target.AdditionalConfigurableField3 = value;}, source.FREE_STR3);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.Tool source, Server.Core.Entities.Tool target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.SEQID);
			_assigner.Assign((value) => {target.SerialNumber = value;}, source.SERIALNO);
			_assigner.Assign((value) => {target.InventoryNumber = value;}, source.USERNO);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.MODELID);
			_assigner.Assign((value) => {target.ToolModel = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.Status = value;}, source.STATEID);
			_assigner.Assign((value) => {target.Accessory = value;}, source.PTACCESS);
			_assigner.Assign((value) => {target.ConfigurableField = value;}, source.ORDERID);
			_assigner.Assign((value) => {target.CostCenter = value;}, source.KOSTID);
			_assigner.Assign((value) => {target.AdditionalConfigurableField1 = value;}, source.FREE_STR1);
			_assigner.Assign((value) => {target.AdditionalConfigurableField2 = value;}, source.FREE_STR2);
			_assigner.Assign((value) => {target.AdditionalConfigurableField3 = value;}, source.FREE_STR3);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
		}

		public Server.Core.Entities.Tool ReflectedPropertyMapping(DbEntities.Tool source)
		{
			var target = new Server.Core.Entities.Tool();
			typeof(Server.Core.Entities.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SERIALNO);
			typeof(Server.Core.Entities.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USERNO);
			typeof(Server.Core.Entities.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(Server.Core.Entities.Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATEID);
			typeof(Server.Core.Entities.Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PTACCESS);
			typeof(Server.Core.Entities.Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ORDERID);
			typeof(Server.Core.Entities.Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KOSTID);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FREE_STR1);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FREE_STR2);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FREE_STR3);
			typeof(Server.Core.Entities.Tool).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.Tool source, Server.Core.Entities.Tool target)
		{
			typeof(Server.Core.Entities.Tool).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SEQID);
			typeof(Server.Core.Entities.Tool).GetField("SerialNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SERIALNO);
			typeof(Server.Core.Entities.Tool).GetField("InventoryNumber", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.USERNO);
			typeof(Server.Core.Entities.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.Tool).GetField("ToolModel", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(Server.Core.Entities.Tool).GetField("Status", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.STATEID);
			typeof(Server.Core.Entities.Tool).GetField("Accessory", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PTACCESS);
			typeof(Server.Core.Entities.Tool).GetField("ConfigurableField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ORDERID);
			typeof(Server.Core.Entities.Tool).GetField("CostCenter", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KOSTID);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FREE_STR1);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FREE_STR2);
			typeof(Server.Core.Entities.Tool).GetField("AdditionalConfigurableField3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FREE_STR3);
			typeof(Server.Core.Entities.Tool).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
		}

		// ToolModelDbToToolModel.txt
			// hasDefaultConstructor
			// PropertyMapping: MODELID -> Id
			// PropertyMapping: MODEL -> Description
			// PropertyMapping: Manufacturer -> Manufacturer
			// PropertyMapping: TYPEID -> ModelType
			// PropertyMapping: CLASSID -> Class
			// PropertyMapping: LIMITLO -> MinPower
			// PropertyMapping: LIMITHI -> MaxPower
			// PropertyMapping: PRESSURE -> AirPressure
			// PropertyMapping: VERSIONID -> ToolType
			// PropertyMapping: WEIGHT -> Weight
			// PropertyMapping: VOLT -> BatteryVoltage
			// PropertyMapping: TURN -> MaxRotationSpeed
			// PropertyMapping: AIR -> AirConsumption
			// PropertyMapping: KINDID -> SwitchOff
			// PropertyMapping: MEAID -> DriveSize
			// PropertyMapping: DRIVEID -> DriveType
			// PropertyMapping: SWITCHID -> ShutOff
			// PropertyMapping: FORMID -> ConstructionType
			// PropertyMapping: ALIVE -> Alive
		public Server.Core.Entities.ToolModel DirectPropertyMapping(DbEntities.ToolModel source)
		{
			var target = new Server.Core.Entities.ToolModel();
			_assigner.Assign((value) => {target.Id = value;}, source.MODELID);
			_assigner.Assign((value) => {target.Description = value;}, source.MODEL);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.ModelType = value;}, source.TYPEID);
			_assigner.Assign((value) => {target.Class = value;}, source.CLASSID);
			_assigner.Assign((value) => {target.MinPower = value;}, source.LIMITLO);
			_assigner.Assign((value) => {target.MaxPower = value;}, source.LIMITHI);
			_assigner.Assign((value) => {target.AirPressure = value;}, source.PRESSURE);
			_assigner.Assign((value) => {target.ToolType = value;}, source.VERSIONID);
			_assigner.Assign((value) => {target.Weight = value;}, source.WEIGHT);
			_assigner.Assign((value) => {target.BatteryVoltage = value;}, source.VOLT);
			_assigner.Assign((value) => {target.MaxRotationSpeed = value;}, source.TURN);
			_assigner.Assign((value) => {target.AirConsumption = value;}, source.AIR);
			_assigner.Assign((value) => {target.SwitchOff = value;}, source.KINDID);
			_assigner.Assign((value) => {target.DriveSize = value;}, source.MEAID);
			_assigner.Assign((value) => {target.DriveType = value;}, source.DRIVEID);
			_assigner.Assign((value) => {target.ShutOff = value;}, source.SWITCHID);
			_assigner.Assign((value) => {target.ConstructionType = value;}, source.FORMID);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ToolModel source, Server.Core.Entities.ToolModel target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.MODELID);
			_assigner.Assign((value) => {target.Description = value;}, source.MODEL);
			_assigner.Assign((value) => {target.Manufacturer = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.ModelType = value;}, source.TYPEID);
			_assigner.Assign((value) => {target.Class = value;}, source.CLASSID);
			_assigner.Assign((value) => {target.MinPower = value;}, source.LIMITLO);
			_assigner.Assign((value) => {target.MaxPower = value;}, source.LIMITHI);
			_assigner.Assign((value) => {target.AirPressure = value;}, source.PRESSURE);
			_assigner.Assign((value) => {target.ToolType = value;}, source.VERSIONID);
			_assigner.Assign((value) => {target.Weight = value;}, source.WEIGHT);
			_assigner.Assign((value) => {target.BatteryVoltage = value;}, source.VOLT);
			_assigner.Assign((value) => {target.MaxRotationSpeed = value;}, source.TURN);
			_assigner.Assign((value) => {target.AirConsumption = value;}, source.AIR);
			_assigner.Assign((value) => {target.SwitchOff = value;}, source.KINDID);
			_assigner.Assign((value) => {target.DriveSize = value;}, source.MEAID);
			_assigner.Assign((value) => {target.DriveType = value;}, source.DRIVEID);
			_assigner.Assign((value) => {target.ShutOff = value;}, source.SWITCHID);
			_assigner.Assign((value) => {target.ConstructionType = value;}, source.FORMID);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
		}

		public Server.Core.Entities.ToolModel ReflectedPropertyMapping(DbEntities.ToolModel source)
		{
			var target = new Server.Core.Entities.ToolModel();
			typeof(Server.Core.Entities.ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODEL);
			typeof(Server.Core.Entities.ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TYPEID);
			typeof(Server.Core.Entities.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID);
			typeof(Server.Core.Entities.ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMITLO);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMITHI);
			typeof(Server.Core.Entities.ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PRESSURE);
			typeof(Server.Core.Entities.ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VERSIONID);
			typeof(Server.Core.Entities.ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.WEIGHT);
			typeof(Server.Core.Entities.ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VOLT);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TURN);
			typeof(Server.Core.Entities.ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AIR);
			typeof(Server.Core.Entities.ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KINDID);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MEAID);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DRIVEID);
			typeof(Server.Core.Entities.ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SWITCHID);
			typeof(Server.Core.Entities.ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FORMID);
			typeof(Server.Core.Entities.ToolModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ToolModel source, Server.Core.Entities.ToolModel target)
		{
			typeof(Server.Core.Entities.ToolModel).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODELID);
			typeof(Server.Core.Entities.ToolModel).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MODEL);
			typeof(Server.Core.Entities.ToolModel).GetField("Manufacturer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(Server.Core.Entities.ToolModel).GetField("ModelType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TYPEID);
			typeof(Server.Core.Entities.ToolModel).GetField("Class", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CLASSID);
			typeof(Server.Core.Entities.ToolModel).GetField("MinPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMITLO);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxPower", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.LIMITHI);
			typeof(Server.Core.Entities.ToolModel).GetField("AirPressure", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.PRESSURE);
			typeof(Server.Core.Entities.ToolModel).GetField("ToolType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VERSIONID);
			typeof(Server.Core.Entities.ToolModel).GetField("Weight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.WEIGHT);
			typeof(Server.Core.Entities.ToolModel).GetField("BatteryVoltage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.VOLT);
			typeof(Server.Core.Entities.ToolModel).GetField("MaxRotationSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.TURN);
			typeof(Server.Core.Entities.ToolModel).GetField("AirConsumption", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AIR);
			typeof(Server.Core.Entities.ToolModel).GetField("SwitchOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.KINDID);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveSize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MEAID);
			typeof(Server.Core.Entities.ToolModel).GetField("DriveType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DRIVEID);
			typeof(Server.Core.Entities.ToolModel).GetField("ShutOff", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SWITCHID);
			typeof(Server.Core.Entities.ToolModel).GetField("ConstructionType", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.FORMID);
			typeof(Server.Core.Entities.ToolModel).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
		}

		// ToolModelToDbToolModel.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> MODELID
			// PropertyMapping: Description -> MODEL
			// PropertyMapping: Manufacturer -> MANUID
			// PropertyMapping: ModelType -> TYPEID
			// PropertyMapping: Class -> CLASSID
			// PropertyMapping: MinPower -> LIMITLO
			// PropertyMapping: MaxPower -> LIMITHI
			// PropertyMapping: AirPressure -> PRESSURE
			// PropertyMapping: ToolType -> VERSIONID
			// PropertyMapping: Weight -> WEIGHT
			// PropertyMapping: BatteryVoltage -> VOLT
			// PropertyMapping: MaxRotationSpeed -> TURN
			// PropertyMapping: AirConsumption -> AIR
			// PropertyMapping: SwitchOff -> KINDID
			// PropertyMapping: DriveSize -> MEAID
			// PropertyMapping: DriveType -> DRIVEID
			// PropertyMapping: ShutOff -> SWITCHID
			// PropertyMapping: ConstructionType -> FORMID
			// PropertyMapping: Alive -> ALIVE
		public DbEntities.ToolModel DirectPropertyMapping(Server.Core.Entities.ToolModel source)
		{
			var target = new DbEntities.ToolModel();
			_assigner.Assign((value) => {target.MODELID = value;}, source.Id);
			_assigner.Assign((value) => {target.MODEL = value;}, source.Description);
			_assigner.Assign((value) => {target.MANUID = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.TYPEID = value;}, source.ModelType);
			_assigner.Assign((value) => {target.CLASSID = value;}, source.Class);
			_assigner.Assign((value) => {target.LIMITLO = value;}, source.MinPower);
			_assigner.Assign((value) => {target.LIMITHI = value;}, source.MaxPower);
			_assigner.Assign((value) => {target.PRESSURE = value;}, source.AirPressure);
			_assigner.Assign((value) => {target.VERSIONID = value;}, source.ToolType);
			_assigner.Assign((value) => {target.WEIGHT = value;}, source.Weight);
			_assigner.Assign((value) => {target.VOLT = value;}, source.BatteryVoltage);
			_assigner.Assign((value) => {target.TURN = value;}, source.MaxRotationSpeed);
			_assigner.Assign((value) => {target.AIR = value;}, source.AirConsumption);
			_assigner.Assign((value) => {target.KINDID = value;}, source.SwitchOff);
			_assigner.Assign((value) => {target.MEAID = value;}, source.DriveSize);
			_assigner.Assign((value) => {target.DRIVEID = value;}, source.DriveType);
			_assigner.Assign((value) => {target.SWITCHID = value;}, source.ShutOff);
			_assigner.Assign((value) => {target.FORMID = value;}, source.ConstructionType);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ToolModel source, DbEntities.ToolModel target)
		{
			_assigner.Assign((value) => {target.MODELID = value;}, source.Id);
			_assigner.Assign((value) => {target.MODEL = value;}, source.Description);
			_assigner.Assign((value) => {target.MANUID = value;}, source.Manufacturer);
			_assigner.Assign((value) => {target.TYPEID = value;}, source.ModelType);
			_assigner.Assign((value) => {target.CLASSID = value;}, source.Class);
			_assigner.Assign((value) => {target.LIMITLO = value;}, source.MinPower);
			_assigner.Assign((value) => {target.LIMITHI = value;}, source.MaxPower);
			_assigner.Assign((value) => {target.PRESSURE = value;}, source.AirPressure);
			_assigner.Assign((value) => {target.VERSIONID = value;}, source.ToolType);
			_assigner.Assign((value) => {target.WEIGHT = value;}, source.Weight);
			_assigner.Assign((value) => {target.VOLT = value;}, source.BatteryVoltage);
			_assigner.Assign((value) => {target.TURN = value;}, source.MaxRotationSpeed);
			_assigner.Assign((value) => {target.AIR = value;}, source.AirConsumption);
			_assigner.Assign((value) => {target.KINDID = value;}, source.SwitchOff);
			_assigner.Assign((value) => {target.MEAID = value;}, source.DriveSize);
			_assigner.Assign((value) => {target.DRIVEID = value;}, source.DriveType);
			_assigner.Assign((value) => {target.SWITCHID = value;}, source.ShutOff);
			_assigner.Assign((value) => {target.FORMID = value;}, source.ConstructionType);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
		}

		public DbEntities.ToolModel ReflectedPropertyMapping(Server.Core.Entities.ToolModel source)
		{
			var target = new DbEntities.ToolModel();
			typeof(DbEntities.ToolModel).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ToolModel).GetField("MODEL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.ToolModel).GetField("MANUID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DbEntities.ToolModel).GetField("TYPEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(DbEntities.ToolModel).GetField("CLASSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
			typeof(DbEntities.ToolModel).GetField("LIMITLO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(DbEntities.ToolModel).GetField("LIMITHI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(DbEntities.ToolModel).GetField("PRESSURE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(DbEntities.ToolModel).GetField("VERSIONID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(DbEntities.ToolModel).GetField("WEIGHT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(DbEntities.ToolModel).GetField("VOLT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(DbEntities.ToolModel).GetField("TURN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(DbEntities.ToolModel).GetField("AIR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(DbEntities.ToolModel).GetField("KINDID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(DbEntities.ToolModel).GetField("MEAID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(DbEntities.ToolModel).GetField("DRIVEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(DbEntities.ToolModel).GetField("SWITCHID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(DbEntities.ToolModel).GetField("FORMID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(DbEntities.ToolModel).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ToolModel source, DbEntities.ToolModel target)
		{
			typeof(DbEntities.ToolModel).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ToolModel).GetField("MODEL", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.ToolModel).GetField("MANUID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Manufacturer);
			typeof(DbEntities.ToolModel).GetField("TYPEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ModelType);
			typeof(DbEntities.ToolModel).GetField("CLASSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Class);
			typeof(DbEntities.ToolModel).GetField("LIMITLO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MinPower);
			typeof(DbEntities.ToolModel).GetField("LIMITHI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxPower);
			typeof(DbEntities.ToolModel).GetField("PRESSURE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirPressure);
			typeof(DbEntities.ToolModel).GetField("VERSIONID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolType);
			typeof(DbEntities.ToolModel).GetField("WEIGHT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Weight);
			typeof(DbEntities.ToolModel).GetField("VOLT", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.BatteryVoltage);
			typeof(DbEntities.ToolModel).GetField("TURN", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.MaxRotationSpeed);
			typeof(DbEntities.ToolModel).GetField("AIR", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AirConsumption);
			typeof(DbEntities.ToolModel).GetField("KINDID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SwitchOff);
			typeof(DbEntities.ToolModel).GetField("MEAID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveSize);
			typeof(DbEntities.ToolModel).GetField("DRIVEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.DriveType);
			typeof(DbEntities.ToolModel).GetField("SWITCHID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ShutOff);
			typeof(DbEntities.ToolModel).GetField("FORMID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConstructionType);
			typeof(DbEntities.ToolModel).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// ToolToDbTool.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> SEQID
			// PropertyMapping: SerialNumber -> SERIALNO
			// PropertyMapping: InventoryNumber -> USERNO
			// PropertyMapping: ToolModel -> MODELID
			// PropertyMapping: Status -> STATEID
			// PropertyMapping: Accessory -> PTACCESS
			// PropertyMapping: ConfigurableField -> ORDERID
			// PropertyMapping: CostCenter -> KOSTID
			// PropertyMapping: AdditionalConfigurableField1 -> FREE_STR1
			// PropertyMapping: AdditionalConfigurableField2 -> FREE_STR2
			// PropertyMapping: AdditionalConfigurableField3 -> FREE_STR3
			// PropertyMapping: Alive -> ALIVE
		public DbEntities.Tool DirectPropertyMapping(Server.Core.Entities.Tool source)
		{
			var target = new DbEntities.Tool();
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.SERIALNO = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.USERNO = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.MODELID = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.STATEID = value;}, source.Status);
			_assigner.Assign((value) => {target.PTACCESS = value;}, source.Accessory);
			_assigner.Assign((value) => {target.ORDERID = value;}, source.ConfigurableField);
			_assigner.Assign((value) => {target.KOSTID = value;}, source.CostCenter);
			_assigner.Assign((value) => {target.FREE_STR1 = value;}, source.AdditionalConfigurableField1);
			_assigner.Assign((value) => {target.FREE_STR2 = value;}, source.AdditionalConfigurableField2);
			_assigner.Assign((value) => {target.FREE_STR3 = value;}, source.AdditionalConfigurableField3);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.Tool source, DbEntities.Tool target)
		{
			_assigner.Assign((value) => {target.SEQID = value;}, source.Id);
			_assigner.Assign((value) => {target.SERIALNO = value;}, source.SerialNumber);
			_assigner.Assign((value) => {target.USERNO = value;}, source.InventoryNumber);
			_assigner.Assign((value) => {target.MODELID = value;}, source.ToolModel);
			_assigner.Assign((value) => {target.STATEID = value;}, source.Status);
			_assigner.Assign((value) => {target.PTACCESS = value;}, source.Accessory);
			_assigner.Assign((value) => {target.ORDERID = value;}, source.ConfigurableField);
			_assigner.Assign((value) => {target.KOSTID = value;}, source.CostCenter);
			_assigner.Assign((value) => {target.FREE_STR1 = value;}, source.AdditionalConfigurableField1);
			_assigner.Assign((value) => {target.FREE_STR2 = value;}, source.AdditionalConfigurableField2);
			_assigner.Assign((value) => {target.FREE_STR3 = value;}, source.AdditionalConfigurableField3);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
		}

		public DbEntities.Tool ReflectedPropertyMapping(Server.Core.Entities.Tool source)
		{
			var target = new DbEntities.Tool();
			typeof(DbEntities.Tool).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Tool).GetField("SERIALNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DbEntities.Tool).GetField("USERNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DbEntities.Tool).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(DbEntities.Tool).GetField("STATEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DbEntities.Tool).GetField("PTACCESS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(DbEntities.Tool).GetField("ORDERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(DbEntities.Tool).GetField("KOSTID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(DbEntities.Tool).GetField("FREE_STR1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(DbEntities.Tool).GetField("FREE_STR2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(DbEntities.Tool).GetField("FREE_STR3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
			typeof(DbEntities.Tool).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.Tool source, DbEntities.Tool target)
		{
			typeof(DbEntities.Tool).GetField("SEQID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.Tool).GetField("SERIALNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.SerialNumber);
			typeof(DbEntities.Tool).GetField("USERNO", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.InventoryNumber);
			typeof(DbEntities.Tool).GetField("MODELID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ToolModel);
			typeof(DbEntities.Tool).GetField("STATEID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Status);
			typeof(DbEntities.Tool).GetField("PTACCESS", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Accessory);
			typeof(DbEntities.Tool).GetField("ORDERID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ConfigurableField);
			typeof(DbEntities.Tool).GetField("KOSTID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.CostCenter);
			typeof(DbEntities.Tool).GetField("FREE_STR1", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField1);
			typeof(DbEntities.Tool).GetField("FREE_STR2", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField2);
			typeof(DbEntities.Tool).GetField("FREE_STR3", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AdditionalConfigurableField3);
			typeof(DbEntities.Tool).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// ToolUsageDbToToolUsage.txt
			// hasDefaultConstructor
			// PropertyMapping: POSID -> Id
			// PropertyMapping: POSNAME -> Description
			// PropertyMapping: ALIVE -> Alive
		public Server.Core.Entities.ToolUsage DirectPropertyMapping(DbEntities.ToolUsage source)
		{
			var target = new Server.Core.Entities.ToolUsage();
			_assigner.Assign((value) => {target.Id = value;}, source.POSID);
			_assigner.Assign((value) => {target.Description = value;}, source.POSNAME);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
			return target;
		}

		public void DirectPropertyMapping(DbEntities.ToolUsage source, Server.Core.Entities.ToolUsage target)
		{
			_assigner.Assign((value) => {target.Id = value;}, source.POSID);
			_assigner.Assign((value) => {target.Description = value;}, source.POSNAME);
			_assigner.Assign((value) => {target.Alive = value;}, source.ALIVE);
		}

		public Server.Core.Entities.ToolUsage ReflectedPropertyMapping(DbEntities.ToolUsage source)
		{
			var target = new Server.Core.Entities.ToolUsage();
			typeof(Server.Core.Entities.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSID);
			typeof(Server.Core.Entities.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSNAME);
			typeof(Server.Core.Entities.ToolUsage).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
			return target;
		}

		public void ReflectedPropertyMapping(DbEntities.ToolUsage source, Server.Core.Entities.ToolUsage target)
		{
			typeof(Server.Core.Entities.ToolUsage).GetField("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSID);
			typeof(Server.Core.Entities.ToolUsage).GetField("Description", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.POSNAME);
			typeof(Server.Core.Entities.ToolUsage).GetField("Alive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.ALIVE);
		}

		// ToolUsageToToolUsageDb.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> POSID
			// PropertyMapping: Description -> POSNAME
			// PropertyMapping: Alive -> ALIVE
		public DbEntities.ToolUsage DirectPropertyMapping(Server.Core.Entities.ToolUsage source)
		{
			var target = new DbEntities.ToolUsage();
			_assigner.Assign((value) => {target.POSID = value;}, source.Id);
			_assigner.Assign((value) => {target.POSNAME = value;}, source.Description);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.ToolUsage source, DbEntities.ToolUsage target)
		{
			_assigner.Assign((value) => {target.POSID = value;}, source.Id);
			_assigner.Assign((value) => {target.POSNAME = value;}, source.Description);
			_assigner.Assign((value) => {target.ALIVE = value;}, source.Alive);
		}

		public DbEntities.ToolUsage ReflectedPropertyMapping(Server.Core.Entities.ToolUsage source)
		{
			var target = new DbEntities.ToolUsage();
			typeof(DbEntities.ToolUsage).GetField("POSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ToolUsage).GetField("POSNAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.ToolUsage).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.ToolUsage source, DbEntities.ToolUsage target)
		{
			typeof(DbEntities.ToolUsage).GetField("POSID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.ToolUsage).GetField("POSNAME", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.ToolUsage).GetField("ALIVE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Alive);
		}

		// WorkingCalendarEntryToDbWorkingCalendarEntry.txt
			// hasDefaultConstructor
			// PropertyMapping: Date -> FDDate
			// PropertyMapping: Description -> Name
			// PropertyMapping: Repetition -> Repeat
			// PropertyMapping: Type -> IsFree
		public DbEntities.WorkingCalendarEntry DirectPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source)
		{
			var target = new DbEntities.WorkingCalendarEntry();
			_assigner.Assign((value) => {target.FDDate = value;}, source.Date);
			_assigner.Assign((value) => {target.Name = value;}, source.Description);
			_assigner.Assign((value) => {target.Repeat = value;}, source.Repetition);
			_assigner.Assign((value) => {target.IsFree = value;}, source.Type);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source, DbEntities.WorkingCalendarEntry target)
		{
			_assigner.Assign((value) => {target.FDDate = value;}, source.Date);
			_assigner.Assign((value) => {target.Name = value;}, source.Description);
			_assigner.Assign((value) => {target.Repeat = value;}, source.Repetition);
			_assigner.Assign((value) => {target.IsFree = value;}, source.Type);
		}

		public DbEntities.WorkingCalendarEntry ReflectedPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source)
		{
			var target = new DbEntities.WorkingCalendarEntry();
			typeof(DbEntities.WorkingCalendarEntry).GetField("FDDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(DbEntities.WorkingCalendarEntry).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.WorkingCalendarEntry).GetField("Repeat", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repetition);
			typeof(DbEntities.WorkingCalendarEntry).GetField("IsFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.WorkingCalendarEntry source, DbEntities.WorkingCalendarEntry target)
		{
			typeof(DbEntities.WorkingCalendarEntry).GetField("FDDate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Date);
			typeof(DbEntities.WorkingCalendarEntry).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Description);
			typeof(DbEntities.WorkingCalendarEntry).GetField("Repeat", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Repetition);
			typeof(DbEntities.WorkingCalendarEntry).GetField("IsFree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Type);
		}

		// WorkingCalendarToDbWorkingCalendar.txt
			// hasDefaultConstructor
			// PropertyMapping: Id -> SeqId
			// PropertyMapping: Name -> Name
			// PropertyMapping: AreSaturdaysFree -> FdSat
			// PropertyMapping: AreSundaysFree -> FdSun
		public DbEntities.WorkingCalendar DirectPropertyMapping(Server.Core.Entities.WorkingCalendar source)
		{
			var target = new DbEntities.WorkingCalendar();
			_assigner.Assign((value) => {target.SeqId = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.FdSat = value;}, source.AreSaturdaysFree);
			_assigner.Assign((value) => {target.FdSun = value;}, source.AreSundaysFree);
			return target;
		}

		public void DirectPropertyMapping(Server.Core.Entities.WorkingCalendar source, DbEntities.WorkingCalendar target)
		{
			_assigner.Assign((value) => {target.SeqId = value;}, source.Id);
			_assigner.Assign((value) => {target.Name = value;}, source.Name);
			_assigner.Assign((value) => {target.FdSat = value;}, source.AreSaturdaysFree);
			_assigner.Assign((value) => {target.FdSun = value;}, source.AreSundaysFree);
		}

		public DbEntities.WorkingCalendar ReflectedPropertyMapping(Server.Core.Entities.WorkingCalendar source)
		{
			var target = new DbEntities.WorkingCalendar();
			typeof(DbEntities.WorkingCalendar).GetField("SeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.WorkingCalendar).GetField("FdSat", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSaturdaysFree);
			typeof(DbEntities.WorkingCalendar).GetField("FdSun", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSundaysFree);
			return target;
		}

		public void ReflectedPropertyMapping(Server.Core.Entities.WorkingCalendar source, DbEntities.WorkingCalendar target)
		{
			typeof(DbEntities.WorkingCalendar).GetField("SeqId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Id);
			typeof(DbEntities.WorkingCalendar).GetField("Name", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.Name);
			typeof(DbEntities.WorkingCalendar).GetField("FdSat", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSaturdaysFree);
			typeof(DbEntities.WorkingCalendar).GetField("FdSun", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, source.AreSundaysFree);
		}

	}
}

////////////
//

