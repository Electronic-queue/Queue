using Queue.Application.Users.Queries.GetUserList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Queries.GetWindowList
{
    public  class WindowLIistVm
    {
        public IList<UserLookupDto> Windows { get; set; }
    }
}
