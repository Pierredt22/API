using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyContacts.Models;

namespace MyContactsTest.Controllers
{
    [Route("api/user")]
    public class ContactController : ControllerBase
    {
        private readonly ContactDAL _contactDAL;
      


        public ContactController(ContactDAL contactDAL)
        {
            _contactDAL = contactDAL;
        }
        [HttpPost]
        [Route("Auth")]
        public IActionResult Authenticate([FromBody] User usr)
        {

            var user = _contactDAL.Authenticate(usr.Username, usr.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            string t = TokenGenerator();
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Token = t
            }); ;
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddContact([FromBody] WebUser u)
        {
            var result = _contactDAL.AddUser(u);
            if (result != "")
                return BadRequest(new { message =result });
            return Ok(new { message = "Registration successful" });
        }



        [HttpGet]
        [Route("GetContacts")]
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _contactDAL.GetAllContacts();
        }
        [HttpGet]
        [Route("GetContacts/{uId}")]
        public async Task<IEnumerable<Contact>> GetContacts2(int uId)
        {
            return await _contactDAL.GetAllContacts2(uId);
        }
        [HttpPost]
        [Route("AddContact")]
        public int AddContact([FromBody] Contact c)
        {
            return _contactDAL.AddContact(c);
        }
        [HttpGet]
        [Route("GetContactDetail/{cId}")]
        public Contact GetContactDetail(int cId)
        {
            return _contactDAL.GetContact(cId);
        }
        [HttpPut]
        [Route("EditContact")]
        public int EditContact([FromBody] Contact c)
        {
            return _contactDAL.UpdateContact(c);
        }
        [HttpDelete]
        [Route("DeleteContact/{cID}")]
        public int DeleteContact(int cId)
        {
            return _contactDAL.DeleteContact(cId);
        }



        public string TokenGenerator()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }

    }


}