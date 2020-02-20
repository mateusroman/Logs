using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M.Logs.Interfaces
{
    public enum LogLevel { Desligado, Todos, Debug, Informacao, Erro, Fatal, Alerta }

    public interface ILogConfigurador
    {
        ILogConfigurador DefinirLevel(LogLevel level);
        void Configurar();
    }
}
