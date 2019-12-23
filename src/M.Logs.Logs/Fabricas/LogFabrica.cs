using M.Logs.Interfaces;

namespace M.Log.Logs
{
    public class LogFabrica
    {
        public static ILogConstrutor Criar() {
            return new Log4Net();
        }
    }
}
