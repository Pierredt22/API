using System;
using System.Collections.Generic;

namespace MyContacts.Models
{
    public partial class WebUser
    {
        public WebUser()
        {
            Contact = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
    }
}
