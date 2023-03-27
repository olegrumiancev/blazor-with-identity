using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Server.Models
{
    public class Person : IdentityUser<Guid>
    {
        public Person() { }

        public string? FirstName { get; set; } = default;
        public string? LastName { get; set; }
        public string? PersonalCode { get; set; }
        public new string? PhoneNumber { get; set; }
        public new string? Email { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressStreetNumber { get; set; }
        public string? AddressFlatNumber { get; set; }
    }
}
