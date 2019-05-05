using System;

namespace ERMPower.CSVReader.Common.Responses
{
    public class ConsoleOutput
    {
        public string FileName { get; set; }
        public DateTime DateTime { get; set; }
        public Decimal DataValue { get; set; }
        public Decimal MedianValue { get; set; }
    }
}
