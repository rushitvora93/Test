using Core.Entities;
using Core.UseCases.Communication.DataGate;
using System;
using System.Collections.Generic;
using Core.Enums;
using Client.Core.Entities;

namespace Core.UseCases.Communication
{
    namespace DataGate
    {
        public class SemanticModel
        {
            public SemanticModel(IElement root)
            {
                _root = root;
            }

            public void Accept(IElementVisitor visitor)
            {
                _root.Accept(visitor);
            }

            private IElement _root;
        }

        public interface IElement
        {
            void Accept(IElementVisitor visitor);
            ElementName GetName();
        }

        public interface IElementVisitor
        {
            void Visit(Content element);
            void Visit(Container element);
            void Visit(HiddenContent element);
        }

        public interface ISemanticModelRewriter
        {
            void Apply(ref SemanticModel dataGateSemanticModel);
        }

        public interface ISemanticModelRewriterBuilder
        {
            ISemanticModelRewriter Build(TestEquipment testEquipment);
        }
    }

    public interface ISemanticModelFactory
    {
        SemanticModel Convert(TestEquipment testEquipment, List<LocationToolAssignment> route, (double cm, double cmk) cmCmk, DateTime localNow, TestType testType);
        SemanticModel Convert(TestEquipment testEquipment, List<Location> locations, List<ProcessControlCondition> processControls, DateTime localNow);
        SemanticModel ReadCommand(TestEquipment testEquipment, DateTime timestamp);
        SemanticModel ClearCommand(TestEquipment testEquipment, DateTime timestamp);
    }
}