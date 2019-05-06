using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ERMPower.TOUFileReader.Services;
using System.Configuration;
using System.IO;
using Moq;
using System.Collections.Generic;
using ERMPower.CSVReader.Common.Responses;

namespace ERMPower.CSVReader.TOUFileReader.UnitTests
{
    [TestClass]
    public class TOUFileReaderServiceTests
    {
        public TOUFileReaderServiceTests()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenTOUFileReaderService_WhenFileNameIsInvalid_ThenItShouldThrowException()
        {
            //arrange
            string fileName = "C:\\Test.Csv";

            //act
            TOUFileReaderService tOUFileReaderService = new TOUFileReaderService(fileName);
            tOUFileReaderService.ProcessCSVFile(); 
               
            //assert
        }

        [TestMethod]
        public void GivenTOUFileReaderService_WhenFileNameIsValid_ThenItShouldProcessTheFile()
        {
            //arrange
            string folderPath = "C:\\ERM-Power-code\\Sample files (1) (2) (1) (1)";
            var files = Directory.GetFiles(folderPath, "*.csv");

            var mockMedianPriceCalculatorService = new Mock<MedianPriceCalculatorService>();
            mockMedianPriceCalculatorService.Setup(x => x.CalculateMedianPrice()).Returns(100); 

            var mockGenerateConsoleOutputService = new Mock<GenerateCSVOutputService>();
            mockGenerateConsoleOutputService.Setup(x => x.GenerateConsoleOutput()).Returns(new List<ConsoleOutput>());

            //act
            TOUFileReaderService tOUFileReaderService = new TOUFileReaderService(files[1]);
            var output = tOUFileReaderService.ProcessCSVFile();

            //assert
            Assert.IsNotNull(output);
        }
    }
}
