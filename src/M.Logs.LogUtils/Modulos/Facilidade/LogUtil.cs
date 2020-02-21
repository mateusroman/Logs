using M.Log.Logs;
using M.Logs.Interfaces;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace LogUtils
{
    public class LogUtil
    {
        private LogLevel level;
        private ILogConfigurador configurador;

        public LogUtil(LogLevel level, ILogConfigurador configurador)
        {
            this.level = level;
            this.configurador = configurador;
            DefinirLevelDoLog.Aplicar(configurador, level);
        }

        public LogUtil(ILogConfigurador configurador)
        {
            this.configurador = configurador;
            DefinirLevelDoLog.Aplicar(configurador);
        }

        public void MudarLevel(LogLevel level)
        {
            this.level = level;
            DefinirLevelDoLog.Aplicar(configurador, level);
        }

        public void Escrever(string mensagem)
        {
            StackTrace stackTrace = new StackTrace();
            ILogConstrutor logConstrutor = LogFabrica.Criar();
            logConstrutor.Classe(stackTrace.GetFrame(1).GetMethod().ReflectedType.Name);

            try
            {
                ILog log = logConstrutor.Metodo(stackTrace.GetFrame(1).GetMethod().Name).Mensagem(mensagem).Construir();
                ConstruirParaOLevel(log);
            }
            catch (Exception e)
            {
                logConstrutor.Metodo(stackTrace.GetFrame(1).GetMethod().Name).Excecao(e).Construir().ParaErro();
            }
        }

        public void Escrever<T>(Expression<Func<bool, T>> parametros)
        {
            StackTrace stackTrace = new StackTrace();
            ILogConstrutor logConstrutor = LogFabrica.Criar();
            logConstrutor.Classe(stackTrace.GetFrame(1).GetMethod().ReflectedType.Name);

            try
            {
                ILog log = logConstrutor.Metodo(stackTrace.GetFrame(1).GetMethod().Name).Parametro(parametros).Construir();
                ConstruirParaOLevel(log);
            }
            catch (Exception e)
            {
                logConstrutor.Metodo(stackTrace.GetFrame(1).GetMethod().Name).Excecao(e).Construir().ParaErro();
            }
        }

        private void ConstruirParaOLevel(ILog log)
        {
            switch (this.level)
            {
                case LogLevel.Desligado:
                    break;
                case LogLevel.Todos:
                    break;
                case LogLevel.Debug:
                    log.ParaDebug();
                    break;
                case LogLevel.Informacao:
                    log.ParaInformacao();
                    break;
                case LogLevel.Erro:
                    log.ParaErro();
                    break;
                case LogLevel.Fatal:
                    log.ParaFatal();
                    break;
                case LogLevel.Alerta:
                    log.ParaAlerta();
                    break;
                default:
                    break;
            }
        }
    }
}
