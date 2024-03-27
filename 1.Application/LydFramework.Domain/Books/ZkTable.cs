using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Books
{
    public class ZkTable
    {
        /// <summary>
        /// 批次id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 批次名
        /// </summary>
        public string? Zkid { get; set; }
        /// <summary>
        /// 添加批次的日期
        /// </summary>
        public DateTime ZkDate { get; set; }
        /// <summary>
        /// 图书来源
        /// </summary>
        public string? Tsfrom { get; set; }
        /// <summary>
        /// 凭证号码
        /// </summary>
        public string? Djhm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <summary>
        /// 批次年份
        /// </summary>
        public string? Zkyear { get; set; }
    }
}
