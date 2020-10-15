using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleTests.Data.Entities
{
    public abstract class Entity
    {
        //[Key]
        public int Id { get; set; }

    }
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
    public class Student: NamedEntity
    {
        [Required, MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string Patronymic { get; set; }

        public virtual Group Group { get; set; } //virtual - тут навигационное свойство

        public double AverageMark { get; set; }
    }
    public class Group: NamedEntity
    {
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; } //отложенные запросы к даннымм
    }

}
