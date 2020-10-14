using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleTests.Data.Entities
{
    abstract class Entity
    {
        //[Key]
        public int Id { get; set; }

    }
    abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
    class Student: NamedEntity
    {
        [Required, MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string Patronymic { get; set; }

        public virtual Group Group { get; set; } //virtual - тут навигационное свойство

    }
    class Group: NamedEntity
    {
        public string Description { get; set; }

        public virtual ICollection<Student> strudents { get; set; } //отложенные запросы к даннымм
    }

}
