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
    }
}
