using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MailSender.Data
{
    class MailSenderDbContextInitializer : IDesignTimeDbContextFactory<MailSenderDB>
    {
        public MailSenderDB CreateDbContext(string[] args)
        {
            const string connection =
                @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MailSender.DB;Integrated Security=True";

            var optionsBuilder = new DbContextOptionsBuilder<MailSenderDB>();
            optionsBuilder.UseSqlServer(connection);

            return new MailSenderDB(optionsBuilder.Options);
        }
    }
}
