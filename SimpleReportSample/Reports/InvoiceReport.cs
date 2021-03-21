using SimpleReportSample.Extensions;
using SimpleReportSample.HelperClassesAndInterfaces;
using SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static SimpleReportSample.DataProvider;

namespace SimpleReportSample.Reports
{
    public class InvoiceReport : ReportBase
    {

        public override string ReportName => "InvoiceReport report";

        private readonly DataProvider _dataProvider = new DataProvider();

        public ContractorsAndContracts _contractorsAndContractsData { get; set; }

        public PaymentData _paymentData { get; set; }

        public TimeReport _timeReport { get; set; }

        public decimal _hourlyRate { get; set; }

        private string[] _invoicerName { get; set; }

        private DateTime _dateTo { get; set; }

        private DateTime _dateFrom { get; set; }

        public InvoiceReport(InvoiceReportParams invoiceReportParams)
        {
            _contractorsAndContractsData = invoiceReportParams.ContractorsData;
            _paymentData = invoiceReportParams.PaymentData;
            _timeReport = invoiceReportParams.TimeReportData;
            _dateTo = invoiceReportParams.DateTo;
            _dateFrom = invoiceReportParams.DateFrom;

            _timeReport.TimeReportRows = _timeReport.TimeReportRows.Where(x => x.Date.Value <= invoiceReportParams.DateTo && x.Date.Value >= invoiceReportParams.DateFrom).ToList();

            //if (_timeReport.TimeReportRows == null || !_timeReport.TimeReportRows.Any())
            //{
            //    throw new Exception("Не найдены строки из указанного промежутка времени");
            //}
        }

        public List<Specification> GetSpecificationData()
        {
            int rowCounter = 1;
            List<Specification> specification = new List<Specification>();
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            var recalculatedTimeReportRows = new List<TimeReportRow>();

            foreach (var row in _timeReport.TimeReportRows.OrderByDescending(x => x.RegularLabor.Value))
            {
                if (recalculatedTimeReportRows.Sum(x => x.RegularLabor.Value) >= _paymentData.HoursInvoiced)
                    break;

                    recalculatedTimeReportRows.Add(new TimeReportRow()
                    {
                        Date = row.Date,
                        Task = row.Task,
                        RegularLabor = row.RegularLabor,
                        Description = row.Description,
                        OffHours = row.OffHours,
                        OvertimeLabor = row.OvertimeLabor,
                        ProjectName = row.ProjectName
                    });
            }

            if (recalculatedTimeReportRows.Sum(x => x.RegularLabor.Value) != _paymentData.HoursInvoiced)
            {
                decimal hoursDiff = recalculatedTimeReportRows.Sum(x => x.RegularLabor.Value) - _paymentData.HoursInvoiced;

                var firstRow = recalculatedTimeReportRows.First();

                firstRow.RegularLabor = firstRow.RegularLabor - hoursDiff;
            }

            var groupedTasksData = recalculatedTimeReportRows.OrderBy(x => x.Date).GroupBy(x => x.Description);

            foreach (var task in groupedTasksData.ToList())
            {
                specification.Add(new Specification()
                { 
                    Hours = (task.Sum(x => x.RegularLabor) ?? 0),
                    Number = rowCounter,
                    DeliverDate = string.Format("'{0}", _dateTo.ToString("dd MMM yyyy")),
                    TaskName = task.Key,
                    AmountUsd = (task.Sum(x => x.RegularLabor.Value) * this._hourlyRate).ToString("N", CultureInfo.InvariantCulture),
                    TranslatedTaskName = TranslateTextUsingGoogle.GetTranslateWebRequest(task.Key, "ru")
                });

                rowCounter++;
            }

            return specification;
        }

        public Employee GetData()
        {
            string[] name = _timeReport.Name.Split(' ').ToArray();

            this._invoicerName = name;

            /// находим из Time репорта чисто по имени?

            var contractorsAndContractsDataRow = _contractorsAndContractsData;

            if (contractorsAndContractsDataRow == null)
                throw new Exception("Не найдена строка в контрактах для работника из указанного Time Report. Перепроверьте файлы. (Для корректной работы программмы файлы должны соответсвовать шаблону)");

            var paymentDataRow = _paymentData;
            
            if (paymentDataRow == null)
                throw new Exception("Не найдена строка в таблице Payment Data для работника из указанного Time Report. Перепроверьте файлы. (Для корректной работы программмы файлы должны соответсвовать шаблону)");
            
            CultureInfo ci = new CultureInfo("ru-RU");
            _hourlyRate = paymentDataRow.InvoiceAmount / paymentDataRow.HoursInvoiced;
            string amountInwords = NumberToEnglish.ChangeNumericToWords(paymentDataRow.InvoiceAmount);

            var result = new Employee()
            {
                NameEng = contractorsAndContractsDataRow.NameEng,
                NameRus = contractorsAndContractsDataRow.NameRus,
                ContractId = contractorsAndContractsDataRow.ContractId,
                DocSetNumber = paymentDataRow.DocSetNumber,
                ContractDate = string.Format("'{0}",contractorsAndContractsDataRow.ContractStartDate.ToString("dd MMM yyyy")),
                ContractDateTranslated = string.Format("'{0}", contractorsAndContractsDataRow.ContractStartDate.ToString("dd MMM yyyy", ci)),
                HoursInvoiced = paymentDataRow.HoursInvoiced, 
                Phone = contractorsAndContractsDataRow.Phone,
                ProjectName = paymentDataRow.ProjectName,
                TotalAmount = paymentDataRow.InvoiceAmount, 
                DocumentSetDate = string.Format("'{0}", _dateTo.ToString("dd MMM yyyy")),
                DocumentSetDateTranslated = string.Format("'{0}", _dateTo.ToString("dd MMM yyyy", ci)),
                DatesCovered = GetDatesRange(),
                AmountInwords = amountInwords,
                AmountInWordsTranslated = TranslateTextUsingGoogle.GetTranslateForAmount(amountInwords, "ru")
            };

            return result;
        }

        public InvoicePageData GetDataForInvoicePage()
        {
            string[] name = _timeReport.Name.Split(' ').ToArray();
            /// находим из Time репорта чисто по имени?

            var contractorsAndContractsDataRow = _contractorsAndContractsData;

            if (contractorsAndContractsDataRow == null)
                throw new Exception("Не найдена строка в контрактах для работника из указанного Time Report. Перепроверьте файлы. (Для корректной работы программмы файлы должны соответсвовать шаблону)");

            return new InvoicePageData()
            {
                AccountNumber = $"'{contractorsAndContractsDataRow.AccountNumber}",
                BankName = contractorsAndContractsDataRow.BankName,
                MailingAddress = contractorsAndContractsDataRow.MailingAddress,
                BanksBIC = $"'{contractorsAndContractsDataRow.BanksBIC}",
                CorrespondentAccount = $"'{contractorsAndContractsDataRow.CorrespondentAccount}",
                INN = $"'{contractorsAndContractsDataRow.INN}"
            };
        }


        private string GetDatesRange()
        {
            return string.Format("{0} - {1}", this._dateFrom.Day.ToString("00"), this._dateTo.ToString("dd MMMM yyyy"));
        }

        public string[] GetInvoicerName()
        {
            return this._invoicerName;
        }
    }
}
