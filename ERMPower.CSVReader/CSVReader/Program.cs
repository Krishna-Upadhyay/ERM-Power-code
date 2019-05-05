using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using ERMPower.LPFileReader.Services;
using ERMPower.TOUFileReader.Services;
using ERMPower.PrintClient;

namespace CSVReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = ConfigurationManager.AppSettings["CSVFileLocation"]; 

            foreach (string file in Directory.EnumerateFiles(folderPath, "*.csv"))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.StartsWith("TOU", true, CultureInfo.CurrentCulture))
                {
                    TOUFileReaderService tOUFileReaderService = new TOUFileReaderService(file);
                    PrintCSVOutput printCSVOutput = new PrintCSVOutput(tOUFileReaderService);
                    printCSVOutput.PrintAtConsole();
                }
                else if(fileName.StartsWith("LP", true, CultureInfo.CurrentCulture))
                {
                    LPFileReaderService lPFileReaderService = new LPFileReaderService(file);
                    PrintCSVOutput printCSVOutput = new PrintCSVOutput(lPFileReaderService);
                    printCSVOutput.PrintAtConsole();
                }
            }
            Console.ReadKey();
        }
       
    }   

}
