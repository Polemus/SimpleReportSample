using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReportSample.HelperClassesAndInterfaces.BaseInterfaces
{
    /// <summary>
    /// Базовый интерфейс чтобы обрабатывать классы с базовым набором данных
    /// </summary>
    interface BaseData
    {
        string EmployerName { get; set; }
    }
}
