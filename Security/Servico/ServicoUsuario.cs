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

        public ServicoUsuario(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Usuario> LogOn(string email, string senha)
        {
            return await this.userManager.FindAsync(email, senha);
        }

        public async Task Registrar(string email, string senha)
        {
            var usuario = new Usuario(email);

            var result = await this.userManager.CreateAsync(usuario, senha);

            if (!result.Succeeded)
            {
                throw new ApplicationException(string.Join(Environment.NewLine, result.Errors));
            }
        }
    }
}
