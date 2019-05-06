using System;


namespace ERMPower.TOUFileReader.Entities
{
    public class TOUEntity
    {
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime DateTime { get; set; }
        public string DataType { get; set; }
        public decimal Energy { get; set; }
        public decimal MaximumDemand { get; set; }
        public DateTime TimeOfMaxDemand { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
        public string Period { get; set; }
        public Boolean DLSActive { get; set; }
        public int BillingResetCount { get; set; }
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }
    }
}
