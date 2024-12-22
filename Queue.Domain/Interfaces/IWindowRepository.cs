using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Domain.Interfaces
{
    public interface IWindowRepository:IRepository<Window>
    {
        Task<Result> GetWindowById(int id);
    }
}
