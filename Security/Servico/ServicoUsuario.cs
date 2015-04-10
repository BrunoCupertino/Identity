using Microsoft.AspNet.Identity;
using Security.Entidade;
using Security.UserManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Servico
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IUserManager userManager;
        private readonly IRoleManager roleManager;

        public ServicoUsuario(IUserManager userManager, IRoleManager roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Usuario> LogOn(string email, string senha)
        {
            return await this.userManager.FindAsync(email, senha);
        }

        public async Task Registrar(string email, string senha, string nomePerfil)
        {
            var usuarioNovo = false;

            var usuario = await this.userManager.FindByEmailAsync(email);

            if (usuario == null)
            {
                usuarioNovo = true;

                usuario = new Usuario(email);
            }

            var perfil = await this.roleManager.FindByNameAsync(nomePerfil); 

            if (perfil != null)
            {
                if (usuario.PossuiPerfil(perfil.Id))
                {
                    throw new ApplicationException("Usuário já registrado.");
                }

                usuario.AdicionarPerfil(perfil.Id);
            }

            var result = usuarioNovo ? await this.userManager.CreateAsync(usuario, senha)
                                     : await this.userManager.UpdateAsync(usuario);

            if (!result.Succeeded)
            {
                throw new ApplicationException(string.Join(Environment.NewLine, result.Errors));
            }
        }
    }

    public interface IRoleManager
    {
        Task<IdentityResult> CreateAsync(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role);
        Task<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> FindByNameAsync(string roleName);
    }

    public class RoleManager : RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>, IRoleManager
    {
        public RoleManager(IRoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole, string> store)
            : base(store)
        {
        }
    }

    public class RoleStore : Microsoft.AspNet.Identity.EntityFramework.RoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>
    {
        public RoleStore(Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext<Usuario> context)
            : base(context)
        {
        }
    }
}

