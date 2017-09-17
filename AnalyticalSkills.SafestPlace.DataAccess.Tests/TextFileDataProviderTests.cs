using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnalyticalSkills.SafestPlace.DataAccess.Tests
{
    [TestFixture]
    public class TextFileDataProviderTests
    {
        [SetUp]
        public void Setup()
        {
            mockFileAccess = MockRepository.GenerateMock<IFileReadonlyAccess>();
        }

        [TearDown]
        public void TearDown()
        {
            mockFileAccess = null;
        }

        private IFileReadonlyAccess mockFileAccess;

        [TestCase("")]
        [TestCase(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTestData_should_throw_ArgumentNullException_when_null_or_empty_source_file_path_is_supplied(
            string sourceFileName)
        {
            //
            // Arrange.
            //

            IDataProvider provider = new TextFileDataProvider(mockFileAccess);

            //
            // Act.
            //

            provider.GetTestData(sourceFileName);
        }

        [TestCase("MySourceFile.xml")]
        [TestCase("MySourceFile.json")]
        [TestCase("MySourceFile.pdf")]
        [TestCase("MySourceFile.cs")]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetTestData_should_throw_NotSupportedException_when_non_txt_extension_is_supplied(
            string sourceFileName)
        {
            //
            // Arrange.
            //

            IDataProvider provider = new TextFileDataProvider(mockFileAccess);

            //
            // Act.
            //

            provider.GetTestData(sourceFileName);
        }

        private IEnumerable<string> TestData()
        {
            return new List<string>
            {
                "1, 3, 0,0,0,2,2,2,9,9,9",
                "2, 4, 5,6,3,8,9,6,4,1,2,8,5,2",
                "3, 5, 6,5,8,30,20,10,55,88,55,65,85,41,901,508,100"
            };
        }


        [Test]
        public void GetTestData_should_return_testData_object()
        {
            //
            // Arrange.
            //
            const string FileName = "MyTestData.txt";

            mockFileAccess.Stub(fa => fa.ReadAllLines(FileName)).Return(TestData());

            IDataProvider provider = new TextFileDataProvider(mockFileAccess);

            //
            // Act.
            //


            var testdataList = provider.GetTestData(FileName);


            //
            // Arrange.
            //

            Assert.That(testdataList.Count(), Is.EqualTo(3));

            for (var i = 0; i < testdataList.Count(); i++)
            {
                Assert.That(testdataList.ToArray()[i].TestNumber, Is.EqualTo(i + 1));
                Assert.That(testdataList.ToArray()[i].BombLocations.Count, Is.EqualTo(i + 3));
                foreach (var nextBomb in testdataList.ToArray()[i].BombLocations)
                    Assert.That(nextBomb.X, Is.GreaterThan(-1));
            }
        }
    }
}