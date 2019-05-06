using System;
//using ERMPower.CSVReader.Common;

namespace ERMPower.LPFileReader.Entities
{
    public class LPEntity
    {
        public string MeterPoint { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime DateTime { get; set; }
        public Decimal DataValue { get; set; }
        public string DataType { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }
}
