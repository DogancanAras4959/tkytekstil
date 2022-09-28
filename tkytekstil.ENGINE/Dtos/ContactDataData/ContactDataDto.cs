using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkytekstil.ENGINE.Dtos.ContactDataData
{
    public class ContactDataDto : BaseEntityDto
    {
        public string To { get; set; }
        public string CompanyName { get; set; }
        public string NameSurname { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string MesssageLink { get; set; }
        public string CallbackUrl { get; set; }
    }
}
