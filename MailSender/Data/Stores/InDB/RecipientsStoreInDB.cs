using MailSender.Interfaces;
using MailSender.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.Data.Stores.InDB
{
    class RecipientsStoreInDB : IStore<Recipient>
    {
        private readonly MailSenderDB _db;
        public RecipientsStoreInDB(MailSenderDB db)
        {
            _db = db;
        }
        public Recipient Add(Recipient entity)
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

        public IEnumerable<Recipient> GetAll()
        {
            return _db.Recipients.ToArray();
        }

        public Recipient GetById(int id)
        {
            return _db.Recipients.FirstOrDefault(recipient => recipient.Id.Equals(id));
        }

        public void Update(Recipient entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
