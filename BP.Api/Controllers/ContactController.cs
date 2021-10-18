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
        public string Get()
        {
            return _configuration["ReadMe"].ToString();
        }

        [ResponseCache(Duration = 10)] // ilk istek geldiğinde bu metod çalışıcak bu cache tutulacak 10 saniye boyunca cache'den alacak 10 sniye içinde tekrar çağırılsa önbellekten cevap vericek bu metoda girmicek
        [HttpGet("{id}")]
        public ContactDTO GetContactById(int id)
        {
            return _contactService.GetContactById(id);
        }

        [HttpPost("")]
        public ContactDTO CreateContact(ContactDTO contact)
        {
            // create contact on database interface ile metodu getir

            return contact;
        }
    }
}
