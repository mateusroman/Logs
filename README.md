# Logs
Permite a gravação de logs em arquivo texto.
Arquivos serão gravados na pasta Logs, juntamente com o executável.

Nuget: https://www.nuget.org/packages/M.Logs.Logs

Exemplo:

```csharp
class Program
    {
        static void Main(string[] args)
        {
            //Executa a configuração apenas uma vez ao iniciar o aplicativo
            LogConfiguradorFabrica.Criar().DefinirLevel(LogLevel.Todos).Configurar();

            var exemplo = new Exemplo(LogFabrica.Criar());
            exemplo.Executar(1);
        }

        public class Exemplo
        {
            private ILogConstrutor log;

            public Exemplo(ILogConstrutor log)
            {
                this.log = log;
                log.Classe(nameof(Exemplo));
            }

            public void Executar(int numero)
            {
                log.Metodo(nameof(Executar)).Parametro(x => numero).Construir().ParaDebug();
                try
                {
                    //Faz qualquer coisa
                    log.Metodo(nameof(Executar)).Mensagem("Terminou").Construir().ParaDebug();
                }
                catch (Exception e)
                {
                    log.Metodo(nameof(Executar)).Excecao(e).Construir().ParaErro();
                }
            }
        }
    }
```

Resultado:

2019-12-23 16:09:03,445 [1] DEBUG - Exemplo.Executar -  numero: 1

2019-12-23 16:09:03,495 [1] DEBUG - Exemplo.Executar - Terminou

----------------------------------------------------------------------------------------------------------------------------------------

# LogUtils

```csharp
    public class Program
    {
        public static void Main(string[] args)
        {
            var logConfigurador = LogConfiguradorFabrica.Criar();
            logConfigurador.Configurar();
            var logUtil = new LogUtil(logConfigurador);
            logUtil.MudarLevel(LogLevel.Informacao);
            logUtil.Escrever("Log definido para informação.");
            logUtil.MudarLevel(LogLevel.Alerta);
            logUtil.Escrever("Log definido para alerta.");
            logUtil.MudarLevel(LogLevel.Erro);
            logUtil.Escrever("Log definido para erro.");
            logUtil.MudarLevel(LogLevel.Fatal);
            logUtil.Escrever("Log definido para fatal.");
        }
    }
```

Resultado:

2020-02-20 17:34:51,031 [1] INFO  - Program.Main - Log definido para informação.

2020-02-20 17:34:51,061 [1] WARN  - Program.Main - Log definido para alerta.

2020-02-20 17:34:51,062 [1] ERROR - Program.Main - Log definido para erro.

2020-02-20 17:34:51,063 [1] FATAL - Program.Main - Log definido para fatal.

