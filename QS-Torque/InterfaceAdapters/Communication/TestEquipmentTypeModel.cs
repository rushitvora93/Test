using System;
using Common.Types.Enums;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Communication
{
    public class TestEquipmentTypeModel : BindableBase, IEquatable<TestEquipmentTypeModel>
    {
        private readonly ILocalizationWrapper _localization;
        public TestEquipmentType Type { get; }

        public TestEquipmentTypeModel(ILocalizationWrapper localization, TestEquipmentType type)
        {
            _localization = localization;
            Type = type;
        }

        public string TranslatedName => GetTranslationForTestEquipmentType(Type, _localization);

        public bool Equals(TestEquipmentTypeModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.EqualsByTestEquipmentType(other);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public bool EqualsByTestEquipmentType(TestEquipmentTypeModel other)
        {
            return this.Type.Equals(other?.Type);
        }

        public static string GetTranslationForTestEquipmentType(TestEquipmentType type, ILocalizationWrapper localization)
        {
            switch (type)
            {
                case TestEquipmentType.ManualInput:
                    return localization.Strings.GetParticularString("TestEquipmentType", "Manual input");
                case TestEquipmentType.Bench:
                    return localization.Strings.GetParticularString("TestEquipmentType", "Bench");
                case TestEquipmentType.Wrench:
                    return localization.Strings.GetParticularString("TestEquipmentType", "Test wrench");
                case TestEquipmentType.AcqTool:
                    return localization.Strings.GetParticularString("TestEquipmentType", "AcqTool");
                case TestEquipmentType.Analyse:
                    return localization.Strings.GetParticularString("TestEquipmentType", "Analyse device");
                case TestEquipmentType.TestTool:
                    return localization.Strings.GetParticularString("TestEquipmentType", "Test tool");
                default:
                    return "";
            }
        }
    }
}
