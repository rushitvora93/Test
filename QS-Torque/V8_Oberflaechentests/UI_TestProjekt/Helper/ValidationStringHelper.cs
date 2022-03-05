namespace UI_TestProjekt.Helper
{
    public static class ValidationStringHelper
    {
        public static class LoginValidationStrings
        {
            public const string txtServerEn = "Server Connection";
            public const string txtServerGer = "Serververbindung";
        }
        public static class GlobalToolBarValidationStrings
        {
            public const string AboutQSTEn = "About QS-Torque";
            public const string AboutQSTGer = "Über QS-Torque";
        }
        public static class MpValidationStrings
        {
            public static class MpAssist
            {
                public const string NrUniqueAndRequired = "The location number is required and has to be unique";
                public const string DescriptionRequired = "The location description is required and has to be unique";
                public const string ThresholdGreaterZeroLessEqualSetpointTorque = "The threshold torque has to be greater than 0 and less than or equal to the setpoint";
                public const string MinTorqueLessEqualSetpointTorque = "The minimum torque has to be less than or equal to the setpoint torque";
                public const string MaxTorqueGreaterEqualSetpointTorque = "The maximum torque has to be greater than or equal the setpoint torque";
                public const string MinAngleLessEqualSetpointAngle = "The minimum angle has to be less than or equal to the setpoint angle";
                public const string MaxAngleGreaterEqualSetpointAngle = "The maximum angle has to be greater than or equal to the setpoint angle";
                public const string SetpointTorqueGreaterZero = "The setpoint torque has to be greater than 0";
                public const string SetpointAngleGreaterZero = "The setpoint angle has to be greater than 0";
                public const string ThresholdTorqueGreaterZero = "The threshold torque has to be greater than 0";
            }
            public static class MpValidation
            {
                public const string NrUniqueAndRequired = "The location number is required and has to be unique";
                public const string DescriptionRequired = "The location description is required and has to be unique";
                public const string ThresholdLessEqualSetpointTorque = "The threshold torque has to be less than or equal to the setpoint torque";
                public const string MinTorqueLessEqualSetpointTorque = "The minimum torque has to be less than or equal to the setpoint torque";
                public const string MaxTorqueGreaterEqualSetpointTorque = "The maximum torque has to be greater than or equal to the setpoint torque";
                public const string MinAngleLessEqualSetpointAngle = "The minimum angle has to be less than or equal to the setpoint angle";
                public const string MaxAngleGreaterEqualSetpointAngle = "The maximum angle has to be greater than or equal to the setpoint angle";
                public const string SetpointTorqueGreaterZero = "The setpoint torque has to be greater than 0";
                public const string SetpointAngleGreaterZero = "The setpoint angle has to be greater than 0";
                public const string ThresholdTorqueGreaterZero = "The threshold torque has to be greater than 0";

                public const string MpNotValidOnChange = "The measurement point is not valid, do you want to continue editing? (If not, the measurement point is reseted to the last saved value)";
            }
        }
        public static class ToolValidationStrings
        {
            public const string SerialNumberUnique = "The serial number is a required field and has to be unique";
            public const string InventoryNumberUnique = "The inventory number is a required field and has to be unique";
        }

        public static class ToastNotificationStrings
        {
            public const string SenderQST = "QS-Torque";
            public const string ActionSuccess = "Action was successful";
            public const string ToolTestsCalcSuccess = "The tool test dates have been calculated successfully";
            public const string ProcessTestsCalcSuccess = "The process control dates have been calculated successfully";
        }
        public static class GeneralValidationStrings
        {
            public const string DoYouWantToSaveChanges = "Do you want to save your changes? This change will affect the references.";
            public const string LanguageEn = "English";
            public const string LanguageEnShort = "en-US";
            public const string LanguageGer = "Deutsch";
            public const string LanguageGerShort = "de-DE";

            public const string FieldRequiredUnique = "This field is required and has to be unique";
            public const string Ok = "✓";
            public const string Nok = "✗";
        }

        public static class DefaultStatus
        {
            public const string InBetrieb = "In Betrieb";
            public const string ImLager = "Im Lager";
            public const string ZurReparatur = "Zur Reparatur";
        }
    }
}
