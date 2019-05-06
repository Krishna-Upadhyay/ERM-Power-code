using ERMPower.LPFileReader.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ERMPower.LPFileReader.Services
{
    public class MedianPriceCalculatorService
    {
        public List<LPEntity> DataList;

        public MedianPriceCalculatorService(List<LPEntity> DataList)
        {
            this.DataList = DataList;
        }
        public decimal CalculateMedianPrice()
        {
            int numberCount = DataList.Count();
            int halfIndex = DataList.Count() / 2;
            var sortedNumbers = DataList.OrderBy(n => n.DataValue).Select(e => e.DataValue);
            decimal median;
            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex - 1)) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            return median;
        }
    }
}
