using LydFramework.Common.BaseEntity;
using LydFramework.Common.Utils;
using LydFramework.Domain.Repositorys;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlSugarExpansions
    {
        public static void AddLydSqlSugar(this IServiceCollection services)
        {
            var appsettings = AppUtils.AppSettings;
            services.AddScoped<ISqlSugarClient>(s =>
            {

                List<ConnectionConfig> connectionConfigs = new List<ConnectionConfig>();
                foreach (var db in appsettings.Databases)
                {
                    ConnectionConfig connCinfig = new()
                    {
                        DbType = Enum.Parse<DbType>(db.DbType),
                        ConnectionString = db.Connection,
                        IsAutoCloseConnection = true,
                        ConfigId = db.Id,
                        ConfigureExternalServices=new ConfigureExternalServices()
                        {
                            //AOP设置迁移的时候实体字段可以为空
                            EntityService = (c, p) =>
                            {
                                /***高版C#写法 支持string?和string  ***/
                                if (p.IsPrimarykey == false && new NullabilityInfoContext()
                                    .Create(c).WriteState is NullabilityState.Nullable)
                                {
                                    p.IsNullable = true;
                                }
                            }
                        }
                    };
                    connectionConfigs.Add(connCinfig);
                }


                SqlSugarClient sqlSugar = new SqlSugarClient(connectionConfigs, db =>
                {
                    foreach (var d in appsettings.Databases)
                    {
                        //命令超时时间
                        db.GetConnection(d.Id).Ado.CommandTimeOut = appsettings.SqlSugar.CommandTimeOut;
                        //每次Sql执行前事件
                        db.GetConnection(d.Id).Aop.OnLogExecuting = (sql, pars) =>
                        {
                            if (appsettings.SqlSugar.IsPrintSql)
                            {
                                Console.WriteLine(sql);
                                Log.Logger.Information(sql);
                            }
                        };
                        //审计事件
                        db.GetConnection(d.Id).Aop.DataExecuting = (oldValue, entityInfo) =>
                        {

                            /*** 列级别事件：插入的每个列都会进事件 ***/

                            if(entityInfo.OperationType == DataFilterType.InsertByObject)
                            {
                                if (entityInfo.PropertyName == "CreateTime")
                                    entityInfo.SetValue(DateTime.Now);//修改CreateTime字段
                                if (entityInfo.PropertyName == "CreateBy")
                                    entityInfo.SetValue(AppUtils.UserId);
                            }
                            else if(entityInfo.OperationType == DataFilterType.UpdateByObject)
                            {
                                if (entityInfo.PropertyName == "ModifyTime")
                                    entityInfo.SetValue(DateTime.Now);
                                if (entityInfo.PropertyName == "ModifyBy")
                                    entityInfo.SetValue(AppUtils.UserId);
                            }


                            /*** 行级别事件 ：一条记录只会进一次 ***/
                            if (entityInfo.EntityColumnInfo.IsPrimarykey)
                            {
                                //entityInfo.EntityValue 拿到单条实体对象
                            }

                            //可以写多个IF

                        };
                        //过滤器
                        db.GetConnection(d.Id).QueryFilter.AddTableFilter<IHasDelete>(it => it.IsDeleted == false);
                        db.GetConnection(d.Id).QueryFilter.AddTableFilter<IHasTenant>(it => it.TenantId == AppUtils.TenantId);
                        db.GetConnection(d.Id).QueryFilter.AddTableFilter<IHasUser>(it => it.UserId == AppUtils.UserId);
                    }

                });
                return sqlSugar;
            });
            //注册仓储
            services.AddScoped(typeof(SqlSugarRepository<>));
        }
    }
}
