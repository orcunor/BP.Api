using BP.Api.Models;
using BP.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ContactController> _logger;

        public ContactController(IConfiguration configuration, IContactService contactService, ILogger<ContactController> logger)
        {
            this._configuration = configuration;
            this._contactService = contactService;
            this._logger = logger;
        }



        [HttpGet]
        public IActionResult GetAllContacts()
        {
            _logger.LogInformation("LogInformation --> GetAllContacts Method is called");
            _logger.LogTrace("LogTrace --> GetAllContacts Method is called");
            _logger.LogDebug("LogDebug --> GetAllContacts Method is called");
            _logger.LogWarning("LogWarning --> GetAllContacts Method is called");
            _logger.LogError("LogError --> GetAllContacts Method is called");

            //var contacts = _contactService.GetContacts();
            //if (contacts != null)
            //{

            //    return Ok(contacts);
            //}

            //return NotFound();
            return Ok(_configuration["ReadMe"].ToString());
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
