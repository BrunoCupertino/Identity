using Microsoft.AspNet.Identity.EntityFramework;
using Security.Entidade;

namespace Security.UserManager
{
    public class UserStore : UserStore<Usuario>
    {
        public UserStore(IdentityDbContext<Usuario> context)
            : base(context)
        {
        }
    }
}
