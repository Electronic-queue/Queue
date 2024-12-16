using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid Id { get; }
    }
}
