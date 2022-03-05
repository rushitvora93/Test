using System.Collections.Generic;
using System.Linq;
using Core.UseCases.Communication;
using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace Core.Test.UseCases.Communication
{
    class Sta6000FieldRemoveRewriterTest
    {
        [TestCase("19", "AC_MinimumPulse")]
        [TestCase("19", "AC_MaximumPulse")]
        [TestCase("19", "AC_TorqueCoefficient")]
        [TestCase("19", "AC_SlipTorque")]
        [TestCase("18", "AC_MeasureTorqueAt")]
        [TestCase("18", "AC_CmCmkSpcTestType")]
        [TestCase("18", "AC_MinimumPulse")]
        [TestCase("18", "AC_MaximumPulse")]
        [TestCase("18", "AC_TorqueCoefficient")]
        [TestCase("13", "AC_CycleComplete")]
        [TestCase("13", "AC_MeasureDelayTime")]
        [TestCase("13", "AC_ResetTime")]
        [TestCase("13", "AC_SlipTorque")]
        [TestCase("13", "AC_TorqueCoefficient")]
        [TestCase("13", "AC_MeasureTorqueAt")]
        [TestCase("13", "AC_MinimumPulse")]
        [TestCase("13", "AC_MaximumPulse")]
        [TestCase("14", "AC_CycleComplete")]
        [TestCase("14", "AC_MeasureDelayTime")]
        [TestCase("14", "AC_ResetTime")]
        [TestCase("14", "AC_SlipTorque")]
        [TestCase("14", "AC_MeasureTorqueAt")]
        [TestCase("14", "AC_CmCmkSpcTestType")]
        public void RewritingRemovesElement(string testMethod, string fieldToRemove)
        {
            var semanticModel = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("Route"))
                    {
                        new Container(new ElementName("TestItem"))
                        {
                            new Content(new ElementName("TestMethod"), testMethod),
                            new Content(new ElementName(fieldToRemove), "")
                        }
                    }
                });
            var rewriter = new Sta6000FieldRemoveRewriter();
            rewriter.Apply(ref semanticModel);

            var testItemFinder = new FindFirstByName(new ElementName("TestItem"));
            semanticModel.Accept(testItemFinder);
            Assert.AreEqual(0, (testItemFinder.Result as Container).Count(element => element.GetName().Equals(new ElementName(fieldToRemove))));
        }

        [Test]
        public void RewritingGeneralDriverLeavesRest()
        {
            var leftovers = new List<IElement>
            {
                new Content(new ElementName("unrelated"),""),
                new Content(new ElementName("TestMethod"), "19"), // general
                new Content(new ElementName("AC_CycleComplete"),""),
                new Content(new ElementName("AC_EndTime"),""),
                new Content(new ElementName("AC_MeasureDelayTime"),""),
                new Content(new ElementName("AC_ResetTime"),""),
                new Content(new ElementName("AC_FilterFreq"),""),
                new Content(new ElementName("AC_MeasureTorqueAt"),""),
                new Content(new ElementName("AC_CmCmkSpcTestType"),""),
                new Content(new ElementName("AC_MinimumCm"),""),
                new Content(new ElementName("AC_MinimumCmk"),""),
                new Content(new ElementName("AC_MinimumCmAngle"),""),
                new Content(new ElementName("AC_MinimumCmkAngle"),""),
                new Content(new ElementName("AC_TestType"),""),
                new Content(new ElementName("AC_SubgroupSize"),""),
                new Content(new ElementName("AC_SubgroupFrequency"),""),
            };
            var semanticModel = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("Route"))
                    {
                        new Container(new ElementName("TestItem"))
                        {
                            leftovers[0],
                            leftovers[1],
                            new Content(new ElementName("AC_MaximumPulse"), ""),
                            new Content(new ElementName("AC_TorqueCoefficient"), ""),
                            leftovers[2],
                            new Content(new ElementName("AC_MinimumPulse"), ""),
                            leftovers[3],
                            new Content(new ElementName("AC_SlipTorque"), ""),
                            leftovers[4],
                            leftovers[5],
                            leftovers[6],
                            leftovers[7],
                            leftovers[8],
                            leftovers[9],
                            leftovers[10],
                            leftovers[11],
                            leftovers[12],
                            leftovers[13],
                            leftovers[14],
                            leftovers[15],
                        }
                    }
                });
            var rewriter = new Sta6000FieldRemoveRewriter();
            rewriter.Apply(ref semanticModel);

            var testItemFinder = new FindFirstByName(new ElementName("TestItem"));
            semanticModel.Accept(testItemFinder);
            CollectionAssert.AreEqual(leftovers, testItemFinder.Result as Container);
        }

        [Test]
        public void RewritingClickWrenchLeavesRest()
        {
            var leftovers = new List<IElement>
            {
                new Content(new ElementName("unrelated"),""),
                new Content(new ElementName("TestMethod"), "18"), // click
                new Content(new ElementName("AC_CycleComplete"),""),
                new Content(new ElementName("AC_EndTime"),""),
                new Content(new ElementName("AC_MeasureDelayTime"),""),
                new Content(new ElementName("AC_ResetTime"),""),
                new Content(new ElementName("AC_FilterFreq"),""),
                new Content(new ElementName("AC_MinimumCm"),""),
                new Content(new ElementName("AC_MinimumCmk"),""),
                new Content(new ElementName("AC_MinimumCmAngle"),""),
                new Content(new ElementName("AC_MinimumCmkAngle"),""),
                new Content(new ElementName("AC_TestType"),""),
                new Content(new ElementName("AC_SubgroupSize"),""),
                new Content(new ElementName("AC_SubgroupFrequency"),""),
                new Content(new ElementName("AC_SlipTorque"), ""),
            };
            var semanticModel = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("Route"))
                    {
                        new Container(new ElementName("TestItem"))
                        {
                            leftovers[0],
                            leftovers[1],
                            leftovers[2],
                            new Content(new ElementName("AC_TorqueCoefficient"), ""),
                            leftovers[3],
                            leftovers[4],
                            leftovers[5],
                            leftovers[6],
                            new Content(new ElementName("AC_MinimumPulse"), ""),
                            new Content(new ElementName("AC_MaximumPulse"), ""),
                            leftovers[7],
                            leftovers[8],
                            leftovers[9],
                            new Content(new ElementName("AC_CmCmkSpcTestType"),""),
                            leftovers[10],
                            leftovers[11],
                            leftovers[12],
                            leftovers[13],
                            leftovers[14],
                            new Content(new ElementName("AC_MeasureTorqueAt"),""),
                        }
                    }
                });
            var rewriter = new Sta6000FieldRemoveRewriter();
            rewriter.Apply(ref semanticModel);

            var testItemFinder = new FindFirstByName(new ElementName("TestItem"));
            semanticModel.Accept(testItemFinder);
            CollectionAssert.AreEqual(leftovers, testItemFinder.Result as Container);
        }

        [Test]
        public void RewritingPeakLeavesRest()
        {
            var leftovers = new List<IElement>
            {
                new Content(new ElementName("unrelated"),""),
                new Content(new ElementName("TestMethod"), "13"), // peak
                new Content(new ElementName("AC_EndTime"),""),
                new Content(new ElementName("AC_FilterFreq"),""),
                new Content(new ElementName("AC_MinimumCm"),""),
                new Content(new ElementName("AC_MinimumCmk"),""),
                new Content(new ElementName("AC_MinimumCmAngle"),""),
                new Content(new ElementName("AC_MinimumCmkAngle"),""),
                new Content(new ElementName("AC_TestType"),""),
                new Content(new ElementName("AC_SubgroupSize"),""),
                new Content(new ElementName("AC_SubgroupFrequency"),""),

                new Content(new ElementName("AC_CmCmkSpcTestType"),""),
            };
            var semanticModel = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("Route"))
                    {
                        new Container(new ElementName("TestItem"))
                        {
                            new Content(new ElementName("AC_CycleComplete"),""),
                            new Content(new ElementName("AC_MeasureTorqueAt"),""),
                            leftovers[0],
                            new Content(new ElementName("AC_SlipTorque"), ""),
                            leftovers[1],
                            leftovers[2],
                            leftovers[3],
                            new Content(new ElementName("AC_MinimumPulse"), ""),
                            leftovers[4],
                            new Content(new ElementName("AC_MeasureDelayTime"),""),
                            leftovers[5],
                            leftovers[6],
                            leftovers[7],
                            new Content(new ElementName("AC_TorqueCoefficient"), ""),
                            leftovers[8],
                            leftovers[9],
                            new Content(new ElementName("AC_ResetTime"),""),
                            leftovers[10],
                            leftovers[11],
                            new Content(new ElementName("AC_MaximumPulse"), ""),
                        }
                    }
                });
            var rewriter = new Sta6000FieldRemoveRewriter();
            rewriter.Apply(ref semanticModel);

            var testItemFinder = new FindFirstByName(new ElementName("TestItem"));
            semanticModel.Accept(testItemFinder);
            CollectionAssert.AreEqual(leftovers, testItemFinder.Result as Container);
        }

        [Test]
        public void RewritingPulseLeavesRest()
        {
            var leftovers = new List<IElement>
            {
                new Content(new ElementName("unrelated"),""),
                new Content(new ElementName("TestMethod"), "14"), // pulse
                new Content(new ElementName("AC_EndTime"),""),
                new Content(new ElementName("AC_FilterFreq"),""),
                new Content(new ElementName("AC_MinimumCm"),""),
                new Content(new ElementName("AC_MinimumCmk"),""),
                new Content(new ElementName("AC_MinimumCmAngle"),""),
                new Content(new ElementName("AC_MinimumCmkAngle"),""),
                new Content(new ElementName("AC_TestType"),""),
                new Content(new ElementName("AC_SubgroupSize"),""),
                new Content(new ElementName("AC_SubgroupFrequency"),""),
                new Content(new ElementName("AC_TorqueCoefficient"), ""),
                new Content(new ElementName("AC_MinimumPulse"), ""),
                new Content(new ElementName("AC_MaximumPulse"), ""),
            };
            var semanticModel = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("Route"))
                    {
                        new Container(new ElementName("TestItem"))
                        {
                            leftovers[0],
                            leftovers[1],
                            new Content(new ElementName("AC_MeasureDelayTime"),""),
                            new Content(new ElementName("AC_SlipTorque"), ""),
                            new Content(new ElementName("AC_MeasureTorqueAt"),""),
                            new Content(new ElementName("AC_CmCmkSpcTestType"),""),
                            leftovers[2],
                            new Content(new ElementName("AC_CycleComplete"),""),
                            leftovers[3],
                            leftovers[4],
                            leftovers[5],
                            leftovers[6],
                            leftovers[7],
                            new Content(new ElementName("AC_ResetTime"),""),
                            leftovers[8],
                            leftovers[9],
                            leftovers[10],
                            leftovers[11],
                            leftovers[12],
                            leftovers[13],
                        }
                    }
                });
            var rewriter = new Sta6000FieldRemoveRewriter();
            rewriter.Apply(ref semanticModel);

            var testItemFinder = new FindFirstByName(new ElementName("TestItem"));
            semanticModel.Accept(testItemFinder);
            CollectionAssert.AreEqual(leftovers, testItemFinder.Result as Container);
        }

        [Test]
        public void RewritingWorksOnMultipleItems()
        {
            var leftovers = new List<IElement>
            {
                new Content(new ElementName("TestMethod"), "14"),
                new Content(new ElementName("TestMethod"), "19"),
                new Content(new ElementName(""), "")
            };
            var semanticModel = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("Route"))
                    {
                        new Container(new ElementName("TestItem"))
                        {
                            leftovers[0],
                            new Content(new ElementName("AC_MeasureTorqueAt"),""),
                        },
                        new Container(new ElementName("TestItem"))
                        {
                            leftovers[1],
                            new Content(new ElementName("AC_SlipTorque"), ""),
                            leftovers[2],
                            new Content(new ElementName("AC_SlipTorque"), ""),
                        }
                    }
                });
            var rewriter = new Sta6000FieldRemoveRewriter();
            rewriter.Apply(ref semanticModel);

            CollectionAssert.AreEqual(leftovers, GetLeftoverElementList(semanticModel));
        }

        private static List<IElement> GetLeftoverElementList(SemanticModel semanticModel)
        {
            var routeFinder = new FindFirstByName(new ElementName("Route"));
            semanticModel.Accept(routeFinder);
            var route = routeFinder.Result as Container;
            var result = new List<IElement>();
            foreach(var element in route)
            {
                if (element.GetName().Equals(new ElementName("TestItem")))
                {
                    var testItem = element as Container;
                    foreach (var testItemElement in testItem)
                    {
                        result.Add(testItemElement);
                    }
                }
            }
            return result;
        }
    }
}