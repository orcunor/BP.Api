using BP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Api.Services
{
    public interface IContactService
    {
        public ContactDTO GetContactById(int id);
        public List<ContactDTO> GetContacts();
        public void CreateContact(ContactDTO contactDTO);
    }
}
