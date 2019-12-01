using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translate.Models.Services.Spam
{
    public class SpamService : ISpam
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddReport(SpamReport report)
        {
            throw new NotImplementedException();
        }

        public void DeleteReport(int reportId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SpamReport> GetAllReports()
        {
            return _context.SpamReports;
        }

        public SpamReport GetReport(int reportId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<SpamReason> ISpam.GetAllReasons()
        {
            return _context.SpamReasons;
        }

    }
}