using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Menus
{
    public class Menu:AggregateRoot<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 路由
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        public ICollection<RoleMenu> RoleMenus { get; set; }
        public Menu(string title,string icon,string path,int level,Guid? parentId)
        {
            Title = title;
            Path = path;
            Icon = icon;
            Level = level;
            ParentId = parentId;
        }
        public void Update(string title,string icon,string path,int level,Guid? parentId)
        {
            Title = title;
            Icon = icon;
            Path = path;
            Level = level;
            ParentId = parentId;
            ModifyTime = DateTime.Now;
        }
    }
}
