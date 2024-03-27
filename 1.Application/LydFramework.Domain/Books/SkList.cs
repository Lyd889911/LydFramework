using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Books
{
    public class SkList
    {
        public string? SkName { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public short Sk { get; set; }
        public int? IdDisp { get; set; }
        public int State { get; set; }
        [Navigate(NavigateType.OneToMany, "State", "Id")]
        public SkState SkState { get; set; }
        public SkList()
        {

        }
    }
}
