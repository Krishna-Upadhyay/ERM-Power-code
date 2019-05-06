using ERMPower.TOUFileReader.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ERMPower.TOUFileReader.Services;
using ERMPower.CSVReader.Common.Responses;
using System.Linq;

namespace ERMPower.CSVReader.TOUFileReader.UnitTests
{
    [TestClass]
    public class GenerateCSVOutputServiceTests
    {
        public List<TOUEntity> dataList;
        public GenerateCSVOutputServiceTests()
        {
            setupData();
        }

        [TestMethod]
        public void GivenGenerateCSVOutputService_WhenEnergyValueIsMoreThan20PercentOfMedianPrice_ThenAddRowForPrintingAtConsoleOutput()
        {
            //arrange
            string fileName = "TOUFile.csv";
            decimal medianPrice = 2000;

            //act
            var generateCSVOutputService = new GenerateCSVOutputService(dataList, medianPrice, fileName);
            var output = generateCSVOutputService.GenerateConsoleOutput();

            //assert
            Assert.AreEqual(GenerateConsoleOutput(dataList, medianPrice, fileName), output);
        }

        private void setupData()
        {
            dataList = new List<TOUEntity>();
            dataList.Add(new TOUEntity()
            {
                DateTime = new DateTime(),
                Energy = 1000,
                SerialNumber = "10000001",
                BillingResetCount = 1,
                BillingResetDateTime = new DateTime(),
                DataType = "DT",
                DLSActive = true,
                MaximumDemand = 1200,
                MeterPointCode = "12121212",
                Period = "1010",
                PlantCode = "1010",
                Rate = "kwz",
                Status = "DT",
                TimeOfMaxDemand = new DateTime(),
                Units = "KWz"
            });
            dataList.Add(new TOUEntity()
            {
                DateTime = new DateTime(),
                Energy = 5000,
                SerialNumber = "10000002",
                BillingResetCount = 1,
                BillingResetDateTime = new DateTime(),
                DataType = "DT",
                DLSActive = true,
                MaximumDemand = 1200,
                MeterPointCode = "12121212",
                Period = "1010",
                PlantCode = "1010",
                Rate = "kwz",
                Status = "DT",
                TimeOfMaxDemand = new DateTime(),
                Units = "KWz"
            });
            dataList.Add(new TOUEntity()
            {
                DateTime = new DateTime(),
                Energy = 700,
                SerialNumber = "10000003",
                BillingResetCount = 1,
                BillingResetDateTime = new DateTime(),
                DataType = "DT",
                DLSActive = true,
                MaximumDemand = 1200,
                MeterPointCode = "12121212",
                Period = "1010",
                PlantCode = "1010",
                Rate = "kwz",
                Status = "DT",
                TimeOfMaxDemand = new DateTime(),
                Units = "KWz"
            });
            dataList.Add(new TOUEntity()
            {
                DateTime = new DateTime(),
                Energy = 3000,
                SerialNumber = "10000003",
                BillingResetCount = 1,
                BillingResetDateTime = new DateTime(),
                DataType = "DT",
                DLSActive = true,
                MaximumDemand = 1200,
                MeterPointCode = "12121212",
                Period = "1010",
                PlantCode = "1010",
                Rate = "kwz",
                Status = "DT",
                TimeOfMaxDemand = new DateTime(),
                Units = "KWz"
            });
            dataList.Add(new TOUEntity()
            {
                DateTime = new DateTime(),
                Energy = 2100,
                SerialNumber = "10000003",
                BillingResetCount = 1,
                BillingResetDateTime = new DateTime(),
                DataType = "DT",
                DLSActive = true,
                MaximumDemand = 1200,
                MeterPointCode = "12121212",
                Period = "1010",
                PlantCode = "1010",
                Rate = "kwz",
                Status = "DT",
                TimeOfMaxDemand = new DateTime(),
                Units = "KWz"
            });
        }

        public List<ConsoleOutput> GenerateConsoleOutput(List<TOUEntity> dataList, decimal medianPrice, string fileName)
        {
            List<ConsoleOutput> rowsToPrint = new List<ConsoleOutput>();

            decimal twentyPercentAboveMedianPrice = medianPrice + (medianPrice * Convert.ToDecimal(0.2));
            decimal twentyPercentBelowMedianPrice = medianPrice - (medianPrice * Convert.ToDecimal(0.2));

            foreach (var touEntity in dataList)
            {                
                if (!(touEntity.Energy >= twentyPercentBelowMedianPrice && touEntity.Energy <= twentyPercentAboveMedianPrice ))
                {
                    rowsToPrint.Add(new ConsoleOutput()
                    {
                        FileName = fileName,
                        DateTime = touEntity.DateTime,
                        DataValue = touEntity.Energy,
                        MedianValue = medianPrice
                    });
                }
            }
            return rowsToPrint;
        }
    }
}
