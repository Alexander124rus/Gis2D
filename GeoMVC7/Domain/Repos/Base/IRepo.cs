using System;
using System.Collections.Generic;

namespace GeoMVC7.Domain.Repos.Base
{
    public interface IRepo<T>: IDisposable where T : class
    {
        int Add(T entity, bool persist = true);
        int AddRange(IEnumerable<T> entities, bool persist = true);
        int Update(T entity, bool persist = true);
        //int UpdateRange(IEnumerable<T> entities, bool persist = true);
        //int Delete(int id, byte[] timestamp, bool persist = true);
        int Delete(T entity, bool persist = true);
        //int DeleteRange(IEnumerable<T> entities, bool persist = true);
        T? Find(int? id);
        //T? FindAsNoTracking(int id);
        //T? FindlgnoreQueryFilters(int id);
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetAllIgnoreQueryFilters();
        //void ExecuteQuery(string sql, object[] sqlParametersObjects);
        int SaveChanges();

    }
}
