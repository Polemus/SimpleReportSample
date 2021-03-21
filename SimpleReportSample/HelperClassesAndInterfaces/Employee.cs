using SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces;

namespace SimpleReportSample.HelperClassesAndInterfaces
{
    public class Employee : BaseData
    {
        public string EmployerName { get; set; }

        public string NameEng { get; set; }

        public string NameRus { get; set; }

        public string Phone { get; set; }

        public string ContractId { get; set; }

        public string ContractDate { get; set; }

        public string ContractDateTranslated { get; set; }

        public string DocumentSetDate { get; set; }

        public string DocumentSetDateTranslated { get; set; }

        public int DocSetNumber { get; set; }

        public string DatesCovered { get; set; }

        public string ProjectName { get; set; }

        public decimal HoursInvoiced { get; set; }

        public decimal TotalAmount { get; set; }

        public string AmountInwords { get; set; }

        public string AmountInWordsTranslated { get; set; }
    }
}
