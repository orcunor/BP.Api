﻿using AutoMapper;
using BP.Api.Data.Models;
using BP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BP.Api.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;

        private readonly IHttpClientFactory _httpClientFactory;
        public ContactService(IMapper Mapper, IHttpClientFactory httpClientFactory)
        {
            this._mapper = Mapper;
            _httpClientFactory = httpClientFactory;
        }

        

        public ContactDTO GetContactById(int id)
        {
           

            var dbContact = GetDummyContact();   // veritabanından kaydın getirilmesi işlemi yapılmalı normalde şimdilik fake data yarattım.

            var client =  _httpClientFactory.CreateClient("garantiapi"); // startup services de garantiapi yi set ettik 


            



            ContactDTO resultContact = _mapper.Map<ContactDTO>(dbContact); // db contact'ı  contactDTO a çeviriyor

            return resultContact;


        }


        private Contact GetDummyContact()
        {
            return new Contact
            {
                Id = 1,
                FirstName = "Orçun",
                LastName = "Or"
            };
        }
    }


}