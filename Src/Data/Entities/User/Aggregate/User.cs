using Data.Common.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities.User.Aggregate;

public class User : IdentityUser, IAggregateRoot
{
}
