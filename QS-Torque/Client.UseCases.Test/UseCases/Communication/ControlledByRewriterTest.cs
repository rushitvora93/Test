using System.Collections.Generic;
using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace Core.Test.UseCases.Communication.DataGate
{
    class ControlledByRewriterTest
    {
        [Test]
        public void RewritingTestItemControlledByTorqueMakesUnit1Id0()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeTorqueControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit1Id"));
            model.Accept(finder);
            Assert.AreEqual("0", (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemControlledByTorqueMakesUnit1Nm()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeTorqueControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit1"));
            model.Accept(finder);
            Assert.AreEqual("Nm", (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemControlledByTorqueMakesUnit2Id10()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeTorqueControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit2Id"));
            model.Accept(finder);
            Assert.AreEqual("10", (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemControlledByTorqueMakesUnit2Deg()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeTorqueControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit2"));
            model.Accept(finder);
            Assert.AreEqual("Deg", (finder.Result as Content).GetValue());
        }

        [TestCase("DimensionTorqueNominal", "Nom1", "jadklfjaklfsf")]
        [TestCase("DimensionTorqueNominal", "Nom1", "asdfjaklsf")]
        [TestCase("DimensionTorqueMin", "Min1", "fjfioejfwoefmv")]
        [TestCase("DimensionTorqueMin", "Min1", "iovmeoimlsd")]
        [TestCase("DimensionTorqueMax", "Max1", "fjfioejfwoefmv")]
        [TestCase("DimensionTorqueMax", "Max1", "iovmeoimlsd")]
        [TestCase("DimensionAngleNominal", "Nom2", "fjfioejfwoefmv")]
        [TestCase("DimensionAngleNominal", "Nom2", "iovmeoimlsd")]
        [TestCase("DimensionAngleMin", "Min2", "fjfioejfwoefmv")]
        [TestCase("DimensionAngleMin", "Min2", "iovmeoimlsd")]
        [TestCase("DimensionAngleMax", "Max2", "fjfioejfwoefmv")]
        [TestCase("DimensionAngleMax", "Max2", "iovmeoimlsd")]
        public void RewritingTestItemControlledByTorqueSetsCorrectValues(string source, string target, string value)
        {
            var hiddenValues = MakeTorqueControlledHiddenValues();
            hiddenValues[source] = value;
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(hiddenValues)
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName(target));
            model.Accept(finder);
            Assert.AreEqual(value, (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemControlledByAngleMakesUnit1Id10()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeAngleControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit1Id"));
            model.Accept(finder);
            Assert.AreEqual("10", (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemControlledByAngleMakesUnit1Deg()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeAngleControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit1"));
            model.Accept(finder);
            Assert.AreEqual("Deg", (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemControlledByAngleMakesUnit2Id0()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeAngleControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit2Id"));
            model.Accept(finder);
            Assert.AreEqual("0", (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemControlledByAngleMakesUnit2Nm()
        {
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(MakeAngleControlledHiddenValues())
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName("Unit2"));
            model.Accept(finder);
            Assert.AreEqual("Nm", (finder.Result as Content).GetValue());
        }

        [TestCase("DimensionAngleNominal", "Nom1", "jadklfjaklfsf")]
        [TestCase("DimensionAngleNominal", "Nom1", "asdfjaklsf")]
        [TestCase("DimensionAngleMin", "Min1", "fjfioejfwoefmv")]
        [TestCase("DimensionAngleMin", "Min1", "iovmeoimlsd")]
        [TestCase("DimensionAngleMax", "Max1", "fjfioejfwoefmv")]
        [TestCase("DimensionAngleMax", "Max1", "iovmeoimlsd")]
        [TestCase("DimensionTorqueNominal", "Nom2", "fjfioejfwoefmv")]
        [TestCase("DimensionTorqueNominal", "Nom2", "iovmeoimlsd")]
        [TestCase("DimensionTorqueMin", "Min2", "fjfioejfwoefmv")]
        [TestCase("DimensionTorqueMin", "Min2", "iovmeoimlsd")]
        [TestCase("DimensionTorqueMax", "Max2", "fjfioejfwoefmv")]
        [TestCase("DimensionTorqueMax", "Max2", "iovmeoimlsd")]
        public void RewritingTestItemControlledByAngleSetsCorrectValues(string source, string target, string value)
        {
            var hiddenValues = MakeAngleControlledHiddenValues();
            hiddenValues[source] = value;
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    new Container(new ElementName("RouteList"))
                    {
                        new Container(new ElementName("Route"))
                        {
                            CreateTestItemParametrized(hiddenValues)
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            var finder = new FindFirstByName(new ElementName(target));
            model.Accept(finder);
            Assert.AreEqual(value, (finder.Result as Content).GetValue());
        }

        [Test]
        public void RewritingTestItemLeavesUnrelatedContentUntouched()
        {
            var unrelatedNodes = new List<Content>
            {
                new Content(new ElementName("unrelated"), "originalValue"),
                new Content(new ElementName("unrelated"), "originalValue"),
                new Content(new ElementName("unrelated"), "originalValue"),
                new Content(new ElementName("unrelated"), "originalValue")
            };
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    unrelatedNodes[0],
                    new Container(new ElementName("RouteList"))
                    {
                        unrelatedNodes[1],
                        new Container(new ElementName("Route"))
                        {
                            unrelatedNodes[2],
                            new Container(new ElementName("TestItem"))
                            {
                                unrelatedNodes[3],
                                new HiddenContent(new ElementName("ControlDimension"), "1"),
                                new HiddenContent(new ElementName("DimensionTorqueNominal"), "torquenom"),
                                new HiddenContent(new ElementName("DimensionTorqueMin"), "torquemin"),
                                new HiddenContent(new ElementName("DimensionTorqueMax"), "torquemax"),
                                new HiddenContent(new ElementName("DimensionAngleNominal"), "anglenom"),
                                new HiddenContent(new ElementName("DimensionAngleMin"), "anglemin"),
                                new HiddenContent(new ElementName("DimensionAngleMax"), "anglemax"),
                                new Content(new ElementName("Unit1Id"), ""),
                                new Content(new ElementName("Unit1"), ""),
                                new Content(new ElementName("Nom1"), ""),
                                new Content(new ElementName("Min1"), ""),
                                new Content(new ElementName("Max1"), ""),
                                new Content(new ElementName("Unit2Id"), ""),
                                new Content(new ElementName("Unit2"), ""),
                                new Content(new ElementName("Nom2"), ""),
                                new Content(new ElementName("Min2"), ""),
                                new Content(new ElementName("Max2"), ""),
                            }
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            foreach (var unrelatedNode in unrelatedNodes)
            {
                Assert.AreEqual("originalValue", unrelatedNode.GetValue());
            }
        }

        [Test]
        public void RewritingTestItemLeavesUnrelatedContentWithSameNameUntouched()
        {
            var unrelatedNodes = new List<Content>
            {
                new Content(new ElementName("Unit1Id"), "originalValue"),
                new Content(new ElementName("Unit1Id"), "originalValue"),
                new Content(new ElementName("Unit1Id"), "originalValue")
            };
            var model = new SemanticModel(
                new Container(new ElementName("sDataGate"))
                {
                    unrelatedNodes[0],
                    new Container(new ElementName("RouteList"))
                    {
                        unrelatedNodes[1],
                        new Container(new ElementName("Route"))
                        {
                            unrelatedNodes[2],
                            new Container(new ElementName("TestItem"))
                            {
                                new HiddenContent(new ElementName("ControlDimension"), "1"),
                                new HiddenContent(new ElementName("DimensionTorqueNominal"), "torquenom"),
                                new HiddenContent(new ElementName("DimensionTorqueMin"), "torquemin"),
                                new HiddenContent(new ElementName("DimensionTorqueMax"), "torquemax"),
                                new HiddenContent(new ElementName("DimensionAngleNominal"), "anglenom"),
                                new HiddenContent(new ElementName("DimensionAngleMin"), "anglemin"),
                                new HiddenContent(new ElementName("DimensionAngleMax"), "anglemax"),
                                new Content(new ElementName("Unit1Id"), ""),
                                new Content(new ElementName("Unit1"), ""),
                                new Content(new ElementName("Nom1"), ""),
                                new Content(new ElementName("Min1"), ""),
                                new Content(new ElementName("Max1"), ""),
                                new Content(new ElementName("Unit2Id"), ""),
                                new Content(new ElementName("Unit2"), ""),
                                new Content(new ElementName("Nom2"), ""),
                                new Content(new ElementName("Min2"), ""),
                                new Content(new ElementName("Max2"), ""),
                            }
                        }
                    }
                });

            var controlledByRewriter = new ControlledByRewriter();
            controlledByRewriter.Apply(ref model);

            foreach (var unrelatedNode in unrelatedNodes)
            {
                Assert.AreEqual("originalValue", unrelatedNode.GetValue());
            }
        }

        private static Container CreateTestItemParametrized(Dictionary<string, string> hiddenvalues)
        {
            var testItem = new Container(new ElementName("TestItem"));
            foreach (var hiddenvalue in hiddenvalues)
            {
                testItem.Add(new HiddenContent(new ElementName(hiddenvalue.Key), hiddenvalue.Value));
            }

            var items =
                new List<Content>
                {
                    new Content(new ElementName("Unit1Id"), ""),
                    new Content(new ElementName("Unit1"), ""),
                    new Content(new ElementName("Nom1"), ""),
                    new Content(new ElementName("Min1"), ""),
                    new Content(new ElementName("Max1"), ""),
                    new Content(new ElementName("Unit2Id"), ""),
                    new Content(new ElementName("Unit2"), ""),
                    new Content(new ElementName("Nom2"), ""),
                    new Content(new ElementName("Min2"), ""),
                    new Content(new ElementName("Max2"), ""),
                };
            foreach (var item in items)
            {
                testItem.Add(item);
            }

            return testItem;
        }

        private static Dictionary<string, string> MakeTorqueControlledHiddenValues()
        {
            return new Dictionary<string, string>
            {
                { "ControlDimension", "1" },
                { "DimensionTorqueNominal", "torquenom" },
                { "DimensionTorqueMin", "torquemin" },
                { "DimensionTorqueMax", "torquemax" },
                { "DimensionAngleNominal", "anglenom" },
                { "DimensionAngleMin", "anglemin" },
                { "DimensionAngleMax", "anglemax" },
            };
        }

        private static Dictionary<string, string> MakeAngleControlledHiddenValues()
        {
            return new Dictionary<string, string>
            {
                { "ControlDimension", "0" },
                { "DimensionTorqueNominal", "torquenom" },
                { "DimensionTorqueMin", "torquemin" },
                { "DimensionTorqueMax", "torquemax" },
                { "DimensionAngleNominal", "anglenom" },
                { "DimensionAngleMin", "anglemin" },
                { "DimensionAngleMax", "anglemax" },
            };
        }
    }
}