using CMS.Core.Validations;
using FluentValidation;

namespace CMS.Domain.Requests.Contacts
{
    public class UpdateContactRequest : CreateContactRequest
    {
        public Guid Id { get; set; }
    }

    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(x => x.Id).Required();
            RuleFor(x => x).SetInheritanceValidator(x => x.Add<UpdateContactRequest>(new CreateContactRequestValidator()));
        }
    }
}
