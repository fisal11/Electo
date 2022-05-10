using Electo.DAL.Entities;
using System;

namespace Electo.PL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Brand> BrandRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }


        void Save();


    }
}
