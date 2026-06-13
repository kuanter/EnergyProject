using EnergyProject.Common.Enums;
using EnergyProject.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.ViewModels
{
    public class MeterReadingFilterViewModel
    {
        public DateTimeFilter dateTimeFilter { get; set; }
        public List<SelectListItem> DateTimeFilterOptions { get; set; }

        public string meterId { get; set; }
        public List<MeterReading> meterReadings { get; set; }

        public MeterReadingFilterViewModel()
        {
            DateTimeFilterOptions = new List<SelectListItem>();
            meterReadings = new List<MeterReading>();
            foreach (var elem in Enum.GetValues<DateTimeFilter>())
            {
                SelectListItem item = new SelectListItem(elem.ToString(), elem.ToString());
                DateTimeFilterOptions.Add(item);
            }
        }
    }
}