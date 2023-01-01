using System.Linq.Expressions;
using BSc_Thesis.DataBase.Models;

namespace BSc_Thesis.DataBase.Stores;

public interface IProcessStore
{
    Task<ProcessDb> GetLast();
    Task<ProcessDb[]> GetAll();
    Task<ProcessDb[]> GetByID(int id);
    Task<ProcessDb[]> GetWithFilter(Expression<Func<ProcessDb, bool>> predicate);
}