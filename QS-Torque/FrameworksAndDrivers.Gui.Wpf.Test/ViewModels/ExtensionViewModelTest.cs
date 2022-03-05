using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters;
using InterfaceAdapters.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.Core.Validator;
using Client.TestHelper.Factories;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    [TestFixture]
    public class ExtensionViewModelTest
    {
        public class ExtensionUseCaseMock : IExtensionUseCase
        {
            public bool ShowExtensionsCalled { get; set; }
            public IExtensionErrorGui ShowExtensionsParameter { get; set; }
            public bool ShowReferencedLocationsCalled { get; set; }
            public IExtensionErrorGui ShowRefencedLocationsGuiErrorParameter { get; set; }
            public ExtensionId ShowReferencedLocationsIdParameter { get; set; }
            public IExtensionErrorGui AddExtensionParameterError { get; set; }
            public Extension AddExtensionParameterExtension { get; set; }
            public bool IsInventoryNumberUniqueReturnValue { get; set; }
            public ExtensionInventoryNumber IsInventoryNumberUniqueParameterInventoryNumber { get; set; }
            public IExtensionSaveGuiShower SaveExtensionSaveGuiShower { get; set; }
            public IExtensionErrorGui SaveExtensionErrorHandler;
            public ExtensionDiff SaveExtensionDiff;
            public ExtensionDiff UpdateExtensionDiff;
            public IExtensionErrorGui UpdateExtensionError;
            public IExtensionErrorGui RemoveExtensionErrorHandler;
            public Extension RemoveExtensionExtension;
            public IExtensionDependencyGui RemoveExtensionDependencyGui;

            public void ShowExtensions(IExtensionErrorGui loadingError)
            {
                ShowExtensionsCalled = true;
                ShowExtensionsParameter = loadingError;
            }

            public void ShowReferencedLocations(IExtensionErrorGui loadingError, ExtensionId id)
            {
                ShowRefencedLocationsGuiErrorParameter = loadingError;
                ShowReferencedLocationsCalled = true;
                ShowReferencedLocationsIdParameter = id;
            }

            public void AddExtension(Extension extension, IExtensionErrorGui errorHandler)
            {
                AddExtensionParameterExtension = extension;
                AddExtensionParameterError = errorHandler;
            }

            public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
            {
                IsInventoryNumberUniqueParameterInventoryNumber = inventoryNumber;
                return IsInventoryNumberUniqueReturnValue;
            }

            public void SaveExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler, IExtensionSaveGuiShower saveGuiShower)
            {
                SaveExtensionDiff = diff;
                SaveExtensionErrorHandler = errorHandler;
                SaveExtensionSaveGuiShower = saveGuiShower;
            }

            public void UpdateExtension(ExtensionDiff diff, IExtensionErrorGui errorHandler)
            {
                UpdateExtensionDiff = diff;
                UpdateExtensionError = errorHandler;
            }

            public void RemoveExtension(Extension extension, IExtensionErrorGui errorHandler, IExtensionDependencyGui dependencyGui)
            {
                RemoveExtensionExtension = extension;
                RemoveExtensionErrorHandler = errorHandler;
                RemoveExtensionDependencyGui = dependencyGui;
            }
        }

        public class ExtensionValidatorMock : IExtensionValidator
        {
            public bool Validate(Extension extension)
            {
                ValidateParameter = extension;
                return ValidateReturnValue;
            }

            public bool ValidateReturnValue { get; set; }
            public Extension ValidateParameter { get; set; }
        }

        public class ExtensionInterfaceAdapterMock : IExtensionInterface
        {
            public ExtensionModel SelectedExtension { get; set; }
            public ExtensionModel SelectedExtensionWithoutChanges { get; set; }

            private ObservableCollection<ExtensionModel> _extensions = new ObservableCollection<ExtensionModel>();
            public ObservableCollection<ExtensionModel> Extensions
            {
                get => _extensions;
                set
                {
                    _extensions = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExtensionModel)));
                }
            }

            private ObservableCollection<LocationReferenceLink> _referenecedLocations;
            public ObservableCollection<LocationReferenceLink> ReferencedLocations
            {
                get => _referenecedLocations;
                set
                {
                    _referenecedLocations = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LocationReferenceLink)));
                }
            }

            public event EventHandler<bool> ShowLoadingControlRequest;
            public event PropertyChangedEventHandler PropertyChanged;

            public void SetDispatcher(Dispatcher dispatcher)
            {
                SetDispatcherParameter = dispatcher;
            }
            public Dispatcher SetDispatcherParameter { get; set; }
        }

        [Test]
        public void LoadedCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.ViewModel.LoadedCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void LoadedCommandCallsUseCaseShowExtensions()
        {
            var environment = new Environment();
            environment.ViewModel.LoadedCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.UseCase.ShowExtensionsCalled);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.ShowExtensionsParameter);
        }

        [Test]
        public void LoadReferencedLocationsCommandDoesNotCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.ViewModel.LoadReferencedLocationsCommand.Invoke(null);
            Assert.IsNull(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void LoadReferencedLocationsCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            ExtensionModel extension = new ExtensionModel(new Extension(), new NullLocalizationWrapper());
            extension.Id = 3000;
            environment.Mock.InterfaceAdapter.SelectedExtension = extension;
            environment.ViewModel.LoadReferencedLocationsCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void LoadReferencedLocationsCommandCallsUseCase()
        {
            var environment = new Environment();
            ExtensionModel extension = new ExtensionModel(new Extension(), new NullLocalizationWrapper());
            extension.Id = 3000;
            environment.Mock.InterfaceAdapter.SelectedExtension = extension;
            environment.ViewModel.LoadReferencedLocationsCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.UseCase.ShowReferencedLocationsCalled);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.ShowRefencedLocationsGuiErrorParameter);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddExtensionCommandCallsStartUp()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            environment.Mock.StartUp.OpenAddExtensionAssistantReturnValue = new View.AssistentView("");
            var extension = CreateExtension.Randomized(124);
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(extension, new NullLocalizationWrapper());
            environment.ViewModel.AddExtensionCommand.Invoke(null);
            Assert.AreSame(extension, environment.Mock.StartUp.OpenAddExtensionAssistantExtension);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddExtensionCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.StartUp.OpenAddExtensionAssistantReturnValue = new View.AssistentView("");
            environment.ViewModel.AddExtensionCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddExtensionCommandCallsRaiseShowLoadingControlAtClose()
        {
            var environment = new Environment();
            environment.Mock.StartUp.OpenAddExtensionAssistantReturnValue = new View.AssistentView("");
            environment.ViewModel.AddExtensionCommand.Invoke(null);
            environment.Mock.StartUp.OpenAddExtensionAssistantReturnValue.Close();
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test, RequiresThread(ApartmentState.STA)]
        public void AddExtensionCommandShowAssistantDialog()
        {
            var environment = new Environment();
            environment.Mock.StartUp.OpenAddExtensionAssistantReturnValue = new View.AssistentView("");

            ICanShowDialog arg = null;
            environment.ViewModel.ShowDialogRequest += (s, e) => arg = e;
            environment.ViewModel.AddExtensionCommand.Invoke(null);

            Assert.AreEqual(environment.Mock.StartUp.OpenAddExtensionAssistantReturnValue, arg);
        }

        [Test]
        public void SaveExtensionCanExecuteReturnsCorrectValue()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            environment.Mock.InterfaceAdapter.SelectedExtension = null;

            Assert.IsFalse(environment.ViewModel.SaveExtensionCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(12324), new NullLocalizationWrapper());
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = environment.Mock.InterfaceAdapter.SelectedExtension.CopyDeep();

            Assert.IsFalse(environment.ViewModel.SaveExtensionCommand.CanExecute(null));

            environment.Mock.InterfaceAdapter.SelectedExtension.FactorTorque = environment.Mock.InterfaceAdapter.SelectedExtension.FactorTorque + 1;

            Assert.IsTrue(environment.ViewModel.SaveExtensionCommand.CanExecute(null));

            environment.Mock.Validator.ValidateReturnValue = false;

            Assert.IsFalse(environment.ViewModel.SaveExtensionCommand.CanExecute(null));
        }

        [Test]
        public void SaveExtensionCanExecuteCallsValidator()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            environment.Mock.InterfaceAdapter.SelectedExtension = null;

            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(12324), new NullLocalizationWrapper());
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(CreateExtension.Randomized(43545657), new NullLocalizationWrapper());
            environment.ViewModel.SaveExtensionCommand.CanExecute(null);

            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedExtension.Entity, environment.Mock.Validator.ValidateParameter);
        }

        [Test]
        public void SaveExtensionExecuteCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(12324), new NullLocalizationWrapper());
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(CreateExtension.Randomized(12324), new NullLocalizationWrapper());
            environment.ViewModel.SaveExtensionCommand.Execute(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void SaveExtensionExecuteCallsUseCase()
        {
            var environment = new Environment();
            var oldControl = CreateExtension.Randomized(12324);
            var newControl = CreateExtension.Randomized(12324);
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(newControl, new NullLocalizationWrapper());
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(oldControl, new NullLocalizationWrapper());
            environment.ViewModel.SaveExtensionCommand.Execute(null);
            Assert.AreSame(oldControl, environment.Mock.UseCase.SaveExtensionDiff.OldExtension);
            Assert.AreSame(newControl, environment.Mock.UseCase.SaveExtensionDiff.NewExtension);
            Assert.IsNull(environment.Mock.UseCase.SaveExtensionDiff.User);
            Assert.IsNull(environment.Mock.UseCase.SaveExtensionDiff.Comment);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveExtensionErrorHandler);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.SaveExtensionSaveGuiShower);
        }

        [Test]
        public void SaveExtensionDontShowChangesIfExtensionHasNoChanges()
        {
            var environment = new Environment();
            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) => showDialogRequestInvoked = true;

            var extension = CreateExtension.Randomized(12324);
            var diff = new ExtensionDiff(null, null, extension, extension);
            environment.ViewModel.SaveExtension(diff, () => { });

            Assert.IsFalse(showDialogRequestInvoked);
        }

        [Test]
        public void SaveExtensionWithNoResultTest()
        {
            var environment = new Environment();
           
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(CreateExtension.Randomized(12324), new NullLocalizationWrapper());
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(684543), new NullLocalizationWrapper());

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };
            var oldExtension = CreateExtension.Randomized(12324);
            var newExtension = CreateExtension.Randomized(6578);

            var diff = new ExtensionDiff(null, null, oldExtension, newExtension);

            environment.ViewModel.SaveExtension(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsTrue(environment.Mock.InterfaceAdapter.SelectedExtension.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges));
        }

        [Test]
        public void SaveExtensionWithCancelResultTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(CreateExtension.Randomized(12324), new NullLocalizationWrapper());
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(684543), new NullLocalizationWrapper());

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            var oldExtension = CreateExtension.Randomized(12324);
            var newExtension = CreateExtension.Randomized(6578);

            var diff = new ExtensionDiff(null, null, oldExtension, newExtension);

            environment.ViewModel.SaveExtension(diff, () => { });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsFalse(environment.Mock.InterfaceAdapter.SelectedExtension.EqualsByContent(environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges));
        }

        [Test]
        public void SaveExtensionWithYesResultCallsFinishedAction()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var oldExtension = CreateExtension.Randomized(12324);
            var newExtension = CreateExtension.Randomized(6578);

            var diff = new ExtensionDiff(null, null, oldExtension, newExtension);
            var finishedActionCalled = false;
            environment.ViewModel.SaveExtension(diff, () => { finishedActionCalled = true; });

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(finishedActionCalled);
        }


        [Test]
        public void SelectNewExtensionCallsValidatorWithSelectedExtension()
        {
            var environment = new Environment();
            var selectedExtension = new ExtensionModel(CreateExtension.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = selectedExtension;

            environment.ViewModel.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(234), null);

            Assert.AreSame(selectedExtension.Entity, environment.Mock.Validator.ValidateParameter);
        }

        [Test]
        public void SelectNewExtensionWithInvalidExtensionContinueEditing()
        {
            var environment = new Environment();
            var oldExtension = new ExtensionModel(CreateExtension.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = oldExtension;
         
            environment.Mock.Validator.ValidateReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };
            
            environment.ViewModel.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(3443), null);

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.AreSame(oldExtension, environment.ViewModel.SelectedExtension);
        }

        [Test]
        public void SelectNewExtensionWithInvalidExtensionDontContinueEditing()
        {
            var environment = new Environment();
            var oldExtension = new ExtensionModel(CreateExtension.Randomized(343), null);
            var changedExtension = new ExtensionModel(CreateExtension.Randomized(2342), null);

            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = oldExtension;
            environment.Mock.InterfaceAdapter.SelectedExtension = changedExtension;

            environment.Mock.Validator.ValidateReturnValue = false;

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };
            
            var newExtension = new ExtensionModel(CreateExtension.Randomized(4564), null);
            environment.ViewModel.SelectedExtension = newExtension;

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsTrue(changedExtension.EqualsByContent(oldExtension));
            Assert.AreSame(newExtension, environment.ViewModel.SelectedExtension);
        }

        [Test]
        public void SelectNewExtensionWithChangedExtensionConditionCancelTest()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(CreateExtension.Randomized(4564), null);
            var oldExtension = new ExtensionModel(CreateExtension.Randomized(232), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = oldExtension;
            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Cancel;
            };
            
            var selectedExtension = new ExtensionModel(CreateExtension.Randomized(684543), null);
            environment.ViewModel.SelectedExtension = selectedExtension;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.AreSame(oldExtension, environment.ViewModel.SelectedExtension);
            Assert.AreNotSame(selectedExtension, environment.ViewModel.SelectedExtension);
        }

        [Test]
        public void SelectNewExtensionWithChangedExtensionConditionYesTest()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            var oldExtensionCondition = new ExtensionModel(CreateExtension.Randomized(12324), null);
            var changedExtensionCondition = new ExtensionModel(CreateExtension.Randomized(684543), null);

            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = oldExtensionCondition;
            environment.Mock.InterfaceAdapter.SelectedExtension = changedExtensionCondition;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.Yes;
            };

            var newExtension = new ExtensionModel(CreateExtension.Randomized(546), null);
            environment.ViewModel.SelectedExtension = newExtension;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.AreSame(oldExtensionCondition.Entity, environment.Mock.UseCase.UpdateExtensionDiff.OldExtension);
            Assert.AreSame(changedExtensionCondition.Entity, environment.Mock.UseCase.UpdateExtensionDiff.NewExtension);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateExtensionError);
            Assert.AreSame(newExtension, environment.ViewModel.SelectedExtension);
        }

        [Test]
        public void SelectNewExtensionWithChangedExtensionConditionNoTest()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            var oldExtensionCondition = new ExtensionModel(CreateExtension.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = oldExtensionCondition;
            var oldExtensionChanged = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = oldExtensionChanged;

            var showDialogRequestInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                showDialogRequestInvoked = true;
                e.Result = System.Windows.MessageBoxResult.No;
            };

            var newExtension = new ExtensionModel(CreateExtension.Randomized(11111), null);
            environment.ViewModel.SelectedExtension = newExtension;

            Assert.IsTrue(showDialogRequestInvoked);
            Assert.IsTrue(oldExtensionChanged.EqualsByContent(oldExtensionCondition));
            Assert.AreSame(newExtension, environment.ViewModel.SelectedExtension);
        }

        [Test]
        public void CanCloseCallsValidator()
        {
            var environment = new Environment();
            var extension = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = extension;
            environment.ViewModel.CanClose();
            Assert.AreSame(extension.Entity, environment.Mock.Validator.ValidateParameter);
        }

        [Test]
        public void CanCloseWithInvalidExtensionContinueEditingTest()
        {
            var environment = new Environment();
            var extension = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = extension;

            var messageBoxInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };
            var result = environment.ViewModel.CanClose();

            Assert.IsFalse(result);
            Assert.AreSame(extension, environment.ViewModel.SelectedExtension);
            Assert.IsTrue(messageBoxInvoked);
        }

        [Test]
        public void CanCloseWithInvalidExtensionDontContinueEditingTest()
        {
            var environment = new Environment();
            var extension = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = extension;

            var messageBoxInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(result);
            Assert.IsNull(environment.ViewModel.SelectedExtension);
            Assert.IsTrue(messageBoxInvoked);
        }

        [Test]
        public void CanCloseWithChangedExtensionYesTest()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            var changedExtension = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = changedExtension;

            var extension = new ExtensionModel(CreateExtension.Randomized(123), null);
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = extension;

            var messageBoxInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                messageBoxInvoked = true;
                e.Result = MessageBoxResult.Yes;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxInvoked);
            Assert.AreSame(extension.Entity, environment.Mock.UseCase.UpdateExtensionDiff.OldExtension);
            Assert.AreSame(changedExtension.Entity, environment.Mock.UseCase.UpdateExtensionDiff.NewExtension);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.UpdateExtensionError);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsNull(environment.ViewModel.SelectedExtension);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithChangedExtensionNoTest()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            var changedExtension = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = changedExtension;

            var extension = new ExtensionModel(CreateExtension.Randomized(123), null);
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = extension;

            var messageBoxInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                messageBoxInvoked = true;
                e.Result = MessageBoxResult.No;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxInvoked);
            Assert.IsNull(environment.ViewModel.SelectedExtension);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanCloseWithChangedExtensionCancelTest()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = true;
            var changedExtension = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = changedExtension;

            var extension = new ExtensionModel(CreateExtension.Randomized(123), null);
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = extension;

            var messageBoxInvoked = false;
            environment.ViewModel.RequestVerifyChangesView += (s, e) =>
            {
                messageBoxInvoked = true;
                e.Result = MessageBoxResult.Cancel;
            };

            var result = environment.ViewModel.CanClose();

            Assert.IsTrue(messageBoxInvoked);
            Assert.AreSame(extension, environment.ViewModel.SelectedExtensionWithoutChanges);
            Assert.AreSame(changedExtension, environment.ViewModel.SelectedExtension);
            Assert.IsFalse(result);
        }

        [Test]
        public void CanExecuteAbortIfChangedExtensionAndAbort()
        {
            var environment = new Environment();
            environment.Mock.Validator.ValidateReturnValue = false;
            var extension = new ExtensionModel(CreateExtension.Randomized(34523), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = extension;

            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                e.ResultAction(MessageBoxResult.No);
            };

            Assert.IsNull(environment.Mock.StartUp.OpenAddExtensionAssistantExtension);
        }

        [Test]
        public void RemoveCommandCanExecutedTest()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedExtension = null;
            Assert.IsFalse(environment.ViewModel.RemoveExtensionCommand.CanExecute(null));
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(12324), null);
            Assert.IsTrue(environment.ViewModel.RemoveExtensionCommand.CanExecute(null));
        }

        [Test]
        public void RemoveCommandCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(CreateExtension.Randomized(12324), null);
            environment.Mock.InterfaceAdapter.SelectedExtension = environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges.CopyDeep();

            environment.ViewModel.RemoveExtensionCommand.Invoke(null);
            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }

        [Test]
        public void RemoveCommandCancelTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(684543), null);

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.No);
            };

            environment.ViewModel.RemoveExtensionCommand.Invoke(null);

            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsNull(environment.Mock.UseCase.RemoveExtensionExtension);
        }

        [Test]
        public void RemoveCommandYesTest()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.Mock.InterfaceAdapter.SelectedExtension = new ExtensionModel(CreateExtension.Randomized(684543), null);
            environment.Mock.InterfaceAdapter.SelectedExtensionWithoutChanges = new ExtensionModel(CreateExtension.Randomized(12324), null);

            var messageBoxRequestInvoked = false;
            environment.ViewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxRequestInvoked = true;
                e.ResultAction(MessageBoxResult.Yes);
            };

            environment.ViewModel.RemoveExtensionCommand.Invoke(null);

            Assert.IsTrue(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
            Assert.IsTrue(messageBoxRequestInvoked);
            Assert.IsTrue(
                environment.Mock.InterfaceAdapter.SelectedExtension.EqualsByContent(environment.Mock
                    .InterfaceAdapter.SelectedExtensionWithoutChanges));


            Assert.AreSame(environment.Mock.InterfaceAdapter.SelectedExtension.Entity, environment.Mock.UseCase.RemoveExtensionExtension);
            Assert.AreSame(environment.ViewModel, environment.Mock.UseCase.RemoveExtensionErrorHandler);
        }

        [Test]
        public void ShowRemoveExtensionPreventingReferencesCallsRaiseShowLoadingControl()
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);
            environment.ViewModel.ShowRemoveExtensionPreventingReferences(new List<LocationReferenceLink>());
            Assert.IsFalse(environment.Mock.StartUp.RaiseShowLoadingControlShowLoadingControl);
        }


        private static IEnumerable<List<LocationReferenceLink>> ShowRemoveExtensionPreventingReferencesCallsDialogData =
            new List<List<LocationReferenceLink>>()
            {
                new List<LocationReferenceLink>()
                {
                    new LocationReferenceLink(new QstIdentifier(1), new LocationNumber("235"),new LocationDescription("436"),  new MockLocationDisplayFormatter() ),
                    new LocationReferenceLink(new QstIdentifier(19), new LocationNumber("6575"),new LocationDescription("23424"), new MockLocationDisplayFormatter() )
                },
                new List<LocationReferenceLink>()
                {
                    new LocationReferenceLink(new QstIdentifier(4351), new LocationNumber("aaa"),new LocationDescription("bbb"), new MockLocationDisplayFormatter())
                }
            };

        [TestCaseSource(nameof(ShowRemoveExtensionPreventingReferencesCallsDialogData))]
        public void ShowRemoveExtensionPreventingReferencesCallsDialog(List<LocationReferenceLink> references)
        {
            var environment = new Environment();
            environment.ViewModel.SetDispatcher(Dispatcher.CurrentDispatcher);

            var calledList = new ReferenceList();
            environment.ViewModel.ReferencesDialogRequest += (s, e) =>
            {
                calledList = e;
            };

            environment.ViewModel.ShowRemoveExtensionPreventingReferences(references);

            Assert.AreEqual(references.Select(x => x.DisplayName).ToList(), calledList.References.ToList());

        }

        [Test]
        public void DisposeDisposesFilteredObservableCollections()
        {
            var extensions = new FilteredObservableCollectionMock<ExtensionModel>();
            var environment = new Environment(extensions);
            environment.ViewModel.Dispose();
            Assert.IsTrue(extensions.IsDisposed);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    UseCase = new ExtensionUseCaseMock();
                    InterfaceAdapter = new ExtensionInterfaceAdapterMock();
                    StartUp = new StartUpMock();
                    Validator = new ExtensionValidatorMock();
                }

                public readonly ExtensionUseCaseMock UseCase;
                public readonly ExtensionInterfaceAdapterMock InterfaceAdapter;
                public readonly StartUpMock StartUp;
                public readonly ExtensionValidatorMock Validator;
            }
            public Environment(IFilteredObservableCollectionExtension<ExtensionModel> extensions = null)
            {
                Mock = new Mocks();
                ViewModel = new ExtensionViewModel(Mock.StartUp, Mock.UseCase, Mock.InterfaceAdapter, new NullLocalizationWrapper(), Mock.Validator, extensions);
            }

            public readonly Mocks Mock;
            public readonly ExtensionViewModel ViewModel;
        }
    }
}
