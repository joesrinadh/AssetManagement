using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.DataAccess
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public void Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public TEntity Get(int Id);
        public int SaveChanges();
    }
}
