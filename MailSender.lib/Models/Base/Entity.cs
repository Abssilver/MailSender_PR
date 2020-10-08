using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace MailSender.Models.Base
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }
    public abstract class NamedEntity : Entity 
    {
        public virtual string Name { get; set; }
    }
    public abstract class Person : NamedEntity
    {
        public virtual string Address { get; set; }
    }
}
