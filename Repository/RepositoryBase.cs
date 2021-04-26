using Infrastructure.Interface.DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private ASMContext _context;

        public RepositoryBase(ASMContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add data
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// Delete data
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Get data by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntity Get(int Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
