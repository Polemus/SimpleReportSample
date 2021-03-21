using System;

namespace SimpleReportSample.HelperClassesAndInterfaces
{
    public class TimeReportRow
    {
        public string ProjectName { get; set; }

        public string Task { get; set; }

        public string Description { get; set; }

        public decimal? RegularLabor { get; set; }

        public decimal? OvertimeLabor { get; set; }

        public decimal? OffHours { get; set; }

        public DateTime? Date { get; set; }
    }
}
