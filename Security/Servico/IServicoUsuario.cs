using Security.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Servico
{
    public interface IServicoUsuario
    {
        Task<Usuario> LogOn(string email, string senha);
        Task Registrar(string email, string password, string nomePerfil);
    }
}
