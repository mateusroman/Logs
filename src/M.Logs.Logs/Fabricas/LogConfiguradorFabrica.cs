using M.Logs.Interfaces;
using M.Logs.Logs.Configuradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M.Logs.Logs.Fabricas
{
    public class LogConfiguradorFabrica
    {
        public static ILogConfigurador Criar() {
            return new ConfiguradorLog4Net();
        }
    }
}
