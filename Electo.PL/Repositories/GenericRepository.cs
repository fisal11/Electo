using Electo.DAL.Context;
using Electo.PL.Interfaces;
using Electo.PL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Electo.PL.Repositories
{
    public class GenericRepository <TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        ApplicationDbContext _context;
        DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(int id )
        {
            var deletdata =_dbSet.Find(id );
            _dbSet.Remove(deletdata);
            _context.SaveChanges();
        }

        public virtual void Edit(TEntity entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            var data =  _dbSet.ToList();
            return data;

        }
        public virtual TEntity GetById(object Id)
        {
            return _dbSet.Find(Id);
        }

        public ProductVM GetItmeWithQyry(int Id)
        {
            var data= _context.Product.Where(a=>a.Id==Id)
                .Select(a => new ProductVM{
                    Id = a.Id,
                    Name = a.Name,
                    Salary = a.Salary,
                    Image = a.Image,
                    Descrption = a.Descrption,
                    BrandName = a.Brand.BrandName,
                    CategoryName = a.Category.CategoryName

                }).FirstOrDefault();
            return data;
        }
    }
}
