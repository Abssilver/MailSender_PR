using ConsoleTests.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTests.Data
{
    public class StudentsDB: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public StudentsDB(DbContextOptions<StudentsDB> options): base(options)
        {

        }
    }
}
