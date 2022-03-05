using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class Statistic
    {
        //functions for calculating values ​​from inductive statistics
        private static void ValidateValues(double[] values, int neededvalues, int? maxvalues = null)
        {
            if (values == null || values.Count() < neededvalues || values.Min() < 0)
            {
                throw new ArgumentException("");
            }
            if (maxvalues != null && values.Count() > maxvalues)
            {
                throw new ArgumentException("");
            }
        }

        public static double GetStandardDeviation(double[] values)
        {
            return Math.Sqrt(GetVariance(values));
        }

        public static double GetRange(double[] values)
        {
            ValidateValues(values,1);
            return values.Max() - values.Min();
        }

        public static double GetVariance(double[] values)
        {
            ValidateValues(values,3);
            var average = GetAverage(values);
            var result = 0.0;
            foreach (var value in values)
            {
                result += Math.Pow(value - average, 2);
            }
            //for inductive statistic calculate with 1/(n-1)
            result /= (values.Count() - 1);
            return result;
        }

        public static double GetAverage(double[] values)
        {
            ValidateValues(values,1);
            return values.Average(); 
        }

        public static double GetLowerSigma(double average, double standardDeviation, double sigmaFactor)
        {
            return average - sigmaFactor * standardDeviation;
        }

        public static double GetUpperSigma(double average, double standardDeviation, double sigmaFactor)
        {
            return average + sigmaFactor * standardDeviation;
        }

        public static double GetCk(double[] values, double uppertolerance, double lowertolerance)
        {
            var average = GetAverage(values);
            var standardDeviation = GetStandardDeviation(values);
            if (standardDeviation == 0)
            {
                throw new DivideByZeroException();
            }
            return Math.Min((uppertolerance-average)/(3*standardDeviation), (average - lowertolerance) / (3 * standardDeviation));
        }
        public static double GetC(double[] values, double uppertolerance, double lowertolerance)
        {
            var standardDeviation = GetStandardDeviation(values);
            if (standardDeviation == 0)
            {
                throw new DivideByZeroException();
            }
            return (uppertolerance - lowertolerance) / (6 * standardDeviation);
        }

        public static bool AreValuesOutsideToleranceLimits(double[] values, double uppertolerance, double lowertolerance)
        {
            ValidateValues(values, 1);
            return values.Max() > uppertolerance || values.Min() < lowertolerance;
        }

        public static double GetEppsPulleyTestValue(double[] values)
        {
            ValidateValues(values, 8, 200);
            double valuesCount = values.Count();
            var average = GetAverage(values);

            double m2 = 0;            
            foreach (var val in values)
            {
                m2 += Math.Pow((val - average), 2);
            }
            m2 /= valuesCount;

            var linearPart = 1 + valuesCount / Math.Sqrt(3);
            double doubleSum = 0;
            for (var k = 1; k < valuesCount; k++)
            {
                for (var j = 0; j < k; j++)
                {
                    doubleSum += Math.Exp((-Math.Pow(values[j] - values[k], 2))/(2*m2));
                }
            }
            doubleSum *= (2 / valuesCount);

            double sum = 0;
            for (var j = 0; j < valuesCount; j++)
            {
                sum += Math.Exp((-Math.Pow((values[j]-average),2))/(4*m2));
            }
            sum *= Math.Sqrt(2);

            return linearPart + doubleSum - sum;
        }

        public static Dictionary<double, double> GetStandardGaussCurve(double precision)
        {
            var dict = new Dictionary<double, double>();
            for (double x = -3; x <= 3; x += precision)
            {
                dict.Add(x, (1 / Math.Sqrt(2 * Math.PI)) * Math.Exp((-Math.Pow(x, 2)) / (2)));
            }
            return dict;
        }

        private static bool CheckStretchedGaussCurvParameter(Dictionary<double, double> standardGausCurve, double minimumXValue, double maximumXValue)
        {
            if (standardGausCurve == null || standardGausCurve.Count < 3)
            {
                return false;
            }
            if (minimumXValue > maximumXValue)
            {
                return false;
            }
            return true;
        }

        public static Dictionary<double, double> GetStretchedGaussCurve(Dictionary<double, double> gaussCurveValues, double minimumXValue, double maximumXValue, double centerXValue, double yFactor)
        {
            if (!CheckStretchedGaussCurvParameter(gaussCurveValues, minimumXValue, maximumXValue))
            {
                return new Dictionary<double, double>();
            }

            var xValueStepSize = (minimumXValue - maximumXValue) / gaussCurveValues.Count * -1;

            var stretchedGaussCurve = new Dictionary<double, double>();
            var centerIndexOfGaussCurve = (gaussCurveValues.Count - (gaussCurveValues.Count - 1) / 2 ) - 1;
            var centerOfGaussCurve = gaussCurveValues.ElementAt(centerIndexOfGaussCurve);
            
            //move left part of gausscurve
            var j = centerIndexOfGaussCurve;
            for (var i = 0; i < centerIndexOfGaussCurve; ++i)
            {
                var val = gaussCurveValues.ElementAt(i);
                var stretchedXValue = centerXValue - (j-- * xValueStepSize);
                var stretchedYValue = val.Value * yFactor;
                stretchedGaussCurve[stretchedXValue] = stretchedYValue;
            }

            //move center of gausscurve
            stretchedGaussCurve[centerXValue] = centerOfGaussCurve.Value * yFactor;

            //move right part of gausscurve
            j = 1;
            for (var i = centerIndexOfGaussCurve + 1; i < gaussCurveValues.Count(); ++i)
            {
                var val = gaussCurveValues.ElementAt(i);

                var stretchedXValue = centerXValue + (j++ * xValueStepSize);
                var stretchedYValue = val.Value * yFactor;
                stretchedGaussCurve[stretchedXValue] = stretchedYValue;
            }

            return stretchedGaussCurve;
        }

        public static List<int> GetHistogramFrequency(double columnWidthInXunit, int barCount, double[] values)
        {
            ValidateValues(values, 1);
            if (barCount == 0)
            {
                throw new ArgumentException("");
            }

            var frequencies = new int[barCount];
            var classes = new double[barCount];
            var minValue = values.Min();

            classes[0] = minValue - (columnWidthInXunit / 2.0);

            for (var i = 1; i < barCount; ++i)
            {
                classes[i] = classes[i - 1] + columnWidthInXunit;
                frequencies[i - 1] = 0;

                foreach (var val in values)
                {
                    if (val >= classes[i - 1] && (val < classes[i]))
                    {
                        frequencies[i - 1]++;
                    }
                }
            }

            return frequencies.ToList();
        }
    }
}
