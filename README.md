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
