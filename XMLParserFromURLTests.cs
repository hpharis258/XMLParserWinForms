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
    public class XMLParserFromURLTests
    {
        [TestMethod()]
        public void GetAllDataTestDataType()
        {
            // Arrange
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            XMLParser.GetRecordCount = 0;
            // Act
            List<MemberOfParliament> List = XMLParser.GetAllData();
            // Assert
            Assert.IsInstanceOfType(List, typeof(List<MemberOfParliament>));
        }

        [TestMethod()]
        public void GetAllDataTestIsNotNull()
        {
            // Arrange
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            XMLParser.GetRecordCount = 0;
            // Act
            List<MemberOfParliament> List = XMLParser.GetAllData();
            // Assert
            Assert.IsNotNull(List);
        }

        [TestMethod()]
        public void GetAllDataTestDidReturnAllMembers()
        {
            // Arrange
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            XMLParser.GetRecordCount = 0;
            // Act
            List<MemberOfParliament> List = XMLParser.GetAllData();
            // Assert
            Assert.IsTrue(List.Count == 647);
        }
    }
}