using ERMPower.CSVReader.Common.Responses;
using ERMPower.TOUFileReader.Entities;
using System;
using System.Collections.Generic;

namespace ERMPower.TOUFileReader.Services
{
    public class GenerateCSVOutputService
    {
        public List<TOUEntity> DataList;
        public decimal MedianPrice;
        public string FileName;
        public GenerateCSVOutputService(List<TOUEntity> DataList, decimal MedianPrice, string FileName)
        {
            this.DataList = DataList;
            this.MedianPrice = MedianPrice;
            this.FileName = FileName;
        }

        public List<ConsoleOutput> GenerateConsoleOutput()
        {
            List<ConsoleOutput> rowsToPrint = new List<ConsoleOutput>();

            decimal twentyPercentAboveMedianPrice = MedianPrice + (MedianPrice * Convert.ToDecimal(0.2));
            decimal twentyPercentBelowMedianPrice = MedianPrice - (MedianPrice * Convert.ToDecimal(0.2));

            foreach (var touEntity in DataList)
            {
                if (!(touEntity.Energy >= twentyPercentBelowMedianPrice && touEntity.Energy <= twentyPercentAboveMedianPrice))
                {
                    rowsToPrint.Add(new ConsoleOutput()
                    {
                        FileName = this.FileName,
                        DateTime = touEntity.DateTime,
                        DataValue = touEntity.Energy,
                        MedianValue = this.MedianPrice
                    });
                }
            }
            return rowsToPrint;
        }
    }
}
