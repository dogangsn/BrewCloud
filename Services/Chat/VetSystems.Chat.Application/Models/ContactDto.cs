using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Chat.Application.Models
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public List<ContactDetailsDto> Details { get; set; }
    }

    public class ContactDetailsDto
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public DateTime? Birthday { get; set; }
        public string Adress { get; set; }
        public List<ContactEmailsDto> Emails { get; set; }
        public List<ContactPhoneNumberDto> PhoneNumbers { get; set; }
    }

    public class ContactEmailsDto
    {
        public string Email { get; set; }
        public string Label { get; set; }
    }

    public class ContactPhoneNumberDto
    {
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Label { get; set; }
    }

}
