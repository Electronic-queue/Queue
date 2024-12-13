using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
        public int Iin {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }

    }
}
