using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Shared
{
    public class PersonDto : UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressStreetNumber { get; set; }
        public string AddressFlatNumber { get; set; }
    }
}
