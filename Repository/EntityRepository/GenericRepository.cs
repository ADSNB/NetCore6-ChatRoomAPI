using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Entity.Interfaces;
using Repository.EntityRepository.Interfaces;
using System.Data;

namespace Repository.EntityRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefault(e => e.Id == id);
        }

        public TEntity Create(TEntity entity)
        {
            var response = _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return response.Entity;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void Update(List<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> FromSql(string sql, SqlParameter[] parameters)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public int ExecuteSqlCommand(string sqlCommand, SqlParameter[] parameters)
        {
            return _dbContext.Database.ExecuteSqlRaw(sqlCommand, parameters);
        }

        public DataTable ExecuteSqlCommandDatatable(string sqlCommand, SqlParameter[] parameters)
        {
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlCommand;
                command.Parameters.AddRange(parameters);

                _dbContext.Database.OpenConnection();
                using (var dr = command.ExecuteReader())
                {
                    var tb = new DataTable();
                    tb.Load(dr);
                    return tb;
                }
            }
        }
    }
}