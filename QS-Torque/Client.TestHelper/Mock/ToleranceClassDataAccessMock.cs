using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class ToleranceClassDataAccessMock : IToleranceClassData
    {
        public List<ToleranceClass> LoadToleranceClassesData { get; set; }
        public bool LoadToleranceClassesThrowsException { get; set; }
        public bool RemoveToleranceClassThrowsException { get; set; }

        public ToleranceClass AddedToleranceClass { get; set; }
        public ToleranceClassId AddId { get; set; }
        public bool AddToleranceCLassThrowsException { get; set; }
        public ToleranceClass SavedToleranceClass { get; set; }
        public bool SaveToleranceClassThrowsException { get; set; }
        public User RemovedToleranceClassUser { get; set; }
        public User AddToleranceClassUser { get; set; }
        public User SaveToleranceClassUser { get; set; }

        public bool ThrowsLoadReferencedLocationsException = false;
        public ToleranceClassId LoadReferencedLocationsParameter;
        public List<LocationReferenceLink> LoadReferencedLocationsReturnValue;

        
        public ToleranceClassId LoadReferencedLocationToolAssignmentsParameter;
        public List<LocationToolAssignmentId> LoadReferencedLocationToolAssignmentsReturnValue;
        public bool ThrowsLoadReferencedLocationToolAssignmentsException;

        public Dictionary<long, ToleranceClass> GetToleranceClassFromHistoryForIdsReturnValue { get; set; } = new Dictionary<long, ToleranceClass>();
        public List<Tuple<long, long, DateTime>> GetToleranceClassFromHistoryForIdsParameter { get; set; } = new List<Tuple<long, long, DateTime>>();

        public List<ToleranceClass> LoadToleranceClasses()
        {
            if (LoadToleranceClassesThrowsException)
            {
                throw new Exception();
            }
            return LoadToleranceClassesData;
        }

        public void RemoveToleranceClass(ToleranceClassDiff toleranceClass)
        {
            if (RemoveToleranceClassThrowsException)
            {
                throw new Exception();
            }

            RemovedToleranceClassUser = toleranceClass.User;
        }

        public ToleranceClass AddToleranceClass(ToleranceClass toleranceClass, User byUser)
        {
            if (AddToleranceCLassThrowsException)
            {
                throw new Exception();
            }

            toleranceClass.Id = AddId;
            AddedToleranceClass = toleranceClass;
            AddToleranceClassUser = byUser;
            return toleranceClass;
        }

        public ToleranceClass SaveToleranceClass(ToleranceClassDiff toleranceClass)
        {
            if (SaveToleranceClassThrowsException)
            {
                throw new Exception();
            }

            SaveToleranceClassUser = toleranceClass.User;
            SavedToleranceClass = toleranceClass.NewToleranceClass;
            return toleranceClass.NewToleranceClass;
        }

        public List<LocationReferenceLink> LoadReferencedLocations(ToleranceClassId id)
        {
            if (ThrowsLoadReferencedLocationsException)
            {
                throw new Exception();
            }

            LoadReferencedLocationsParameter = id;
            return LoadReferencedLocationsReturnValue;
        }

        public List<LocationToolAssignmentId> LoadReferencedLocationToolAssignments(ToleranceClassId id)
        {
            if (ThrowsLoadReferencedLocationToolAssignmentsException)
            {
                throw new Exception();
            }

            LoadReferencedLocationToolAssignmentsParameter = id;
            return LoadReferencedLocationToolAssignmentsReturnValue;
        }

        public Dictionary<long, ToleranceClass> GetToleranceClassFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithClassData)
        {
            GetToleranceClassFromHistoryForIdsParameter.AddRange(idsWithClassData);
            return GetToleranceClassFromHistoryForIdsReturnValue;
        }
    }

}
