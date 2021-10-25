using AutoMapper;
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

        private static List<Contact> contacts = new List<Contact>
        {
               new Contact{Id = 1, FirstName = "Orçun", LastName = "Or"},
               new Contact{Id = 2, FirstName = "Test", LastName = "Test"},
               new Contact{Id = 3, FirstName = "Test1", LastName = "Test1"},
               new Contact{Id = 4, FirstName = "Test2", LastName = "Test2"},
               new Contact{Id = 5, FirstName = "Test3", LastName = "Test3"},
        };

        private readonly IMapper _mapper;

        private readonly IHttpClientFactory _httpClientFactory;
        public ContactService(IMapper Mapper, IHttpClientFactory httpClientFactory)
        {
            this._mapper = Mapper;
            _httpClientFactory = httpClientFactory;
        }

        public void CreateContact(ContactDTO contactDTO)
        {
            if (contactDTO != null)
            {
                var firstName = contactDTO.FullName.Split(" ")[0];
                var lastName = contactDTO.FullName.Split(" ")[1];
                var contact = new Contact
                {
                    Id = contactDTO.Id,
                    FirstName = firstName,
                    LastName = lastName
                };
                //Contact contact = _mapper.Map<Contact>(contactDTO);
                contacts.Add(contact);
            }
        }

        public ContactDTO GetContactById(int id)
        {
            var contact = contacts.FirstOrDefault(x => x.Id == id);

            //var dbContact = GetDummyContact();   // veritabanından kaydın getirilmesi işlemi yapılmalı normalde şimdilik fake data yarattım.

           // var client =  _httpClientFactory.CreateClient("garantiapi"); // startup services de garantiapi yi set ettik 

            ContactDTO resultContact = _mapper.Map<ContactDTO>(contact); // db contact'ı  contactDTO a çeviriyor

            return resultContact;

        }

        public List<ContactDTO> GetContacts()
        {
            var contacts = GetAllContacts(); // fake datamız

            List<ContactDTO> contactDTOs = new List<ContactDTO>();

            foreach (var item in contacts)
            {
                contactDTOs.Add(_mapper.Map<ContactDTO>(item)); // mapleme işlemi
            }

            return contactDTOs;
           
        }


        #region Fake data olusturma fonksiyonları
        private Contact GetDummyContact()
        {
            //return new Contact
            //{
            //    Id = 1,
            //    FirstName = "Orçun",
            //    LastName = "Or"
            //};
            return contacts.FirstOrDefault();
        }

        private List<Contact> GetAllContacts()
        {
           // return new List<Contact>();
            return contacts;
        }

      

        #endregion
    }


}
