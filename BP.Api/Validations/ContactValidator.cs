using BP.Api.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api.Validations
{
    public class ContactValidator : AbstractValidator<ContactDTO>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("İsim soyisim boş olamaz");
            RuleFor(x => x.Id).LessThan(100).WithMessage("Id 100 den büyük olamaz");
        }
    }
}
