using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTests.Data
{
    class StudentsDB: DbContext
    {
        public StudentsDB(DbContextOptions<StudentsDB> options): base(options)
        {

        }
    }
}
