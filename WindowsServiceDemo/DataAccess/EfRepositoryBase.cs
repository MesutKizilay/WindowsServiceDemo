using Dapper;
using System;
using System.Data.Entity;
using WindowsServiceDemo.Entities;
using WindowsServiceDemo.Helper;

namespace WindowsServiceDemo.DataAccess
{
    public class EfRepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly TsContext _tsContext;
        private readonly DbSet<T> _object;

        public EfRepositoryBase(TsContext tsContext)
        {
            _tsContext = tsContext;
            _object = _tsContext.Set<T>();
        }

        public void Add(T entity)
        {
            try
            {
                var addedEntity = _tsContext.Entry(entity);
                addedEntity.State = EntityState.Added;
                _object.Add(entity);
                _tsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                //throw;
            }
        }

        public void AddDapper(T entity)
        {
            string query = "INSERT INTO CARS (NAME) VALUES (@Name)";

            using (var connection = _tsContext.CreateConnection())
            {
                connection.Execute(query, entity);
            }
        }
    }

    public interface ICarDal : IRepository<Car>
    {

    }

    public class CarDal : EfRepositoryBase<Car>, ICarDal
    {
        public CarDal(TsContext tsContext) : base(tsContext)
        {
        }
    }
}