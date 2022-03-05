
using System;
using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;


namespace Server.UseCases.UseCases
{
    public interface IToleranceClassUseCase
    {
        List<ToleranceClass> LoadToleranceClasses();
        List<LocationReferenceLink> GetToleranceClassLocationLinks(ToleranceClassId classId);
        List<ToleranceClass> InsertToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs, bool returnList);
        List<ToleranceClass> UpdateToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs);
        List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinks(ToleranceClassId toleranceClassId);
        Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithClassData);
    }

    public interface IToleranceClassDataAccess
    {
        void Commit();
        List<ToleranceClass> LoadToleranceClasses();
        List<LocationReferenceLink> GetToleranceClassLocationLinks(ToleranceClassId classId);
        List<ToleranceClass> InsertToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs, bool returnList);
        List<ToleranceClass> UpdateToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs);
        List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinks(ToleranceClassId toleranceClassId);
        Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithToleranceClassData);
    }

    public class ToleranceClassUseCase : IToleranceClassUseCase
    {
        private readonly IToleranceClassDataAccess _toleranceClassData;

        public ToleranceClassUseCase(IToleranceClassDataAccess toleranceClassData)
        {
            _toleranceClassData = toleranceClassData;
        }

        public List<ToleranceClass> LoadToleranceClasses()
        {
            return _toleranceClassData.LoadToleranceClasses();
        }

        public List<LocationReferenceLink> GetToleranceClassLocationLinks(ToleranceClassId classId)
        {
            return _toleranceClassData.GetToleranceClassLocationLinks(classId);
        }

        public List<ToleranceClass> InsertToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs, bool returnList)
        {
            var toleranceClasses = _toleranceClassData.InsertToleranceClassesWithHistory(toleranceClassDiffs, returnList);
            _toleranceClassData.Commit();
            return toleranceClasses;
        }

        public List<ToleranceClass> UpdateToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs)
        {
            var toleranceClasses = _toleranceClassData.UpdateToleranceClassesWithHistory(toleranceClassDiffs);
            _toleranceClassData.Commit();
            return toleranceClasses;
        }

        public List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinks(ToleranceClassId toleranceClassId)
        {
            return _toleranceClassData.GetToleranceClassLocationToolAssignmentLinks(toleranceClassId);
        }

        public Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithClassData)
        {
            return _toleranceClassData.GetToleranceClassesFromHistoryForIds(idsWithClassData);
        }
    }
}
