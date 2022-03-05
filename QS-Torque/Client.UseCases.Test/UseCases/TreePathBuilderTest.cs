using System;
using System.Collections.Generic;
using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using TestHelper.Factories;

namespace Core.Test.UseCases
{
    class TreePathBuilderTest
    {
        [Test]
        public void GetTreePathWithNullThrowsArgumentException()
        {
            var treePathBuilder = new TreePathBuilder();
            Assert.Throws<ArgumentException>(() => treePathBuilder.GetTreePath(null, " - "));
        }

        private static IEnumerable<(List<LocationDirectory>, string, string)> GetTreePathDataSource = new List<(List<LocationDirectory>, string, string)>()
        {
            (new List<LocationDirectory>
            {
                CreateLocationDirectory.Parameterized(1,"Werk", 0),
                CreateLocationDirectory.Parameterized(2,"Halle", 1),
                CreateLocationDirectory.Parameterized(3,"Bereich", 2),
                CreateLocationDirectory.Parameterized(4,"Regal", 3),
                CreateLocationDirectory.Parameterized(5,"Fläche", 4),
            },
            "Werk - Halle - Bereich - Regal - Fläche", " - "),
            (new List<LocationDirectory>
                {
                    CreateLocationDirectory.Parameterized(1,"A", 0),
                    CreateLocationDirectory.Parameterized(2,"B", 1),
                    CreateLocationDirectory.Parameterized(3,"C", 2),
                },
             "A/B/C", "/"),
            (new List<LocationDirectory>(), "", "x")
        };

        [TestCaseSource(nameof(GetTreePathDataSource))]
        public void GetTreePathReturnsCorrectPath((List<LocationDirectory> directories, string result, string seperator) data)
        {
            var treePathBuilder = new TreePathBuilder();
            var location = CreateLocation.Anonymous();
            location.LocationDirectoryPath = data.directories;
            var path = treePathBuilder.GetTreePath(location, data.seperator);
            Assert.AreEqual(data.result, path);
        }

        private static IEnumerable<(List<LocationDirectory>, string)> GetMaskedTreePathWithBase64ReturnsCorrectValueData = new List<(List<LocationDirectory>, string)>()
        {
            (new List<LocationDirectory>()
                {
                    CreateLocationDirectory.Parameterized(1,"Werk", 0),
                    CreateLocationDirectory.Parameterized(2,"Halle", 1),
                    CreateLocationDirectory.Parameterized(3,"Bereich", 2),
                    CreateLocationDirectory.Parameterized(4,"Regal", 3),
                    CreateLocationDirectory.Parameterized(5,"Fläche", 4),
                },
                "V2Vyaw==|SGFsbGU=|QmVyZWljaA==|UmVnYWw=|RmzDpGNoZQ=="),
            (new List<LocationDirectory>()
                {
                    CreateLocationDirectory.Parameterized(1,"A", 0),
                    CreateLocationDirectory.Parameterized(2,"B", 1),
                    CreateLocationDirectory.Parameterized(3,"C", 2),
                },
                "QQ==|Qg==|Qw=="),
            (new List<LocationDirectory>()
                {
                    CreateLocationDirectory.Parameterized(1,"A|", 0),
                    CreateLocationDirectory.Parameterized(2,"|B", 1),
                    CreateLocationDirectory.Parameterized(3,"|C|", 2),
                },
                "QXw=|fEI=|fEN8"),
            (new List<LocationDirectory>(), "")
        };

        [TestCaseSource(nameof(GetMaskedTreePathWithBase64ReturnsCorrectValueData))]
        public void GetMaskedTreePathWithBase64ReturnsCorrectValue((List<LocationDirectory> directories, string result) data)
        {
            var treePathBuilder = new TreePathBuilder();
            var location = CreateLocation.Anonymous();
            location.LocationDirectoryPath = data.directories;
            var path = treePathBuilder.GetMaskedTreePathWithBase64(location);
            Assert.AreEqual(data.result, path);
        }

        [TestCase("", "/", "")]
        [TestCase("V2Vyaw==", "/", "Werk")]
        [TestCase("V2Vyaw==|SGFsbGU=", " - ", "Werk - Halle")]
        [TestCase("fFJvb3R8|fEZvbGRlcnw=", " / ", "|Root| / |Folder|")]
        public void GetDeMaskedTreePathFromBase64(string maskedPath, string seperator, string result)
        {
            var treePathBuilder = new TreePathBuilder();
            var demaskedPath = treePathBuilder.GetDeMaskedTreePathFromBase64(maskedPath, seperator);
            Assert.AreEqual(result, demaskedPath);
        }


        private static IEnumerable<(List<LocationDirectory>, string, string)> MaskAndDeMaskTreePathWithBase64ReturnsCorrectPathData = new List<(List<LocationDirectory>, string, string)>()
        {
            (new List<LocationDirectory>
                {
                    CreateLocationDirectory.Parameterized(1,"Werk", 0),
                    CreateLocationDirectory.Parameterized(2,"Halle", 1),
                    CreateLocationDirectory.Parameterized(3,"Bereich", 2),
                    CreateLocationDirectory.Parameterized(4,"Regal", 3),
                    CreateLocationDirectory.Parameterized(5,"Fläche", 4),
                },
                "-",
                "Werk-Halle-Bereich-Regal-Fläche"),
            (new List<LocationDirectory>
                {
                    CreateLocationDirectory.Parameterized(1,"sfdghfsg fseriu rehtu ä", 0),
                    CreateLocationDirectory.Parameterized(2," |23 %6 &&8435 | < >> 8345z8", 1),
                    CreateLocationDirectory.Parameterized(3,"4375 34857 38 456 sf'435\\ 43 5fduig|", 2),
                },
                "-",
                "sfdghfsg fseriu rehtu ä- |23 %6 &&8435 | < >> 8345z8-4375 34857 38 456 sf'435\\ 43 5fduig|"),
            (new List<LocationDirectory>
                {
                    CreateLocationDirectory.Parameterized(1,"74358@ 234A#", 0),
                    CreateLocationDirectory.Parameterized(2,"°^^°|<<`´JH 435", 1),
                    CreateLocationDirectory.Parameterized(3,"78''#|\\|4?ß?)$§&/(%", 2),
                },
                " - ",
                "74358@ 234A# - °^^°|<<`´JH 435 - 78''#|\\|4?ß?)$§&/(%"),
            (new List<LocationDirectory>(), "-", "")
        };

        [TestCaseSource(nameof(MaskAndDeMaskTreePathWithBase64ReturnsCorrectPathData))]
        public void MaskAndDeMaskTreePathWithBase64ReturnsCorrectPath((List<LocationDirectory> directories, string seperator, string result) data)
        {
            var treePathBuilder = new TreePathBuilder();
            var location = CreateLocation.Anonymous();
            location.LocationDirectoryPath = data.directories;
            var maskedPath = treePathBuilder.GetMaskedTreePathWithBase64(location);
            var path = treePathBuilder.GetDeMaskedTreePathFromBase64(maskedPath, data.seperator);
            Assert.AreEqual(data.result, path);
        }
    }
}
