using ERMPower.CSVReader.Common.Interface;
using ERMPower.CSVReader.Common.Responses;
using ERMPower.TOUFileReader.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ERMPower.TOUFileReader.Services
{
    public class TOUFileReaderService : IService
    {
        private string FileName;
        public TOUFileReaderService(string FileName)
        {
            this.FileName = FileName;
        }
        public List<ConsoleOutput> ProcessCSVFile()
        {
            var dataList = ReadFileIntoEntity();

            var medianPrice = CalculateMedianPrice(dataList);

            return GenerateConsoleOutput(dataList, medianPrice);           
        }
        private List<TOUEntity> ReadFileIntoEntity()
        {
            var reader = new StreamReader(File.OpenRead(this.FileName));
            List<TOUEntity> dataList = new List<TOUEntity>();

            //skip the first header line
            reader.ReadLine();

            //read until end of the file
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<string> lineValues = line.Split(',').ToList();
                dataList.Add(new TOUEntity()
                {
                    MeterPointCode = lineValues[0],
                    SerialNumber = lineValues[1],
                    PlantCode = lineValues[2],
                    DateTime = DateTime.Parse(lineValues[3]),
                    DataType = lineValues[4],
                    Energy = Convert.ToDecimal(lineValues[5]),
                    MaximumDemand = Convert.ToDecimal(lineValues[6]),
                    TimeOfMaxDemand = DateTime.Parse(lineValues[7]),
                    Units = lineValues[8],
                    Status = lineValues[9],
                    Period = lineValues[10],
                    DLSActive = Convert.ToBoolean(lineValues[11]),
                    BillingResetCount = Convert.ToInt32(lineValues[12]),
                    BillingResetDateTime = DateTime.Parse(lineValues[13]),
                    Rate = lineValues[14]
                });
            }
            return dataList;
        }
        private decimal CalculateMedianPrice(List<TOUEntity> entryList)
        {
            int numberCount = entryList.Count();
            int halfIndex = entryList.Count() / 2;
            var sortedNumbers = entryList.OrderBy(n => n.Energy).Select(e => e.Energy);
            decimal median;
            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) +
                    sortedNumbers.ElementAt(halfIndex - 1)) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            return median;
        }
        private List<ConsoleOutput> GenerateConsoleOutput(List<TOUEntity> entryList, decimal medianPrice)
        {
            List<ConsoleOutput> rowsToPrint = new List<ConsoleOutput>();           
            foreach (var touEntity in entryList)
            {
                decimal twentyPercentMedianPrice = medianPrice * Convert.ToDecimal(0.2);
                if ((touEntity.Energy > twentyPercentMedianPrice || touEntity.Energy < twentyPercentMedianPrice) &&
                    touEntity.Energy != twentyPercentMedianPrice)
                {
                    rowsToPrint.Add(new ConsoleOutput()
                    {
                        FileName = Path.GetFileName(this.FileName),
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
