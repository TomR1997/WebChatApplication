//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chat.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class chatter
    {
        public int ChatterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Screenname { get; set; }
    
        public virtual chatclient chatclient { get; set; }
        public virtual chatsupporter chatsupporter { get; set; }
    }
}
