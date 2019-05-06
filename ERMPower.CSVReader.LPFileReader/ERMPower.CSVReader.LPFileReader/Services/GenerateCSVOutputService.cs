using ERMPower.CSVReader.Common.Responses;
using ERMPower.LPFileReader.Entities;
using System;
using System.Collections.Generic;

namespace ERMPower.LPFileReader.Services
{
    public class GenerateCSVOutputService
    {
        public List<LPEntity> DataList;
        public decimal MedianPrice;
        public string FileName;
        public GenerateCSVOutputService(List<LPEntity> DataList, decimal MedianPrice, string FileName)
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
                if (!(touEntity.DataValue >= twentyPercentBelowMedianPrice && touEntity.DataValue <= twentyPercentAboveMedianPrice))
                {
                    rowsToPrint.Add(new ConsoleOutput()
                    {
                        FileName = this.FileName,
                        DateTime = touEntity.DateTime,
                        DataValue = touEntity.DataValue,
                        MedianValue = this.MedianPrice
                    });
                }
            }
            return rowsToPrint;
        }
    }
}
