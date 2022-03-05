using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using NUnit.Framework;

namespace Core.Test.Entities
{
    class StatisticTest
    {
        private const double StandardDoubleComparisonDelta = 0.000000001;

        [TestCase(new double[] { 16.25364162, 22.91414816, 26.65296375, 28.89955632, 16.87575858, 24.33775845, 28.65871777, 10.60209435, 16.10084039, 13.48582094, 29.95374612, 16.45724514, 26.4506641, 15.73719229, 29.58285244, 17.60080457, 27.87010362, 13.99855308, 18.70097593, 13.9277982, 23.85284178, 18.31920387, 13.22135245, 15.04909936, 23.86964825 }, 6.125779534)]
        [TestCase(new double[] { 23.51070978, 22.66532622, 28.17583092, 21.83007277, 22.78154063, 25.2060118, 17.83001501, 12.96988008, 18.92628675, 12.06385645, 13.2198093, 23.24768245, 17.66617751, 12.90634097, 17.79476329, 23.97400221, 24.31069775, 15.67630615, 28.07611315, 17.85701141, 11.43159283, 13.34248173, 13.15079415, 25.79101054, 14.0000436 }, 5.394032778)]
        public void StandardDeviationIsCalculatedCorrectly(double[] values, double result)
        {
            var calculatedResult = Statistic.GetStandardDeviation(values);
            Assert.AreEqual(calculatedResult, result, StandardDoubleComparisonDelta);
        }

        [Test]
        public void CalculatingStandardDeviationWithNoValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetStandardDeviation(new double[] { }));
        }

        [Test]
        public void CalculatingStandardDeviationWithNotEnoughValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetStandardDeviation(new double[] { 3.13, 9.5}));
        }

        [Test]
        public void CalculatingVarianzWithNotEnoughValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetVariance(new double[] { 3.13, 9.5 }));
        }


        [Test]
        public void CalculatingStandardDeviationWithNegativeValuesThrowsArgumentException() 
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetStandardDeviation(new double[] { 3.13, -3.34, 4.0 }));
        }

        [TestCase(new double[] { 16.25364162, 22.91414816, 26.65296375, 28.89955632, 16.87575858, 24.33775845, 28.65871777, 10.60209435, 16.10084039, 13.48582094, 29.95374612, 16.45724514, 26.4506641, 15.73719229, 29.58285244, 17.60080457, 27.87010362, 13.99855308, 18.70097593, 13.9277982, 23.85284178, 18.31920387, 13.22135245, 15.04909936, 23.86964825 }, 19.35165177)]
        [TestCase(new double[] { 23.51070978, 22.66532622, 28.17583092, 21.83007277, 22.78154063, 25.2060118, 17.83001501, 12.96988008, 18.92628675, 12.06385645, 13.2198093, 23.24768245, 17.66617751, 12.90634097, 17.79476329, 23.97400221, 24.31069775, 15.67630615, 28.07611315, 17.85701141, 11.43159283, 13.34248173, 13.15079415, 25.79101054, 14.0000436 }, 16.74423809)]
        public void RangeIsCalculatedCorrectly(double[] values, double result)
        {
            var calculatedResult = Statistic.GetRange(values);
            Assert.AreEqual(calculatedResult,result, StandardDoubleComparisonDelta);
        }

        [Test]
        public void CalculatingRangeWithNoValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetRange(new double[] { }));
        }

        [Test]
        public void CalculatingRangeWithRangeWithNegativeValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetRange(new double[] { -3.34, 3.4, 4.3 }));
        }

        [Test]
        public void CalculatingAverageWithNoValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetAverage(new double[] { }));
        }

        [Test]
        public void CalculatingAverageWithNegativeValuesThrowsArgumentException() 
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetAverage(new double[] { -3.13, 3.34, 4.3 }));
        }


        [TestCase(new double[] { 23.51070978, 22.66532622, 28.17583092, 21.83007277, 22.78154063, 25.2060118, 17.83001501, 12.96988008, 18.92628675, 12.06385645, 13.2198093, 23.24768245, 17.66617751, 12.90634097, 17.79476329, 23.97400221, 24.31069775, 15.67630615, 28.07611315, 17.85701141, 11.43159283, 13.34248173, 13.15079415, 25.79101054, 14.0000436 }, 29, 11, 0.502788583)]
        [TestCase(new double[] { 16.25364162, 22.91414816, 26.65296375, 28.89955632, 16.87575858, 24.33775845, 28.65871777, 10.60209435, 16.10084039, 13.48582094, 29.95374612, 16.45724514, 26.4506641, 15.73719229, 29.58285244, 17.60080457, 27.87010362, 13.99855308, 18.70097593, 13.9277982, 23.85284178, 18.31920387, 13.22135245, 15.04909936, 23.86964825 }, 29, 11, 0.469331546)]
        public void CkValueIsCalculatedCorrectly(double[] values, double uppertolerance, double lowertolerance, double result)
        {
            var calculatedResult = Statistic.GetCk(values, uppertolerance, lowertolerance);
            Assert.AreEqual(calculatedResult, result, StandardDoubleComparisonDelta);
        }

        [TestCase(new double[] { 23.51070978, 22.66532622, 28.17583092, 21.83007277, 22.78154063, 25.2060118, 17.83001501, 12.96988008, 18.92628675, 12.06385645, 13.2198093, 23.24768245, 17.66617751, 12.90634097, 17.79476329, 23.97400221, 24.31069775, 15.67630615, 28.07611315, 17.85701141, 11.43159283, 13.34248173, 13.15079415, 25.79101054, 14.0000436 }, 29, 11, 0.556170146)]
        [TestCase(new double[] { 16.25364162, 22.91414816, 26.65296375, 28.89955632, 16.87575858, 24.33775845, 28.65871777, 10.60209435, 16.10084039, 13.48582094, 29.95374612, 16.45724514, 26.4506641, 15.73719229, 29.58285244, 17.60080457, 27.87010362, 13.99855308, 18.70097593, 13.9277982, 23.85284178, 18.31920387, 13.22135245, 15.04909936, 23.86964825 }, 29, 11, 0.48973359 )]
        public void CValueIsCalculatedCorrectly(double[] values, double uppertolerance, double lowertolerance, double result)
        {
            var calculatedResult = Statistic.GetC(values, uppertolerance, lowertolerance);
            Assert.AreEqual(calculatedResult, result, StandardDoubleComparisonDelta);
        }

        [Test]
        public void CalculatingCkValueWithStandardDeviationIsZeroThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => Statistic.GetCk(new double[] {1, 1, 1, 1, 1}, 0.5, 1.5));
        }

        [Test]
        public void CalculatingCValueWithStandardDeviationIsZeroThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => Statistic.GetC(new double[] { 1, 1, 1, 1, 1 }, 0.5, 1.5));
        }

        [Test]
        public void AreValuesOutsideToleranceLimitsWithAllValuesInsideLimitsResultsInFalse()
        {
            Assert.IsFalse(Statistic.AreValuesOutsideToleranceLimits(new double[] {5,6,7 },8,4));
        }

        [TestCase(new double[] { 5, 6, 7 }, 7, 4)]
        [TestCase(new double[] { 5, 6, 7 }, 8, 5)]
        [TestCase(new double[] { 5, 6, 7 }, 7, 5)]
        public void AreValuesOutsideToleranceLimitsWithValuesEqualsLimitsResultsInFalse(double[] values, double uppertolerance, double lowertolerance)
        {
            Assert.IsFalse(Statistic.AreValuesOutsideToleranceLimits(values, uppertolerance, lowertolerance));
        }

        [TestCase(new double[] { 3, 6, 7 }, 8, 4)]
        [TestCase(new double[] { 5, 6, 9 }, 8, 4)]
        [TestCase(new double[] { 3, 6, 9 }, 8, 4)]
        public void AreValuesOutsideToleranceLimitsWithValuesOutsideLimitsResultsInTrue(double[] values, double uppertolerance, double lowertolerance)
        {
            Assert.IsTrue(Statistic.AreValuesOutsideToleranceLimits(values, uppertolerance, lowertolerance));
        }

        [Test]
        public void AreValuesOutsideToleranceLimitsWithNegativeValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                Statistic.AreValuesOutsideToleranceLimits(new double[] {-3.13, 3.34, 4.3}, 3, 5));
        }

        [Test]
        public void AreValuesOutsideToleranceLimitsWithNoValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                Statistic.AreValuesOutsideToleranceLimits(new double[] { }, 3, 5));
        }

        [TestCase(new double[] { 147, 186, 141, 183, 190, 123, 155, 164, 183, 150, 134, 170, 144, 99, 156, 176, 160, 174, 153, 162, 167, 179, 78, 173, 168 },0.6115)]
        public void EppsPulleyTestValueIsCalculatedCorrectly(double[] values, double result)
        {
            var calculatedResult = Statistic.GetEppsPulleyTestValue(values);
            Assert.AreEqual(calculatedResult, result, 0.0001);
        }

        [Test]
        public void CalculatingEppsPulleyTestValueWithNoValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetEppsPulleyTestValue(new double[] { }));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void CalculatingEppsPulleyTestValueWithNotEnoughValuesThrowsArgumentException(int valueCount)
        {
            var values = new double[valueCount];
            Assert.Throws<ArgumentException>(() => Statistic.GetEppsPulleyTestValue(values));
        }

        [Test]
        public void CalculatingEppsPulleyTestValueWithToManyValuesThrowsArgumentException()
        {
            var values = new double[201];
            Assert.Throws<ArgumentException>(() => Statistic.GetEppsPulleyTestValue(values));
        }

        [Test]
        public void CalculatingEppsPulleyTestValueWithMaximumCountOfValuesNotThrowsArgumentException() 
        {
            try
            {
                var values = new double[200];
                Statistic.GetEppsPulleyTestValue(values);
                Assert.Pass();
            }
            catch (ArgumentException)
            {
                Assert.Fail();
            }
        }


        [TestCase(7.5, 1.7078, 3, 2.3766)]
        [TestCase(21.97, 14.1082, 5, -48.571)]
        public void LowerSigmaIsCalculatedCorrectly(double average, double standardDeviation, int sigmaFactor, double result)
        {
            var lowerSigma = Statistic.GetLowerSigma(average, standardDeviation, sigmaFactor);
            Assert.AreEqual(lowerSigma,result, StandardDoubleComparisonDelta);
        }

        [TestCase(7.5, 1.7078, 3, 12.6234)]
        [TestCase(21.97, 14.1082, 5, 92.511)]
        public void UpperSigmaIsCalculatedCorrectly(double average, double standardDeviation, int sigmaFactor, double result)
        {
            var upperSigma = Statistic.GetUpperSigma(average, standardDeviation, sigmaFactor);
            Assert.AreEqual(upperSigma, result, StandardDoubleComparisonDelta);
        }

        private static readonly object[] HistogramFrequencyTestSource =
        {
            new object[] {
                2,
                8,
                new double[] {1, 1, 2, 5, 6, 8, 8, 9, 9, 11 },
                new List<int> {2, 1, 1, 1, 4, 1, 0, 0}},

            new object[] {
                3,
                9,
                new double[] {1, 1, 2, 5, 6, 8, 8, 9, 9, 11 },
                new List<int> {3, 1, 3, 3, 0, 0, 0, 0, 0}},
        };

        [TestCaseSource("HistogramFrequencyTestSource")]
        public void HistogramFrequencyIsCalculatedCorrectly(double columnWidthInXunit, int barcount, double[] values, List<int> erg)
        {
            var frequency = Statistic.GetHistogramFrequency(columnWidthInXunit, barcount,  values);
            Assert.AreEqual(frequency, erg);
        }

        [Test]
        public void CalculatingHistogramFrequencyWithNotEnoughValuesThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Statistic.GetHistogramFrequency(1, 9, new double[0]));
        }

        [Test]
        public void CalculatingHistogramFrequencyWithNotEnoughBarsThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                Statistic.GetHistogramFrequency(1, 0, new double[] {1, 1, 2, 5, 6, 8, 8, 9, 9, 11}));
        }

        [Test]
        public void CalculatingStretchedGaussCurveWithNotEnoughValuesReturnsEmptyList()
        {
            var stretchedCurve = 
                Statistic.GetStretchedGaussCurve(new Dictionary<double, double>(), 1, 10, 5, 1);
            Assert.AreEqual(0, stretchedCurve.Count);
        }

        [Test]
        public void CalculatingStretchedGaussCurveWithWrongMinimumXAndMaximumXValueReturnsEmptyList()
        {
            var gausCurve = new Dictionary<double, double>();
            gausCurve[0] = 1;
            gausCurve[1] = 2;
            gausCurve[2] = 1;

            var stretchedCurve =
                Statistic.GetStretchedGaussCurve(gausCurve, 5.1, 5, 5, 1);
            Assert.AreEqual(0, stretchedCurve.Count);
        }

        private static readonly object[] GetStretchedGaussTestSource =
        {
            new object[] {
                new Dictionary<double, double>()
                {
                    {0, 0 },
                    {1, 1 },
                    {2, 2 },
                    {3, 3 },
                    {4, 4 },
                    {5, 5 },
                    {6, 4 },
                    {7, 3 },
                    {8, 2 },
                    {9, 1 } 
                },
                0, 20, 5, 2,
                new Dictionary<double, double>()
                {
                    {-5, 0 },
                    {-3, 2 },
                    {-1, 4 },
                    {1, 6 },
                    {3, 8 },
                    {5, 10 },
                    {7, 8 },
                    {9, 6 },
                    {11, 4 },
                    {13, 2 }
                }
            },

            new object[] {
                new Dictionary<double, double>()
                {
                    {0, 0 },
                    {2, 2 },
                    {4, 4 },
                    {6, 6 },
                    {8, 8 },
                    {10, 10 },
                    {12, 8 },
                    {14, 6 },
                    {16, 4 },
                    {18, 2 }
                },
                0, 40, 5, 5,
                new Dictionary<double, double>()
                {
                    {-15, 0 },
                    {-11, 10 },
                    {-7, 20 },
                    {-3, 30 },
                    {1, 40 },
                    {5, 50 },
                    {9, 40 },
                    {13, 30 },
                    {17, 20 },
                    {21, 10 }
                }
            }
        };

        [TestCaseSource("GetStretchedGaussTestSource")]
        public void StretchedGaussCurveIsCalculatedCorrectly(Dictionary<double, double> gausCruveValues, double minimumXValue, double maximumXValue, double centerXValue, double factor, Dictionary<double, double> resultGaussValues)
        {
            var stretchedGaussCurve = Statistic.GetStretchedGaussCurve(gausCruveValues, minimumXValue, maximumXValue, centerXValue, factor);
            Assert.AreEqual(stretchedGaussCurve, resultGaussValues);
        }

        private static readonly object[] GetStandardGaussCurveTestSource =
        {
            new object[]
            {
               0.001,

               new Dictionary<int, Tuple<double, double>>
               {
                   {0, new Tuple<double, double>(-3, 0.00443184841193801) },
                   {100, new Tuple<double, double>(-2.90000000000001, 0.00595253241977566) },
                   {500, new Tuple<double, double>(-2.50000000000006, 0.0175283004935661) }
               },
               6001
            },
            new object[]
            {
                0.01,

                new Dictionary<int, Tuple<double, double>>
                {
                    {0, new Tuple<double, double>(-3, 0.00443184841193801) },
                    {500, new Tuple<double, double>(1.99999999999998, 0.05399096651319) },
                },
                601
            },
        };

        [TestCaseSource("GetStandardGaussCurveTestSource")]
        public void StandardGaussCurveIsCalculatedCorrectly(double precision, Dictionary<int, Tuple<double, double>> resultSamples, int count)
        {
            var standardGaussCurve = Statistic.GetStandardGaussCurve(precision);
            Assert.AreEqual(count, standardGaussCurve.Count);

            foreach (var sample in resultSamples)
            {
                var element = standardGaussCurve.ElementAt(sample.Key);
                Assert.AreEqual(element.Key, sample.Value.Item1, StandardDoubleComparisonDelta);
                Assert.AreEqual(element.Value,sample.Value.Item2, StandardDoubleComparisonDelta);
            }
        }
    }
}
