using MailSender.Interfaces;
using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.Data.Stores.InMemory
{
    class RecipientsStoreInMemory : IStore<Recipient>
    {
        public Recipient Add(Recipient entity)
        {
            if (TestData.Recipients.Contains(entity))
            {
                return entity;
            }
            entity.Id = TestData.Recipients.DefaultIfEmpty().Max(recipient => recipient.Id) + 1;
            TestData.Recipients.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity is null)
            {
                return;
            }
            TestData.Recipients.Remove(entity);
        }

        public IEnumerable<Recipient> GetAll()
        {
            return TestData.Recipients;
        }

        public Recipient GetById(int id)
        {
            return GetAll().FirstOrDefault(recipient => recipient.Id.Equals(id));
        }

        public void Update(Recipient entity)
        {
        }
    }
}
