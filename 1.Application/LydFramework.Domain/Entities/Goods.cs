using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LydFramework.Common.BaseEntity;

namespace LydFramework.Domain.Entities
{
    public class Goods:AggregateRoot
    {
        public int Bid { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
