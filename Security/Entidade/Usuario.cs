using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Entidade
{
    public class Usuario : IdentityUser, IUsuario
    {
        protected Usuario()
            : base()
        {
        }

        public Usuario(string userName)
            : base(userName)
        {
            this.Email = userName;
        }

        public void AdicionarPerfil(string perfil)
        {
            if (!this.PossuiPerfil(perfil))
            {
                this.Roles.Add(new IdentityUserRole { RoleId = perfil, UserId = this.Id });
            }
        }

        public bool PossuiPerfil(string perfil)
        {
            var possuiPerfil = this.Roles.Select(r => r.RoleId).Contains(perfil);

            return possuiPerfil;
        }
    }
}
