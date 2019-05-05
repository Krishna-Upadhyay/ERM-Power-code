using ERMPower.CSVReader.Common.Interface;
using ERMPower.CSVReader.Common.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMPower.PrintClient
{
    public class PrintCSVOutput
    {
        private IService Service;
        public PrintCSVOutput(IService Service)
        {
            this.Service = Service;
        }
        public void PrintAtConsole()
        {
            List<ConsoleOutput> rowsToPrint = this.Service.ProcessCSVFile();
            StringBuilder printAtConsoleBuilder;
            foreach (var row in rowsToPrint)
            {
                printAtConsoleBuilder = new StringBuilder();
                printAtConsoleBuilder.Append(row.FileName);
                printAtConsoleBuilder.Append(" ");
                printAtConsoleBuilder.Append(row.DateTime);
                printAtConsoleBuilder.Append(" ");
                printAtConsoleBuilder.Append(row.DataValue);
                printAtConsoleBuilder.Append(" ");
                printAtConsoleBuilder.AppendLine(row.MedianValue.ToString());

                Console.WriteLine(printAtConsoleBuilder.ToString());
            }
        }
    }
}
