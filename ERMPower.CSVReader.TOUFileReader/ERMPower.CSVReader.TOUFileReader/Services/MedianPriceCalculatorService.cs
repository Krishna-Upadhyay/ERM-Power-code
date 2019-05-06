
using ERMPower.TOUFileReader.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ERMPower.TOUFileReader.Services
{
    public class MedianPriceCalculatorService
    {
        public List<TOUEntity> DataList;

        public MedianPriceCalculatorService(List<TOUEntity> DataList)
        {
            this.DataList = DataList;
        }
        public decimal CalculateMedianPrice()
        {
            int numberCount = DataList.Count();
            int halfIndex = DataList.Count() / 2;
            var sortedNumbers = DataList.OrderBy(n => n.Energy).Select(e => e.Energy);
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
