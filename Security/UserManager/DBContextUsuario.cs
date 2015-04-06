using Microsoft.AspNet.Identity.EntityFramework;
using Security.Entidade;

namespace Security.UserManager
{
    public class DBContextUsuario : IdentityDbContext<Usuario>
    {
        public DBContextUsuario()
            : base("DefaultConnection")
        {
        }
    }
}
