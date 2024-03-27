
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common
{
    /// <summary>
    /// 程序配置
    /// </summary>
    public class AppSettings
    {
        public Application Application { get; set; }
        public Serilog Serilog { get;set;}
        public Job Job { get; set; }
        public List<Databases> Databases { get; set; }
        public SqlSugar SqlSugar { get; set; }
    }
    public class Serilog
    {
        /// <summary>
        /// 写入到文件
        /// </summary>
        public bool WriteToFile { get; set; }
        /// <summary>
        /// 写入到控制台
        /// </summary>
        public bool WriteToConsole { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 滚动频率，0无限，1年，2月，3日，4时，5分
        /// </summary>
        public int RollingInterval { get; set; }
        /// <summary>
        /// 文件数量限制
        /// </summary>
        public int FileCountLimit { get; set; }
        /// <summary>
        /// 记录日志的模板
        /// </summary>
        public string SerilogOutputTemplate { get; set; }
        /// <summary>
        /// 排除哪些全类名
        /// </summary>
        public List<string> Excludes { get; set; }
    }
    public class Application
    {
        /// <summary>
        /// 项目名字
        /// </summary>
        public string Name { get; set;}
        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description { get; set;}
        /// <summary>
        /// 项目启动端口
        /// </summary>
        public string RunUrl { get; set;}
        /// <summary>
        /// 启动注册成windows服务
        /// </summary>
        public bool EnableWindowsService { get; set;}
        /// <summary>
        /// 跨域
        /// </summary>
        public List<string> CorsWithOrigins { get; set; }
        /// <summary>
        /// jwt密钥
        /// </summary>
        public string JwtSecurityKey { get; set;}
        /// <summary>
        /// 静态文件的共享路径
        /// </summary>
        public string StaticFilePath { get; set; }
    }
    public class Job
    {
        /// <summary>
        /// 任务存储方式，redis，sqlserver，memory
        /// </summary>
        public string Storage { get; set;}
        /// <summary>
        /// 连接字符串，Storage为redis或sqlserver时有效
        /// </summary>
        public string Connection { get; set;}
        /// <summary>
        /// 是否启动任务
        /// </summary>
        public bool IsEnabled { get; set;}
        /// <summary>
        /// 心跳，每隔几秒检测一次
        /// </summary>
        public int Heartbeat { get; set;}
    }
    public class Databases
    {
        public string Id { get; set; }
        public string Connection { get; set; }
        public string DbType { get; set; }
    }
    public class SqlSugar
    {
        /// <summary>
        /// 是否打印sql
        /// </summary>
        public bool IsPrintSql {  get; set; }
        /// <summary>
        /// 执行命令超时时间
        /// </summary>
        public int CommandTimeOut { get; set; }
    }
}
