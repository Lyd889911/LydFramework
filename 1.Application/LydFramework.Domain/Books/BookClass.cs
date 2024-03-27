using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Books
{
    public class BookClass
    {
        public string? Bcid { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public string? Bid { get; set; }
        public byte State { get; set; }
        public string? Clerk { get; set; }
        public DateTime? EnterDate { get; set; }
        public float? Price { get; set; }
        public short? Sk { get; set; }
        public int? InForm { get; set; }
        public string? Ddid { get; set; }
        public string? ShelfNumber { get; set; }
        public int? Id { get; set; }
        //[Navigate(NavigateType.OneToMany, "Sk","Sk")]
        //public SkList SkList { get; set; }
        //[Navigate(NavigateType.OneToMany, "InForm", "Id")]
        //public ZkTable ZkTable { get; set; }
    }
}
