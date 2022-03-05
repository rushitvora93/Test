using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using SetupService;
using TestHelper.Checker;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class SetupUseCaseMock : ISetupUseCase
    {
        public List<QstSetup> GetQstSetupsByUserIdAndName(long userId, string name)
        {
            GetQstSetupsByUserIdAndNameParameterUserId = userId;
            GetQstSetupsByUserIdAndNameParameterName = name;
            return GetQstSetupsByUserIdAndNameReturnValue;
        }

        public List<QstSetup> GetColumnWidthsForGrid(long userId, string gridName, List<string> columns)
        {
            GetColumnWidthsForGridParameterUserId = userId;
            GetColumnWidthsForGridParameterGridName = gridName;
            GetColumnWidthsForGridParameterColumns = columns;
            return GetColumnWidthsForGridReturnValue;
        }

        public List<QstSetup> InsertOrUpdateQstSetups(List<QstSetup> qstSetups, bool returnList)
        {
            InsertOrUpdateQstSetupsParameterSetups = qstSetups;
            InsertOrUpdateQstSetupsParameterReturnList = returnList;
            return InsertOrUpdateQstSetupsReturnValue;
        }

        public List<QstSetup> InsertOrUpdateQstSetupsReturnValue { get; set; } = new List<QstSetup>();
        public bool InsertOrUpdateQstSetupsParameterReturnList { get; set; }
        public List<QstSetup> InsertOrUpdateQstSetupsParameterSetups { get; set; } = new List<QstSetup>();
        public List<QstSetup> GetQstSetupsByUserIdAndNameReturnValue { get; set; } = new List<QstSetup>();
        public string GetQstSetupsByUserIdAndNameParameterName { get; set; }
        public long GetQstSetupsByUserIdAndNameParameterUserId { get; set; }
        public List<QstSetup> GetColumnWidthsForGridReturnValue { get; set; } = new List<QstSetup>();
        public List<string> GetColumnWidthsForGridParameterColumns { get; set; }
        public string GetColumnWidthsForGridParameterGridName { get; set; }
        public long GetColumnWidthsForGridParameterUserId { get; set; }
    }

    public class SetupServiceTest
    {
        static IEnumerable<GetColumnWidthsForGridRequest> GetColumnWidthsForGridCallsUseCaseData = new List<GetColumnWidthsForGridRequest>()
        {
            new GetColumnWidthsForGridRequest()
            {
                UserId = 1, GridName = "grid1", Columns = { "test1", "test2", "test3"}
            },
            new GetColumnWidthsForGridRequest()
            {
                UserId = 99, GridName = "grid19", Columns = { "test19", "test27", "test73"}
            }
        };

        [TestCaseSource(nameof(GetColumnWidthsForGridCallsUseCaseData))]
        public void GetColumnWidthsForGridCallsUseCase(GetColumnWidthsForGridRequest request)
        {
            var useCase = new SetupUseCaseMock();
            var service = new NetworkView.Services.SetupService(null, useCase);

            service.GetColumnWidthsForGrid(request, null);

            Assert.AreEqual(request.UserId, useCase.GetColumnWidthsForGridParameterUserId);
            Assert.AreEqual(request.GridName, useCase.GetColumnWidthsForGridParameterGridName);
            Assert.AreEqual(request.Columns.ToList(),useCase.GetColumnWidthsForGridParameterColumns);
        }

        private static IEnumerable<List<QstSetup>> setupData = new List<List<QstSetup>>()
        {
            new List<QstSetup>()
            {
               new QstSetup() {Id = new QstSetupId(1), Name = new QstSetupName("name"), Value = new QstSetupValue("3245346")},
               new QstSetup() {Id = new QstSetupId(19), Name = new QstSetupName("efg"), Value = new QstSetupValue("abcd")}
            },
            new List<QstSetup>()
            {
                new QstSetup() {Id = new QstSetupId(1235), Name = new QstSetupName("rtzrtz"), Value = new QstSetupValue("754")},
            }

        };

        [TestCaseSource(nameof(setupData))]
        public void GetColumnWidthsForGridReturnsCorrectValue(List<QstSetup> setupList)
        {
            var useCase = new SetupUseCaseMock();
            var service = new NetworkView.Services.SetupService(null, useCase);

            useCase.GetColumnWidthsForGridReturnValue = setupList;

            var result = service.GetColumnWidthsForGrid(new GetColumnWidthsForGridRequest(),null);

            var comparer = new Func<QstSetup, DtoTypes.QstSetup, bool>((setup, dtoSetup) =>
                EqualityChecker.CompareQstSetupDtoWithQstSetup(dtoSetup, setup)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(setupList, result.Result.SetupList, comparer);
        }

        [TestCase(1, "name 1")]
        [TestCase(12, "name 12")]
        public void GetQstSetupsByUserIdAndNameCallsUseCase(long userId, string name)
        {
            var useCase = new SetupUseCaseMock(); 
            var service = new NetworkView.Services.SetupService(null, useCase);

            var request = new GetQstSetupsByUserIdAndNameRequest()
            {
                UserId = userId,
                Name = name
            };
            service.GetQstSetupsByUserIdAndName(request, null);

            Assert.AreEqual(request.UserId, useCase.GetQstSetupsByUserIdAndNameParameterUserId);
            Assert.AreEqual(request.Name, useCase.GetQstSetupsByUserIdAndNameParameterName);
        }


        [TestCaseSource(nameof(setupData))]
        public void GetQstSetupsByUserIdAndNameReturnsCorrectValue(List<QstSetup> setupList)
        {
            var useCase = new SetupUseCaseMock();
            var service = new NetworkView.Services.SetupService(null, useCase);
            useCase.GetQstSetupsByUserIdAndNameReturnValue = setupList;

            var result = service.GetQstSetupsByUserIdAndName(new GetQstSetupsByUserIdAndNameRequest(), null);

            var comparer = new Func<QstSetup, DtoTypes.QstSetup, bool>((setup, dtoSetup) =>
                EqualityChecker.CompareQstSetupDtoWithQstSetup(dtoSetup, setup)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(setupList, result.Result.SetupList, comparer);
        }

        static IEnumerable<InsertOrUpdateQstSetupsRequest> InsertOrUpdateQstSetupsCallsUseCaseData = new List<InsertOrUpdateQstSetupsRequest>()
        {
            (
                new InsertOrUpdateQstSetupsRequest()
                {
                    SetupList =
                    {
                        new DtoTypes.QstSetup() {Id = 1, Name = "Name 45", Value = "345756"},
                        new DtoTypes.QstSetup() {Id = 14, Name = "Test", Value = "Value"}
                    },
                    ReturnList = true
                }
            ),
            (
                new InsertOrUpdateQstSetupsRequest()
                {
                    SetupList =
                    {
                        new DtoTypes.QstSetup() {Id = 99, Name = "Name XX", Value = "hans"},
                    },
                    ReturnList = false
                }
            )
        };

        [TestCaseSource(nameof(InsertOrUpdateQstSetupsCallsUseCaseData))]
        public void InsertOrUpdateQstSetupsCallsUseCase(InsertOrUpdateQstSetupsRequest request)
        {
            var useCase = new SetupUseCaseMock();
            var service = new NetworkView.Services.SetupService(null, useCase);

            service.InsertOrUpdateQstSetups(request, null);

            var comparer = new Func<QstSetup, DtoTypes.QstSetup, bool>((setup, dtoSetup) =>
                EqualityChecker.CompareQstSetupDtoWithQstSetup(dtoSetup, setup)
            );

            Assert.AreEqual(request.ReturnList, useCase.InsertOrUpdateQstSetupsParameterReturnList);

            CheckerFunctions.CollectionAssertAreEquivalent(useCase.InsertOrUpdateQstSetupsParameterSetups, request.SetupList, comparer);
        }

        [TestCaseSource(nameof(setupData))]
        public void InsertOrUpdateQstSetupsReturnsCorrectValue(List<QstSetup> setupList)
        {
            var useCase = new SetupUseCaseMock();
            var service = new NetworkView.Services.SetupService(null, useCase);

            useCase.InsertOrUpdateQstSetupsReturnValue = setupList;

            var result = service.InsertOrUpdateQstSetups(new InsertOrUpdateQstSetupsRequest(), null);

            var comparer = new Func<QstSetup, DtoTypes.QstSetup, bool>((setup, dtoSetup) =>
                EqualityChecker.CompareQstSetupDtoWithQstSetup(dtoSetup, setup)
            );

            CheckerFunctions.CollectionAssertAreEquivalent(setupList, result.Result.SetupList, comparer);
        }
    }
}
