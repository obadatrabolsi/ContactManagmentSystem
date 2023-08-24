using CMS.Core.Validations;
using FluentValidation;

namespace CMS.Domain.Requests.Companies
{
    public class UpdateCompanyRequest : CreateCompanyRequest
    {
        public Guid Id { get; set; }
    }

    public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
    {
        public UpdateCompanyRequestValidator()
        {
            RuleFor(x => x.Id).Required();
            RuleFor(x => x).SetInheritanceValidator(x => x.Add<UpdateCompanyRequest>(new CreateCompanyRequestValidator()));
        }
    }
}
