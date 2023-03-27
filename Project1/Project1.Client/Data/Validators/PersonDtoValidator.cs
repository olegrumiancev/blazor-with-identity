using FluentValidation;
using Project1.Shared;

namespace Project1.Client.Data.Validators
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.PersonalCode).NotEmpty();
            RuleFor(x => x.AddressCity).NotEmpty();
            RuleFor(x => x.AddressStreet).NotEmpty();
            RuleFor(x => x.AddressStreetNumber).NotEmpty();
            RuleFor(x => x.AddressFlatNumber).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
