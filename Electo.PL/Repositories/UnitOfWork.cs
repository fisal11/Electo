using Electo.DAL.Context;
using Electo.DAL.Entities;
using Electo.PL.Interfaces;
using System;

namespace Electo.PL.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        private GenericRepository<Category> categoryRepository;
        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(_context);
                }
                return categoryRepository;
            }
        }

        private GenericRepository<Product> productRepository;
        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(_context);
                }
                return productRepository;
            }
        }

        private GenericRepository<Brand> brandRepository;
        public IGenericRepository<Brand> BrandRepository
        {
            get
            {
                if (this.brandRepository == null)
                {
                    this.brandRepository = new GenericRepository<Brand>(_context);
                }
                return brandRepository;
            }
        }

        private GenericRepository<Order> orderRepository;
        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(_context);
                }
                return orderRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}
