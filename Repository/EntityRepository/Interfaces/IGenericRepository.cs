using Microsoft.Data.SqlClient;
using System.Data;

namespace Repository.EntityRepository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);

        //void TruncateDelete();

        IQueryable<TEntity> FromSql(string sql, SqlParameter[] parameters);

        int ExecuteSqlCommand(string sqlCommand, SqlParameter[] parameters);

        DataTable ExecuteSqlCommandDatatable(string sqlCommand, SqlParameter[] parameters);
    }
}