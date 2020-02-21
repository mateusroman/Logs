using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using M.Logs.Interfaces;

namespace M.Logs.Logs.Configuradores
{
    public class ConfiguradorLog4Net: ILogConfigurador
    {
        private LogLevel level = LogLevel.Todos;

        public void Configurar() {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.StaticLogFileName = false;
            roller.DatePattern = "";
            roller.DatePattern = "dd.MM.yyyy'.log'";
            roller.File = @"Logs/";
            roller.Layout = patternLayout;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Date;
            roller.ActivateOptions();
            hierarchy.Root.CloseNestedAppenders();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            switch (level) {
                case LogLevel.Desligado: hierarchy.Root.Level = Level.Off; break;
                case LogLevel.Todos: hierarchy.Root.Level = Level.All; break;
                case LogLevel.Debug: hierarchy.Root.Level = Level.Debug; break;
                case LogLevel.Fatal: hierarchy.Root.Level = Level.Fatal; break;
                case LogLevel.Erro: hierarchy.Root.Level = Level.Error; break;
                case LogLevel.Informacao: hierarchy.Root.Level = Level.Info; break;
                case LogLevel.Alerta: hierarchy.Root.Level = Level.Warn; break;
            }            
            hierarchy.Configured = true;
        }

        public ILogConfigurador DefinirLevel(LogLevel level)
        {
            this.level = level;
            return this;
        }
    }
}
