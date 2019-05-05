using ERMPower.CSVReader.Common.Interface;
using ERMPower.CSVReader.Common.Responses;
using ERMPower.LPFileReader.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ERMPower.LPFileReader.Services
{
    public class LPFileReaderService : IService
    {
        private string FileName;
        public LPFileReaderService(string fileName)
        {
            this.FileName = fileName;
        }
        public List<ConsoleOutput> ProcessCSVFile()
        {
            var dataList = ReadFileIntoEntity();

            var medianPrice = CalculateMedianPrice(dataList);

            return GenerateConsoleOutput(dataList, medianPrice);
        }

        private List<LPEntity> ReadFileIntoEntity()
        {
            var reader = new StreamReader(File.OpenRead(this.FileName));
            List<LPEntity> dataList = new List<LPEntity>();

            //skip the first header line
            reader.ReadLine();

            //read until end of the file
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<string> lineValues = line.Split(',').ToList();
                dataList.Add(new LPEntity()
                {
                    MeterPoint = lineValues[0],
                    SerialNumber = lineValues[1],
                    PlantCode = lineValues[2],
                    DateTime = DateTime.Parse(lineValues[3]),
                    DataType = lineValues[4],
                    DataValue = Convert.ToDecimal(lineValues[5]),
                    Units = lineValues[6],
                    Status = lineValues[7],
                });
            }
            return dataList;
        }

        private decimal CalculateMedianPrice(List<LPEntity> entryList)
        {
            int numberCount = entryList.Count();
            int halfIndex = entryList.Count() / 2;
            var sortedNumbers = entryList.OrderBy(n => n.DataValue).Select(e => e.DataValue);
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

        private List<ConsoleOutput> GenerateConsoleOutput(List<LPEntity> entryList, decimal medianPrice)
        {
            List<ConsoleOutput> rowsToPrint = new List<ConsoleOutput>();
            foreach (var touEntity in entryList)
            {
                decimal twentyPercentMedianPrice = medianPrice * Convert.ToDecimal(0.2);
                if ((touEntity.DataValue > twentyPercentMedianPrice || touEntity.DataValue < twentyPercentMedianPrice) &&
                    touEntity.DataValue != twentyPercentMedianPrice)
                {
                    rowsToPrint.Add(new ConsoleOutput()
                    {
                        FileName = Path.GetFileName(this.FileName),
                        DateTime = touEntity.DateTime,
                        DataValue = touEntity.DataValue,
                        MedianValue = medianPrice
                    });
                }
            }
            return rowsToPrint;
        }      
    }
}
