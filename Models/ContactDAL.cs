using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyContacts.Models
{
    public class ContactDAL
    {
        private readonly ContactsContext _context;
        public ContactDAL(ContactsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            try
            {
                return await _context.Contact.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public int AddContact(Contact c)
        {
            try
            {
                _context.Contact.Add(c);
                _context.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }
      
        public int UpdateContact(Contact c)
        {
            try
            {

                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        
        public Contact GetContact(int cId)
        {
            try
            {
                Contact c = _context.Contact.Find(cId);
                return c;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContacts2(int uId)
        {
            try
            {
                return await _context.Contact.Where(x => x.FkUserId == uId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
       
        public int DeleteContact(int cId)
        {
            try
            {
                Contact c = _context.Contact.Find(cId);
                _context.Contact.Remove(c);
                _context.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }



        /****************************************************************************************/
        // Users data

        public string AddUser(WebUser u)
        {
            try
            {

                try
                {
                    string username = u.Username;
                    string password = u.Password;


                    if (username == null || username.Length == 0)
                        return "Incorrect username";

                    var user = _context.WebUser.SingleOrDefault(x => x.Username == username);

                    if (user != null)
                        return "Username already exists";

                    if (password == null || password.Length == 0)
                        return "Incorrect password";

                    _context.WebUser.Add(u);
                    _context.SaveChanges();
                    return "";


                }
                catch
                {
                    throw;


                }


               
            }
            catch
            {
                throw;
            }
        }

        public WebUser Authenticate(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return null;

                var user = _context.WebUser.SingleOrDefault(x => x.Username == username);

                if (user == null)
                    return null;

                if (user.Password == password)
                {
                    return user;
                }
                else
                {
                    return null;
                }


            }
            catch
            {
                throw;


            }
        }
    }
}
