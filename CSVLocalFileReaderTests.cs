using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLParserV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1.Tests
{
    [TestClass()]
    public class CSVLocalFileReaderTests
    {
        [TestMethod()]
        public void GetAllDataTestNullCheck()
        {
            // Arrange
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.filePath = "B:\\repos\\XMLParserV1\\mps.csv";
            reader.Counter = 0;
            // Act
            List<string> LocalOutput = reader.GetAllData();
            // Assert
            Assert.IsNotNull(LocalOutput);
        }

        [TestMethod()]
        public void GetAllDataTestDataTypeCheck()
        {
            // Arrange
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.filePath = "B:\\repos\\XMLParserV1\\mps.csv";
            reader.Counter = 0;
            // Act
            List<string> LocalOutput = reader.GetAllData();
            // Assert
            Assert.IsInstanceOfType(LocalOutput, typeof(List<string>));
        }

        [TestMethod()]
        public void GetAllDataTestDataCountCheck()
        {
            // Arrange
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.filePath = "B:\\repos\\XMLParserV1\\mps.csv";
            reader.Counter = 0;
            // Act
            List<string> LocalOutput = reader.GetAllData();
            // Assert
            Assert.IsTrue(LocalOutput.Count > 647);   
        }
    }
}