using ERMPower.CSVReader.Common.Responses;
using System.Collections.Generic;

namespace ERMPower.CSVReader.Common.Interface
{
    public interface IService
    {        
        List<ConsoleOutput> ProcessCSVFile();
    }
}
