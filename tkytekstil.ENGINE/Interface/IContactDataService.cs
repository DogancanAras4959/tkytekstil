using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ContactDataData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IContactDataService : ICrudService<ContactData, ContactDataDto>
    {
        Task sendMailContact(ContactDataDto contact);

        Task sendMailOrderComplete(ContactDataDto contact);
    }
}
