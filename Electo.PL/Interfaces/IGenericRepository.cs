
using Electo.PL.Models;
using System.Collections.Generic;

namespace Electo.PL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        IEnumerable<TEntity> Get();
        TEntity GetById(object  Id);
        void Add(TEntity entity );
        void Edit(TEntity entity);
        void Delete(int id);
        ProductVM GetItmeWithQyry(int Id);

    }
}
