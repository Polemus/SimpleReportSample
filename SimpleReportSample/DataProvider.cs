using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using SimpleReportSample.HelperClassesAndInterfaces;
using SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SimpleReportSample
{
    public class DataProvider
    {
        public List<PaymentData> GetPaymentTableData(string filePath, string tableName)
        {
           ///так и передаем? наименование таблицы?
           return GetPaymentDataValues(GetDataTableCollection(filePath), tableName);
        }

        public List<ContractorsAndContracts> GetContractorsAndContractsTableData(string filePath, string tableName)
        {
            ///так и передаем? наименование таблицы? 
            return GetContractorsAndContractsValues(GetDataTableCollection(filePath, 4), tableName);
        }

        public TimeReport GetTimeReportByProject(List<string> filesInPath, PaymentData paymentData)
        {
            TimeReport timeReportResult = new TimeReport();
            ///добавить фильтр по дате из названия файла
            var reportFileByProject = filesInPath.Where(x => x.Contains(paymentData.ProjectName)).SingleOrDefault();
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(@reportFileByProject);

            // Keeping track
            bool found = false;
            // Loop through all worksheets in the workbook
            foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in wb.Sheets)
            {
                // Check the name of the current sheet
                if (sheet.Name == paymentData.EmployerName)
                {
                    found = true;
                    break; // Exit the loop now
                }
            }

            wb.Close();

            if (found)
            {
                var table = GetDataTableCollectionByEmployeeName(reportFileByProject, paymentData.EmployerName, 11);

                ///Костыль тк мы не знаем
                if (!table.Columns.Contains("#"))
                    table = GetDataTableCollectionByEmployeeName(reportFileByProject, paymentData.EmployerName, 14);

                timeReportResult.TimeReportRows = new List<TimeReportRow>();
                //timeReportResult.SetName(filePath);

                foreach (DataRow row in table.Rows)
                {
                    TimeReportRow timeReportRow = new TimeReportRow();

                    foreach (DataColumn column in table.Columns)
                    {
                        PutValueByTheCaptionTimeReport(row, column, timeReportRow);
                    }

                    timeReportResult.TimeReportRows.Add(timeReportRow);
                }

                /// удаляем последнюю строку с Total и все которые не заполнены в полях Task и Description
                RemoveEmptyAndTotalRows(timeReportResult);
            }
            else
            {
                return null;
            }

            return timeReportResult;
        }

        public TimeReport GetTimeReport(string filePath)
        {
            var table = GetTimeReportTable(GetDataTableCollection(filePath, 11));
            TimeReport timeReportResult = new TimeReport();

            ///Костыль тк мы не знаем
            if (!table.Columns.Contains("#"))
                table = GetTimeReportTable(GetDataTableCollection(filePath, 14));

            timeReportResult.TimeReportRows = new List<TimeReportRow>();
            timeReportResult.SetName(filePath);

            foreach (DataRow row in table.Rows)
            {
                TimeReportRow timeReportRow = new TimeReportRow();

                foreach (DataColumn column in table.Columns)
                {
                    PutValueByTheCaptionTimeReport(row, column, timeReportRow);
                }

                timeReportResult.TimeReportRows.Add(timeReportRow);
            }

            /// удаляем последнюю строку с Total и все которые не заполнены в полях Task и Description
            RemoveEmptyAndTotalRows(timeReportResult);

            return timeReportResult;
        }

        private void RemoveEmptyAndTotalRows(TimeReport timeReportResult)
        {
            timeReportResult.TimeReportRows = timeReportResult.TimeReportRows.Where(x => !string.IsNullOrWhiteSpace(x.Task) && !string.IsNullOrWhiteSpace(x.Description)).ToList();
        }


        private System.Data.DataTable GetTimeReportTable(DataTableCollection dataTableCollection)
        {
            /// тк таблица репорта начинается не с 1 колонки скипаем её
            dataTableCollection[0].Columns.RemoveAt(0);

            return dataTableCollection[0];
        }

        /// <summary>
        /// Очень сильно подумать что можно сделать с файлом TimeReport (получилось сделать 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="timeReportRow"></param>
        private void PutValueByTheCaptionTimeReport(DataRow row, DataColumn column, TimeReportRow timeReportRow)
        {
            if (column.Caption.Contains("Project"))
            {
                timeReportRow.ProjectName = row[column].ToString();
            }

            if (column.Caption.Contains("Task"))
            {
                timeReportRow.Task = row[column].ToString();
            }

            if (column.Caption.Contains("Description"))
            {
                timeReportRow.Description = row[column].ToString();
            }

            if (column.Caption.Contains("Regular Labor") || column.Caption.Contains("Regular Time"))
            {
                decimal.TryParse(row[column].ToString(), out decimal result);
                timeReportRow.RegularLabor = result;
            }

            if (column.Caption.Contains("Overtime Labor") || column.Caption.Contains("Overtime"))
            {
                decimal.TryParse(row[column].ToString(), out decimal result);
                timeReportRow.OvertimeLabor = result;
            }

            if (column.Caption.Contains("Off hours"))
            {
                decimal.TryParse(row[column].ToString(), out decimal result);
                timeReportRow.OffHours = result;
            }

            if (column.Caption.Contains("Date"))
            {
                DateTime.TryParse(row[column].ToString(), out DateTime result);
                timeReportRow.Date = result;
            }
        }

        private System.Data.DataTable GetDataTableCollectionByEmployeeName(string filePath, string employeeName, int skipRows = 0)
        {
            DataTableCollection dataTableCollection;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                            ReadHeaderRow = rowReader =>
                            {
                                /// скипаем строки если есть какие либо ненужные нам для считывания
                                for (int i = 0; i < skipRows; i++)
                                    rowReader.Read();
                            }
                        }
                    });

                    dataTableCollection = result.Tables;
                }
            }

            return dataTableCollection["employeeName"];
        }

        private DataTableCollection GetDataTableCollection(string filePath, int skipRows = 0)
        {
            DataTableCollection dataTableCollection;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                            ReadHeaderRow = rowReader =>
                            {
                                /// скипаем строки если есть какие либо ненужные нам для считывания
                                for (int i = 0; i < skipRows; i++)
                                    rowReader.Read();
                            }
                        }
                    });

                    dataTableCollection = result.Tables;
                }
            }

            return dataTableCollection;
        }

        private List<ContractorsAndContracts> GetContractorsAndContractsValues(DataTableCollection dataTableCollection, string tableName)
        {
            System.Data.DataTable table = dataTableCollection[tableName];

            if (table == null)
                table = dataTableCollection[0];

            List<ContractorsAndContracts> contractorsAndContracts = new List<ContractorsAndContracts>();

            foreach (DataRow row in table.Rows)
            {
                ContractorsAndContracts contractorsAndContractRow = new ContractorsAndContracts();

                foreach (DataColumn column in table.Columns)
                {
                        PutValueByTheCaptionContractsAndContracters(row, column, contractorsAndContractRow);
                }

                contractorsAndContracts.Add(contractorsAndContractRow);
            }

            return contractorsAndContracts;
         }

        private List<PaymentData> GetPaymentDataValues(DataTableCollection dataTableCollection, string tableName)
        {
            System.Data.DataTable table = dataTableCollection[tableName];
            
            if (table == null)
                table = dataTableCollection[0];

            List<PaymentData> paymentData = new List<PaymentData>();

            foreach (DataRow row in table.Rows)
            {
                PaymentData paymentDataRow = new PaymentData();

                foreach (DataColumn column in table.Columns)
                {
                        PutValueByTheCaptionPaymentData(row, column, paymentDataRow);
                }

                paymentData.Add(paymentDataRow);
            }

            return paymentData;
        }

        private void PutValueByTheCaptionPaymentData(DataRow row, DataColumn column, PaymentData paymentDataRow)
        {
            if (column.Caption.Contains("Docs #"))
            {
                paymentDataRow.DocSetNumber = Int32.Parse(row[column].ToString());
            }

            if (column.Caption.Contains("Name, En"))
            {
                paymentDataRow.EmployerName = row[column].ToString();
            }

            if (column.Caption.Contains("Округ. $"))
            {
                paymentDataRow.RoundedCurrency = decimal.Parse(row[column].ToString());
            }

            if (column.Caption.Contains("Hours"))
            {
                paymentDataRow.HoursInvoiced = Decimal.Parse(row[column].ToString());
            }

            if (column.Caption.Contains("Invoice, ₽"))
            {
                paymentDataRow.InvoiceAmount = decimal.Parse(row[column].ToString());
            }

            if (column.Caption.Contains("Rate ₽/hr"))
            {
                paymentDataRow.HourlyRate = decimal.Parse(row[column].ToString());
            }

            if (column.Caption.Contains("Project"))
            {
                paymentDataRow.ProjectName = row[column].ToString();
            }
        }

        /// <summary>
        /// переделать эту страшную штуку во что то приличное
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="contractorsAndContract"></param>
        private void PutValueByTheCaptionContractsAndContracters(DataRow row, DataColumn column, ContractorsAndContracts contractorsAndContractRow)
        {
            if (column.Caption.Contains("Contract #"))
            {
                contractorsAndContractRow.ContractId = row[column].ToString();
            }

            if (column.Caption.Contains("Name Eng"))
            {
                contractorsAndContractRow.NameEng = row[column].ToString();
            }

            if (column.Caption.Contains("Name Rus"))
            {
                contractorsAndContractRow.NameRus = row[column].ToString();
            }

            if (column.Caption.Contains("Contract Date Eng"))
            {
                contractorsAndContractRow.ContractStartDate = DateTime.Parse(row[column].ToString());
            }

            if (column.Caption.Contains("Contract Date Rus"))
            {
                contractorsAndContractRow.ContractStartDateRus = row[column].ToString();
            }

            if (column.Caption.Contains("INN"))
            {
                contractorsAndContractRow.INN = row[column].ToString();
            }

            if (column.Caption.Contains("Mailing Address"))
            {
                contractorsAndContractRow.MailingAddress = row[column].ToString();
            }

            if (column.Caption.Contains("Bank") && !column.Caption.Contains("BIC"))
            {
                contractorsAndContractRow.BankName = row[column].ToString();
            }

            if (column.Caption.Contains("Bank's BIC"))
            {
                contractorsAndContractRow.BanksBIC = row[column].ToString();
            }

            if (column.Caption.Contains("Correspondent Account"))
            {
                contractorsAndContractRow.CorrespondentAccount = row[column].ToString();
            }

            if (column.Caption.Contains("Account #"))
            {
                contractorsAndContractRow.AccountNumber = row[column].ToString();
            }

            if (column.Caption.Contains("Phone #"))
            {
                contractorsAndContractRow.Phone = row[column].ToString();
            }
        }

        public class TimeReport
        { 
            public string Name { get; set; }

            public List<TimeReportRow> TimeReportRows { get; set; }

            internal void SetName(string filePath)
            {
                this.Name = System.IO.Path.GetFileName(filePath).Split('_')[0];
            }
        }
    }
}
