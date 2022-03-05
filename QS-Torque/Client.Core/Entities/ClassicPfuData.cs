using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Core.Entities;

namespace Client.Core.Entities
{
    public class ClassicPfuData
    {
        
        private readonly int _doubleDecimalDigits;
        

        public ClassicPfuData(List<ClassicProcessTest> classicProcessTests, int doubleDecimalDigits)
        {
            ClassicProcessTests = classicProcessTests;
            _doubleDecimalDigits = doubleDecimalDigits;
            ClassicProcessTestValues = new List<ClassicProcessTestValue>();
            ClassicProcessTests.OrderBy(x => x.Timestamp).ToList().ForEach(t =>
                t.TestValues.OrderBy(x => x.Position).ToList().ForEach(v => ClassicProcessTestValues.Add(v)));
            TestValues = ClassicProcessTestValues.Select(x => x.ControlledByValue).ToList();
        }

        public readonly List<ClassicProcessTestValue> ClassicProcessTestValues;
        public readonly List<ClassicProcessTest> ClassicProcessTests;
        public readonly List<double> TestValues;
        public MeaUnit ControlledByUnitId => ClassicProcessTests.Last().ControlledByUnitId;
        public double TestValueMinimum => TestValues.Min();
        public double TestValueMaximum => TestValues.Max();
        public double Average => Statistic.GetAverage(TestValues.ToArray());
        public double Range => Statistic.GetRange(new[] { Math.Round(TestValueMinimum, _doubleDecimalDigits), Math.Round(TestValueMaximum, _doubleDecimalDigits)});
        public double LowerLimit => ClassicProcessTests.Last().LowerLimit;
        public double UpperLimit => ClassicProcessTests.Last().UpperLimit;
        public double NominalValue => ClassicProcessTests.Last().NominalValue;
        public ToleranceClass ToleranceClass => ClassicProcessTests.Last().ToleranceClass;
        public bool Result => CmValue.GetValueOrDefault(0) >= 1.33 && CmkValue.GetValueOrDefault() >= 1.33;

        public double? StandardDeviation
        {
            get
            {
                try
                {
                    return Statistic.GetStandardDeviation(TestValues.ToArray());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public double? Variance
        {
            get
            {
                try
                {
                    return Statistic.GetVariance(TestValues.ToArray());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public double? CmValue
        {
            get
            {
                try
                {
                    if (TestValues.Count < 10)
                    {
                        return null;
                    }

                    return Statistic.GetC(TestValues.ToArray(), UpperLimit, LowerLimit);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public double? CmkValue
        {
            get
            {
                try
                {
                    if (TestValues.Count < 10)
                    {
                        return null;
                    }
                    return Statistic.GetCk(TestValues.ToArray(), UpperLimit, LowerLimit);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public double GetLowerSigma(double sigmaFactor)
        {
            return Statistic.GetLowerSigma(Average, StandardDeviation.GetValueOrDefault(0), sigmaFactor);
        }

        public double GetUpperSigma(double sigmaFactor)
        {
            return Statistic.GetUpperSigma(Average, StandardDeviation.GetValueOrDefault(0), sigmaFactor);
        }
    }
}

