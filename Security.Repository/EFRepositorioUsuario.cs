using Microsoft.AspNet.Identity.EntityFramework;
using Security.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Repository
{
    public class EFRepositorioUsuario : IRepositorioUsuario
    {
        private readonly IdentityDbContext context;

        public EFRepositorioUsuario(IdentityDbContext context)
        {
            this.context = context;
        }

        public void Registar(Entidade.Usuario usuario)
        {
            this.context.Users.Add(usuario);
        }

        public Entidade.Usuario LogOn(string email, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
