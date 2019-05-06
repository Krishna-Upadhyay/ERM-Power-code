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
            if (!File.Exists(FileName))
                throw new InvalidOperationException("Invalid File");

            var dataList = ReadFileIntoEntity();

            var medianPriceCalculatorService = new MedianPriceCalculatorService(dataList);
            var medianPrice = medianPriceCalculatorService.CalculateMedianPrice();

            var generateCSVOutputService = new GenerateCSVOutputService(dataList, medianPrice, Path.GetFileName(FileName));
            var consoleOutput = generateCSVOutputService.GenerateConsoleOutput();

            return consoleOutput;          
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
       
    }
}
