using Microsoft.EntityFrameworkCore;

namespace LydFramework.EFCore.MySql
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
                optionsBuilder.UseMySql(connectionString,new MySqlServerVersion(dbversion));

                Type t = typeof(TDbContext);
                var constructor = t.GetConstructor(new Type[] { typeof(DbContextOptions<>).MakeGenericType(typeof(TDbContext)) });
                object objdb = constructor.Invoke(new object[] { optionsBuilder.Options });
                DbContextContainer.Value = objdb as TDbContext;
            }
            return DbContextContainer.Value!;
        }
    }
}