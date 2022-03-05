using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases;

namespace InterfaceAdapters
{
    public interface IClassicTestInterface : INotifyPropertyChanged
    {
        ObservableCollection<PowToolClassicTestHumbleModel> PowToolClassicTests { get; }
        ObservableCollection<MfuHeaderClassicTestHumbleModel> MfuHeaderClassicTests { get; }
        ObservableCollection<ChkHeaderClassicTestHumbleModel> ChkHeaderClassicTests { get; }
        ObservableCollection<ProcessHeaderClassicTestHumbleModel> ProcessHeaderClassicTests { get; }
    }

    public class ClassicTestInterfaceAdapter : BindableBase, IClassicTestGui, IClassicTestInterface
    {
        public void ShowToolsForSelectedLocation(Dictionary<Tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive)> tools)
        {
            if (tools != null)
            {
                PowToolClassicTests = new ObservableCollection<PowToolClassicTestHumbleModel>(tools.Select(x => new PowToolClassicTestHumbleModel(x.Key, x.Value)));
            }
            else
            {
                PowToolClassicTests = new ObservableCollection<PowToolClassicTestHumbleModel>();
            }
        }

        public void ShowProcessHeaderForSelectedLocation(List<ClassicProcessTest> header)
        {
            ProcessHeaderClassicTests = new ObservableCollection<ProcessHeaderClassicTestHumbleModel>(header.Select(x => new ProcessHeaderClassicTestHumbleModel(x)));
        }

        public void ShowMfuHeaderForSelectedTool(List<ClassicMfuTest> header)
        {
            MfuHeaderClassicTests = new ObservableCollection<MfuHeaderClassicTestHumbleModel>(header.Select(x => new MfuHeaderClassicTestHumbleModel(x)));
        }

        public void ShowChkHeaderForSelectedTool(List<ClassicChkTest> header)
        {
            ChkHeaderClassicTests = new ObservableCollection<ChkHeaderClassicTestHumbleModel>(header.Select(x => new ChkHeaderClassicTestHumbleModel(x)));
        }

        private ObservableCollection<ProcessHeaderClassicTestHumbleModel> _processHeaderClassicTests = new ObservableCollection<ProcessHeaderClassicTestHumbleModel>();
        public ObservableCollection<ProcessHeaderClassicTestHumbleModel> ProcessHeaderClassicTests
        {
            get => _processHeaderClassicTests;
            private set
            {
                _processHeaderClassicTests = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<MfuHeaderClassicTestHumbleModel> _mfuHeaderClassicTests = new ObservableCollection<MfuHeaderClassicTestHumbleModel>();
        public ObservableCollection<MfuHeaderClassicTestHumbleModel> MfuHeaderClassicTests
        {
            get => _mfuHeaderClassicTests;
            private set
            {
                _mfuHeaderClassicTests = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ChkHeaderClassicTestHumbleModel> _chkHeaderClassicTests = new ObservableCollection<ChkHeaderClassicTestHumbleModel>();
        public ObservableCollection<ChkHeaderClassicTestHumbleModel> ChkHeaderClassicTests
        {
            get => _chkHeaderClassicTests;
            private set
            {
                _chkHeaderClassicTests = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PowToolClassicTestHumbleModel> _powToolClassicTests = new ObservableCollection<PowToolClassicTestHumbleModel>();
        public ObservableCollection<PowToolClassicTestHumbleModel> PowToolClassicTests
        {
            get => _powToolClassicTests;
            private set
            {
                _powToolClassicTests = value;
                RaisePropertyChanged();
            }
        }  
    }

    public class PowToolClassicTestHumbleModel : BindableBase
    {
        public bool IsToolAssignmentActive { get; set; }
        public DateTime? FirstTest { get; set; }
        public DateTime? LastTest { get; set; }

        private readonly Tool _tool;
        public Tool Entity { get => _tool; }

        public string InvNo { get => _tool.InventoryNumber?.ToDefaultString(); }
        public string SerNo { get => _tool.SerialNumber?.ToDefaultString(); }

        public string Model { get => _tool.ToolModel?.Description?.ToDefaultString(); }

        public PowToolClassicTestHumbleModel(Tool tool, (DateTime? firsttest, DateTime? lasttest, bool isToolAssignmentActive) testranges)
        {
            _tool = tool;
            FirstTest = testranges.firsttest;
            LastTest = testranges.lasttest;
            IsToolAssignmentActive = testranges.isToolAssignmentActive;
        }
    }


    public class MfuHeaderClassicTestHumbleModel : BindableBase
    {
        public ClassicMfuTest Entity { get => _classicMfuTest; }
        private ClassicMfuTest _classicMfuTest;

        public MfuHeaderClassicTestHumbleModel(ClassicMfuTest classicMfuTest)
        {
            _classicMfuTest = classicMfuTest;
        }

        public long BatchSize { get => _classicMfuTest.NumberOfTests; }
        public double NominalTorque { get => _classicMfuTest.GetNominalValueByMeaUnit(MeaUnit.Nm); }
        public double NominalAngle { get => _classicMfuTest.GetNominalValueByMeaUnit(MeaUnit.Deg); }
        public DateTime Timestamp { get => _classicMfuTest.Timestamp; }

    }

    public class ProcessHeaderClassicTestHumbleModel : BindableBase
    {
        public ClassicProcessTest Entity { get => _classicProcessTest; }
        private ClassicProcessTest _classicProcessTest;

        public ProcessHeaderClassicTestHumbleModel(ClassicProcessTest classicProcessTest)
        {
            _classicProcessTest = classicProcessTest;
        }

        public long BatchSize { get => _classicProcessTest.NumberOfTests; }
        public double LowerToleranceLimit { get => _classicProcessTest.LowerLimit; }
        public double UpperToleranceLimit { get => _classicProcessTest.UpperLimit; }
        public DateTime Timestamp { get => _classicProcessTest.Timestamp; }
    }

    public class ChkHeaderClassicTestHumbleModel : BindableBase
    {
        public ClassicChkTest Entity { get => _classicChkTest; }
        private ClassicChkTest _classicChkTest;

        public ChkHeaderClassicTestHumbleModel(ClassicChkTest classicChkTest)
        {
            _classicChkTest = classicChkTest;
        }

        public long BatchSize { get => _classicChkTest.NumberOfTests; }
        public double NominalTorque { get => _classicChkTest.GetNominalValueByMeaUnit(MeaUnit.Nm); }
        public double NominalAngle { get => _classicChkTest.GetNominalValueByMeaUnit(MeaUnit.Deg); }
        public DateTime Timestamp { get => _classicChkTest.Timestamp; }
    }
}
