using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Client.Core.Entities;
using Core.Entities;
using Core.UseCases.Communication;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters.Communication
{
    public interface ITestEquipmentInterface: INotifyPropertyChanged
    {
        TestEquipmentHumbleModel SelectedTestEquipment { get; set; }
        TestEquipmentHumbleModel SelectedTestEquipmentWithoutChanges { get; set; }
        TestEquipmentModelHumbleModel SelectedTestEquipmentModel { get; set; }
        TestEquipmentModelHumbleModel SelectedTestEquipmentModelWithoutChanges { get; set; }
        ObservableCollection<TestEquipmentModelHumbleModel> TestEquipmentModels { get; }
        ObservableCollection<DataGateVersion> DataGateVersions { get; set; }
        ObservableCollection<TestEquipmentHumbleModel> TestEquipments { get; }
        void SetDispatcher(Dispatcher dispatcher);

        event EventHandler<bool> ShowLoadingControlRequest;
        event EventHandler<TestEquipmentHumbleModel> SelectionRequestTestEquipment;
    }

    public class TestEquipmentInterfaceAdapter: BindableBase, ITestEquipmentGui, ITestEquipmentInterface
    {
        private readonly ILocalizationWrapper _localization;
        private Dispatcher _guiDispatcher;

        public TestEquipmentInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
            DataGateVersions = new ObservableCollection<DataGateVersion>();
            foreach (var version in Client.Core.Entities.DataGateVersion.DataGateVersions)
            {
                DataGateVersions.Add(new DataGateVersion(version.Key));
            }
        }

        public void UpdateTestEquipment(TestEquipment testEquipment)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var item in TestEquipments)
                {
                    if (item.Entity.EqualsById(testEquipment))
                    {
                        item.UpdateWith(testEquipment);
                    }
                }

                if (SelectedTestEquipment?.Entity.EqualsById(testEquipment) ?? false)
                {
                    SelectedTestEquipmentWithoutChanges.UpdateWith(testEquipment);
                }
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }

        public void ShowTestEquipments(List<Core.Entities.TestEquipmentModel> testEquipmentModels)
        {
            _guiDispatcher.Invoke(() =>
            {
                TestEquipments.Clear();
                TestEquipmentModels.Clear();

                foreach (var model in testEquipmentModels)
                {
                    var modelModel = TestEquipmentModelHumbleModel.GetModelFor(model, _localization);
                    TestEquipmentModels.Add(modelModel);
                    if (model.TestEquipments == null || model.TestEquipments.Count <= 0) 
                        continue;

                    foreach (var t in model.TestEquipments)
                    {
                        TestEquipments.Add(TestEquipmentHumbleModel.GetModelFor(t, _localization));
                    }
                }
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }

        public void UpdateTestEquipmentModel(TestEquipmentModel testEquipmentModel)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var item in TestEquipmentModels)
                {
                    if (item.Entity.EqualsById(testEquipmentModel))
                    {
                        item.UpdateWith(testEquipmentModel);
                    }
                }

                if (SelectedTestEquipmentModel?.Entity.EqualsById(testEquipmentModel) ?? false)
                {
                    SelectedTestEquipmentModelWithoutChanges.UpdateWith(testEquipmentModel);
                }
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }

        public void RemoveTestEquipment(TestEquipment removedTestEquipment)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var testEquipment in TestEquipments)
                {
                    if (!testEquipment.Entity.EqualsById(removedTestEquipment)) 
                        continue;

                    TestEquipments.Remove(testEquipment);
                    break;
                }
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }

        public void AddTestEquipment(TestEquipment testEquipment)
        {
            _guiDispatcher.Invoke(() =>
            {
                var model = TestEquipmentModels.SingleOrDefault(x => x?.Entity?.Id.ToLong() == testEquipment.TestEquipmentModel?.Id?.ToLong());
                if (model != null)
                {
                   model.UpdateWith(testEquipment.TestEquipmentModel);
                   testEquipment.TestEquipmentModel = model.Entity;
                }

                var testEquipmentHumbleModel = TestEquipmentHumbleModel.GetModelFor(testEquipment, _localization);
                TestEquipments.Add(testEquipmentHumbleModel);
                SelectedTestEquipmentModel = null;
                SelectedTestEquipment = testEquipmentHumbleModel;
                SelectionRequestTestEquipment?.Invoke(null, testEquipmentHumbleModel);
            });
        }

        public void LoadTestEquipmentModels(List<TestEquipmentModel> testEquipmentModels)
        {
            TestEquipmentModels = new ObservableCollection<TestEquipmentModelHumbleModel>(testEquipmentModels.Select(x => TestEquipmentModelHumbleModel.GetModelFor(x, _localization)));
            foreach (var testEquipment in TestEquipments)
            {
                var model = TestEquipmentModels.Select(x => x.Entity)
                    .SingleOrDefault(x => x.Id.Equals(testEquipment.Entity.TestEquipmentModel.Id));

                if (model != null)
                {
                    testEquipment.Entity.TestEquipmentModel = model;
                }
            }
        }


        private TestEquipmentHumbleModel _selectedTestEquipment;
        public TestEquipmentHumbleModel SelectedTestEquipment
        {
            get => _selectedTestEquipment;
            set
            {
                _selectedTestEquipment = value;
                RaisePropertyChanged();
                SelectedTestEquipmentWithoutChanges = _selectedTestEquipment?.CopyDeep();
            }
        }

        private TestEquipmentHumbleModel _selectedTestEquipmentWithoutChanges;
        public TestEquipmentHumbleModel SelectedTestEquipmentWithoutChanges
        {
            get => _selectedTestEquipmentWithoutChanges;
            set
            {
                _selectedTestEquipmentWithoutChanges = value;
                RaisePropertyChanged();
            }
        }

        private TestEquipmentModelHumbleModel _selectedTestEquipmentModel;
        public TestEquipmentModelHumbleModel SelectedTestEquipmentModel
        {
            get => _selectedTestEquipmentModel;
            set
            {
                _selectedTestEquipmentModel = value;
                RaisePropertyChanged();
                SelectedTestEquipmentModelWithoutChanges = _selectedTestEquipmentModel?.CopyDeep();
            }
        }

        private TestEquipmentModelHumbleModel _selectedTestEquipmentModelWithoutChanges;
        public TestEquipmentModelHumbleModel SelectedTestEquipmentModelWithoutChanges
        {
            get => _selectedTestEquipmentModelWithoutChanges;
            set
            {
                _selectedTestEquipmentModelWithoutChanges = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<TestEquipmentModelHumbleModel> _testEquipmentModels = new ObservableCollection<TestEquipmentModelHumbleModel>();
        public ObservableCollection<TestEquipmentModelHumbleModel> TestEquipmentModels
        {
            get => _testEquipmentModels;
            private set
            {
                _testEquipmentModels = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<DataGateVersion> DataGateVersions { get; set; }
        public ObservableCollection<TestEquipmentHumbleModel> TestEquipments { get; } = new ObservableCollection<TestEquipmentHumbleModel>();

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }

        public event EventHandler<bool> ShowLoadingControlRequest;
        public event EventHandler<TestEquipmentHumbleModel> SelectionRequestTestEquipment;
    }
}
