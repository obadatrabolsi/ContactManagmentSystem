using FluentValidation;

namespace CMS.Core.Validations
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotNull()
                             .NotEmpty();
        }
    }
}
