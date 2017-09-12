using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatApplicationSignalR.Models {
    public interface IChatter {
        int Id { get; set; }
        string Screenname { get; set; }
        //TODO Extract these values from somewhere else later.
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
    }
}
