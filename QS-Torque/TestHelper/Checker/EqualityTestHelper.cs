using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TestHelper.Checker
{
    /// <typeparam name="T">Type of the to testing Entity</typeparam>
    public class EqualityParameter<T>
    {
        public Action<T, object> SetParameter { get; set; }
        public Func<object> CreateParameterValue { get; set; }
        public Func<object> CreateOtherParameterValue { get; set; }

        /// <summary>
        /// Just for debugging
        /// </summary>
        public string ParameterName { get; set; }
    }


    /// <typeparam name="T">Type of the to testing Entity</typeparam>
    public class EqualityTestHelper<T> where T : class
    {
        public List<EqualityParameter<T>> EqualityParameterList { get; private set; }
        private Func<T, T, bool> EqualityFunction { get; set; }
        private Func<T> _createNewEntity;


        public EqualityTestHelper(Func<T, T, bool> equalityFunction, Func<T> createNewEntity, List<EqualityParameter<T>> equalityParameterList)
        {
            EqualityFunction = equalityFunction;
            _createNewEntity = createNewEntity;
            EqualityParameterList = equalityParameterList;
        }


        public T GetFilledEntity(List<EqualityParameter<T>> parameterList)
        {
            var entity = _createNewEntity();

            foreach (var param in parameterList)
            {
                param.SetParameter(entity, param.CreateParameterValue());
            }

            return entity;
        }

        private T GetDifferentFilledEntity(List<EqualityParameter<T>> parameterList)
        {
            var entity = _createNewEntity();

            foreach (var param in parameterList)
            {
                param.SetParameter(entity, param.CreateOtherParameterValue());
            }

            return entity;
        }

        public void CheckInequalityForParameter(EqualityParameter<T> parameter)
        {
            var left = GetFilledEntity(EqualityParameterList);
            var right = GetFilledEntity(EqualityParameterList);

            parameter.SetParameter(right, parameter.CreateOtherParameterValue());

            Assert.IsFalse(EqualityFunction(left, right));
        }

        public void CheckEqualityForParameterList()
        {
            var left = GetFilledEntity(EqualityParameterList);
            var right = GetFilledEntity(EqualityParameterList);

            Assert.IsTrue(EqualityFunction(left, right));
        }

        public void CheckInequalityWithRightIsNull()
        {
            var left = GetFilledEntity(EqualityParameterList);

            Assert.IsFalse(EqualityFunction(left, null));
        }
        
        public void CheckEqualityAfterUpdate(Action<T, T> updateMethod)
        {
            var left = GetFilledEntity(EqualityParameterList);
            var right = GetDifferentFilledEntity(EqualityParameterList);

            updateMethod(left, right);

            Assert.IsTrue(EqualityFunction(left, right));
        }

        public void CheckEqualityAfterCopy(Func<T, T> copyMethod)
        {
            var obj = GetFilledEntity(EqualityParameterList);

            var copy = copyMethod(obj);

            Assert.IsFalse(obj == copy);
            Assert.IsTrue(EqualityFunction(obj, copy));
        }
    }
}