using System;
using System.Collections.Generic;
using System.Linq;
using GeoMVC7.Domain.Entities;
using GeoMVC7.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GeoMVC7.Domain.Repos.Base
{
    public class BaseRepo<T> : IRepo<T> where T : class
    {
        private readonly bool _disposeContext;
        public ApplicationContext Context { get; }
        public DbSet<T> Table { get; }

        public BaseRepo(ApplicationContext context)
        {
            Context = context;
            Table = Context.Set<T>();
            _disposeContext = false;
        }
        //public BaseRepo(DbContextOptions<ApplicationContext> options)
        //: this(new ApplicationContext(options))
        //{
        //    _disposeContext = true;
        //}
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (
                _disposeContext)
                {
                    Context.Dispose();
                }
            }
            _isDisposed = true;
        }
        ~BaseRepo()
        {
            Dispose(false);
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (CustomException ex)
            {
                // Подлежит надлежащей обработке — уже зарегистрировано в журнале,
                throw;
            }
            catch (Exception ex)
            {
                // Подлежит регистрации в журнале и надлежащей обработке.
                throw new CustomException("An error occurred updating the database", ex);
            }
        }
        public virtual IEnumerable<T> GetAll() => Table;
        public virtual IEnumerable<T> GetAllIgnoreQueryFilters() => Table.IgnoreQueryFilters();



        //Добавить сущьность
        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges(): 0 ;
        }
        //Добавить сущьности
        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }
        //Обновить сущьность
        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }
        //Обновить сущьности
        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }
        //Удалить сущьность
        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }
        

        //Удалить сущьности
        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        //Найти сущьность
        public virtual T? Find(int? id) => Table.Find(id);




        

    }
}
