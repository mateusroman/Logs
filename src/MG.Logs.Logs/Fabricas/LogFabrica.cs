using MG.Logs.Interfaces;

namespace MG.Log.Logs
{
    public class LogFabrica
    {
        public static ILogConstrutor Criar() {
            return new Log4Net();
        }
    }
}
