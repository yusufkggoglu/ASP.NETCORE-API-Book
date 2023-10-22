using Entities.DataTransferObjects;
using Entities.Models;
using FluentValidation;

namespace WebApi.Utilities.Validator.FluentValidation
{
    [Serializable]
    public class BookValidator : AbstractValidator<BookDtoForCreate>
    {
        public BookValidator()
        {
            RuleFor(x => x.Name).Length(3, 50).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0).NotEmpty();
        }
    }
}
