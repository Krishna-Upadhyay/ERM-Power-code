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

            var medianPriceCalculatorService = new MedianPriceCalculatorService(dataList);
            var medianPrice = medianPriceCalculatorService.CalculateMedianPrice();

            var generateCSVOutputService = new GenerateCSVOutputService(dataList, medianPrice, Path.GetFileName(FileName));
            var consoleOutput = generateCSVOutputService.GenerateConsoleOutput();

            return consoleOutput;
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
     
    }
}
