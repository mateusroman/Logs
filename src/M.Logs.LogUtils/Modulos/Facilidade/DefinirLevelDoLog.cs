using M.Logs.Interfaces;

namespace LogUtils
{
    public static class DefinirLevelDoLog
    {
        public static void Aplicar(ILogConfigurador configurador, LogLevel? level = null)
        {
            switch (level)
            {
                case LogLevel.Desligado:
                    configurador.DefinirLevel(LogLevel.Desligado);
                    break;
                case LogLevel.Todos:
                    configurador.DefinirLevel(LogLevel.Todos);
                    break;
                case LogLevel.Debug:
                    configurador.DefinirLevel(LogLevel.Debug);
                    break;
                case LogLevel.Informacao:
                    configurador.DefinirLevel(LogLevel.Informacao);
                    break;
                case LogLevel.Erro:
                    configurador.DefinirLevel(LogLevel.Erro);
                    break;
                case LogLevel.Fatal:
                    configurador.DefinirLevel(LogLevel.Fatal);
                    break;
                case LogLevel.Alerta:
                    configurador.DefinirLevel(LogLevel.Alerta);
                    break;
                default:
                    configurador.DefinirLevel(LogLevel.Desligado);
                    break;
            }
        }
    }
}
