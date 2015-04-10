using Microsoft.AspNet.Identity;
using Security.Entidade;

namespace Security.UserManager
{
    public class UserManager : UserManager<Usuario>, IUserManager
    {
        public UserManager(IUserStore<Usuario> store)
            : base(store)
        {           
            this.UserValidator = new UserValidator<Usuario>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };         
        }        
    }
}
