using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Domain
{
    public class User
    {
        public Guid Id {  get; set; }
        public int Iin {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public DateTime CreationDate { get; set; }
    }
}
