using MailSender.Interfaces;
using MailSender.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Data.Stores.InDB
{
    class StoreInDB<TEntity> : IStore<TEntity> where TEntity : Entity
    {
        private readonly MailSenderDB _db;
        private readonly DbSet<TEntity> _set;
        public StoreInDB(MailSenderDB db)
        {
            _db = db;
            _set = _db.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity is null)
            {
                return;
            }
            _db.Entry(entity).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set.ToArray();
        }

        public TEntity GetById(int id)
        {
            return _set.FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
