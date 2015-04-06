using Microsoft.AspNet.Identity;
using Security.Entidade;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Security.UserManager
{
    public interface IUserManager
    {
        Task<ClaimsIdentity> CreateIdentityAsync(Usuario usuario, string authenticationType);
        Task<IdentityResult> CreateAsync(Usuario usuario, string senha);
        Task<Usuario> FindAsync(string usuario, string senha);
    }
}
