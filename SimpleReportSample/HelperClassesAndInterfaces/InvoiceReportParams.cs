using SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces;
using System;
using static SimpleReportSample.DataProvider;

namespace SimpleReportSample.HelperClassesAndInterfaces
{
    public class InvoiceReportParams
    {
        public ContractorsAndContracts ContractorsData { get; set; }

        public PaymentData PaymentData { get; set; }

        public TimeReport TimeReportData { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
