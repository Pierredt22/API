using System;
using System.Collections.Generic;

namespace MyContacts.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int FkUserId { get; set; }

        public virtual WebUser FkUser { get; set; }
    }
}
