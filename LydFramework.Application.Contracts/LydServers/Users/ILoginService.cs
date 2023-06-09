﻿using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.LydServers.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Contracts.LydServers.Users
{
    public interface ILoginService
    {
        /// <summary>
        /// 登陆
        /// </summary>
        public Task<ResultDto> Login(LoginDto dto);
    }
}
