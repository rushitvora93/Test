using Client.TestHelper.Mock;
using Client.UseCases.UseCases;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters;
using InterfaceAdapters.Models;
using InterfaceAdapters.Models.Diffs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TestHelper.Mock;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class LocationInterfaceMock : ILocationInterface
    {
        public LocationTreeModel LocationTree => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            throw new NotImplementedException();
        }
    }

    public class LocationHistoryViewModelTest
    {
        [Test]
        public void LoadedCallsLocationUseCase()
        {
            var environment = CreateEnvironment();
            environment.viewModel.LoadedCommand.Execute(null);
            Assert.IsTrue(environment.locationUseCase.WasLoadTreeCalled);
        }

        [TestCase(1)]
        [TestCase(15)]
        public void SetSelectedLocationCallsHistoryUseCase(long idVal)
        {
            var environment = CreateEnvironment();
            var id = new LocationId(idVal);
            environment.viewModel.SelectedLocation = new LocationModel(new Location() { Id = id }, new NullLocalizationWrapper(), null);
            Assert.AreSame(id, environment.historyUseCase.LoadLocationHistoryLocationId);
            Assert.AreEqual(environment.viewModel, environment.historyUseCase.LoadLocationHistoryErrorHandler);
        }


        static Environment CreateEnvironment()
        {
            var environment = new Environment();
            environment.historyUseCase = new HistoryUseCaseMock();
            environment.historyInterface = new HistoryInterfaceMock();
            environment.locationUseCase = new LocationUseCaseMock(null);
            environment.locationInterface = new LocationInterfaceMock();
            environment.viewModel = new LocationHistoryViewModel(environment.historyUseCase, environment.historyInterface, environment.locationUseCase, environment.locationInterface, new NullLocalizationWrapper(), new StartUpMock());
            return environment;
        }

        class Environment
        {
            public LocationHistoryViewModel viewModel;
            public HistoryUseCaseMock historyUseCase;
            public HistoryInterfaceMock historyInterface;
            public LocationUseCaseMock locationUseCase;
            public LocationInterfaceMock locationInterface;
        }
    }
}
