using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LydFramework.Common.BaseEntity;
using SqlSugar;

namespace LydFramework.Domain.Entities
{
    public class User:AggregateRoot
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public long Id { get; set; }
        [SugarColumn(OldColumnName ="UName")]
        public string XXXName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
        
    }
}
