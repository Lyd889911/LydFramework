
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Books
{
    public class BookList
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string? Isbn { get; set; }
        public string? Bcid { get; set; }
        public string? Title { get; set; }
        public string? Writer { get; set; }
        public string? Epitome { get; set; }
        public string? Pages { get; set; }
        public float? Price { get; set; }
        public string? PublishDate { get; set; }
        public string? PageMode { get; set; }
        public string? Version { get; set; }
        public string? ExtName { get; set; }
        public string? Translator { get; set; }
        public string? Keyword { get; set; }
        public string? PubId { get; set; }
        public short? Number { get; set; }
        public string? FrameNo { get; set; }
        public DateTime EnterDate { get; set; }
        public int? BcidSn { get; set; }
        public string? Cbm { get; set; }
        public string? Cbzz { get; set; }
        public string? Pytitle { get; set; }
        public string? Pywriter { get; set; }
        public string? Zzh { get; set; }
        public string? Sjh { get; set; }
        public string? Adds { get; set; }
        public string? Publish { get; set; }
        public int? Type { get; set; }
        public string? Img { get; set; }
        [Navigate(NavigateType.OneToMany, "Id","Id")]//一对一 SchoolId是StudentA类里面的
        public List<BookClass> BookClass { get; set; }
        public BookList()
        {
            Price = 0;
            BcidSn = 0;
            Type = 0;
        }
    }
}
