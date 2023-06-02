using Microsoft.AspNetCore.Identity;
using MindMission.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MindMission.API.Utilities
{
    namespace Identity.IdentityPolicy
    {
        public class CustomUserValidator : IUserValidator<User>
        {
            public CustomUserValidator(IdentityErrorDescriber errors = null)
            {
                Describer = errors ?? new IdentityErrorDescriber();
            }

            public IdentityErrorDescriber Describer { get; private set; }

            public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
            {
                if (manager == null)
                {
                    throw new ArgumentNullException(nameof(manager));
                }
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                var errors = new List<IdentityError>();
                if (manager.Options.User.RequireUniqueEmail)
                {
                    await ValidateEmail(manager, user, errors);
                }
                return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
            }

            private async Task ValidateEmail(UserManager<User> manager, User user, List<IdentityError> errors)
            {
                var email = await manager.GetEmailAsync(user);
                if (string.IsNullOrWhiteSpace(email))
                {
                    errors.Add(Describer.InvalidEmail(email));
                    return;
                }
                if (!new EmailAddressAttribute().IsValid(email))
                {
                    errors.Add(Describer.InvalidEmail(email));
                    return;
                }
                var owner = await manager.FindByEmailAsync(email);
                if (owner != null &&
                    !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
                {
                    errors.Add(Describer.DuplicateEmail(email));
                }
            }
        }
    }
}