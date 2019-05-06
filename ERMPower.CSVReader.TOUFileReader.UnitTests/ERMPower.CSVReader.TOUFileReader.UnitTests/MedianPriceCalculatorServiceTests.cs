using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERMPower.TOUFileReader.Services;
using ERMPower.TOUFileReader.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ERMPower.CSVReader.TOUFileReader.UnitTests
{
    [TestClass]
    public class MedianPriceCalculatorServiceTests
    {
        public List<TOUEntity> dataList;
        public MedianPriceCalculatorServiceTests()
        {
            setupData();
        }

        [TestMethod]
        public void GivenMedianPriceCalculatorService_WhenTotalRowCountIsEvenNumber_ThenShouldCalculateCorrectMedianPrice()
        {
            //arrange
            
            //act 
            MedianPriceCalculatorService medianPriceCalculatorService = new MedianPriceCalculatorService(dataList);
            var medianPrice = medianPriceCalculatorService.CalculateMedianPrice();

            //assert
            Assert.AreEqual(CalculateMedianPrice(dataList), medianPrice);
        }

        [TestMethod]
        public void GivenMedianPriceCalculatorService_WhenTotalRowCountIsOddNumber_ThenShouldCalculateCorrectMedianPrice()
        {
            //arrange
            dataList.RemoveAt(0);

            //act 
            MedianPriceCalculatorService medianPriceCalculatorService = new MedianPriceCalculatorService(dataList);
            var medianPrice = medianPriceCalculatorService.CalculateMedianPrice();

            //assert
            Assert.AreEqual(CalculateMedianPrice(dataList), medianPrice);
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
                PlantCode ="1010",
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
        }

        private decimal CalculateMedianPrice(List<TOUEntity> DataList)
        {
            int numberCount = DataList.Count();
            int halfIndex = DataList.Count() / 2;
            var sortedNumbers = DataList.OrderBy(n => n.Energy).Select(e => e.Energy);
            decimal median;
            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex - 1)) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            return median;
        }
    }
}
