using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.SqlServer
{
    public class DbContextFactory<TDbContext> where TDbContext : DbContext, new()
    {
        public AsyncLocal<TDbContext> DbContextContainer { get; set; }
        public TDbContext CreateDbContext(string connectionString,string dbversion)
        {
            if (DbContextContainer == null)
            {
                DbContextContainer = new AsyncLocal<TDbContext>();
                var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

                if (dbversion == "2008" || dbversion == "2005")
                {
                    optionsBuilder.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();
                }

                optionsBuilder.UseSqlServer(connectionString);

                Type t = typeof(TDbContext);
                var constructor = t.GetConstructor(new Type[] { typeof(DbContextOptions<>).MakeGenericType(typeof(TDbContext)) });
                object objdb = constructor.Invoke(new object[] { optionsBuilder.Options });
                DbContextContainer.Value = objdb as TDbContext;
            }
            return DbContextContainer.Value!;
        }
    }
}
