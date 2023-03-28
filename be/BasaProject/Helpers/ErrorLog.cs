using BasaProject.Models;
using BasaProject.Helpers;

namespace BasaProject.Helpers
{
    public class ErrorLog
    {
        public static void ErrorException(Exception e, Guid userid, DataContext _db)
        {
            TrErrorLog log = new TrErrorLog()
            {
                Message = e.Message,
                InnerException = e.InnerException?.Message,
                StackTrace = e.StackTrace,
                Source = e.InnerException?.Source,
                UserIn = userid
            };

            _db.TrErrorLogs.Add(log);
            _db.SaveChanges();
        }
    }
}
