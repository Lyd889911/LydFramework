using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Entities
{
    public class Reader
    {
        public string? Name { get; set; }
        public string? Rid { get; set; }
        public int? DepId { get; set; }
        public byte? IsGs { get; set; }
        public int? Rgid { get; set; }
        public string? GuestPassWord { get; set; }
        public string? Tel { get; set; }
        public string? Sex { get; set; }
        public string? Idcard { get; set; }
        public string? ValidCard { get; set; }
        public string? Addresss { get; set; }
        public DateTime? Enddate { get; set; }
    }
}
