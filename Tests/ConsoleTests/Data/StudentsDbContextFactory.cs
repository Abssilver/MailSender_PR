using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTests.Data
{
    public class StudentsDBContextFactory : IDesignTimeDbContextFactory<StudentsDB>
    {
        public StudentsDB CreateDbContext(string[] args)
        {
            const string connection =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB;Integrated Security=True";

            var optionsBuilder = new DbContextOptionsBuilder<StudentsDB>();
            optionsBuilder.UseSqlServer(connection);

            return new StudentsDB(optionsBuilder.Options);
        }
    }
}
