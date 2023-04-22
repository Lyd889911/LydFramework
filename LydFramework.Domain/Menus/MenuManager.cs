using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Menus
{
    public class MenuManager
    {
        private readonly IMenuRepository _menuRepository;
        public MenuManager(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        
    }
}
