using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters.Models;
using NUnit.Framework;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Models
{
    class LocationToolAssignmentChangeStateModelTest
    {
        private static IEnumerable<Tuple<List<(long, long, long)>, (long, List<long>)>>
            SetStateForToolInLocationToolAssignmentData = new List<Tuple<List<(long, long, long)>, (long, List<long>)>>
            {
                new Tuple<List<(long, long, long)>, (long, List<long>)>
                (
                    new List<(long, long, long)>
                    {
                        (1, 1, 10),
                        (2, 2, 11),
                        (3, 1, 10),
                    },
                    (
                        99, 
                        new List<long>
                        {
                            99,
                            11,
                            99
                        }
                    )
                ),
                new Tuple<List<(long, long, long)>, (long, List<long>)>
                (
                    new List<(long, long, long)>
                    {
                        (6, 1, 60),
                        (7, 2, 70),
                        (8, 3, 70),
                        (9, 1, 60),
                        (10, 6, 60),
                        (10, 1, 60),
                    },
                    (
                        1001,
                        new List<long>
                        {
                            1001,
                            70,
                            70,
                            1001,
                            60,
                            1001
                        }
                    )
                ),
            };

        [TestCaseSource(nameof(SetStateForToolInLocationToolAssignmentData))]
        public void SetSateForToolInLocationToolAssignmentSetsStateOfSameToolInRemainingLocationToolAssignments(Tuple<List<(long assigmentId,long toolId,long stateId)>, (long newStateId, List<long> expectedStates)> datas)
        {
            var locationToolAssignments = new List<LocationToolAssignment>();
            foreach (var data in datas.Item1)
            {
                locationToolAssignments.Add(new LocationToolAssignment()
                {
                    Id = new LocationToolAssignmentId(data.assigmentId),
                    AssignedTool = CreateTool.WithIdAndState(data.toolId, data.stateId)
                });
            }

            var locationToolAssignmentChangeStateModels = new List<LocationToolAssignmentChangeStateModel>();
            foreach (var assigment in locationToolAssignments)
            {
                var test = new LocationToolAssignmentChangeStateModel(null, assigment, "", "",
                    assigment.AssignedTool.Status, locationToolAssignmentChangeStateModels);
                locationToolAssignmentChangeStateModels.Add(test);
            }

            var newStatus = new Status() {ListId = new HelperTableEntityId(datas.Item2.newStateId)};

            locationToolAssignmentChangeStateModels.First().Status = new HelperTableItemModel<Status, string>(newStatus,
                e => e.Value.ToDefaultString(),
                (e, value) => newStatus.Value = new StatusDescription(""),
                () => new Status());

            var i = 0;
            foreach (var stateModel in locationToolAssignmentChangeStateModels)
            {
                Assert.AreEqual(datas.Item2.expectedStates[i], stateModel.StateId.ListId.ToLong());
                i++;
            }
        }
    }
}
