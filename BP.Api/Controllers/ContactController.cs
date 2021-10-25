using BP.Api.Models;
using BP.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;

        public ContactController(IConfiguration configuration, IContactService contactService)
        {
            this._configuration = configuration;
            this._contactService = contactService;
        }



        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = _contactService.GetContacts();
            if (contacts != null)
            {
                return Ok(contacts);
            }

            return NotFound();
            //return Ok(_configuration["ReadMe"].ToString());
        }



        [ResponseCache(Duration = 10)] // ilk istek geldiğinde bu metod çalışıcak bu cache tutulacak 10 saniye boyunca cache'den alacak 10 sniye içinde tekrar çağırılsa önbellekten cevap vericek bu metoda girmicek, 10 saniyenin sonunda tekrar istek gelirse tekrar bu metoda girecek
        [HttpGet("{id}")]
        public ActionResult<ContactDTO> GetContactById(int id)
        {
            var contact = _contactService.GetContactById(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            return NotFound();
        }



        [HttpPost]
        public ActionResult<ContactDTO> CreateContact(ContactDTO contact)
        {
            _contactService.CreateContact(contact);
            // create contact on database interface ile metodu getir

            //return Ok();
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }


         [HttpDelete]
        public IActionResult DeleteContact(int id)
        {
            // interface ' e metod ekle class içinden listeden sil
            return Ok();
        }
    }
}
