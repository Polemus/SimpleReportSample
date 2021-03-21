using SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces;
using System;

namespace SimpleReportSample.HelperClassesAndInterfaces
{
    public class ContractorsAndContracts : BaseData
    {
        public string EmployerName { get; set; }

        public string ContractId { get; set; }

        public DateTime ContractStartDate { get; set; }

        public string NameEng { get; set; }

        public string NameRus { get; set; }

        public string Phone { get; set; }

        public string ContractStartDateRus { get; set; }

        public string INN { get; set; }

        public string MailingAddress { get; set; }

        public string BankName { get; set; }

        public string BanksBIC { get; set; }

        public string CorrespondentAccount { get; set; }

        public string AccountNumber { get; set; }
    }
}
