﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserList
{
    public class UserListVm
    {
        public IList<UserLookupDto> Users { get; set; }
    }
}