using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Entidade
{
    //TODO: do domínio
    public interface IUsuario
    {
        string Email { get; set; }
        void AdicionarPerfil(string perfil);
        bool PossuiPerfil(string perfil);
    }
}
