using ConsoleTests.Data;
using ConsoleTests.Data.Entities;
using MailSender.Interfaces;
using MailSender.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string connection = 
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB;Integrated Security=True";

            /*
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<StudentsDB>(opt => opt.UseSqlServer(connection));
            var services = serviceCollection.BuildServiceProvider();

            using (var db = services.GetRequiredService<StudentsDB>())
            { }
            */

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection).Options))
            {
                await db.Database.MigrateAsync();
                //await db.Database.EnsureCreatedAsync();
                var studentsCount = await db.Students.CountAsync();
                Console.WriteLine($"Число студентов {studentsCount}");
            }

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection).Options))
            {
                if (await db.Students.CountAsync() == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var group = new Group
                        {
                            Name = $"Группа {i}",
                            Description = $"Описание группы {i}",
                            Students = new List<Student>()
                        };
                        for (int j = 0; j < 10; j++)
                        {
                            var student = new Student
                            {
                                Name = $"Студент {i + j}",
                                Surname = $"Фамилия {i + j}",
                                Patronymic = $"Отчество {i + j}",
                                AverageMark = i + j
                            };
                            group.Students.Add(student);
                        }
                        await db.Groups.AddAsync(group);
                    }
                    await db.SaveChangesAsync();
                }
            }

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection).Options))
            {
                var students = await db.Students
                    .Include(student => student.Group) // join
                    .Where(student => student.Group.Name == "Группа 1")
                    .ToListAsync();

                students.ForEach(student =>
                Console.WriteLine($"ID:{student.Id} | Name: {student.Name} | Group: {student.Group.Name}"));
            }
            /*
            var task = AsyncAwaitTest.StartAsync();
            var processMessages = AsyncAwaitTest.ProcessDataTestAsync();

            Console.WriteLine("Тестовая задача запущена");
            Task.WaitAll(task, processMessages);
            */
        }
    }
}
