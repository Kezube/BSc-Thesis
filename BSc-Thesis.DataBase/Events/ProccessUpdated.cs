using BSc_Thesis.DataBase.Models;
using Easy.MessageHub;

namespace BSc_Thesis.DataBase.Events
{
    //public class ProcessUpdated
    //{
    //    private readonly ProcessDb _db;
    //    public ProcessUpdated(ProcessDb processDb)
    //    {
    //        _db = processDb;
    //    }

    //    public ProcessDb GetCurrentProcces()
    //    {
    //        return _db;
    //    }



    //}

    public record ProcessUpdated(ProcessDb pro)
    {
        ProcessDb x = pro;
    }

}