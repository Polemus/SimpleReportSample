namespace SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces
{
    public class PaymentData : BaseData
    {
        public int DocSetNumber { get; set; }

        public string EmployerName { get; set; }

        //public decimal ToPay { get; set; }

        public decimal RoundedCurrency { get; set; }

        public decimal HoursInvoiced { get; set; }

        public decimal InvoiceAmount { get; set; }

        public decimal HourlyRate { get; set; }

        public string ProjectName { get; set; }
    }
}
