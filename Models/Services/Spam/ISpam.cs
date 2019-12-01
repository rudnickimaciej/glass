using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.Models.Services.Spam
{
    public interface ISpam
    {
        //Reports
        SpamReport GetReport(int reportId);
        IEnumerable<SpamReport> GetAllReports();
        void AddReport(SpamReport report);
        void DeleteReport(int reportId);

        //Reasons

        IEnumerable<SpamReason> GetAllReasons();


    }
}