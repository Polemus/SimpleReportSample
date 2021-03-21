using SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces;
using System;

namespace SimpleReportSample.HelperClassesAndInterfaces
{
    public class InvoiceReportParams
    {
        public ContractorsAndContracts ContractorsData { get; set; }

        public PaymentData PaymentData { get; set; }

        public string PathToTaskSummaryDocs { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
