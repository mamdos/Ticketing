using Data.Common.Abstractions;
using Data.Common.Exceptions;
using Data.Entities.User.Dtos;

namespace Data.Entities.User.Policies;

internal class UserCreatingPolicy : BasePolicy<Aggregate.User, CreateUserDto>
{
    internal UserCreatingPolicy(in Aggregate.User entity, in CreateUserDto input) : base(entity, input)
    {
    }

    internal override void CheckConstraints()
    {
        CheckIfAllFieldsAreFilled();
    }

    private void CheckIfAllFieldsAreFilled()
    {
        if (string.IsNullOrWhiteSpace(_input.UserName) ||
            string.IsNullOrWhiteSpace(_input.Email) ||
            string.IsNullOrWhiteSpace(_input.FirstName) ||
            string.IsNullOrWhiteSpace(_input.LastName))
            throw new InvalidEntityStateException("all the required fields should be filled with data");
    }
}
