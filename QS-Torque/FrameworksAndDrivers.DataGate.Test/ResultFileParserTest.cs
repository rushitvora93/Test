using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Core.UseCases.Communication;
using NUnit.Framework;
using TestHelper.Checker;

namespace FrameworksAndDrivers.DataGate.Test
{
    class ResultFileParserTest
    {
        public class ResultParserTestDefinition
        {
            public string ResultFile;
            public DataGateResults ExpectedResults;
        }

        public static List<ResultParserTestDefinition> ResultParserTestDefinitions =
            new List<ResultParserTestDefinition>
            {
                new ResultParserTestDefinition // single loc, single test, single sample
                {
                    ResultFile =
                        @"<sDataGate>
                            <ResultList>
                                <FileItem>
                                    <TestId1>123456</TestId1>
                                    <Unit1Id>0</Unit1Id>
                                    <Nom1>5.5</Nom1>
                                    <Min1>4.2</Min1>
                                    <Max1>6.8</Max1>
                                    <Unit2Id>50</Unit2Id>
                                    <Nom2>7.33</Nom2>
                                    <Min2>123.6</Min2>
                                    <Max2>167.08</Max2>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>1</SamplePos>
                                        <TimeStamp>1993-11-30 18:55:59</TimeStamp>
                                        <Value1>20</Value1>
                                        <Value2>8.56</Value2>
                                    </Test>
                                </FileItem>
                            </ResultList>
                        </sDataGate>",
                    ExpectedResults = new DataGateResults
                    {
                        Results = new List<DataGateResult>
                        {
                            new DataGateResult
                            {
                                LocationToolAssignmentId = 123456,
                                Unit1Id = 0,
                                Nom1 = 5.5,
                                Min1 = 4.2,
                                Max1 = 6.8,
                                Unit2Id = 50,
                                Nom2 = 7.33,
                                Min2 = 123.6,
                                Max2 = 167.08,
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Timestamp = new DateTime(1993, 11, 30, 18, 55, 59),
                                        Value1 = 20,
                                        Value2 = 8.56
                                    }
                                }
                            }
                        }
                    }
                },
                new ResultParserTestDefinition // different values single loc, single test, single sample
                {
                    ResultFile =
                        @"<sDataGate>
                            <ResultList>
                                <FileItem>
                                    <TestId1>666</TestId1>
                                    <Unit1Id>10</Unit1Id>
                                    <Nom1>8</Nom1>
                                    <Min1>3.1</Min1>
                                    <Max1>7.9</Max1>
                                    <Unit2Id>99</Unit2Id>
                                    <Nom2>4.5</Nom2>
                                    <Min2>2</Min2>
                                    <Max2>999.99</Max2>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>1</SamplePos>
                                        <TimeStamp>2020-07-24 02:34:40</TimeStamp>
                                        <Value1>1.62252927</Value1>
                                        <Value2>0.3515625</Value2>
                                    </Test>
                                </FileItem>
                            </ResultList>
                        </sDataGate>",
                    ExpectedResults = new DataGateResults
                    {
                        Results = new List<DataGateResult>
                        {
                            new DataGateResult
                            {
                                LocationToolAssignmentId = 666,
                                Unit1Id = 10,
                                Nom1 = 8,
                                Min1 = 3.1,
                                Max1 = 7.9,
                                Unit2Id = 99,
                                Nom2 = 4.5,
                                Min2 = 2,
                                Max2 = 999.99,
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Timestamp = new DateTime(2020, 07, 24, 2, 34, 40),
                                        Value1 = 1.62252927,
                                        Value2 = 0.3515625
                                    }
                                }
                            }
                        }
                    }
                },
                new ResultParserTestDefinition // single loc, single test, multiple samples
                {
                    ResultFile =
                        @"<sDataGate>
                            <ResultList>
                                <FileItem>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>1</SamplePos>
                                        <Value1>5</Value1>
                                        <Value2>6</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>2</SamplePos>
                                        <Value1>8</Value1>
                                        <Value2>9</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>3</SamplePos>
                                        <Value1>7</Value1>
                                        <Value2>1</Value2>
                                    </Test>
                                </FileItem>
                            </ResultList>
                        </sDataGate>",
                    ExpectedResults = new DataGateResults
                    {
                        Results = new List<DataGateResult>
                        {
                            new DataGateResult
                            {
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Value1 = 5,
                                        Value2 = 6
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 8,
                                        Value2 = 9
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 7,
                                        Value2 = 1
                                    }
                                }
                            }
                        }
                    }
                }, 
                new ResultParserTestDefinition // different values single loc, single test, multiple samples
                {
                    ResultFile =
                        @"<sDataGate>
                            <ResultList>
                                <FileItem>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>1</SamplePos>
                                        <Value1>1</Value1>
                                        <Value2>2</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>2</SamplePos>
                                        <Value1>3</Value1>
                                        <Value2>4</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>3</SamplePos>
                                        <Value1>5</Value1>
                                        <Value2>6</Value2>
                                    </Test>
                                </FileItem>
                            </ResultList>
                        </sDataGate>",
                    ExpectedResults = new DataGateResults
                    {
                        Results = new List<DataGateResult>
                        {
                            new DataGateResult
                            {
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Value1 = 1,
                                        Value2 = 2
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 3,
                                        Value2 = 4
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 5,
                                        Value2 = 6
                                    }
                                }
                            }
                        }
                    }
                },
                new ResultParserTestDefinition // single loc, multiple tests, multiple samples
                {
                    ResultFile =
                        @"<sDataGate>
                            <ResultList>
                                <FileItem>
                                    <TestId1>7</TestId1>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>1</SamplePos>
                                        <Value1>6</Value1>
                                        <Value2>7</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>2</SamplePos>
                                        <Value1>9</Value1>
                                        <Value2>10</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>3</SamplePos>
                                        <Value1>8</Value1>
                                        <Value2>2</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>2</Sample>
                                        <SamplePos>1</SamplePos>
                                        <Value1>1</Value1>
                                        <Value2>2</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>2</Sample>
                                        <SamplePos>2</SamplePos>
                                        <Value1>3</Value1>
                                        <Value2>4</Value2>
                                    </Test>
                                    <Test>
                                        <Sample>2</Sample>
                                        <SamplePos>3</SamplePos>
                                        <Value1>5</Value1>
                                        <Value2>6</Value2>
                                    </Test>
                                </FileItem>
                            </ResultList>
                        </sDataGate>",
                    ExpectedResults = new DataGateResults
                    {
                        Results = new List<DataGateResult>
                        {
                            new DataGateResult
                            {
                                LocationToolAssignmentId = 7,
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Value1 = 6,
                                        Value2 = 7
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 9,
                                        Value2 = 10
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 8,
                                        Value2 = 2
                                    }
                                }
                            },
                            new DataGateResult
                            {
                                LocationToolAssignmentId = 7,
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Value1 = 1,
                                        Value2 = 2
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 3,
                                        Value2 = 4
                                    },
                                    new DataGateResultValue
                                    {
                                        Value1 = 5,
                                        Value2 = 6
                                    }
                                }
                            }
                        }
                    }
                },
                                new ResultParserTestDefinition // multiple loc, single test, single sample
                {
                    ResultFile =
                        @"<sDataGate>
                            <ResultList>
                                <FileItem>
                                    <TestId1>123456</TestId1>
                                    <Unit1Id>0</Unit1Id>
                                    <Nom1>5.5</Nom1>
                                    <Min1>4.2</Min1>
                                    <Max1>6.8</Max1>
                                    <Unit2Id>50</Unit2Id>
                                    <Nom2>7.33</Nom2>
                                    <Min2>123.6</Min2>
                                    <Max2>167.08</Max2>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>1</SamplePos>
                                        <TimeStamp>1990-05-01 18:55:59</TimeStamp>
                                        <Value1>20</Value1>
                                        <Value2>8.56</Value2>
                                    </Test>
                                </FileItem>
                                <FileItem>
                                    <TestId1>67891011</TestId1>
                                    <Unit1Id>6</Unit1Id>
                                    <Nom1>6</Nom1>
                                    <Min1>4</Min1>
                                    <Max1>1</Max1>
                                    <Unit2Id>7</Unit2Id>
                                    <Nom2>9</Nom2>
                                    <Min2>3</Min2>
                                    <Max2>3.08</Max2>
                                    <Test>
                                        <Sample>1</Sample>
                                        <SamplePos>1</SamplePos>
                                        <TimeStamp>1993-11-30 18:55:59</TimeStamp>
                                        <Value1>1</Value1>
                                        <Value2>2</Value2>
                                    </Test>
                                </FileItem>
                            </ResultList>
                        </sDataGate>",
                    ExpectedResults = new DataGateResults
                    {
                        Results = new List<DataGateResult>
                        {
                            new DataGateResult
                            {
                                LocationToolAssignmentId = 123456,
                                Unit1Id = 0,
                                Nom1 = 5.5,
                                Min1 = 4.2,
                                Max1 = 6.8,
                                Unit2Id = 50,
                                Nom2 = 7.33,
                                Min2 = 123.6,
                                Max2 = 167.08,
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Timestamp = new DateTime(1990, 5, 1, 18, 55, 59),
                                        Value1 = 20,
                                        Value2 = 8.56
                                    }
                                }
                            },
                            new DataGateResult
                            {
                                LocationToolAssignmentId = 67891011,
                                Unit1Id = 6,
                                Nom1 = 6,
                                Min1 = 4,
                                Max1 = 1,
                                Unit2Id = 7,
                                Nom2 = 9,
                                Min2 = 3,
                                Max2 = 3.08,
                                Values = new List<DataGateResultValue>
                                {
                                    new DataGateResultValue
                                    {
                                        Timestamp = new DateTime(1993, 11, 30, 18, 55, 59),
                                        Value1 = 1,
                                        Value2 = 2
                                    }
                                }
                            }
                        }
                    }
                }
            };

        [TestCaseSource(nameof(ResultParserTestDefinitions))]
        public void ParsingResultsFileMakesDataGateResult(ResultParserTestDefinition testData)
        {
            var result = new ResultFileParser().Parse(XElement.Parse(testData.ResultFile));
            for (var i = 0; i < result.Results.Count; ++i)
            {
                CheckerFunctions.CollectionAssertAreEquivalent(
                    testData.ExpectedResults.Results[i].Values, 
                    result.Results[i].Values,
                    (expected, actual) =>
                        (expected.Timestamp,
                            expected.Value1,
                            expected.Value2)
                        .Equals((actual.Timestamp,
                            actual.Value1,
                            actual.Value2)));
            }
            CheckerFunctions.CollectionAssertAreEquivalent(
                testData.ExpectedResults.Results,
                result.Results,
                (expected, actual) =>
                    (expected.LocationToolAssignmentId,
                        expected.Unit1Id,
                        expected.Nom1,
                        expected.Min1,
                        expected.Max1,
                        expected.Unit2Id,
                        expected.Nom2,
                        expected.Min2,
                        expected.Max2)
                    .Equals(
                        (actual.LocationToolAssignmentId,
                            actual.Unit1Id,
                            actual.Nom1,
                            actual.Min1,
                            actual.Max1,
                            actual.Unit2Id,
                            actual.Nom2,
                            actual.Min2,
                            actual.Max2)));
        }
    }
}
